using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WargameLibTestWinforms
{
    public class PalettePanel : Panel
    {
        WargameLib.WADImage _image;
        public WargameLib.WADImage Image
        {
            get => _image;
            set
            {
                if (_image == value) return;
                _image = value;
                _dirty = true;
                Invalidate();
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

            g = _buffer.Graphics;
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
            var img = _image;
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
            _buffer.Render(CreateGraphics());
        }
    }
}
