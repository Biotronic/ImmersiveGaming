using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmersiveGaming
{
    public partial class SimpleDisplay : Display
    {
        public SimpleDisplay()
        {
            BackColor = Color.FromArgb(97, 117, 170);
            BorderColor = Color.FromArgb(220, 220, 220);
            InitializeComponent();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            BrightBack = ((ColorF)BackColor * 1.25).FullAlpha.Clamped;
            DarkBack = ((ColorF)BackColor * 0.72).FullAlpha.Clamped;
        }

        private Color _borderColor;
        public Color BorderColor
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

                    var clr = (ColorF)_borderColor / 220.0;

                    BrightHighlight = (clr * 234).FullAlpha;
                    DarkHighlight = (clr * 192).FullAlpha;
                    BrightShadow = (clr * 212).FullAlpha;
                    DarkShadow = (clr * 130).FullAlpha;
                    BrightFace = (clr * 248).FullAlpha;
                    DarkFace = (clr * 178).FullAlpha;

                    Invalidate();
                }
            }
        }
    }
}
