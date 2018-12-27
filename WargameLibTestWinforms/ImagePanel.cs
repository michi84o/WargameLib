using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WargameLibTestWinforms
{
    public class ImagePanel : Panel
    {
        public BitmapBuffer BitmapBuffer { get; } = new BitmapBuffer(5);

        WargameLib.WADImage _image;
        public WargameLib.WADImage Image
        {
            get => _image;
            set
            {
                if (_image == value) return;
                _image = value;
                _dirty = true;
                DoResize();
                Invalidate();
            }
        }

        int _zoom = 1;
        public int Zoom
        {
            get => _zoom;
            set
            {
                if (_zoom == value) return;
                _zoom = value;
                _dirty = true;
                DoResize();
                Invalidate();
            }
        }

        public ImagePanel()
        {
            //DoubleBuffered = true;
        }

        void DoResize()
        {
            if (_image != null)
            {
                Width = (int)_image.Width * _zoom;
                Height = (int)_image.Height * _zoom;
            }
            else
            {
                Width = 100;
                Height = 50;
            }
        }

        BufferedGraphics _buffer;
        bool _dirty = true;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!_dirty && _buffer != null)
            {
                //_buffer.Render(CreateGraphics()); Not required
                return;
            }

            if (_dirty && _buffer != null)
            {
                _buffer.Dispose();
                _buffer = null;
            }

            if (_buffer == null)
            {
                var currentContext = BufferedGraphicsManager.Current;
                _buffer = currentContext.Allocate(CreateGraphics(), DisplayRectangle);
            }

            var g = _buffer.Graphics;

            g.Clear(Color.LightGray);
            //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            var img = _image;
            if (img != null)
            {
                var b = BitmapBuffer.Get(img.Name);
                if (b == null)
                {
                    b = new Bitmap((int)img.Width, (int)img.Height);
                    for (int h = 0; h < img.Height; ++h)
                        for (int w = 0; w < img.Width; ++w)
                        {
                            var pix = img.Pixels[h, w];
                            b.SetPixel(w, h, Color.FromArgb(pix.Opacity, pix.R, pix.G, pix.B));
                        }
                    BitmapBuffer.Push(img.Name, b);
                }
                g.DrawImage(b, new Rectangle(0, 0, (int)img.Width * _zoom, (int)img.Height * _zoom), new Rectangle(0, 0, (int)img.Width, (int)img.Height), GraphicsUnit.Pixel);
            }
            else
            {
                using (var font = new Font("Arial", 16))
                {
                    g.DrawString("No Image", new Font("Arial", 16), Brushes.Black, 10, 10);
                }
            }
            _buffer.Render(CreateGraphics());
        }
    }
}
