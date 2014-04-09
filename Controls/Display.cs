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

            BackColor = Color.FromArgb(97, 117, 170);
            BorderColor = Color.FromArgb(220, 220, 220);
        }

        private Color _borderColor;
        public virtual Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                if (value != _borderColor)
                {
                    _borderColor = value;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawBorder(e.Graphics, ClientRectangle, BorderColor);
            e.Graphics.Clip = new Region(e.Graphics.ClipBounds.Deflated(3, 3, 3, 3));
            OnPaintWallpaper(new PaintEventArgs(e.Graphics, e.ClipRectangle.Deflated(3, 3, 3, 3)));
        }

        protected virtual void OnPaintWallpaper(PaintEventArgs e)
        {
            var BrightBack = ((ColorF)BackColor * 1.25).FullAlpha.Clamped;
            var DarkBack = ((ColorF)BackColor * 0.72).FullAlpha.Clamped;
            DrawWallpaper(e.Graphics, ClientRectangle, BrightBack, DarkBack);
        }

        public static void DrawBorder(Graphics graphics, Rectangle rect, Color color)
        {
            var BrightHighlight = ((ColorF)color * 234 / 220.0).FullAlpha;
            var DarkHighlight = ((ColorF)color * 192 / 220.0).FullAlpha;
            var BrightShadow = ((ColorF)color * 212 / 220.0).FullAlpha;
            var DarkShadow = ((ColorF)color * 130 / 220.0).FullAlpha;
            var BrightFace = ((ColorF)color * 248 / 220.0).FullAlpha;
            var DarkFace = ((ColorF)color * 178 / 220.0).FullAlpha;
            var bgBrush = new SolidBrush(((ColorF)BrightFace + (ColorF)DarkFace) / 2);
            float angle = (float)(Math.Atan2(rect.Width, rect.Height) * 180 / Math.PI);
            var hlPen = new Pen(new LinearGradientBrush(rect, BrightHighlight, DarkHighlight, angle));
            var shPen = new Pen(new LinearGradientBrush(rect, BrightShadow, DarkShadow, angle));
            var fcPen = new Pen(new LinearGradientBrush(rect, BrightFace, DarkFace, angle));

            graphics.FillRectangle(bgBrush, rect);


            graphics.DrawLine(hlPen, new Point(1, 0), new Point(rect.Width - 2, 0));
            graphics.DrawLine(hlPen, new Point(0, 1), new Point(0, rect.Height - 2));
            graphics.DrawLine(hlPen, new Point(rect.Width - 3, 3), new Point(rect.Width - 3, rect.Height - 3));
            graphics.DrawLine(hlPen, new Point(3, rect.Height - 3), new Point(rect.Width - 3, rect.Height - 3));
            
            
            graphics.DrawLine(shPen, new Point(3, 2), new Point(rect.Width - 4, 2));
            graphics.DrawLine(shPen, new Point(2, 3), new Point(2, rect.Height - 4));
            graphics.DrawLine(shPen, new Point(rect.Width - 1, 1), new Point(rect.Width - 1, rect.Height - 2));
            graphics.DrawLine(shPen, new Point(1, rect.Height - 1), new Point(rect.Width - 1, rect.Height - 1));
            
            graphics.DrawRectangle(fcPen, rect.Deflated(1, 1, 2, 2));
        }

        public static void DrawWallpaper(Graphics graphics, Rectangle rect, Color light, Color shadow)
        {
            float angle = (float)(Math.Atan2(rect.Width, rect.Height) * 180 / Math.PI);
            var screenRect = rect.Deflated(3, 3);
            var wpBrush = new LinearGradientBrush(rect, light, shadow, angle);
            var shadowColorA = ColorF.FromHSLA(shadow.GetHue() / 360, shadow.GetSaturation() * 2, shadow.GetBrightness() / 4, 0.5);
            var shadowColorB = ColorF.FromHSLA(light.GetHue() / 360, light.GetSaturation() * 2, light.GetBrightness() / 4, 0.5);
            var shadowPen = new Pen(new LinearGradientBrush(rect, shadowColorA, shadowColorB, angle));
            var shadowColorC = (shadowColorA + shadowColorB) / 2;
            shadowColorC.A /= 2;
            var innerShadowPen = new Pen(shadowColorC);
            graphics.FillRectangle(wpBrush, screenRect);
            graphics.DrawRectangle(innerShadowPen, 4, 3, rect.Width - 9, rect.Height - 6);
            graphics.DrawRectangle(shadowPen, screenRect.Deflated(0, 0, 1, 1));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Invalidate();
            base.OnSizeChanged(e);
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
