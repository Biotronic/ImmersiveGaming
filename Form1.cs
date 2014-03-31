using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ImmersiveGaming
{
    public partial class Form1 : Form
    {
        Immersifier _imm = new Immersifier();

        bool _closing = false;
        bool _allowShow = false;

        public Form1()
        {
            InitializeComponent();

            _imm.Load("Immersive Gaming.xml");

            UpdateGameList();
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(_allowShow ? value : false);
            _allowShow = true;
        }

        private void UpdateGameList()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();

            foreach (var game in _imm.Games)
            {
                var item = new ListViewItem(game.name);

                listView1.Items.Add(item);
            }
            listView1.EndUpdate();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            notifyIcon1.Visible = e.Cancel = e.CloseReason == CloseReason.UserClosing && !_closing;

            if (!e.Cancel)
            {
                SaveGamesList();
            }
            else
            {
                Hide();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _closing = true;
            Close();
        }

        private void UpdateState(object sender, EventArgs e)
        {
            _imm.Update(Win32Form.ForegroundWindow);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _imm.Merge(openFileDialog1.FileName);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveGamesList();
            if (listView1.SelectedIndices.Count > 0)
            {
                newGameForm1.Game = _imm.Games[listView1.SelectedIndices[0]];
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _imm.Add(new GameInfo());
            UpdateGameList();
            listView1.SelectedIndices.Clear();
            listView1.SelectedIndices.Add(listView1.Items.Count - 1);
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            _imm.Games[listView1.SelectedIndices[0]].name = e.Label;
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            deleteToolStripMenuItem.Enabled = listView1.SelectedIndices.Count != 0;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
            {
                MessageBox.Show("No games selected.");
                return;
            }
            _imm.Remove(listView1.SelectedIndices.OfType<int>());
            UpdateGameList();
        }

        void SaveGamesList()
        {
            _imm.Save("Immersive Gaming.xml");
        }

        private void startWithWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowsFeatures.StartsOnBoot(WindowsFeatures.BootSegment.user, "ImmersiveGaming"))
            {
                WindowsFeatures.RemoveFromBoot(WindowsFeatures.BootSegment.user, "ImmersiveGaming");
            }
            else
            {
                WindowsFeatures.AddToBoot(WindowsFeatures.BootSegment.user, "ImmersiveGaming");
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startWithWindowsToolStripMenuItem.Checked = WindowsFeatures.StartsOnBoot(WindowsFeatures.BootSegment.user, "ImmersiveGaming");
        }
    }
}
