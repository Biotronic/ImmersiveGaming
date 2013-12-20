using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Biotronic;

namespace ImmersiveGaming
{
    public static partial class MouseHider
    {
        static bool shown = true;
        static uint[] cursorIds;
        static IntPtr[] OriginalCursors;
        static IntPtr[] blankCursors;

        static MouseHider()
        {
            cursorIds = Enums.Values<User32Types.CursorName>().Cast<uint>().ToArray();
            OriginalCursors = new IntPtr[cursorIds.Length];
            blankCursors = new IntPtr[cursorIds.Length];
            int i = 0;
            var blank = User32.CreateCursor(IntPtr.Zero, 0, 0, 32, 32, ((byte)0xFF).Repeat(32 * 4).ToArray(), ((byte)0x00).Repeat(32 * 4).ToArray());
            foreach (var cursor in cursorIds)
            {
                var hc = User32.LoadCursor(IntPtr.Zero, (IntPtr)cursor);
                OriginalCursors[i] = User32.CopyImage(hc, 2, 0, 0, 0);
                blankCursors[i] = blank;
                i++;
            }

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            ShowCursors();
        }

        private static void UpdateCursors(IntPtr[] cursors)
        {
            int i = 0;
            foreach (var cursor in cursorIds)
            {
                var hc = User32.CopyImage(cursors[i], 2, 0, 0, 0);
                User32.SetSystemCursor(hc, (UInt32)cursor);
            }
        }

        public static void HideCursors()
        {
            if (shown)
            {
                UpdateCursors(blankCursors);
                shown = false;
            }
        }

        public static void ShowCursors()
        {
            if (!shown)
            {
                UpdateCursors(OriginalCursors);
                shown = true;
                User32.SystemParametersInfo(User32Types.SPI.SPI_SETCURSORS, 0, IntPtr.Zero, User32Types.SPIF.SPIF_UPDATEINIFILE);
                //SystemParametersInfo(SPI_SETCURSORS, 0, 0, WM_SETTINGCHANGE | SPIF_UPDATEINIFILE );
            }
        }
    }
}
