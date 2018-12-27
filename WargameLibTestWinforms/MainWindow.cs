using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WargameLib;

// This is a simple WindowsForms test application.
// No fancy MVVM Pattern here, sorry. I reserve that for WPF.

namespace WargameLibTestWinforms
{
    public partial class MainWindow : Form
    {
        //Dictionary<string, string> _wadIndex = new Dictionary<string, string>(); // Key=Image File, value=WAD File
        //CancellationTokenSource _wadIndexCancel;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Open Folder

        private void chooseDatosDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenDirectoryDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                UpdateWADDirectory(dlg.SelectedDirectory);
                Properties.Settings.Default.DatosDir = dlg.SelectedDirectory;
                Properties.Settings.Default.Save();
            }
        }

        void UpdateWADDirectory(string directory)
        {
            // Search for WAD files
            // They are normally only in RECURSOS and FONTS withing DATOS
            List<string> files = new List<string>();
            var recursos = Path.Combine(directory, "RECURSOS");
            if (Directory.Exists(recursos))
            {
                files.AddRange(Directory.EnumerateFiles(recursos, "*.WAD", SearchOption.AllDirectories));
                // TODO: The used RLE format cannot be read ad the moment
                // files.AddRange(Directory.EnumerateFiles(recursos, "*.RLE", SearchOption.AllDirectories));
            }
            var fonts = Path.Combine(directory, "FONTS");
            if (Directory.Exists(fonts))
            {
                files.AddRange(Directory.EnumerateFiles(fonts, "*.WAD", SearchOption.AllDirectories));
                // TODO: The used RLE format cannot be read ad the moment
                // files.AddRange(Directory.EnumerateFiles(fonts, "*.RLE", SearchOption.AllDirectories));
            }
            var items = new List<FileNameItem>();
            foreach (var file in files)
            {
                items.Add(new FileNameItem() { FileName = file });
            }
            listBoxFiles.DataSource = items;

            // Not required
            //CreateWadIndex(files.ToList()); // Creates background Task that uses the list
            files.Clear();

            // Search Level Files
            var misiones = Path.Combine(directory, "MISIONES");
            if (Directory.Exists(recursos))
            {
                files.AddRange(Directory.EnumerateFiles(misiones, "*.VOL", SearchOption.AllDirectories));
            }

            items = new List<FileNameItem>();
            foreach (var file in files)
            {
                items.Add(new FileNameItem() { FileName = file });
            }
            listBoxVolFiles.DataSource = items;
        }

        // This is just a lazy workaround to hide the path of the file
        class FileNameItem
        {
            public string FileName;
            public override string ToString()
            {
                if (string.IsNullOrEmpty(FileName)) return "<not set>";
                return Path.GetFileName(FileName);
            }
        }

        #endregion

        #region WAD and Export

        //// Not required, level files and wad files use numbered indexes
        //void CreateWadIndex(List<string> files)
        //{
        //    busyBar.Value = 0;
        //    busyLabel.Text = "Indexing WAD files...";
        //    Task.Run(() =>
        //    {
        //        Debug.WriteLine("WAD Indexing started...");
        //        _wadIndexCancel?.Cancel();
        //        lock (_wadIndex)
        //        {
        //            var src = new CancellationTokenSource();
        //            var token = src.Token;
        //            _wadIndexCancel = src;
        //            _wadIndex.Clear();

        //            int progress = 0;
        //            foreach (var file in files)
        //            {
        //                statusStrip.Invoke((Action)(() => { busyBar.Value = (int)(++progress * 100.0 / files.Count + .5); }));

        //                if (token.IsCancellationRequested) return;
        //                var wadFiles = WAD.Extract(file);
        //                foreach (var wad in wadFiles)
        //                    _wadIndex[wad.Name] = file;
        //            }
        //            _wadIndexCancel = null;
        //        }
        //        statusStrip.Invoke((Action)(() =>
        //        {
        //            busyBar.Value = 0;
        //            busyLabel.Text = "";
        //        }));
        //        Debug.WriteLine("WAD Indexing finished!");
        //    });
        //}

        private void listBoxFiles_SelectedValueChanged(object sender, EventArgs e)
        {
            listBoxWADContent.DataSource = null;
            var value = listBoxFiles.SelectedValue as FileNameItem;
            if (value == null)
            {
                PanelImageGraphicsDirty = true;
                return;
            }

            // TODO: RLE files use a special form of the Windows Bitmap format. Need a different decoder.
            //if (value.FileName.EndsWith(".RLE",StringComparison.OrdinalIgnoreCase))
            //{
            //    listBoxWADContent.DataSource = null;
            //    try
            //    {
            //        // Not a wad. Show image directly
            //        List<WADImage> list = new List<WADImage>();
            //        var bytes = File.ReadAllBytes(value.FileName);
            //        list.Add(new RLE(bytes));
            //        listBoxWADContent.DataSource = list;
            //    }
            //    catch {  }
            //    return;
            //}

            listBoxWADContent.DataSource = WAD.Extract(value.FileName);
        }

        private void listBoxWADContent_SelectedValueChanged(object sender, EventArgs e)
        {
            textBoxHeader.Text = "";
            var img = listBoxWADContent.SelectedValue as WADImage;

            imagePanel.Image = img;
            palettePanel.Image = img;

            buttonExportPng.Enabled = (img != null);

            PanelImageGraphicsDirty = true;

            if (img != null)
            {
                var data = img.GetHeader();
                // 32 byte name
                string h = Encoding.ASCII.GetString(data, 0, 32).TrimEnd('\0') + "\r\n";
                // 8 byte pixel data size
                int start = 32;
                UInt64 dataSize64 =
                    (UInt64)data[start++] |
                    (UInt64)data[start++] << 8 |
                    (UInt64)data[start++] << 16 |
                    (UInt64)data[start++] << 24 |
                    (UInt64)data[start++] << 32 |
                    (UInt64)data[start++] << 40 |
                    (UInt64)data[start++] << 48 |
                    (UInt64)data[start++] << 56;
                h += "PSize: " + dataSize64 + "\r\n";

                for (int j = 0; j < 2; ++j)
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        h += string.Format("{0:X2} ", data[start++]);
                    }
                    h += "\r\n";
                }

                start = 32 + 8 + 8;
                var height =
                    (UInt32)data[start++] |
                    (UInt32)data[start++] << 8 |
                    (UInt32)data[start++] << 16 |
                    (UInt32)data[start++] << 24;
                h += "Height: " + height + "\r\n";
                var width =
                    (UInt32)data[start++] |
                    (UInt32)data[start++] << 8 |
                    (UInt32)data[start++] << 16 |
                    (UInt32)data[start++] << 24;
                h += "Width: " + width + "\r\n";
                var colDep =
                    (UInt32)data[start++] |
                    (UInt32)data[start++] << 8;
                h += "Depth: " + colDep + "\r\n";

                for (int j = 0; j < 3; ++j)
                {
                    for (int i = 0; i < 2; ++i)
                    {
                        h += string.Format("{0:X2} ", data[start++]);
                    }
                    h += "\r\n";
                }

                textBoxHeader.Text = h;
            }
        }

        bool _panelImageGraphicsDirty = true;
        bool PanelImageGraphicsDirty { get => _panelImageGraphicsDirty; set { _panelImageGraphicsDirty = value; Invalidate(); } }
        BufferedGraphics panelImageGraphics;
        BufferedGraphics panelPaletteGraphics;

        Graphics panelLevelGraphics;

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    _lastUiUpdate = DateTime.UtcNow;
        //    base.OnPaint(e);

        //    // TODO: Doing this in OnPaint is inefficient
        //    // Need to implement this directly in a derived panel class
        //    if (tabControl1.SelectedIndex == 0)
        //    {
        //        //if (!PanelImageGraphicsDirty && panelImageGraphics != null && panelPaletteGraphics != null)
        //        //{
        //        //    panelImageGraphics.Render(panelImage.CreateGraphics());
        //        //    panelPaletteGraphics.Render(panelPalette.CreateGraphics());
        //        //}
        //        //else
        //        //{
        //        //    PanelImageGraphicsDirty = false;
        //        //    Debug.WriteLine("Image dirty, repaint");

        //        //    var img = listBoxWADContent.SelectedValue as WADImage;

        //        //    if (img != null)
        //        //    {
        //        //        panelImage.Width = (int)(img.Width * _zoom);
        //        //        panelImage.Height = (int)(img.Height * _zoom);
        //        //    }
        //        //    else { panelImage.Width = 1; panelImage.Height = 1; }

        //        //    // Double Buffering
        //        //    if (panelImageGraphics != null) panelPaletteGraphics.Dispose();
        //        //    if (panelPaletteGraphics != null) panelPaletteGraphics.Dispose();
        //        //    BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
        //        //    panelImageGraphics = currentContext.Allocate(panelImage.CreateGraphics(), panelImage.DisplayRectangle);
        //        //    panelPaletteGraphics = currentContext.Allocate(panelPalette.CreateGraphics(), panelPalette.DisplayRectangle);

        //        //    var g = panelImageGraphics.Graphics;

        //        //    g.Clear(Color.LightGray);
        //        //    //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
        //        //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

        //        //    if (img != null)
        //        //    {
        //        //        var b = BitmapBuffer.Get(img.Name);
        //        //        if (b == null)
        //        //        {
        //        //            b = GetBitmap(img);
        //        //            BitmapBuffer.Push(img.Name, b);
        //        //        }
        //        //        g.DrawImage(b, new Rectangle(0, 0, (int)img.Width * _zoom, (int)img.Height * _zoom), new Rectangle(0, 0, (int)img.Width, (int)img.Height), GraphicsUnit.Pixel);
        //        //    }

        //        //    //buf.Render();
        //        //    panelImageGraphics.Render(panelImage.CreateGraphics());

        //        //    // Color palette

        //        //    g = panelPaletteGraphics.Graphics;
        //        //    g.Clear(Color.LightGray);
        //        //    int y = 32 * 10 + 32 + 1 - 1;
        //        //    int x = 8 * 10 + 8 + 1 - 1;
        //        //    g.DrawRectangle(Pens.Black, 0, 0, x, y);
        //        //    for (int i = 1; i < 8; ++i)
        //        //    {
        //        //        g.DrawLine(Pens.Black, i * 10 + i, 0, i * 10 + i, y);
        //        //    }
        //        //    for (int i = 1; i < 32; ++i)
        //        //    {
        //        //        g.DrawLine(Pens.Black, 0, i * 10 + i, x, i * 10 + i);
        //        //    }
        //        //    if (img != null)
        //        //    {
        //        //        var paletteColors = img.GetPaletteColors();
        //        //        int p = 0;
        //        //        if (paletteColors != null)
        //        //        {
        //        //            for (int i = 0; i < 32; ++i)
        //        //                for (int j = 0; j < 8; ++j)
        //        //                {
        //        //                    using (var b = new SolidBrush(Color.FromArgb(
        //        //                        255,
        //        //                        paletteColors[p].R,
        //        //                        paletteColors[p].G,
        //        //                        paletteColors[p].B)))
        //        //                    {
        //        //                        g.FillRectangle(b, j * 10 + 1 + j, i * 10 + 1 + i, 10, 10);
        //        //                    }
        //        //                    ++p;
        //        //                }
        //        //        }
        //        //    }
        //        //    //buf.Render();
        //        //    panelPaletteGraphics.Render(panelPalette.CreateGraphics());
        //        //}
        //    }
        //    else if (tabControl1.SelectedIndex == 1)
        //    {
        //        //var vol = _vol;
        //        //var div = levelZoomDivider;

        //        //int centerOffsetX = 0;
        //        //int centerOffsetY = 0;

        //        //if (vol != null)
        //        //{
        //        //    panelLevel.Width = (vol.XMax - vol.XMin)/div;
        //        //    panelLevel.Height = (vol.YMax - vol.YMin)/div;
        //        //    if (vol.XMin < 0) centerOffsetX = -1 * vol.XMin; // div is applied later
        //        //    if (vol.YMin < 0) centerOffsetY = -1 * vol.YMin;
        //        //}

        //        //// Double Buffering
        //        //BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
        //        //BufferedGraphics buf = currentContext.Allocate(panelLevel.CreateGraphics(), panelLevel.DisplayRectangle);
        //        //var g = buf.Graphics;

        //        //g.Clear(Color.LightGray);
        //        ////g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
        //        //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

        //        //// Read the VOL structure and place sprites

        //        //if (vol != null)
        //        //{
        //        //    foreach (var poly in vol.Polys)
        //        //    {
        //        //        var x0 = poly.Center.X + centerOffsetX;
        //        //        var y0 = poly.Center.Y + centerOffsetY;

        //        //        // TODO: Result looks very random
        //        //        // Bad performance
        //        //        foreach (var tile in poly.Tiles)
        //        //        {
        //        //            if (tile.SpriteName.StartsWith("-")) continue; // Tile set to invisible
        //        //            //var b = GetBitmap(tile.SpriteName);
        //        //            var b = BitmapBuffer.Get(tile.SpriteName);
        //        //            if (b != null)
        //        //            {
        //        //                g.DrawImage(b,
        //        //                    new Rectangle((x0 + tile.Position.X) / div, (y0 + tile.Position.Y) / div, (int)b.Width / div, (int)b.Height / div),
        //        //                    new Rectangle(0, 0, (int)b.Width, (int)b.Height), GraphicsUnit.Pixel);
        //        //            }
        //        //            else
        //        //            {
        //        //                g.DrawRectangle(Pens.Violet, new Rectangle((x0 + tile.Position.X) / div, (y0 + tile.Position.Y) / div, (int)tile.Width / div, (int)tile.Height / div));
        //        //            }
        //        //        }

        //        //        if (poly.Vertices.Count > 1)
        //        //        {
        //        //            WargameLib.Point prev;
        //        //            WargameLib.Point cur;
        //        //            for (int i = 1; i < poly.Vertices.Count; ++i)
        //        //            {
        //        //                prev = poly.Vertices[i-1];
        //        //                cur = poly.Vertices[i];
        //        //                g.DrawLine(Pens.Blue, (x0 + prev.X) / div, (y0 + prev.Y) / div, (x0 + cur.X) / div, (y0 + cur.Y) / div);
        //        //            }
        //        //            prev = poly.Vertices[poly.Vertices.Count - 1];
        //        //            cur = poly.Vertices[0];
        //        //            g.DrawLine(Pens.Blue, (x0 + prev.X) / div, (y0 + prev.Y) / div, (x0 + cur.X) / div, (y0 + cur.Y) / div);
        //        //        }
        //        //    }
        //        //}

        //        //buf.Render(panelLevel.CreateGraphics());
        //        //buf.Dispose();
        //    }
        //    _lastUiUpdate = DateTime.UtcNow;
        //}

        //Bitmap GetBitmap(string filename)
        //{
        //    var bitmap = BitmapBuffer.Get(filename);
        //    if (bitmap != null) return bitmap;
        //    if (_wadIndex.ContainsKey(filename))
        //    {
        //        var wads = WAD.Extract(_wadIndex[filename]);
        //        var wad = wads.FirstOrDefault(o => o.Name == filename);
        //        bitmap = GetBitmap(wad);
        //        BitmapBuffer.Push(filename, bitmap);
        //        return bitmap;
        //    }
        //    return null;
        //}

        Bitmap GetBitmap(WADImage image)
        {
            Bitmap b = new Bitmap((int)image.Width, (int)image.Height);
            for (int h = 0; h < image.Height; ++h)
                for (int w = 0; w < image.Width; ++w)
                {
                    var pix = image.Pixels[h, w];
                    b.SetPixel(w, h, Color.FromArgb(pix.Opacity, pix.R, pix.G, pix.B));
                }
            return b;
        }

        //Bitmap GetScaledBitmap(WADImage image, double factor)
        //{
        //    int targetWidth = (int)(image.Width * factor + .5);
        //    int targetHeight = (int)(image.Height * factor + .5);

        //    // We need to project the target pixel onto the source pixels


        //}

        private void label1x_Click(object sender, EventArgs e)
        {
            imagePanel.Zoom = 1;
            label1x.BackColor = Color.LightGreen;
            label2x.BackColor = Color.White;
            label4x.BackColor = Color.White;
            PanelImageGraphicsDirty = true;
        }

        private void label2x_Click(object sender, EventArgs e)
        {
            imagePanel.Zoom = 2;
            label1x.BackColor = Color.White;
            label2x.BackColor = Color.LightGreen;
            label4x.BackColor = Color.White;
            PanelImageGraphicsDirty = true;
        }

        private void label4x_Click(object sender, EventArgs e)
        {
            imagePanel.Zoom = 4;
            label1x.BackColor = Color.White;
            label2x.BackColor = Color.White;
            label4x.BackColor = Color.LightGreen;
            PanelImageGraphicsDirty = true;
        }

        private void buttonExportPng_Click(object sender, EventArgs e)
        {
            var img = listBoxWADContent.SelectedValue as WADImage;
            if (img == null) return;
            Bitmap b = new Bitmap((int)img.Width, (int)img.Height);
            for (int h = 0; h < img.Height; ++h)
                for (int w = 0; w < img.Width; ++w)
                {
                    var pix = img.Pixels[h, w];
                    b.SetPixel(w, h, Color.FromArgb(pix.Opacity, pix.R, pix.G, pix.B));
                }
            var dlg = new SaveFileDialog();
            dlg.Filter = "PNG Files|*.png";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                b.Save(dlg.FileName, ImageFormat.Png);
            }
        }

        private void extractDIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new ExtractDirDialog();
            dlg.ShowDialog();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Alt) Invalidate();
        }

        #endregion

        #region Level Viewer

        VOL _vol;
        MapPolygon _selectedMapPolygon;
        MapPolygon SelectedMapPolygon
        {
            get { return _selectedMapPolygon; }
            set
            {
                if (_selectedMapPolygon == value) return;

                var poly = value;
                _selectedMapPolygon = value;
                if (poly == null)
                {
                    tbSelectedPolygon.Text = "";
                    lbTiles.DataSource = null;
                    lbVertices.DataSource = null;
                    SelectedTile = null;
                    return;
                }
                var sb = new StringBuilder();
                sb.AppendLine(poly.Name);
                sb.AppendLine("Type: " + Enum.GetName(typeof(MapPolygonType), poly.Type));
                sb.AppendLine("Center: " + poly.Center);
                sb.AppendLine("Altitude: " + poly.Altitude);
                if (poly.Type == MapPolygonType.Ramp)
                    sb.AppendLine("Altitude Offset: " + poly.AltitudeOffset);
                if (poly.Type == MapPolygonType.Zoom)
                    sb.AppendLine("Zoom: " + poly.Zoom);
                sb.AppendLine("Extra Info: " +
                    poly.ExtraInfo[0] + ";" +
                    poly.ExtraInfo[1] + ";" +
                    poly.ExtraInfo[2] + ";" +
                    poly.ExtraInfo[3] + ";" +
                    poly.ExtraInfo[4] + ";" +
                    poly.ExtraInfo[5] + ";" +
                    poly.ExtraInfo[6] + ";" +
                    poly.ExtraInfo[7]);
                if (poly.Radio != 0)
                    sb.AppendLine("Radio: " + poly.Radio);

                tbSelectedPolygon.Text = sb.ToString();

                lbTiles.DataSource = value.Tiles;
                lbVertices.DataSource = value.Vertices;

            }
        }

        MapTile _selectedTile;
        public MapTile SelectedTile
        {
            get { return _selectedTile; }
            set
            {
                if (_selectedTile == value) return;

                if (value == null)
                {
                    tbSelectedTile.Text = "";
                    return;
                }
                var tile = value;
                _selectedTile = value;

                var sb = new StringBuilder();
                sb.AppendLine(tile.SpriteName);
                sb.AppendLine("Width: " + tile.Width);
                sb.AppendLine("Height: " + tile.Height);
                sb.AppendLine("Position: " + tile.Position);
                sb.AppendLine("Offset: " + tile.Offset);
                sb.AppendLine("Brightness: " + tile.Brightness);
                if (tile.Transformation != MapTileTransformation.None)
                {
                    sb.AppendLine("Transformation:");
                    if ((tile.Transformation & MapTileTransformation.MirrorX) != 0)
                        sb.AppendLine("- MirrorX");
                    if ((tile.Transformation & MapTileTransformation.FlipY) != 0)
                        sb.AppendLine("- FlipY");
                    if ((tile.Transformation & MapTileTransformation.LightOrExplosion) != 0)
                        sb.AppendLine("- LightOrExplosion");
                }

                tbSelectedTile.Text = sb.ToString();
            }
        }

        private void listBoxVolFiles_SelectedValueChanged(object sender, EventArgs e)
        {

            var item = listBoxVolFiles.SelectedValue as FileNameItem;

            _vol = null;
            levelPanel.Vol = null;
            lbPolygons.DataSource = null;
            tbSelectedVol.Text = "";

            if (item == null)
            {
                SelectedMapPolygon = null;
                return;
            }

            try
            {
                _vol = new VOL(item.FileName);
                levelPanel.Vol = _vol;
                lbPolygons.DataSource = _vol.Polys;
                tbSelectedVol.Text =
                    "Width: " + _vol.Width + "\r\nHeight: " + _vol.Height +
                    "\r\nLimits: {"+_vol.XMin+";"+_vol.YMin+ "}-{" + _vol.XMax + ";" + _vol.YMax + "}";

                // Load all textures for that level:
                HashSet<string> wadFiles = new HashSet<string>();
                HashSet<string> imageFiles = new HashSet<string>();
                busyLabel.Text = "Loading Level";
                Application.DoEvents();
                foreach (var poly in _vol.Polys)
                {
                    foreach (var tile in poly.Tiles)
                    {
                        // TODO
                        //if (_wadIndex.TryGetValue(tile.SpriteName, out var file))
                        //{
                        //    wadFiles.Add(file);
                        //    imageFiles.Add(tile.SpriteName);
                        //}
                    }
                }
                int progress = 0;
                foreach (var file in wadFiles)
                {
                    busyBar.Value = (int)(++progress * 100.0 / wadFiles.Count + .5);
                    Application.DoEvents();
                    var wadImages = WAD.Extract(file);
                    foreach (var image in wadImages)
                    {
                        if (imageFiles.Contains(image.Name))
                        {
                            if (!BitmapBuffer.Contains(image.Name))
                            {
                                var b = GetBitmap(image);
                                BitmapBuffer.Push(image.Name, b);
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            busyLabel.Text = "";
            busyBar.Value = 0;
        }

        private void lbPolygons_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedMapPolygon = lbPolygons.SelectedValue as MapPolygon;
        }

        private void lbTiles_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedTile = lbTiles.SelectedValue as MapTile;
        }

        #endregion

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var path = Properties.Settings.Default.DatosDir;
            if (!string.IsNullOrEmpty(path))
                UpdateWADDirectory(path);
        }

        private void labelZoomLevel1_Click(object sender, EventArgs e)
        {
            levelPanel.Zoom = 1;
            labelZoomLevel1.BackColor = Color.LightGreen;
            labelZoomLevel12.BackColor = Color.White;
            labelZoomLevel14.BackColor = Color.White;

        }

        private void labelZoomLevel12_Click(object sender, EventArgs e)
        {
            levelPanel.Zoom = 2;
            labelZoomLevel1.BackColor = Color.White;
            labelZoomLevel12.BackColor = Color.LightGreen;
            labelZoomLevel14.BackColor = Color.White;
        }

        private void labelZoomLevel14_Click(object sender, EventArgs e)
        {
            levelPanel.Zoom = 4;
            labelZoomLevel1.BackColor = Color.White;
            labelZoomLevel12.BackColor = Color.White;
            labelZoomLevel14.BackColor = Color.LightGreen;
        }

        DateTime _lastUiUpdate = DateTime.UtcNow;
        private void UiUpdater_Tick(object sender, EventArgs e)
        {
            if ((DateTime.UtcNow - _lastUiUpdate).TotalMilliseconds > 100)
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    if (listBoxWADContent.SelectedIndex >= 0)
                        Invalidate();
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    if (listBoxVolFiles.SelectedIndex >= 0)
                        Invalidate();
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BitmapBuffer.Clear();
        }
    }
}
