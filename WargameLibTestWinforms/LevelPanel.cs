using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WargameLib;

namespace WargameLibTestWinforms
{
    public class LevelPanel : Panel
    {
        public BitmapBuffer BitmapBuffer { get; } = new BitmapBuffer(1000);

        VOL _vol;
        public VOL Vol
        {
            get => _vol;
            set
            {
                if (_vol == value) return;
                _vol = value;
                _dirty = true;
                DoResize();
                Invalidate();
            }
        }

        int _zoom = 1;
        // Inverted zoom factor!
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

        BufferedGraphics _buffer;
        bool _dirty = true;

        void DoResize()
        {
            if (_vol != null)
            {
                Width = (int)(_vol.XMax - _vol.XMin) / _zoom;
                Height = (int)(_vol.YMax - _vol.YMin) / _zoom;
            }
            else
            {
                Width = 100;
                Height = 50;
            }
        }

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

            var vol = _vol;
            var div = _zoom;

            int centerOffsetX = 0;
            int centerOffsetY = 0;

            if (vol != null)
            {
                if (vol.XMin < 0) centerOffsetX = -1 * vol.XMin; // div is applied later
                if (vol.YMin < 0) centerOffsetY = -1 * vol.YMin;
            }

            var g = _buffer.Graphics;
            g.Clear(Color.LightGray);
            //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            // Read the VOL structure and place sprites

            if (vol != null)
            {
                foreach (var poly in vol.Polys)
                {
                    var x0 = poly.Center.X + centerOffsetX;
                    var y0 = poly.Center.Y + centerOffsetY;

                    // TODO: Result looks very random
                    // Bad performance
                    foreach (var tile in poly.Tiles)
                    {
                        if (tile.SpriteName.StartsWith("-")) continue; // Tile set to invisible
                                                                       //var b = GetBitmap(tile.SpriteName);
                        var b = BitmapBuffer.Get(tile.SpriteName);
                        if (b != null)
                        {
                            g.DrawImage(b,
                                new Rectangle((x0 + tile.Position.X) / div, (y0 + tile.Position.Y) / div, (int)b.Width / div, (int)b.Height / div),
                                new Rectangle(0, 0, (int)b.Width, (int)b.Height), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            g.DrawRectangle(Pens.Violet, new Rectangle((x0 + tile.Position.X) / div, (y0 + tile.Position.Y) / div, (int)tile.Width / div, (int)tile.Height / div));
                        }
                    }

                    if (poly.Vertices.Count > 1)
                    {
                        WargameLib.Point prev;
                        WargameLib.Point cur;
                        for (int i = 1; i < poly.Vertices.Count; ++i)
                        {
                            prev = poly.Vertices[i - 1];
                            cur = poly.Vertices[i];
                            g.DrawLine(Pens.Blue, (x0 + prev.X) / div, (y0 + prev.Y) / div, (x0 + cur.X) / div, (y0 + cur.Y) / div);
                        }
                        prev = poly.Vertices[poly.Vertices.Count - 1];
                        cur = poly.Vertices[0];
                        g.DrawLine(Pens.Blue, (x0 + prev.X) / div, (y0 + prev.Y) / div, (x0 + cur.X) / div, (y0 + cur.Y) / div);
                    }
                }
            }

            _buffer.Render(CreateGraphics());
        }
    }
}
