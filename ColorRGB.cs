using System;
using System.Diagnostics;
using System.Drawing;

namespace ImmersiveGaming
{
    public struct ColorF
    {
        public double R;
        public double G;
        public double B;
        public double A;

        public ColorF(Color value)
        {
            this.R = value.R / 255.0;
            this.G = value.G / 255.0;
            this.B = value.B / 255.0;
            this.A = value.A / 255.0;
        }

        public ColorF(double a, double r, double g, double b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public ColorF Clamped
        {
            get
            {
                return new ColorF(A, Math.Min(1, R), Math.Min(1, G), Math.Min(1, B));
            }
        }

        public ColorF FullAlpha
        {
            get
            {
                return new ColorF(1.0, R, G, B);
            }
        }

        public static implicit operator Color(ColorF rgb)
        {
            Debug.Assert(rgb.A >= 0 && rgb.A <= 1.0);
            Debug.Assert(rgb.R >= 0 && rgb.R <= 1.0);
            Debug.Assert(rgb.G >= 0 && rgb.G <= 1.0);
            Debug.Assert(rgb.B >= 0 && rgb.B <= 1.0);
            return Color.FromArgb((byte)(rgb.A * 255), (byte)(rgb.R * 255), (byte)(rgb.G * 255), (byte)(rgb.B * 255));
        }

        public static explicit operator ColorF(Color c)
        {
            return new ColorF(c);
        }

        public static ColorF operator +(ColorF a, ColorF b)
        {
            return new ColorF(
                a.A + b.A,
                a.R + b.R,
                a.G + b.G,
                a.B + b.B);
        }

        public static ColorF operator -(ColorF a, ColorF b)
        {
            return new ColorF(
                a.A + b.A,
                a.R + b.R,
                a.G + b.G,
                a.B + b.B);
        }

        public static ColorF operator *(ColorF a, double b)
        {
            return new ColorF(
                a.A * b,
                a.R * b,
                a.G * b,
                a.B * b);
        }

        public static ColorF operator *(double a, ColorF b)
        {
            return b * a;
        }

        public static ColorF operator /(ColorF a, double b)
        {
            return a * (1.0/b);
        }
    }

    public struct ColorRGB
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public ColorRGB(Color value)
        {
            this.R = value.R;
            this.G = value.G;
            this.B = value.B;
            this.A = value.A;
        }

        public static implicit operator Color(ColorRGB rgb)
        {
            Color c = Color.FromArgb(rgb.A, rgb.R, rgb.G, rgb.B);
            return c;
        }

        public static explicit operator ColorRGB(Color c)
        {
            return new ColorRGB(c);
        }

        public static explicit operator ColorRGB(ColorF c)
        {
            return new ColorRGB(c);
        }

        // Given H,S,L in range of 0-1
        // Returns a Color (RGB struct) in range of 0-255
        public static ColorRGB FromHSL(double H, double S, double L)
        {
            return FromHSLA(H, S, L, 1.0);
        }

        // Given H,S,L,A in range of 0-1
        // Returns a Color (RGB struct) in range of 0-255
        public static ColorRGB FromHSLA(double H, double S, double L, double A)
        {
            double v;
            double r, g, b;
            if (A > 1.0)
                A = 1.0;

            r = L;   // default to gray
            g = L;
            b = L;
            v = (L <= 0.5) ? (L * (1.0 + S)) : (L + S - L * S);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = L + L - v;
                sv = (v - m) / v;
                H *= 6.0;
                sextant = (int)H;
                fract = H - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            ColorRGB rgb = new ColorRGB();
            rgb.R = Convert.ToByte(Math.Max(0, r * 255.0f));
            rgb.G = Convert.ToByte(Math.Max(0, g * 255.0f));
            rgb.B = Convert.ToByte(Math.Max(0, b * 255.0f));
            rgb.A = Convert.ToByte(Math.Max(0, A * 255.0f));
            return rgb;
        }

        // Hue in range from 0.0 to 1.0
        public float H
        {
            get
            {
                // Use System.Drawing.Color.GetHue, but divide by 360.0F 
                // because System.Drawing.Color returns hue in degrees (0 - 360)
                // rather than a number between 0 and 1.
                return ((Color)this).GetHue() / 360.0F;
            }
            set
            {
                this = ColorRGB.FromHSLA(value, S, L, A);
            }
        }

        // Saturation in range 0.0 - 1.0
        public float S
        {
            get
            {
                return ((Color)this).GetSaturation();
            }
            set
            {
                this = ColorRGB.FromHSLA(H, value, L, A);
            }
        }

        // Lightness in range 0.0 - 1.0
        public float L
        {
            get
            {
                return ((Color)this).GetBrightness();
            }
            set
            {
                this = ColorRGB.FromHSLA(H, S, value, A);
            }
        }
    }
}