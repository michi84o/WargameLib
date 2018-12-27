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

        List<FileNameItem> _wadFiles = new List<FileNameItem>();

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
            _wadFiles.Clear();
            foreach (var file in files)
            {
                _wadFiles.Add(new FileNameItem() { FileName = file });
            }
            listBoxFiles.DataSource = null;
            listBoxFiles.DataSource = _wadFiles;

            // Not required
            //CreateWadIndex(files.ToList()); // Creates background Task that uses the list
            files.Clear();

            // Search Level Files
            var misiones = Path.Combine(directory, "MISIONES");
            if (Directory.Exists(recursos))
            {
                files.AddRange(Directory.EnumerateFiles(misiones, "*.VOL", SearchOption.AllDirectories));
            }

            var items = new List<FileNameItem>();
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
            imagePanel.BitmapBuffer.Clear();
            listBoxWADContent.DataSource = null;
            var value = listBoxFiles.SelectedValue as FileNameItem;
            if (value == null)
                return;

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
        }

        private void label2x_Click(object sender, EventArgs e)
        {
            imagePanel.Zoom = 2;
            label1x.BackColor = Color.White;
            label2x.BackColor = Color.LightGreen;
            label4x.BackColor = Color.White;
        }

        private void label4x_Click(object sender, EventArgs e)
        {
            imagePanel.Zoom = 4;
            label1x.BackColor = Color.White;
            label2x.BackColor = Color.White;
            label4x.BackColor = Color.LightGreen;
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
                lbPolygons.DataSource = _vol.Polys;
                tbSelectedVol.Text =
                    "Width: " + _vol.Width + "\r\nHeight: " + _vol.Height +
                    "\r\nLimits: {"+_vol.XMin+";"+_vol.YMin+ "}-{" + _vol.XMax + ";" + _vol.YMax + "}";

                // Load all textures for that level:
                //HashSet<string> wadFiles = new HashSet<string>();
                //HashSet<string> imageFiles = new HashSet<string>();
                //busyLabel.Text = "Loading Level";
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

                // Load bitmaps for level
                levelPanel.BitmapBuffer.Clear();
                try
                {
                    // Get WAD file with same index:
                    var file = Path.GetFileName(_vol.File);
                    if (file.Length > 4)
                    {
                        file = file.Substring(0, file.Length - 4);
                        int numindex = file.Length - 1;
                        if (Char.IsDigit(file[file.Length - 1]))
                        {
                            for (int i = file.Length - 1; i >= 0; --i)
                            {
                                if (!Char.IsDigit(file[i]))
                                {
                                    numindex = i + 1;
                                    break;
                                }
                            }
                            file = file.Substring(numindex, file.Length - numindex);
                            var wadfile = _wadFiles.FirstOrDefault(o => o.FileName.EndsWith(file + ".wad", StringComparison.InvariantCultureIgnoreCase));
                            if (wadfile != null)
                            {
                                var wads = WAD.Extract(wadfile.FileName);
                                foreach (var wad in wads)
                                {
                                    var bitmap = GetBitmap(wad);
                                    if (bitmap != null)
                                        levelPanel.BitmapBuffer.Push(wad.Name, bitmap);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("WAD extraction for level failed: " + ex.Message);
                }

                levelPanel.Vol = _vol;

                //int progress = 0;
                //foreach (var file in wadFiles)
                //{
                //    busyBar.Value = (int)(++progress * 100.0 / wadFiles.Count + .5);
                //    Application.DoEvents();
                //    var wadImages = WAD.Extract(file);
                //    foreach (var image in wadImages)
                //    {
                //        if (imageFiles.Contains(image.Name))
                //        {
                //            if (!BitmapBuffer.Contains(image.Name))
                //            {
                //                var b = GetBitmap(image);
                //                BitmapBuffer.Push(image.Name, b);
                //            }
                //        }
                //    }
                //}


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
    }
}
