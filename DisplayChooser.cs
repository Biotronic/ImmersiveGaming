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
    public partial class DisplayChooser : DisplayList
    {

        public class SelectableDisplay : SimpleDisplay
        {
            private bool _selected;
            private Color _selectedColor;
            private Color _borderColor;

            public SelectableDisplay()
            {
                _selected = false;
                SelectedColor = Color.FromArgb(130, 162, 221);
                BorderColor = Color.FromArgb(220, 220, 220);
            }

            public bool Selected
            {
                get
                {
                    return _selected;
                }
                set
                {
                    if (value != _selected)
                    {
                        _selected = value;
                        OnSelectedChanged(EventArgs.Empty);
                        base.BorderColor = Selected ? SelectedColor : BorderColor;
                        Invalidate();
                    }
                }
            }

            new public Color BorderColor
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

            public Color SelectedColor
            {
                get
                {
                    return _selectedColor;
                }
                set
                {
                    if (value != _selectedColor)
                    {
                        _selectedColor = value;
                        Invalidate();
                    }
                }
            }

            protected virtual void OnSelectedChanged(EventArgs e)
            {
                if (SelectedChanged != null)
                {
                    SelectedChanged(this, e);
                }
            }

            public event EventHandler SelectedChanged;
        }

        public DisplayChooser()
        {
            InitializeComponent();
        }

        protected override Display CreateDisplay(Screen screen)
        {
            return new SelectableDisplay();
        }

        protected override void OnMonitorClick(MonitorEventArgs e)
        {
            base.OnMonitorClick(e);

            (e.Monitor as SelectableDisplay).Selected = !(e.Monitor as SelectableDisplay).Selected;
        }
    }
}
