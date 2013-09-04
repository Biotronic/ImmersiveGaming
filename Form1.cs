﻿using System;
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

namespace SkyrimMouseHider
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private struct IniItem
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string Group { get; set; }
        }

        Win32Form oldForm = null;

        struct WindowInfo
        {
            public string Text { get; set; }
            public string ClassName { get; set; }
            public bool HideCursor { get; set; }
            public bool BlackOut { get; set; }

            public WindowInfo(string text, string className, bool hideCursor = true, bool blackout = true)
                : this()
            {
                Text = text;
                ClassName = className;
                HideCursor = hideCursor;
                BlackOut = blackout;
            }
        }

        private List<WindowInfo> Windows = new List<WindowInfo>()
        {
            {new WindowInfo("Kerbal Space Program","UnityWndClass", false, true)},
            {new WindowInfo("Skyrim", "Skyrim", true, true)},
            {new WindowInfo("Fallout: New Vegas", "Fallout: New Vegas", true, true)},
            {new WindowInfo("Dishonored", "LaunchUnrealUWindowsClient", false, true)},
            {new WindowInfo("Star Citizen", "CryENGINE",false,false)}
        };

        WindowInfo? GetWindowInfo(Win32Form frm)
        {
            if (frm != null)
            {
                var tmp = Windows.Where(a => a.Text == frm.Text && a.ClassName == frm.ClassName);
                if (tmp.Any())
                {
                    return tmp.First();
                }
            }
            return null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var frm = Win32Form.ForegroundWindow;
            var formInfo = GetWindowInfo(frm);
            if (formInfo.HasValue)
            {
                if (frm != oldForm)
                {
                    frm.WindowStyle = User32Types.WindowStyles.MinimizeBox | User32Types.WindowStyles.Popup | User32Types.WindowStyles.Tiled | User32Types.WindowStyles.Visible;
                    frm.WindowExStyle = User32Types.WindowExStyles.Left;
                    frm.ClientRect = Screen.FromPoint(frm.Location).Bounds;
                    oldForm = frm;
                }
                if (formInfo.Value.HideCursor)
                {
                    MouseHider.HideCursors();
                }
                if (formInfo.Value.BlackOut && blackOutToolStripMenuItem.Checked)
                {
                    BlackOut(Screen.FromPoint(frm.Location));
                }
                else
                {
                    WhiteIn();
                }
            }
            else
            {
                WhiteIn();
                MouseHider.ShowCursors();
            }
        }

        private void BlackOut(Screen screen)
        {
            if (blackoutForms.Count + 1 < Screen.AllScreens.Length)
            {
                blackoutForms = Screen.AllScreens.Except(new[] { screen }).Select(a => new BlackoutForm(a)).ToList();
            }
            foreach (var frm in blackoutForms)
            {
                frm.Show();
            }
        }

        private void WhiteIn()
        {
            foreach (var frm in blackoutForms)
            {
                frm.Hide();
            }
        }

        private List<BlackoutForm> blackoutForms = new List<BlackoutForm>();

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
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

        private void startSKSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var loc = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Bethesda Softworks\Skyrim", "Installed Path", null);
            if (loc is string)
            {
                var info = new ProcessStartInfo("skse_loader.exe");
                info.WorkingDirectory = loc as string;
                Process.Start(info);
            }
        }
    }
}