using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmersiveGaming
{
    public partial class MouseCaptureForm : Form
    {
        public static Point GetMouseClickPosition()
        {
            var f = new MouseCaptureForm();
            Point p = new Point(0,0);
            f.MouseDown += (a, e) => p = e.Location;
            f.ShowDialog();
            return f.PointToScreen(p);
        }

        private MouseCaptureForm()
        {
            InitializeComponent();
            var bounds = Screen.AllScreens.Select(a => a.Bounds).Aggregate(Rectangle.Union);
            this.Bounds = bounds;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80000;
                return cp;
            }
        }
    }
}
