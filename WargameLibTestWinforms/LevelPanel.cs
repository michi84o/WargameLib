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

        public LevelPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            if (!_dirty && _buffer != null)
            {
                _buffer.Render(/*CreateGraphics()*/ e.Graphics);
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
            g.Clear(Color.DarkMagenta);
            //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            // Read the VOL structure and place sprites

            if (vol != null)
            {
                foreach (var poly in vol.Polys)
                {
                    // TODO: Bad performance
                    foreach (var tile in poly.Tiles)
                    {
                        if (tile.SpriteName.StartsWith("-")) continue; // Tile set to invisible

                        //var b = GetBitmap(tile.SpriteName);
                        var bb = BitmapBuffer.Get(tile.SpriteName);
                        if (bb != null)
                        {
                            Bitmap b;
                            bool needDispose = false;
                            if (bb.Width != tile.Width || bb.Height != tile.Height || tile.Offset.X != 0 || tile.Offset.Y != 0)
                            {
                                // Make copy. Apply offset
                                b = new Bitmap(tile.Width, tile.Height);
                                needDispose = true;

                                // Offset correction
                                int ho = tile.Offset.X;
                                int vo = tile.Offset.Y;
                                // TODO: Could mean tile.Width/Height instead of bb
                                while (ho < 0) ho += bb.Width;
                                while (ho >= bb.Width) ho -= bb.Width;
                                while (vo < 0) vo += bb.Height;
                                while (vo >= bb.Height) vo -= bb.Height;

                                using (Graphics grD = Graphics.FromImage(b))
                                {
                                    // Horizontal offset means: Shift Image left (=start copy at offset)
                                    // Also if target region is larger, use source bitmap as tile

                                    var sourceRect = new Rectangle(ho, vo, bb.Width - ho, bb.Height - vo);
                                    var destRect = new Rectangle(0, 0, sourceRect.Width, sourceRect.Height);
                                    grD.DrawImage(bb,
                                        destRect,
                                        sourceRect,
                                        GraphicsUnit.Pixel);

                                    // Repeat start tile vertically:
                                    if (tile.Height > (bb.Height - vo))
                                    {
                                        int yRepeatCnt = tile.Height / bb.Height;
                                        if (yRepeatCnt > 0)
                                        {
                                            // Ignore vertical offset, just take horizontal offset
                                            sourceRect = new Rectangle(ho, 0, bb.Width - ho, bb.Height);
                                            for (int i = 0; i < yRepeatCnt; ++i)
                                            {
                                                destRect = new Rectangle(0, vo + i * bb.Height, sourceRect.Width, sourceRect.Height);
                                                grD.DrawImage(bb, destRect, sourceRect, GraphicsUnit.Pixel);
                                            }
                                        }
                                    }
                                    // Repeat tile horizontally and vertically
                                    if (tile.Width > (bb.Width - ho))
                                    {
                                        int xRepeatCnt = tile.Width / bb.Width;
                                        if (xRepeatCnt > 0)
                                        {
                                            // Repeat the whole image without offset
                                            sourceRect = new Rectangle(0, 0, bb.Width, bb.Height);
                                            for (int x = bb.Width - ho; x < tile.Width; x += bb.Width)
                                            {
                                                for (int y = 0; y < tile.Height; y += bb.Height)
                                                {
                                                    destRect = new Rectangle(x, y, sourceRect.Width, sourceRect.Height);
                                                    grD.DrawImage(bb, destRect, sourceRect, GraphicsUnit.Pixel);
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            else b = bb;

                            if (tile.Transformation != MapTileTransformation.None)
                            {
                                if (!needDispose)
                                {
                                    b = new Bitmap(b); // b == bb at this point. Don't touch original
                                    needDispose = true;
                                }

                                if ((tile.Transformation & MapTileTransformation.FlipY) != 0)
                                    b.RotateFlip(RotateFlipType.RotateNoneFlipY);
                                if ((tile.Transformation & MapTileTransformation.MirrorX) != 0)
                                    b.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                // TODO: Light or explosion
                            }
                            g.DrawImage(b,
                                new Rectangle((tile.Position.X) / div, (tile.Position.Y) / div, (int)b.Width / div, (int)b.Height / div),
                                new Rectangle(0, 0, (int)b.Width, (int)b.Height), GraphicsUnit.Pixel);


                            if (needDispose)
                            {
                                b.Dispose();
                                // Debug Only:
                                //g.DrawRectangle(Pens.Yellow,
                                //    new Rectangle((tile.Position.X) / div, (tile.Position.Y) / div, (int)tile.Width / div, (int)tile.Height / div));
                            }

                        }
                        else
                        {
                            g.DrawRectangle(Pens.Violet, new Rectangle((tile.Position.X) / div, ( tile.Position.Y) / div, (int)tile.Width / div, (int)tile.Height / div));
                        }
                    }

                    // For some reason we have to divide y by 1.6 to stretch polygons correctly. Maybe that is due to perspective
                    var f1 = 100;
                    var f2 = 155;

                    var x0 = poly.Center.X + centerOffsetX;
                    var y0 = poly.Center.Y + centerOffsetY;
                    if (poly.Vertices.Count > 1)
                    {
                        WargameLib.Point prev;
                        WargameLib.Point cur;
                        for (int i = 1; i < poly.Vertices.Count; ++i)
                        {
                            prev = poly.Vertices[i - 1];
                            cur = poly.Vertices[i];
                            g.DrawLine(Pens.Blue, /*f1**/ (x0 + prev.X) / (div/**f2*/), f1 * (y0 + prev.Y) / (div * f2), /*f1 **/ (x0 + cur.X) / (div/* * f2*/), f1 * (y0 + cur.Y) / (div * f2));
                        }
                        prev = poly.Vertices[poly.Vertices.Count - 1];
                        cur = poly.Vertices[0];
                        g.DrawLine(Pens.Blue, /*f1 **/ (x0 + prev.X) / (div/* * f2*/), f1 * (y0 + prev.Y) / (div * f2), /*f1 **/ (x0 + cur.X) / (div/* * f2*/), f1 * (y0 + cur.Y) / (div * f2));
                    }
                }
            }

            _buffer.Render(/*CreateGraphics()*/ e.Graphics);
            _dirty = false;
        }
    }
}
