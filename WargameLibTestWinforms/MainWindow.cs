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
using System.Windows.Forms;
using WargameLib;

// This is a simple WindowsForms test application.
// No fancy MVVM Pattern here, sorry. I reserve that for WPF.

namespace WargameLibTestWinforms
{
    public partial class MainWindow : Form
    {
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
            }
            var fonts = Path.Combine(directory, "FONTS");
            if (Directory.Exists(fonts))
            {
                files.AddRange(Directory.EnumerateFiles(fonts, "*.WAD", SearchOption.AllDirectories));
            }
            var items = new List<FileNameItem>();
            foreach (var file in files)
                items.Add(new FileNameItem() { FileName = file });
            listBoxFiles.DataSource = items;

            // Search Level Files
            files.Clear();
            var misiones = Path.Combine(directory, "MISIONES");
            if (Directory.Exists(recursos))
            {
                files.AddRange(Directory.EnumerateFiles(misiones, "*.VOL", SearchOption.AllDirectories));
            }
            items = new List<FileNameItem>();
            foreach (var file in files)
                items.Add(new FileNameItem() { FileName = file });
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

        private void listBoxFiles_SelectedValueChanged(object sender, EventArgs e)
        {
            listBoxWADContent.DataSource = null;
            var value = listBoxFiles.SelectedValue as FileNameItem;
            if (value == null) return;
            listBoxWADContent.DataSource = WAD.Extract(value.FileName);
        }

        private void listBoxWADContent_SelectedValueChanged(object sender, EventArgs e)
        {
            textBoxHeader.Text = "";
            Invalidate();

            var img = listBoxWADContent.SelectedValue as WADImage;
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

        int _zoom = 1;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // TODO: Doing this in OnPaint is inefficient
            if (tabControl1.SelectedIndex == 0)
            {

                // Double Buffering
                BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
                BufferedGraphics buf = currentContext.Allocate(panelImage.CreateGraphics(), panelImage.DisplayRectangle);
                var g = buf.Graphics;

                g.Clear(Color.LightGray);
                //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                var img = listBoxWADContent.SelectedValue as WADImage;
                if (img != null)
                {
                    Bitmap b = new Bitmap((int)img.Width, (int)img.Height);
                    for (int h = 0; h < img.Height; ++h)
                        for (int w = 0; w < img.Width; ++w)
                        {
                            var pix = img.Pixels[h, w];
                            b.SetPixel(w, h, Color.FromArgb(pix.Opacity, pix.R, pix.G, pix.B));
                        }
                    g.DrawImage(b, new Rectangle(0, 0, (int)img.Width * _zoom, (int)img.Height * _zoom), new Rectangle(0, 0, (int)img.Width, (int)img.Height), GraphicsUnit.Pixel);
                }

                //buf.Render();
                buf.Render(panelImage.CreateGraphics());

                // Color palette
                buf = currentContext.Allocate(panelPalette.CreateGraphics(), panelPalette.DisplayRectangle);
                g = buf.Graphics;
                g.Clear(Color.LightGray);
                int y = 32 * 10 + 32 + 1 - 1;
                int x = 8 * 10 + 8 + 1 - 1;
                g.DrawRectangle(Pens.Black, 0, 0, x, y);
                for (int i = 1; i < 8; ++i)
                {
                    g.DrawLine(Pens.Black, i * 10 + i, 0, i * 10 + i, y);
                }
                for (int i = 1; i < 32; ++i)
                {
                    g.DrawLine(Pens.Black, 0, i * 10 + i, x, i * 10 + i);
                }
                if (img != null)
                {
                    var paletteColors = img.GetPaletteColors();
                    int p = 0;
                    if (paletteColors != null)
                    {
                        for (int i = 0; i < 32; ++i)
                            for (int j = 0; j < 8; ++j)
                            {
                                using (var b = new SolidBrush(Color.FromArgb(
                                    255,
                                    paletteColors[p].R,
                                    paletteColors[p].G,
                                    paletteColors[p].B)))
                                {
                                    g.FillRectangle(b, j * 10 + 1 + j, i * 10 + 1 + i, 10, 10);
                                }
                                ++p;
                            }
                    }
                }
                //buf.Render();
                buf.Render(panelPalette.CreateGraphics());
            }
        }

        private void label1x_Click(object sender, EventArgs e)
        {
            _zoom = 1;
            label1x.BackColor = Color.LightGreen;
            label2x.BackColor = Color.White;
            label4x.BackColor = Color.White;
            Invalidate();
        }

        private void label2x_Click(object sender, EventArgs e)
        {
            _zoom = 2;
            label1x.BackColor = Color.White;
            label2x.BackColor = Color.LightGreen;
            label4x.BackColor = Color.White;
            Invalidate();
        }

        private void label4x_Click(object sender, EventArgs e)
        {
            _zoom = 4;
            label1x.BackColor = Color.White;
            label2x.BackColor = Color.White;
            label4x.BackColor = Color.LightGreen;
            Invalidate();
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

            if (item == null)
            {
                SelectedMapPolygon = null;
                return;
            }

            try
            {
                _vol = new VOL(item.FileName);
                lbPolygons.DataSource = _vol.Polys;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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


    }
}
