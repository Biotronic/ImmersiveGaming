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
            _borderColor = Color.FromArgb(220, 220, 220);
            InitializeComponent();
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

                    var clr = new ColorF(_borderColor) / 220.0;

                    BrightHighlight = clr * 234.0;
                    DarkHighlight = clr * 192;
                    BrightShadow = clr * 212;
                    DarkShadow = clr * 130;
                    BrightFace = clr * 248;
                    DarkFace = clr * 178;

                    Invalidate();
                }
            }
        }
    }
}
