using Biotronic;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ImmersiveGaming
{
    public partial class NewGameForm : UserControl
    {
        bool checkContiguousMonitors = true;
        GameInfo game = null;

        public NewGameForm()
        {
            InitializeComponent();
            UpdateSelectedMonitorColors();
        }

        public GameInfo Game
        {
            get
            {
                return game;
            }
            set
            {
                if (game != value)
                {
                    game = null;
                    UpdateFromGame(value);
                    game = value;
                }
            }
        }

        private void UpdateFromGame(GameInfo value)
        {
            Enabled = value != null;
            if (value == null)
            {
                return;
            }

            chkWindowTitle.Checked = value.title.active;
            cmbWindowTitle.SelectedIndex = (int)value.title.operation;
            txtWindowTitle.Text = value.title.pattern;
            chkClassName.Checked = value.className.active;
            cmbClassName.SelectedIndex = (int)value.className.operation;
            txtClassName.Text = value.className.pattern;
            chkFileName.Checked = value.file.active;
            cmbFileName.SelectedIndex = (int)value.file.operation;
            txtFileName.Text = value.file.pattern;

            chkBlackout.Checked = value.blackoutUnused;
            chkHideCursor.Checked = value.hideMouse;

            foreach (DisplayChooser.SelectableDisplay screen in monitors.Monitors)
            {
                screen.Selected = false;
            }

            foreach (DisplayChooser.SelectableDisplay screen in value.monitors.Select(a=>monitors.ScreenFromBounds(a.Bounds)).Where(a=>a!=null))
            {
                screen.Selected = true;
            }
        }

        private void UpdateToGame()
        {
            if (game == null)
            {
                return;
            }

            game.title = new Comparer(
                chkWindowTitle.Checked, (ComparisonOperator)
                cmbWindowTitle.SelectedIndex,
                txtWindowTitle.Text);
            game.className = new Comparer(
                chkClassName.Checked, (ComparisonOperator)
                cmbClassName.SelectedIndex,
                txtClassName.Text);
            game.file = new Comparer(
                chkFileName.Checked, (ComparisonOperator)
                cmbFileName.SelectedIndex,
                txtFileName.Text);

            game.blackoutUnused = chkBlackout.Checked;
            game.hideMouse = chkHideCursor.Checked;
            game.monitors = Screen.AllScreens.ZipFilter(monitors.Monitors.Cast<DisplayChooser.SelectableDisplay>().Select(a => a.Selected)).Select(a => new ScreenInfo(a)).ToArray();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            chkFileName.Checked = false;
            cmbWindowTitle.SelectedIndex = 0;
            cmbClassName.SelectedIndex = 0;
            cmbFileName.SelectedIndex = 0;

            var windows = Win32Form.AllWindows.Where(a => a.Visible && !string.IsNullOrWhiteSpace(a.Text)).ToArray();

            txtWindowTitle.Items.AddRange(windows.Select(a => a.Text).ToArray());
            txtClassName.Items.AddRange(windows.Select(a => a.ClassName).ToArray());
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            cmbWindowTitle.Enabled = chkWindowTitle.Checked;
            txtWindowTitle.Enabled = chkWindowTitle.Checked;
            cmbClassName.Enabled = chkClassName.Checked;
            txtClassName.Enabled = chkClassName.Checked;
            cmbFileName.Enabled = chkFileName.Checked;
            txtFileName.Enabled = chkFileName.Checked;
            UpdateToGame();
        }

        private void NewGameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkContiguousMonitors)
            {
                var monitorBounds = monitors.Monitors.Cast<DisplayChooser.SelectableDisplay>().Where(a => a.Selected).Select(a => a.Bounds).ToList();
                if (monitorBounds.Count == 0)
                {
                    return;
                }
                if (!monitorBounds.TryUnion().HasValue)
                {
                    e.Cancel = true;
                    MessageBox.Show("Selected monitors must be contiguous.");
                }
            }
        }

        private void monitors_MonitorSelectedChanged(object sender, MonitorEventArgs e)
        {
            UpdateSelectedMonitorColors();
            UpdateToGame();
        }

        private void UpdateSelectedMonitorColors()
        {
            foreach (ImmersiveGaming.DisplayChooser.SelectableDisplay m in monitors.Monitors)
            {
                if (chkBlackout.Checked)
                {
                    m.BackColor = m.Selected ? Color.Blue : Color.Black;
                }
                else
                {
                    m.BackColor = m.Selected ? Color.Blue : (Color)((new ColorF(Color.Blue) + Color.White) / 2);
                }
            }
        }

        private void chkBlackout_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSelectedMonitorColors();
            UpdateToGame();
        }

        private void buttons_ButtonClick(object sender, Controls.DialogResultEventArgs e)
        {
            if (e.Result == System.Windows.Forms.DialogResult.Cancel)
            {
                checkContiguousMonitors = false;
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            var p = MouseCaptureForm.GetMouseClickPosition();
            var form = Win32Form.AllWindows.Where(a => a.Visible && a.Rect.Contains(p)).FirstOrDefault();

            txtWindowTitle.Text = form.Text;
            txtClassName.Text = form.ClassName;
            txtFileName.Text = form.Process.MainModule.FileName;
            cmbWindowTitle.SelectedIndex = 0;
            cmbClassName.SelectedIndex = 0;
            cmbFileName.SelectedIndex = 0;
        }

        private void InputChanged(object sender, EventArgs e)
        {
            UpdateToGame();
        }
    }
}
