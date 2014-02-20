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
        Immersifier imm = new Immersifier();

        public Form1()
        {
            InitializeComponent();

            imm.Load("Immersive Gaming.xml");

            UpdateGameList();
        }

        private void UpdateGameList()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();

            foreach (var game in imm.Games)
            {
                var item = new ListViewItem(game.name);

                listView1.Items.Add(item);
            }
            listView1.EndUpdate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            notifyIcon1.Visible = FormWindowState.Minimized == this.WindowState;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!e.Cancel)
            {
                notifyIcon1.Visible = false;
                imm.Save("Immersive Gaming.xml");
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateState(object sender, EventArgs e)
        {
            imm.Update(Win32Form.ForegroundWindow);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imm.Merge(openFileDialog1.FileName);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                newGameForm1.Game = imm.Games[listView1.SelectedIndices[0]];
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imm.Add(new GameInfo());
            UpdateGameList();
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            imm.Games[listView1.SelectedIndices[0]].name = e.Label;
        }
    }
}
