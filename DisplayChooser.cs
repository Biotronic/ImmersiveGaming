﻿using System;
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

        public class SelectableDisplay : Display
        {
            private bool _selected;
            private Color _selectedColor;
            private Color _borderColor;

            public SelectableDisplay()
            {
                _selected = false;
                SelectedColor = Color.FromArgb(135, 167, 220);
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
                        if (!Selected)
                        {
                            base.BorderColor = _borderColor;
                        }
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
                        if (Selected)
                        {
                            base.BorderColor = _selectedColor;
                        }
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
            var disp = new SelectableDisplay();
            disp.SelectedChanged += disp_SelectedChanged;
            return disp;
        }

        void disp_SelectedChanged(object sender, EventArgs e)
        {
            OnMonitorSelectedChanged(new MonitorEventArgs(sender as Display));
        }
        virtual protected void OnMonitorSelectedChanged(MonitorEventArgs e)
        {
            if (MonitorSelectedChanged != null)
            {
                MonitorSelectedChanged(this, e);
            }
        }

        public event EventHandler<MonitorEventArgs> MonitorSelectedChanged;

        protected override void OnMonitorClick(MonitorEventArgs e)
        {
            base.OnMonitorClick(e);

            (e.Monitor as SelectableDisplay).Selected = !(e.Monitor as SelectableDisplay).Selected;
        }
    }
}
