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

        private void chooseWADDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenDirectoryDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                UpdateWADDirectory(dlg.SelectedDirectory);
            }
        }

        void UpdateWADDirectory(string directory)
        {
            var files = Directory.GetFiles(directory, "*.WAD");
            var items = new List<FileNameItem>();
            foreach (var file in files)
                items.Add(new FileNameItem() { FileName = file });
            listBoxFiles.DataSource = items;
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

            buf.Render();
            buf.Render(panelImage.CreateGraphics());
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
    }
}
