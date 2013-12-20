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
    public partial class NewGameForm : Form
    {
        public NewGameForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            chkFileName.Checked = false;
            cmbWindowTitle.SelectedIndex = 0;
            cmbClassName.SelectedIndex = 0;
            cmbFileName.SelectedIndex = 0;
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            txtWindowTitle.Enabled = chkWindowTitle.Checked;
            txtClassName.Enabled = chkClassName.Checked;
            txtFileName.Enabled = chkFileName.Checked;
        }
    }
}
