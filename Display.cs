using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace ImmersiveGaming
{
    public partial class Display : UserControl
    {
        public Display()
        {
            InitializeComponent();

            BrightHighlight = Color.FromArgb(234, 234, 234);
            DarkHighlight = Color.FromArgb(192, 192, 192);
            BrightShadow = Color.FromArgb(212, 212, 212);
            DarkShadow = Color.FromArgb(130, 130, 130);
            BrightFace = Color.FromArgb(248, 248, 248);
            DarkFace = Color.FromArgb(178, 178, 178);
            BrightBack = Color.FromArgb(131, 150, 199);
            DarkBack = Color.FromArgb(58, 80, 138);
        }

        private Image _Wallpaper;
        private Color _BrightHighlight;
        private Color _DarkHighlight;
        private Color _BrightShadow;
        private Color _DarkShadow;
        private Color _BrightFace;
        private Color _DarkFace;
        private Color _BrightBack;
        private Color _DarkBack;

        public Image Wallpaper
        {
            get
            {
                return _Wallpaper;
            }
            set
            {
                if (value != _Wallpaper)
                {
                    _Wallpaper = value;
                    Invalidate();
                }
            }
        }
        protected Color BrightHighlight
        {
            get
            {
                return _BrightHighlight;
            }
            set
            {
                if (value != _BrightHighlight)
                {
                    _BrightHighlight = value;
                    Invalidate();
                }
            }
        }
        protected Color DarkHighlight
        {
            get
            {
                return _DarkHighlight;
            }
            set
            {
                if (value != _DarkHighlight)
                {
                    _DarkHighlight = value;
                    Invalidate();
                }
            }
        }
        protected Color BrightShadow
        {
            get
            {
                return _BrightShadow;
            }
            set
            {
                if (value != _BrightShadow)
                {
                    _BrightShadow = value;
                    Invalidate();
                }
            }
        }
        protected Color DarkShadow
        {
            get
            {
                return _DarkShadow;
            }
            set
            {
                if (value != _DarkShadow)
                {
                    _DarkShadow = value;
                    Invalidate();
                }
            }
        }
        protected Color BrightFace
        {
            get
            {
                return _BrightFace;
            }
            set
            {
                if (value != _BrightFace)
                {
                    _BrightFace = value;
                    Invalidate();
                }
            }
        }
        protected Color DarkFace
        {
            get
            {
                return _DarkFace;
            }
            set
            {
                if (value != _DarkFace)
                {
                    _DarkFace = value;
                    Invalidate();
                }
            }
        }
        protected Color BrightBack
        {
            get
            {
                return _BrightBack;
            }
            set
            {
                if (value != _BrightBack)
                {
                    _BrightBack = value;
                    Invalidate();
                }
            }
        }
        protected Color DarkBack
        {
            get
            {
                return _DarkBack;
            }
            set
            {
                if (value != _DarkBack)
                {
                    _DarkBack = value;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var bgBrush = new SolidBrush(((ColorF)BrightFace + (ColorF)DarkFace) / 2);

            e.Graphics.FillRectangle(bgBrush, ClientRectangle);

            float angle = (float)(Math.Atan2(ClientRectangle.Width, ClientRectangle.Height) * 180 / Math.PI);

            var hlPen = new Pen(new LinearGradientBrush(ClientRectangle, BrightHighlight, DarkHighlight, angle));
            var shPen = new Pen(new LinearGradientBrush(ClientRectangle, BrightShadow, DarkShadow, angle));
            var fcPen = new Pen(new LinearGradientBrush(ClientRectangle, BrightFace, DarkFace, angle));

            e.Graphics.DrawLine(hlPen, new Point(1, 0), new Point(ClientRectangle.Width - 2, 0));
            e.Graphics.DrawLine(hlPen, new Point(0, 1), new Point(0, ClientRectangle.Height - 2));
            e.Graphics.DrawLine(hlPen, new Point(ClientRectangle.Width - 3, 3), new Point(ClientRectangle.Width - 3, ClientRectangle.Height - 3));
            e.Graphics.DrawLine(hlPen, new Point(3, ClientRectangle.Height - 3), new Point(ClientRectangle.Width - 3, ClientRectangle.Height - 3));


            e.Graphics.DrawLine(shPen, new Point(3, 2), new Point(ClientRectangle.Width - 4, 2));
            e.Graphics.DrawLine(shPen, new Point(2, 3), new Point(2, ClientRectangle.Height - 4));
            e.Graphics.DrawLine(shPen, new Point(ClientRectangle.Width - 1, 1), new Point(ClientRectangle.Width - 1, ClientRectangle.Height - 2));
            e.Graphics.DrawLine(shPen, new Point(1, ClientRectangle.Height - 1), new Point(ClientRectangle.Width - 1, ClientRectangle.Height - 1));

            e.Graphics.DrawRectangle(fcPen, ClientRectangle.Deflated(1,1,2,2));

            e.Graphics.Clip = new Region(e.Graphics.ClipBounds.Deflated(3,3,3,3));
            OnPaintWallpaper(new PaintEventArgs(e.Graphics, e.ClipRectangle.Deflated(3, 3, 3, 3)));
        }

        protected virtual void OnPaintWallpaper(PaintEventArgs e)
        {
            float angle = (float)(Math.Atan2(ClientRectangle.Width, ClientRectangle.Height) * 180 / Math.PI);
            var screenRect = ClientRectangle.Deflated(3, 3);

            if (Wallpaper == null)
            {
                var wpBrush = new LinearGradientBrush(ClientRectangle, BrightBack, DarkBack, angle);
                e.Graphics.FillRectangle(wpBrush, screenRect);
            }
            else
            {
                var wpBounds = new Rectangle(0, 0, Wallpaper.Width, Wallpaper.Height);
                e.Graphics.DrawImage(Wallpaper, screenRect, wpBounds, GraphicsUnit.Pixel);
            }

            var shadowColorA = ColorRGB.FromHSLA(DarkBack.GetHue() / 360, DarkBack.GetSaturation() * 2, DarkBack.GetBrightness() / 4, 0.5);
            var shadowColorB = ColorRGB.FromHSLA(BrightBack.GetHue() / 360, BrightBack.GetSaturation() * 2, BrightBack.GetBrightness() / 4, 0.5);

            var shadowPen = new Pen(new LinearGradientBrush(ClientRectangle, shadowColorA, shadowColorB, angle));

            shadowColorA.A /= 2;
            shadowColorB.A /= 2;
            var p1 = new SolidBrush(shadowColorA);
            var p2 = new SolidBrush(shadowColorB);
            e.Graphics.FillRectangle(p1, 3, 4, 1, ClientRectangle.Height - 8);
            e.Graphics.FillRectangle(p2, ClientRectangle.Width - 5, 4, 1, ClientRectangle.Height - 8);
            e.Graphics.DrawRectangle(shadowPen, screenRect.Deflated(0, 0, 1, 1));
        }
    }

    static partial class Extensions
    {
        public static Rectangle Deflated(this Rectangle rect, Size size)
        {
            return rect.Deflated(size.Width, size.Height);
        }
        public static Rectangle Deflated(this Rectangle rect, int width, int height)
        {
            rect.Inflate(-width, -height);
            return rect;
        }
        public static Rectangle Deflated(this Rectangle rect, int left, int top, int right, int bottom)
        {
            return new Rectangle(rect.Left + left, rect.Top + top, rect.Width - left - right, rect.Height - top - bottom);
        }
        public static RectangleF Deflated(this RectangleF rect, Size size)
        {
            return rect.Deflated(size.Width, size.Height);
        }
        public static RectangleF Deflated(this RectangleF rect, int width, int height)
        {
            rect.Inflate(-width, -height);
            return rect;
        }
        public static RectangleF Deflated(this RectangleF rect, int left, int top, int right, int bottom)
        {
            return new RectangleF(rect.Left + left, rect.Top + top, rect.Width - left - right, rect.Height - top - bottom);
        }
        public static void DrawLines(Graphics graphic, Pen pen, Point[][] lines)
        {
            foreach (var line in lines)
            {
                graphic.DrawLines(pen, line);
            }
        }
    }
}
