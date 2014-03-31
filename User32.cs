using ImmersiveGaming.User32Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace ImmersiveGaming
{
    static class User32
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr windowHandle, uint Msg, IntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags flags, uint uTimeout, out IntPtr result);
        [DllImport("user32.dll", EntryPoint = "SendMessageTimeout", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint SendMessageTimeoutText(IntPtr hWnd, int Msg, int countOfChars, StringBuilder text, SendMessageTimeoutFlags flags, uint uTimeout, out IntPtr result);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        public static int GetWindowTextTimeout(IntPtr hWnd, StringBuilder lpString, int nMaxCount, SendMessageTimeoutFlags flags, uint uTimeout)
        {
            unsafe
            {
                int result = 0;
                IntPtr tmp = new IntPtr(&result);
                SendMessageTimeoutText(hWnd, (int)WindowMessages.WM_GETTEXT, nMaxCount, lpString, flags, uTimeout, out tmp);
                return result;
            }
        }
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int GetWindowTextLength(IntPtr hWnd);
        public static int GetWindowTextLengthTimeout(IntPtr hWnd, SendMessageTimeoutFlags flags, uint uTimeout)
        {
            unsafe
            {
                int result = 0;
                IntPtr tmp = new IntPtr(&result);
                SendMessageTimeout(hWnd, (int)WindowMessages.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero, flags, uTimeout, out tmp);
                return result;
            }
        }
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out User32Types.Rect lpRect);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, User32Types.SetWindowPosFlags uFlags);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, User32Types.ShowWindowCommand nCmdShow);
        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, User32Types.WindowLongIndex nIndex);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, User32Types.WindowLongIndex nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, IntPtr lpCursorName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CopyImage(IntPtr hImage, uint uType, Int32 cxDesired, Int32 cyDesired, uint fuFlags);
        [DllImport("user32.dll")]
        public static extern IntPtr CreateCursor(IntPtr hInst, Int32 xHotSpot, Int32 yHotSpot,
           Int32 nWidth, Int32 nHeight, byte[] pvANDPlane, byte[] pvXORPlane);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetSystemCursor(IntPtr hcur, UInt32 id);
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out User32Types.Rect lpRect);
        [DllImport("user32.dll")]
        public static extern bool AdjustWindowRectEx(ref Rect lpRect, User32Types.WindowStyles dwStyle,
           bool bMenu, User32Types.WindowExStyles dwExStyle);
        [DllImport("user32.dll")]
        public static extern IntPtr GetMenu(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(SPI uiAction, uint uiParam, IntPtr pvParam, SPIF fWinIni);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(SPI uiAction, uint uiParam, String pvParam, SPIF fWinIni);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(SPI uiAction, uint uiParam, ref ANIMATIONINFO pvParam, SPIF fWinIni);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out IntPtr lpdwProcessId);
    }
    namespace User32Types
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            private int left, top, right, bottom;
            public Rect(int left, int top, int right, int bottom)
            {
                this.top = top;
                this.left = left;
                this.right = right;
                this.bottom = bottom;
            }

            public int Left
            {
                get
                {
                    return left;
                }
                set
                {
                    left = value;
                }
            }
            public int Top
            {
                get
                {
                    return top;
                }
                set
                {
                    top = value;
                }
            }
            public int Right
            {
                get
                {
                    return right;
                }
                set
                {
                    right = value;
                }
            }
            public int Bottom
            {
                get
                {
                    return bottom;
                }
                set
                {
                    bottom = value;
                }
            }
            public int Width
            {
                get
                {
                    return right - left;
                }
            }
            public int Height
            {
                get
                {
                    return bottom - top;
                }
            }
            public override bool Equals(object obj)
            {
                if (obj is Rect)
                {
                    var rect = (Rect)obj;
                    return left == rect.left && top == rect.top && right == rect.right && bottom == rect.bottom;
                }
                return false;
            }
            public override int GetHashCode()
            {
                return left ^ top ^ right ^ bottom;
            }
            public static bool operator ==(Rect left, Rect right)
            {
                return left.left == right.left && left.top == right.top && left.right == right.right && left.bottom == right.bottom;
            }
            public static bool operator !=(Rect left, Rect right)
            {
                return !(left == right);
            }
            public static implicit operator Rectangle(Rect rect)
            {
                return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
            public static implicit operator Rect(Rectangle rect)
            {
                return new Rect(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }
        }
        public enum CursorName : uint
        {
            AppStarting = 32650,
            Arrow = 32512,
            Cross = 32515,
            Hand = 32649,
            Help = 32651,
            IBeam = 32513,
            Icon = 32641,
            No = 32648,
            Size = 32640,
            SizeAll = 32646,
            SizeNESW = 32643,
            SizeNS = 32645,
            SizeNWSE = 32642,
            SizeWE = 32644,
            UpArrow = 32516,
            Wait = 32514
        }
        public enum WindowLongIndex : int
        {
            ExStyle = -20,
            Instance = -6,
            Parent = -8,
            Id = -12,
            Style = -16,
            UserData = -21,
            WindowProcedure = -4,
            User = 0x8,
            MessageResult = 0x0,
            DialogProcedure = 0x4
        }
        [Flags()]
        public enum WindowStyles : uint
        {
            Overlapped = 0,
            Popup = 0x80000000,
            Child = 0x40000000,
            Minimize = 0x20000000,
            Visible = 0x10000000,
            Disabled = 0x8000000,
            ClipSiblings = 0x4000000,
            ClipChildren = 0x2000000,
            Maximize = 0x1000000,
            Caption = 0xC00000,      // WS_BORDER or WS_DLGFRAME  
            Border = 0x800000,
            DialogFrame = 0x400000,
            VerticalScroll = 0x200000,
            HorizontalScroll = 0x100000,
            SystemMenu = 0x80000,
            ThickFrame = 0x40000,
            Group = 0x20000,
            TabStop = 0x10000,
            MinimizeBox = 0x20000,
            MaximizeBox = 0x10000,
            Tiled = Overlapped,
            Iconic = Minimize,
            SizeBox = ThickFrame,
        }
        [Flags()]
        public enum WindowExStyles : uint
        {
            DialogModalFrame = 0x0001,
            NoParentModify = 0x0004,
            Topmost = 0x0008,
            AcceptFiles = 0x0010,
            Transparent = 0x0020,
            MdiChild = 0x0040,
            ToolWindow = 0x0080,
            WindowEdge = 0x0100,
            ClientEdge = 0x0200,
            ContextHelp = 0x0400,
            Right = 0x1000,
            Left = 0x0000,
            RtlReading = 0x2000,
            LtrReading = 0x0000,
            LeftScrollBar = 0x4000,
            RightScrollBar = 0x0000,
            ControlParent = 0x10000,
            StaticEdge = 0x20000,
            AppWindow = 0x40000,
            OverlappedWindow = (WindowEdge | ClientEdge),
            PaletteWindow = (WindowEdge | ToolWindow | Topmost),
            Layered = 0x00080000,
            NoInheritLayout = 0x00100000, // Disable inheritence of mirroring by children
            LayoutRtl = 0x00400000, // Right to left mirroring
            Composited = 0x02000000,
            NoActivate = 0x08000000,
        }
        [Flags()]
        public enum SetWindowPosFlags : uint
        {
            /// <summary>If the calling thread and the thread that owns the window are attached to different input queues, 
            /// the system posts the request to the thread that owns the window. This prevents the calling thread from 
            /// blocking its execution while other threads process the request.</summary>
            /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
            AsynchronousWindowPosition = 0x4000,
            /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
            /// <remarks>SWP_DEFERERASE</remarks>
            DeferErase = 0x2000,
            /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
            /// <remarks>SWP_DRAWFRAME</remarks>
            DrawFrame = 0x0020,
            /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to 
            /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE 
            /// is sent only when the window's size is being changed.</summary>
            /// <remarks>SWP_FRAMECHANGED</remarks>
            FrameChanged = 0x0020,
            /// <summary>Hides the window.</summary>
            /// <remarks>SWP_HIDEWINDOW</remarks>
            HideWindow = 0x0080,
            /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the 
            /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter 
            /// parameter).</summary>
            /// <remarks>SWP_NOACTIVATE</remarks>
            DoNotActivate = 0x0010,
            /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid 
            /// contents of the client area are saved and copied back into the client area after the window is sized or 
            /// repositioned.</summary>
            /// <remarks>SWP_NOCOPYBITS</remarks>
            DoNotCopyBits = 0x0100,
            /// <summary>Retains the current position (ignores X and Y parameters).</summary>
            /// <remarks>SWP_NOMOVE</remarks>
            IgnoreMove = 0x0002,
            /// <summary>Does not change the owner window's position in the Z order.</summary>
            /// <remarks>SWP_NOOWNERZORDER</remarks>
            DoNotChangeOwnerZOrder = 0x0200,
            /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to 
            /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent 
            /// window uncovered as a result of the window being moved. When this flag is set, the application must 
            /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
            /// <remarks>SWP_NOREDRAW</remarks>
            DoNotRedraw = 0x0008,
            /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
            /// <remarks>SWP_NOREPOSITION</remarks>
            DoNotReposition = 0x0200,
            /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
            /// <remarks>SWP_NOSENDCHANGING</remarks>
            DoNotSendChangingEvent = 0x0400,
            /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
            /// <remarks>SWP_NOSIZE</remarks>
            IgnoreResize = 0x0001,
            /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
            /// <remarks>SWP_NOZORDER</remarks>
            IgnoreZOrder = 0x0004,
            /// <summary>Displays the window.</summary>
            /// <remarks>SWP_SHOWWINDOW</remarks>
            ShowWindow = 0x0040,
        }
        public enum ShowWindowCommand : int
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            Hide = 0,
            /// <summary>
            /// Activates and displays a window. If the window is minimized or 
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when displaying the window 
            /// for the first time.
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// Maximizes the specified window.
            /// </summary>
            Maximize = 3, // is this the right value?
            /// <summary>
            /// Activates the window and displays it as a maximized window.
            /// </summary>       
            ShowMaximized = 3,
            /// <summary>
            /// Displays a window in its most recent size and position. This value 
            /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except 
            /// the window is not activated.
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// Activates the window and displays it in its current size and position. 
            /// </summary>
            Show = 5,
            /// <summary>
            /// Minimizes the specified window and activates the next top-level 
            /// window in the Z order.
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// Displays the window as a minimized window. This value is similar to
            /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the 
            /// window is not activated.
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// Displays the window in its current size and position. This value is 
            /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the 
            /// window is not activated.
            /// </summary>
            ShowNA = 8,
            /// <summary>
            /// Activates and displays the window. If the window is minimized or 
            /// maximized, the system restores it to its original size and position. 
            /// An application should specify this flag when restoring a minimized window.
            /// </summary>
            Restore = 9,
            /// <summary>
            /// Sets the show state based on the SW_* value specified in the 
            /// STARTUPINFO structure passed to the CreateProcess function by the 
            /// program that started the application.
            /// </summary>
            ShowDefault = 10,
            /// <summary>
            ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread 
            /// that owns the window is not responding. This flag should only be 
            /// used when minimizing windows from a different thread.
            /// </summary>
            ForceMinimize = 11
        }
        #region SPI
        /// <summary>
        /// SPI_ System-wide parameter - Used in SystemParametersInfo function 
        /// </summary>
        [Description("SPI_(System-wide parameter - Used in SystemParametersInfo function )")]
        public enum SPI : uint
        {
            /// <summary>
            /// Determines whether the warning beeper is on. 
            /// The pvParam parameter must point to a BOOL variable that receives TRUE if the beeper is on, or FALSE if it is off.
            /// </summary>
            SPI_GETBEEP = 0x0001,

            /// <summary>
            /// Turns the warning beeper on or off. The uiParam parameter specifies TRUE for on, or FALSE for off.
            /// </summary>
            SPI_SETBEEP = 0x0002,

            /// <summary>
            /// Retrieves the two mouse threshold values and the mouse speed.
            /// </summary>
            SPI_GETMOUSE = 0x0003,

            /// <summary>
            /// Sets the two mouse threshold values and the mouse speed.
            /// </summary>
            SPI_SETMOUSE = 0x0004,

            /// <summary>
            /// Retrieves the border multiplier factor that determines the width of a window's sizing border. 
            /// The pvParam parameter must point to an integer variable that receives this value.
            /// </summary>
            SPI_GETBORDER = 0x0005,

            /// <summary>
            /// Sets the border multiplier factor that determines the width of a window's sizing border. 
            /// The uiParam parameter specifies the new value.
            /// </summary>
            SPI_SETBORDER = 0x0006,

            /// <summary>
            /// Retrieves the keyboard repeat-speed setting, which is a value in the range from 0 (approximately 2.5 repetitions per second) 
            /// through 31 (approximately 30 repetitions per second). The actual repeat rates are hardware-dependent and may vary from 
            /// a linear scale by as much as 20%. The pvParam parameter must point to a DWORD variable that receives the setting
            /// </summary>
            SPI_GETKEYBOARDSPEED = 0x000A,

            /// <summary>
            /// Sets the keyboard repeat-speed setting. The uiParam parameter must specify a value in the range from 0 
            /// (approximately 2.5 repetitions per second) through 31 (approximately 30 repetitions per second). 
            /// The actual repeat rates are hardware-dependent and may vary from a linear scale by as much as 20%. 
            /// If uiParam is greater than 31, the parameter is set to 31.
            /// </summary>
            SPI_SETKEYBOARDSPEED = 0x000B,

            /// <summary>
            /// Not implemented.
            /// </summary>
            SPI_LANGDRIVER = 0x000C,

            /// <summary>
            /// Sets or retrieves the width, in pixels, of an icon cell. The system uses this rectangle to arrange icons in large icon view. 
            /// To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CXICON.
            /// To retrieve this value, pvParam must point to an integer that receives the current value.
            /// </summary>
            SPI_ICONHORIZONTALSPACING = 0x000D,

            /// <summary>
            /// Retrieves the screen saver time-out value, in seconds. The pvParam parameter must point to an integer variable that receives the value.
            /// </summary>
            SPI_GETSCREENSAVETIMEOUT = 0x000E,

            /// <summary>
            /// Sets the screen saver time-out value to the value of the uiParam parameter. This value is the amount of time, in seconds, 
            /// that the system must be idle before the screen saver activates.
            /// </summary>
            SPI_SETSCREENSAVETIMEOUT = 0x000F,

            /// <summary>
            /// Determines whether screen saving is enabled. The pvParam parameter must point to a bool variable that receives TRUE 
            /// if screen saving is enabled, or FALSE otherwise.
            /// Does not work for Windows 7: http://msdn.microsoft.com/en-us/library/windows/desktop/ms724947(v=vs.85).aspx
            /// </summary>
            SPI_GETSCREENSAVEACTIVE = 0x0010,

            /// <summary>
            /// Sets the state of the screen saver. The uiParam parameter specifies TRUE to activate screen saving, or FALSE to deactivate it.
            /// </summary>
            SPI_SETSCREENSAVEACTIVE = 0x0011,

            /// <summary>
            /// Retrieves the current granularity value of the desktop sizing grid. The pvParam parameter must point to an integer variable 
            /// that receives the granularity.
            /// </summary>
            SPI_GETGRIDGRANULARITY = 0x0012,

            /// <summary>
            /// Sets the granularity of the desktop sizing grid to the value of the uiParam parameter.
            /// </summary>
            SPI_SETGRIDGRANULARITY = 0x0013,

            /// <summary>
            /// Sets the desktop wallpaper. The value of the pvParam parameter determines the new wallpaper. To specify a wallpaper bitmap, 
            /// set pvParam to point to a null-terminated string containing the name of a bitmap file. Setting pvParam to "" removes the wallpaper. 
            /// Setting pvParam to SETWALLPAPER_DEFAULT or null reverts to the default wallpaper.
            /// </summary>
            SPI_SETDESKWALLPAPER = 0x0014,

            /// <summary>
            /// Sets the current desktop pattern by causing Windows to read the Pattern= setting from the WIN.INI file.
            /// </summary>
            SPI_SETDESKPATTERN = 0x0015,

            /// <summary>
            /// Retrieves the keyboard repeat-delay setting, which is a value in the range from 0 (approximately 250 ms delay) through 3 
            /// (approximately 1 second delay). The actual delay associated with each value may vary depending on the hardware. The pvParam parameter must point to an integer variable that receives the setting.
            /// </summary>
            SPI_GETKEYBOARDDELAY = 0x0016,

            /// <summary>
            /// Sets the keyboard repeat-delay setting. The uiParam parameter must specify 0, 1, 2, or 3, where zero sets the shortest delay 
            /// (approximately 250 ms) and 3 sets the longest delay (approximately 1 second). The actual delay associated with each value may 
            /// vary depending on the hardware.
            /// </summary>
            SPI_SETKEYBOARDDELAY = 0x0017,

            /// <summary>
            /// Sets or retrieves the height, in pixels, of an icon cell. 
            /// To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CYICON.
            /// To retrieve this value, pvParam must point to an integer that receives the current value.
            /// </summary>
            SPI_ICONVERTICALSPACING = 0x0018,

            /// <summary>
            /// Determines whether icon-title wrapping is enabled. The pvParam parameter must point to a bool variable that receives TRUE 
            /// if enabled, or FALSE otherwise.
            /// </summary>
            SPI_GETICONTITLEWRAP = 0x0019,

            /// <summary>
            /// Turns icon-title wrapping on or off. The uiParam parameter specifies TRUE for on, or FALSE for off.
            /// </summary>
            SPI_SETICONTITLEWRAP = 0x001A,

            /// <summary>
            /// Determines whether pop-up menus are left-aligned or right-aligned, relative to the corresponding menu-bar item. 
            /// The pvParam parameter must point to a bool variable that receives TRUE if left-aligned, or FALSE otherwise.
            /// </summary>
            SPI_GETMENUDROPALIGNMENT = 0x001B,

            /// <summary>
            /// Sets the alignment value of pop-up menus. The uiParam parameter specifies TRUE for right alignment, or FALSE for left alignment.
            /// </summary>
            SPI_SETMENUDROPALIGNMENT = 0x001C,

            /// <summary>
            /// Sets the width of the double-click rectangle to the value of the uiParam parameter. 
            /// The double-click rectangle is the rectangle within which the second click of a double-click must fall for it to be registered 
            /// as a double-click.
            /// To retrieve the width of the double-click rectangle, call GetSystemMetrics with the SM_CXDOUBLECLK flag.
            /// </summary>
            SPI_SETDOUBLECLKWIDTH = 0x001D,

            /// <summary>
            /// Sets the height of the double-click rectangle to the value of the uiParam parameter. 
            /// The double-click rectangle is the rectangle within which the second click of a double-click must fall for it to be registered 
            /// as a double-click.
            /// To retrieve the height of the double-click rectangle, call GetSystemMetrics with the SM_CYDOUBLECLK flag.
            /// </summary>
            SPI_SETDOUBLECLKHEIGHT = 0x001E,

            /// <summary>
            /// Retrieves the logical font information for the current icon-title font. The uiParam parameter specifies the size of a LOGFONT structure, 
            /// and the pvParam parameter must point to the LOGFONT structure to fill in.
            /// </summary>
            SPI_GETICONTITLELOGFONT = 0x001F,

            /// <summary>
            /// Sets the double-click time for the mouse to the value of the uiParam parameter. The double-click time is the maximum number 
            /// of milliseconds that can occur between the first and second clicks of a double-click. You can also call the SetDoubleClickTime 
            /// function to set the double-click time. To get the current double-click time, call the GetDoubleClickTime function.
            /// </summary>
            SPI_SETDOUBLECLICKTIME = 0x0020,

            /// <summary>
            /// Swaps or restores the meaning of the left and right mouse buttons. The uiParam parameter specifies TRUE to swap the meanings 
            /// of the buttons, or FALSE to restore their original meanings.
            /// </summary>
            SPI_SETMOUSEBUTTONSWAP = 0x0021,

            /// <summary>
            /// Sets the font that is used for icon titles. The uiParam parameter specifies the size of a LOGFONT structure, 
            /// and the pvParam parameter must point to a LOGFONT structure.
            /// </summary>
            SPI_SETICONTITLELOGFONT = 0x0022,

            /// <summary>
            /// This flag is obsolete. Previous versions of the system use this flag to determine whether ALT+TAB fast task switching is enabled. 
            /// For Windows 95, Windows 98, and Windows NT version 4.0 and later, fast task switching is always enabled.
            /// </summary>
            SPI_GETFASTTASKSWITCH = 0x0023,

            /// <summary>
            /// This flag is obsolete. Previous versions of the system use this flag to enable or disable ALT+TAB fast task switching. 
            /// For Windows 95, Windows 98, and Windows NT version 4.0 and later, fast task switching is always enabled.
            /// </summary>
            SPI_SETFASTTASKSWITCH = 0x0024,

            //#if(WINVER >= 0x0400)
            /// <summary>
            /// Sets dragging of full windows either on or off. The uiParam parameter specifies TRUE for on, or FALSE for off. 
            /// Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
            /// </summary>
            SPI_SETDRAGFULLWINDOWS = 0x0025,

            /// <summary>
            /// Determines whether dragging of full windows is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// if enabled, or FALSE otherwise. 
            /// Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
            /// </summary>
            SPI_GETDRAGFULLWINDOWS = 0x0026,

            /// <summary>
            /// Retrieves the metrics associated with the nonclient area of nonminimized windows. The pvParam parameter must point 
            /// to a NONCLIENTMETRICS structure that receives the information. Set the cbSize member of this structure and the uiParam parameter 
            /// to sizeof(NONCLIENTMETRICS).
            /// </summary>
            SPI_GETNONCLIENTMETRICS = 0x0029,

            /// <summary>
            /// Sets the metrics associated with the nonclient area of nonminimized windows. The pvParam parameter must point 
            /// to a NONCLIENTMETRICS structure that contains the new parameters. Set the cbSize member of this structure 
            /// and the uiParam parameter to sizeof(NONCLIENTMETRICS). Also, the lfHeight member of the LOGFONT structure must be a negative value.
            /// </summary>
            SPI_SETNONCLIENTMETRICS = 0x002A,

            /// <summary>
            /// Retrieves the metrics associated with minimized windows. The pvParam parameter must point to a MINIMIZEDMETRICS structure 
            /// that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(MINIMIZEDMETRICS).
            /// </summary>
            SPI_GETMINIMIZEDMETRICS = 0x002B,

            /// <summary>
            /// Sets the metrics associated with minimized windows. The pvParam parameter must point to a MINIMIZEDMETRICS structure 
            /// that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(MINIMIZEDMETRICS).
            /// </summary>
            SPI_SETMINIMIZEDMETRICS = 0x002C,

            /// <summary>
            /// Retrieves the metrics associated with icons. The pvParam parameter must point to an ICONMETRICS structure that receives 
            /// the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
            /// </summary>
            SPI_GETICONMETRICS = 0x002D,

            /// <summary>
            /// Sets the metrics associated with icons. The pvParam parameter must point to an ICONMETRICS structure that contains 
            /// the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
            /// </summary>
            SPI_SETICONMETRICS = 0x002E,

            /// <summary>
            /// Sets the size of the work area. The work area is the portion of the screen not obscured by the system taskbar 
            /// or by application desktop toolbars. The pvParam parameter is a pointer to a RECT structure that specifies the new work area rectangle, 
            /// expressed in virtual screen coordinates. In a system with multiple display monitors, the function sets the work area 
            /// of the monitor that contains the specified rectangle.
            /// </summary>
            SPI_SETWORKAREA = 0x002F,

            /// <summary>
            /// Retrieves the size of the work area on the primary display monitor. The work area is the portion of the screen not obscured 
            /// by the system taskbar or by application desktop toolbars. The pvParam parameter must point to a RECT structure that receives 
            /// the coordinates of the work area, expressed in virtual screen coordinates. 
            /// To get the work area of a monitor other than the primary display monitor, call the GetMonitorInfo function.
            /// </summary>
            SPI_GETWORKAREA = 0x0030,

            /// <summary>
            /// Windows Me/98/95:  Pen windows is being loaded or unloaded. The uiParam parameter is TRUE when loading and FALSE 
            /// when unloading pen windows. The pvParam parameter is null.
            /// </summary>
            SPI_SETPENWINDOWS = 0x0031,

            /// <summary>
            /// Retrieves information about the HighContrast accessibility feature. The pvParam parameter must point to a HIGHCONTRAST structure 
            /// that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(HIGHCONTRAST). 
            /// For a general discussion, see remarks.
            /// Windows NT:  This value is not supported.
            /// </summary>
            /// <remarks>
            /// There is a difference between the High Contrast color scheme and the High Contrast Mode. The High Contrast color scheme changes 
            /// the system colors to colors that have obvious contrast; you switch to this color scheme by using the Display Options in the control panel. 
            /// The High Contrast Mode, which uses SPI_GETHIGHCONTRAST and SPI_SETHIGHCONTRAST, advises applications to modify their appearance 
            /// for visually-impaired users. It involves such things as audible warning to users and customized color scheme 
            /// (using the Accessibility Options in the control panel). For more information, see HIGHCONTRAST on MSDN.
            /// For more information on general accessibility features, see Accessibility on MSDN.
            /// </remarks>
            SPI_GETHIGHCONTRAST = 0x0042,

            /// <summary>
            /// Sets the parameters of the HighContrast accessibility feature. The pvParam parameter must point to a HIGHCONTRAST structure 
            /// that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(HIGHCONTRAST).
            /// Windows NT:  This value is not supported.
            /// </summary>
            SPI_SETHIGHCONTRAST = 0x0043,

            /// <summary>
            /// Determines whether the user relies on the keyboard instead of the mouse, and wants applications to display keyboard interfaces 
            /// that would otherwise be hidden. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// if the user relies on the keyboard; or FALSE otherwise.
            /// Windows NT:  This value is not supported.
            /// </summary>
            SPI_GETKEYBOARDPREF = 0x0044,

            /// <summary>
            /// Sets the keyboard preference. The uiParam parameter specifies TRUE if the user relies on the keyboard instead of the mouse, 
            /// and wants applications to display keyboard interfaces that would otherwise be hidden; uiParam is FALSE otherwise.
            /// Windows NT:  This value is not supported.
            /// </summary>
            SPI_SETKEYBOARDPREF = 0x0045,

            /// <summary>
            /// Determines whether a screen reviewer utility is running. A screen reviewer utility directs textual information to an output device, 
            /// such as a speech synthesizer or Braille display. When this flag is set, an application should provide textual information 
            /// in situations where it would otherwise present the information graphically.
            /// The pvParam parameter is a pointer to a BOOL variable that receives TRUE if a screen reviewer utility is running, or FALSE otherwise.
            /// Windows NT:  This value is not supported.
            /// </summary>
            SPI_GETSCREENREADER = 0x0046,

            /// <summary>
            /// Determines whether a screen review utility is running. The uiParam parameter specifies TRUE for on, or FALSE for off.
            /// Windows NT:  This value is not supported.
            /// </summary>
            SPI_SETSCREENREADER = 0x0047,

            /// <summary>
            /// Retrieves the animation effects associated with user actions. The pvParam parameter must point to an ANIMATIONINFO structure 
            /// that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(ANIMATIONINFO).
            /// </summary>
            SPI_GETANIMATION = 0x0048,

            /// <summary>
            /// Sets the animation effects associated with user actions. The pvParam parameter must point to an ANIMATIONINFO structure 
            /// that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(ANIMATIONINFO).
            /// </summary>
            SPI_SETANIMATION = 0x0049,

            /// <summary>
            /// Determines whether the font smoothing feature is enabled. This feature uses font antialiasing to make font curves appear smoother 
            /// by painting pixels at different gray levels. 
            /// The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is enabled, or FALSE if it is not.
            /// Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
            /// </summary>
            SPI_GETFONTSMOOTHING = 0x004A,

            /// <summary>
            /// Enables or disables the font smoothing feature, which uses font antialiasing to make font curves appear smoother 
            /// by painting pixels at different gray levels. 
            /// To enable the feature, set the uiParam parameter to TRUE. To disable the feature, set uiParam to FALSE.
            /// Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
            /// </summary>
            SPI_SETFONTSMOOTHING = 0x004B,

            /// <summary>
            /// Sets the width, in pixels, of the rectangle used to detect the start of a drag operation. Set uiParam to the new value. 
            /// To retrieve the drag width, call GetSystemMetrics with the SM_CXDRAG flag.
            /// </summary>
            SPI_SETDRAGWIDTH = 0x004C,

            /// <summary>
            /// Sets the height, in pixels, of the rectangle used to detect the start of a drag operation. Set uiParam to the new value. 
            /// To retrieve the drag height, call GetSystemMetrics with the SM_CYDRAG flag.
            /// </summary>
            SPI_SETDRAGHEIGHT = 0x004D,

            /// <summary>
            /// Used internally; applications should not use this value.
            /// </summary>
            SPI_SETHANDHELD = 0x004E,

            /// <summary>
            /// Retrieves the time-out value for the low-power phase of screen saving. The pvParam parameter must point to an integer variable 
            /// that receives the value. This flag is supported for 32-bit applications only.
            /// Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            /// Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_GETLOWPOWERTIMEOUT = 0x004F,

            /// <summary>
            /// Retrieves the time-out value for the power-off phase of screen saving. The pvParam parameter must point to an integer variable 
            /// that receives the value. This flag is supported for 32-bit applications only.
            /// Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            /// Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_GETPOWEROFFTIMEOUT = 0x0050,

            /// <summary>
            /// Sets the time-out value, in seconds, for the low-power phase of screen saving. The uiParam parameter specifies the new value. 
            /// The pvParam parameter must be null. This flag is supported for 32-bit applications only.
            /// Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            /// Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_SETLOWPOWERTIMEOUT = 0x0051,

            /// <summary>
            /// Sets the time-out value, in seconds, for the power-off phase of screen saving. The uiParam parameter specifies the new value. 
            /// The pvParam parameter must be null. This flag is supported for 32-bit applications only.
            /// Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            /// Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_SETPOWEROFFTIMEOUT = 0x0052,

            /// <summary>
            /// Determines whether the low-power phase of screen saving is enabled. The pvParam parameter must point to a BOOL variable 
            /// that receives TRUE if enabled, or FALSE if disabled. This flag is supported for 32-bit applications only.
            /// Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            /// Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_GETLOWPOWERACTIVE = 0x0053,

            /// <summary>
            /// Determines whether the power-off phase of screen saving is enabled. The pvParam parameter must point to a BOOL variable 
            /// that receives TRUE if enabled, or FALSE if disabled. This flag is supported for 32-bit applications only.
            /// Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            /// Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_GETPOWEROFFACTIVE = 0x0054,

            /// <summary>
            /// Activates or deactivates the low-power phase of screen saving. Set uiParam to 1 to activate, or zero to deactivate. 
            /// The pvParam parameter must be null. This flag is supported for 32-bit applications only.
            /// Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            /// Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_SETLOWPOWERACTIVE = 0x0055,

            /// <summary>
            /// Activates or deactivates the power-off phase of screen saving. Set uiParam to 1 to activate, or zero to deactivate. 
            /// The pvParam parameter must be null. This flag is supported for 32-bit applications only.
            /// Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            /// Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_SETPOWEROFFACTIVE = 0x0056,

            /// <summary>
            /// Reloads the system cursors. Set the uiParam parameter to zero and the pvParam parameter to null.
            /// </summary>
            SPI_SETCURSORS = 0x0057,

            /// <summary>
            /// Reloads the system icons. Set the uiParam parameter to zero and the pvParam parameter to null.
            /// </summary>
            SPI_SETICONS = 0x0058,

            /// <summary>
            /// Retrieves the input locale identifier for the system default input language. The pvParam parameter must point 
            /// to an HKL variable that receives this value. For more information, see Languages, Locales, and Keyboard Layouts on MSDN.
            /// </summary>
            SPI_GETDEFAULTINPUTLANG = 0x0059,

            /// <summary>
            /// Sets the default input language for the system shell and applications. The specified language must be displayable 
            /// using the current system character set. The pvParam parameter must point to an HKL variable that contains 
            /// the input locale identifier for the default language. For more information, see Languages, Locales, and Keyboard Layouts on MSDN.
            /// </summary>
            SPI_SETDEFAULTINPUTLANG = 0x005A,

            /// <summary>
            /// Sets the hot key set for switching between input languages. The uiParam and pvParam parameters are not used. 
            /// The value sets the shortcut keys in the keyboard property sheets by reading the registry again. The registry must be set before this flag is used. the path in the registry is \HKEY_CURRENT_USER\keyboard layout\toggle. Valid values are "1" = ALT+SHIFT, "2" = CTRL+SHIFT, and "3" = none.
            /// </summary>
            SPI_SETLANGTOGGLE = 0x005B,

            /// <summary>
            /// Windows 95:  Determines whether the Windows extension, Windows Plus!, is installed. Set the uiParam parameter to 1. 
            /// The pvParam parameter is not used. The function returns TRUE if the extension is installed, or FALSE if it is not.
            /// </summary>
            SPI_GETWINDOWSEXTENSION = 0x005C,

            /// <summary>
            /// Enables or disables the Mouse Trails feature, which improves the visibility of mouse cursor movements by briefly showing 
            /// a trail of cursors and quickly erasing them. 
            /// To disable the feature, set the uiParam parameter to zero or 1. To enable the feature, set uiParam to a value greater than 1 
            /// to indicate the number of cursors drawn in the trail.
            /// Windows 2000/NT:  This value is not supported.
            /// </summary>
            SPI_SETMOUSETRAILS = 0x005D,

            /// <summary>
            /// Determines whether the Mouse Trails feature is enabled. This feature improves the visibility of mouse cursor movements 
            /// by briefly showing a trail of cursors and quickly erasing them. 
            /// The pvParam parameter must point to an integer variable that receives a value. If the value is zero or 1, the feature is disabled. 
            /// If the value is greater than 1, the feature is enabled and the value indicates the number of cursors drawn in the trail. 
            /// The uiParam parameter is not used.
            /// Windows 2000/NT:  This value is not supported.
            /// </summary>
            SPI_GETMOUSETRAILS = 0x005E,

            /// <summary>
            /// Windows Me/98:  Used internally; applications should not use this flag.
            /// </summary>
            SPI_SETSCREENSAVERRUNNING = 0x0061,

            /// <summary>
            /// Same as SPI_SETSCREENSAVERRUNNING.
            /// </summary>
            SPI_SCREENSAVERRUNNING = SPI_SETSCREENSAVERRUNNING,
            //#endif /* WINVER >= 0x0400 */

            /// <summary>
            /// Retrieves information about the FilterKeys accessibility feature. The pvParam parameter must point to a FILTERKEYS structure 
            /// that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(FILTERKEYS).
            /// </summary>
            SPI_GETFILTERKEYS = 0x0032,

            /// <summary>
            /// Sets the parameters of the FilterKeys accessibility feature. The pvParam parameter must point to a FILTERKEYS structure 
            /// that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(FILTERKEYS).
            /// </summary>
            SPI_SETFILTERKEYS = 0x0033,

            /// <summary>
            /// Retrieves information about the ToggleKeys accessibility feature. The pvParam parameter must point to a TOGGLEKEYS structure 
            /// that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(TOGGLEKEYS).
            /// </summary>
            SPI_GETTOGGLEKEYS = 0x0034,

            /// <summary>
            /// Sets the parameters of the ToggleKeys accessibility feature. The pvParam parameter must point to a TOGGLEKEYS structure 
            /// that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(TOGGLEKEYS).
            /// </summary>
            SPI_SETTOGGLEKEYS = 0x0035,

            /// <summary>
            /// Retrieves information about the MouseKeys accessibility feature. The pvParam parameter must point to a MOUSEKEYS structure 
            /// that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(MOUSEKEYS).
            /// </summary>
            SPI_GETMOUSEKEYS = 0x0036,

            /// <summary>
            /// Sets the parameters of the MouseKeys accessibility feature. The pvParam parameter must point to a MOUSEKEYS structure 
            /// that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(MOUSEKEYS).
            /// </summary>
            SPI_SETMOUSEKEYS = 0x0037,

            /// <summary>
            /// Determines whether the Show Sounds accessibility flag is on or off. If it is on, the user requires an application 
            /// to present information visually in situations where it would otherwise present the information only in audible form. 
            /// The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is on, or FALSE if it is off. 
            /// Using this value is equivalent to calling GetSystemMetrics (SM_SHOWSOUNDS). That is the recommended call.
            /// </summary>
            SPI_GETSHOWSOUNDS = 0x0038,

            /// <summary>
            /// Sets the parameters of the SoundSentry accessibility feature. The pvParam parameter must point to a SOUNDSENTRY structure 
            /// that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(SOUNDSENTRY).
            /// </summary>
            SPI_SETSHOWSOUNDS = 0x0039,

            /// <summary>
            /// Retrieves information about the StickyKeys accessibility feature. The pvParam parameter must point to a STICKYKEYS structure 
            /// that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(STICKYKEYS).
            /// </summary>
            SPI_GETSTICKYKEYS = 0x003A,

            /// <summary>
            /// Sets the parameters of the StickyKeys accessibility feature. The pvParam parameter must point to a STICKYKEYS structure 
            /// that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(STICKYKEYS).
            /// </summary>
            SPI_SETSTICKYKEYS = 0x003B,

            /// <summary>
            /// Retrieves information about the time-out period associated with the accessibility features. The pvParam parameter must point 
            /// to an ACCESSTIMEOUT structure that receives the information. Set the cbSize member of this structure and the uiParam parameter 
            /// to sizeof(ACCESSTIMEOUT).
            /// </summary>
            SPI_GETACCESSTIMEOUT = 0x003C,

            /// <summary>
            /// Sets the time-out period associated with the accessibility features. The pvParam parameter must point to an ACCESSTIMEOUT 
            /// structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(ACCESSTIMEOUT).
            /// </summary>
            SPI_SETACCESSTIMEOUT = 0x003D,

            //#if(WINVER >= 0x0400)
            /// <summary>
            /// Windows Me/98/95:  Retrieves information about the SerialKeys accessibility feature. The pvParam parameter must point 
            /// to a SERIALKEYS structure that receives the information. Set the cbSize member of this structure and the uiParam parameter 
            /// to sizeof(SERIALKEYS).
            /// Windows Server 2003, Windows XP/2000/NT:  Not supported. The user controls this feature through the control panel.
            /// </summary>
            SPI_GETSERIALKEYS = 0x003E,

            /// <summary>
            /// Windows Me/98/95:  Sets the parameters of the SerialKeys accessibility feature. The pvParam parameter must point 
            /// to a SERIALKEYS structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter 
            /// to sizeof(SERIALKEYS). 
            /// Windows Server 2003, Windows XP/2000/NT:  Not supported. The user controls this feature through the control panel.
            /// </summary>
            SPI_SETSERIALKEYS = 0x003F,
            //#endif /* WINVER >= 0x0400 */ 

            /// <summary>
            /// Retrieves information about the SoundSentry accessibility feature. The pvParam parameter must point to a SOUNDSENTRY structure 
            /// that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(SOUNDSENTRY).
            /// </summary>
            SPI_GETSOUNDSENTRY = 0x0040,

            /// <summary>
            /// Sets the parameters of the SoundSentry accessibility feature. The pvParam parameter must point to a SOUNDSENTRY structure 
            /// that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(SOUNDSENTRY).
            /// </summary>
            SPI_SETSOUNDSENTRY = 0x0041,

            //#if(_WIN32_WINNT >= 0x0400)
            /// <summary>
            /// Determines whether the snap-to-default-button feature is enabled. If enabled, the mouse cursor automatically moves 
            /// to the default button, such as OK or Apply, of a dialog box. The pvParam parameter must point to a BOOL variable 
            /// that receives TRUE if the feature is on, or FALSE if it is off. 
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_GETSNAPTODEFBUTTON = 0x005F,

            /// <summary>
            /// Enables or disables the snap-to-default-button feature. If enabled, the mouse cursor automatically moves to the default button, 
            /// such as OK or Apply, of a dialog box. Set the uiParam parameter to TRUE to enable the feature, or FALSE to disable it. 
            /// Applications should use the ShowWindow function when displaying a dialog box so the dialog manager can position the mouse cursor. 
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_SETSNAPTODEFBUTTON = 0x0060,
            //#endif /* _WIN32_WINNT >= 0x0400 */

            //#if (_WIN32_WINNT >= 0x0400) || (_WIN32_WINDOWS > 0x0400)
            /// <summary>
            /// Retrieves the width, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent 
            /// to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a UINT variable that receives the width. 
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_GETMOUSEHOVERWIDTH = 0x0062,

            /// <summary>
            /// Retrieves the width, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent 
            /// to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a UINT variable that receives the width. 
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_SETMOUSEHOVERWIDTH = 0x0063,

            /// <summary>
            /// Retrieves the height, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent 
            /// to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a UINT variable that receives the height. 
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_GETMOUSEHOVERHEIGHT = 0x0064,

            /// <summary>
            /// Sets the height, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent 
            /// to generate a WM_MOUSEHOVER message. Set the uiParam parameter to the new height.
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_SETMOUSEHOVERHEIGHT = 0x0065,

            /// <summary>
            /// Retrieves the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle for TrackMouseEvent 
            /// to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a UINT variable that receives the time. 
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_GETMOUSEHOVERTIME = 0x0066,

            /// <summary>
            /// Sets the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle for TrackMouseEvent 
            /// to generate a WM_MOUSEHOVER message. This is used only if you pass HOVER_DEFAULT in the dwHoverTime parameter in the call to TrackMouseEvent. Set the uiParam parameter to the new time. 
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_SETMOUSEHOVERTIME = 0x0067,

            /// <summary>
            /// Retrieves the number of lines to scroll when the mouse wheel is rotated. The pvParam parameter must point 
            /// to a UINT variable that receives the number of lines. The default value is 3. 
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_GETWHEELSCROLLLINES = 0x0068,

            /// <summary>
            /// Sets the number of lines to scroll when the mouse wheel is rotated. The number of lines is set from the uiParam parameter. 
            /// The number of lines is the suggested number of lines to scroll when the mouse wheel is rolled without using modifier keys. 
            /// If the number is 0, then no scrolling should occur. If the number of lines to scroll is greater than the number of lines viewable, 
            /// and in particular if it is WHEEL_PAGESCROLL (#defined as UINT_MAX), the scroll operation should be interpreted 
            /// as clicking once in the page down or page up regions of the scroll bar.
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_SETWHEELSCROLLLINES = 0x0069,

            /// <summary>
            /// Retrieves the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is 
            /// over a submenu item. The pvParam parameter must point to a DWORD variable that receives the time of the delay. 
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_GETMENUSHOWDELAY = 0x006A,

            /// <summary>
            /// Sets uiParam to the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is 
            /// over a submenu item. 
            /// Windows 95:  Not supported.
            /// </summary>
            SPI_SETMENUSHOWDELAY = 0x006B,

            /// <summary>
            /// Determines whether the IME status window is visible (on a per-user basis). The pvParam parameter must point to a BOOL variable 
            /// that receives TRUE if the status window is visible, or FALSE if it is not.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETSHOWIMEUI = 0x006E,

            /// <summary>
            /// Sets whether the IME status window is visible or not on a per-user basis. The uiParam parameter specifies TRUE for on or FALSE for off.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETSHOWIMEUI = 0x006F,
            //#endif

            //#if(WINVER >= 0x0500)
            /// <summary>
            /// Retrieves the current mouse speed. The mouse speed determines how far the pointer will move based on the distance the mouse moves. 
            /// The pvParam parameter must point to an integer that receives a value which ranges between 1 (slowest) and 20 (fastest). 
            /// A value of 10 is the default. The value can be set by an end user using the mouse control panel application or 
            /// by an application using SPI_SETMOUSESPEED.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETMOUSESPEED = 0x0070,

            /// <summary>
            /// Sets the current mouse speed. The pvParam parameter is an integer between 1 (slowest) and 20 (fastest). A value of 10 is the default. 
            /// This value is typically set using the mouse control panel application.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETMOUSESPEED = 0x0071,

            /// <summary>
            /// Determines whether a screen saver is currently running on the window station of the calling process. 
            /// The pvParam parameter must point to a BOOL variable that receives TRUE if a screen saver is currently running, or FALSE otherwise.
            /// Note that only the interactive window station, "WinSta0", can have a screen saver running.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETSCREENSAVERRUNNING = 0x0072,

            /// <summary>
            /// Retrieves the full path of the bitmap file for the desktop wallpaper. The pvParam parameter must point to a buffer 
            /// that receives a null-terminated path string. Set the uiParam parameter to the size, in characters, of the pvParam buffer. The returned string will not exceed MAX_PATH characters. If there is no desktop wallpaper, the returned string is empty.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETDESKWALLPAPER = 0x0073,
            //#endif /* WINVER >= 0x0500 */

            //#if(WINVER >= 0x0500)
            /// <summary>
            /// Determines whether active window tracking (activating the window the mouse is on) is on or off. The pvParam parameter must point 
            /// to a BOOL variable that receives TRUE for on, or FALSE for off.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETACTIVEWINDOWTRACKING = 0x1000,

            /// <summary>
            /// Sets active window tracking (activating the window the mouse is on) either on or off. Set pvParam to TRUE for on or FALSE for off.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETACTIVEWINDOWTRACKING = 0x1001,

            /// <summary>
            /// Determines whether the menu animation feature is enabled. This master switch must be on to enable menu animation effects. 
            /// The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE if it is disabled. 
            /// If animation is enabled, SPI_GETMENUFADE indicates whether menus use fade or slide animation.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETMENUANIMATION = 0x1002,

            /// <summary>
            /// Enables or disables menu animation. This master switch must be on for any menu animation to occur. 
            /// The pvParam parameter is a BOOL variable; set pvParam to TRUE to enable animation and FALSE to disable animation.
            /// If animation is enabled, SPI_GETMENUFADE indicates whether menus use fade or slide animation.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETMENUANIMATION = 0x1003,

            /// <summary>
            /// Determines whether the slide-open effect for combo boxes is enabled. The pvParam parameter must point to a BOOL variable 
            /// that receives TRUE for enabled, or FALSE for disabled.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETCOMBOBOXANIMATION = 0x1004,

            /// <summary>
            /// Enables or disables the slide-open effect for combo boxes. Set the pvParam parameter to TRUE to enable the gradient effect, 
            /// or FALSE to disable it.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETCOMBOBOXANIMATION = 0x1005,

            /// <summary>
            /// Determines whether the smooth-scrolling effect for list boxes is enabled. The pvParam parameter must point to a BOOL variable 
            /// that receives TRUE for enabled, or FALSE for disabled.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETLISTBOXSMOOTHSCROLLING = 0x1006,

            /// <summary>
            /// Enables or disables the smooth-scrolling effect for list boxes. Set the pvParam parameter to TRUE to enable the smooth-scrolling effect,
            /// or FALSE to disable it.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETLISTBOXSMOOTHSCROLLING = 0x1007,

            /// <summary>
            /// Determines whether the gradient effect for window title bars is enabled. The pvParam parameter must point to a BOOL variable 
            /// that receives TRUE for enabled, or FALSE for disabled. For more information about the gradient effect, see the GetSysColor function.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETGRADIENTCAPTIONS = 0x1008,

            /// <summary>
            /// Enables or disables the gradient effect for window title bars. Set the pvParam parameter to TRUE to enable it, or FALSE to disable it. 
            /// The gradient effect is possible only if the system has a color depth of more than 256 colors. For more information about 
            /// the gradient effect, see the GetSysColor function.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETGRADIENTCAPTIONS = 0x1009,

            /// <summary>
            /// Determines whether menu access keys are always underlined. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// if menu access keys are always underlined, and FALSE if they are underlined only when the menu is activated by the keyboard.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETKEYBOARDCUES = 0x100A,

            /// <summary>
            /// Sets the underlining of menu access key letters. The pvParam parameter is a BOOL variable. Set pvParam to TRUE to always underline menu 
            /// access keys, or FALSE to underline menu access keys only when the menu is activated from the keyboard.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETKEYBOARDCUES = 0x100B,

            /// <summary>
            /// Same as SPI_GETKEYBOARDCUES.
            /// </summary>
            SPI_GETMENUUNDERLINES = SPI_GETKEYBOARDCUES,

            /// <summary>
            /// Same as SPI_SETKEYBOARDCUES.
            /// </summary>
            SPI_SETMENUUNDERLINES = SPI_SETKEYBOARDCUES,

            /// <summary>
            /// Determines whether windows activated through active window tracking will be brought to the top. The pvParam parameter must point 
            /// to a BOOL variable that receives TRUE for on, or FALSE for off.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETACTIVEWNDTRKZORDER = 0x100C,

            /// <summary>
            /// Determines whether or not windows activated through active window tracking should be brought to the top. Set pvParam to TRUE 
            /// for on or FALSE for off.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETACTIVEWNDTRKZORDER = 0x100D,

            /// <summary>
            /// Determines whether hot tracking of user-interface elements, such as menu names on menu bars, is enabled. The pvParam parameter 
            /// must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled. 
            /// Hot tracking means that when the cursor moves over an item, it is highlighted but not selected. You can query this value to decide 
            /// whether to use hot tracking in the user interface of your application.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETHOTTRACKING = 0x100E,

            /// <summary>
            /// Enables or disables hot tracking of user-interface elements such as menu names on menu bars. Set the pvParam parameter to TRUE 
            /// to enable it, or FALSE to disable it.
            /// Hot-tracking means that when the cursor moves over an item, it is highlighted but not selected.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETHOTTRACKING = 0x100F,

            /// <summary>
            /// Determines whether menu fade animation is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// when fade animation is enabled and FALSE when it is disabled. If fade animation is disabled, menus use slide animation. 
            /// This flag is ignored unless menu animation is enabled, which you can do using the SPI_SETMENUANIMATION flag. 
            /// For more information, see AnimateWindow.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETMENUFADE = 0x1012,

            /// <summary>
            /// Enables or disables menu fade animation. Set pvParam to TRUE to enable the menu fade effect or FALSE to disable it. 
            /// If fade animation is disabled, menus use slide animation. he The menu fade effect is possible only if the system 
            /// has a color depth of more than 256 colors. This flag is ignored unless SPI_MENUANIMATION is also set. For more information, 
            /// see AnimateWindow.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETMENUFADE = 0x1013,

            /// <summary>
            /// Determines whether the selection fade effect is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// if enabled or FALSE if disabled. 
            /// The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out 
            /// after the menu is dismissed.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETSELECTIONFADE = 0x1014,

            /// <summary>
            /// Set pvParam to TRUE to enable the selection fade effect or FALSE to disable it.
            /// The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out 
            /// after the menu is dismissed. The selection fade effect is possible only if the system has a color depth of more than 256 colors.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETSELECTIONFADE = 0x1015,

            /// <summary>
            /// Determines whether ToolTip animation is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// if enabled or FALSE if disabled. If ToolTip animation is enabled, SPI_GETTOOLTIPFADE indicates whether ToolTips use fade or slide animation.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETTOOLTIPANIMATION = 0x1016,

            /// <summary>
            /// Set pvParam to TRUE to enable ToolTip animation or FALSE to disable it. If enabled, you can use SPI_SETTOOLTIPFADE 
            /// to specify fade or slide animation.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETTOOLTIPANIMATION = 0x1017,

            /// <summary>
            /// If SPI_SETTOOLTIPANIMATION is enabled, SPI_GETTOOLTIPFADE indicates whether ToolTip animation uses a fade effect or a slide effect.
            ///  The pvParam parameter must point to a BOOL variable that receives TRUE for fade animation or FALSE for slide animation. 
            ///  For more information on slide and fade effects, see AnimateWindow.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETTOOLTIPFADE = 0x1018,

            /// <summary>
            /// If the SPI_SETTOOLTIPANIMATION flag is enabled, use SPI_SETTOOLTIPFADE to indicate whether ToolTip animation uses a fade effect 
            /// or a slide effect. Set pvParam to TRUE for fade animation or FALSE for slide animation. The tooltip fade effect is possible only 
            /// if the system has a color depth of more than 256 colors. For more information on the slide and fade effects, 
            /// see the AnimateWindow function.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETTOOLTIPFADE = 0x1019,

            /// <summary>
            /// Determines whether the cursor has a shadow around it. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// if the shadow is enabled, FALSE if it is disabled. This effect appears only if the system has a color depth of more than 256 colors.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETCURSORSHADOW = 0x101A,

            /// <summary>
            /// Enables or disables a shadow around the cursor. The pvParam parameter is a BOOL variable. Set pvParam to TRUE to enable the shadow 
            /// or FALSE to disable the shadow. This effect appears only if the system has a color depth of more than 256 colors.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETCURSORSHADOW = 0x101B,

            //#if(_WIN32_WINNT >= 0x0501)
            /// <summary>
            /// Retrieves the state of the Mouse Sonar feature. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// if enabled or FALSE otherwise. For more information, see About Mouse Input on MSDN.
            /// Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_GETMOUSESONAR = 0x101C,

            /// <summary>
            /// Turns the Sonar accessibility feature on or off. This feature briefly shows several concentric circles around the mouse pointer 
            /// when the user presses and releases the CTRL key. The pvParam parameter specifies TRUE for on and FALSE for off. The default is off. 
            /// For more information, see About Mouse Input.
            /// Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_SETMOUSESONAR = 0x101D,

            /// <summary>
            /// Retrieves the state of the Mouse ClickLock feature. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// if enabled, or FALSE otherwise. For more information, see About Mouse Input.
            /// Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_GETMOUSECLICKLOCK = 0x101E,

            /// <summary>
            /// Turns the Mouse ClickLock accessibility feature on or off. This feature temporarily locks down the primary mouse button 
            /// when that button is clicked and held down for the time specified by SPI_SETMOUSECLICKLOCKTIME. The uiParam parameter specifies 
            /// TRUE for on, 
            /// or FALSE for off. The default is off. For more information, see Remarks and About Mouse Input on MSDN.
            /// Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_SETMOUSECLICKLOCK = 0x101F,

            /// <summary>
            /// Retrieves the state of the Mouse Vanish feature. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// if enabled or FALSE otherwise. For more information, see About Mouse Input on MSDN.
            /// Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_GETMOUSEVANISH = 0x1020,

            /// <summary>
            /// Turns the Vanish feature on or off. This feature hides the mouse pointer when the user types; the pointer reappears 
            /// when the user moves the mouse. The pvParam parameter specifies TRUE for on and FALSE for off. The default is off. 
            /// For more information, see About Mouse Input on MSDN.
            /// Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_SETMOUSEVANISH = 0x1021,

            /// <summary>
            /// Determines whether native User menus have flat menu appearance. The pvParam parameter must point to a BOOL variable 
            /// that returns TRUE if the flat menu appearance is set, or FALSE otherwise.
            /// Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETFLATMENU = 0x1022,

            /// <summary>
            /// Enables or disables flat menu appearance for native User menus. Set pvParam to TRUE to enable flat menu appearance 
            /// or FALSE to disable it. 
            /// When enabled, the menu bar uses COLOR_MENUBAR for the menubar background, COLOR_MENU for the menu-popup background, COLOR_MENUHILIGHT 
            /// for the fill of the current menu selection, and COLOR_HILIGHT for the outline of the current menu selection. 
            /// If disabled, menus are drawn using the same metrics and colors as in Windows 2000 and earlier.
            /// Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETFLATMENU = 0x1023,

            /// <summary>
            /// Determines whether the drop shadow effect is enabled. The pvParam parameter must point to a BOOL variable that returns TRUE 
            /// if enabled or FALSE if disabled.
            /// Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETDROPSHADOW = 0x1024,

            /// <summary>
            /// Enables or disables the drop shadow effect. Set pvParam to TRUE to enable the drop shadow effect or FALSE to disable it. 
            /// You must also have CS_DROPSHADOW in the window class style.
            /// Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETDROPSHADOW = 0x1025,

            /// <summary>
            /// Retrieves a BOOL indicating whether an application can reset the screensaver's timer by calling the SendInput function 
            /// to simulate keyboard or mouse input. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// if the simulated input will be blocked, or FALSE otherwise. 
            /// </summary>
            SPI_GETBLOCKSENDINPUTRESETS = 0x1026,

            /// <summary>
            /// Determines whether an application can reset the screensaver's timer by calling the SendInput function to simulate keyboard 
            /// or mouse input. The uiParam parameter specifies TRUE if the screensaver will not be deactivated by simulated input, 
            /// or FALSE if the screensaver will be deactivated by simulated input.
            /// </summary>
            SPI_SETBLOCKSENDINPUTRESETS = 0x1027,
            //#endif /* _WIN32_WINNT >= 0x0501 */

            /// <summary>
            /// Determines whether UI effects are enabled or disabled. The pvParam parameter must point to a BOOL variable that receives TRUE 
            /// if all UI effects are enabled, or FALSE if they are disabled.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETUIEFFECTS = 0x103E,

            /// <summary>
            /// Enables or disables UI effects. Set the pvParam parameter to TRUE to enable all UI effects or FALSE to disable all UI effects.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETUIEFFECTS = 0x103F,

            /// <summary>
            /// Retrieves the amount of time following user input, in milliseconds, during which the system will not allow applications 
            /// to force themselves into the foreground. The pvParam parameter must point to a DWORD variable that receives the time.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000,

            /// <summary>
            /// Sets the amount of time following user input, in milliseconds, during which the system does not allow applications 
            /// to force themselves into the foreground. Set pvParam to the new timeout value.
            /// The calling thread must be able to change the foreground window, otherwise the call fails.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001,

            /// <summary>
            /// Retrieves the active window tracking delay, in milliseconds. The pvParam parameter must point to a DWORD variable 
            /// that receives the time.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002,

            /// <summary>
            /// Sets the active window tracking delay. Set pvParam to the number of milliseconds to delay before activating the window 
            /// under the mouse pointer.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003,

            /// <summary>
            /// Retrieves the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch request. 
            /// The pvParam parameter must point to a DWORD variable that receives the value.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETFOREGROUNDFLASHCOUNT = 0x2004,

            /// <summary>
            /// Sets the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch request. 
            /// Set pvParam to the number of times to flash.
            /// Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETFOREGROUNDFLASHCOUNT = 0x2005,

            /// <summary>
            /// Retrieves the caret width in edit controls, in pixels. The pvParam parameter must point to a DWORD that receives this value.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETCARETWIDTH = 0x2006,

            /// <summary>
            /// Sets the caret width in edit controls. Set pvParam to the desired width, in pixels. The default and minimum value is 1.
            /// Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETCARETWIDTH = 0x2007,

            //#if(_WIN32_WINNT >= 0x0501)
            /// <summary>
            /// Retrieves the time delay before the primary mouse button is locked. The pvParam parameter must point to DWORD that receives 
            /// the time delay. This is only enabled if SPI_SETMOUSECLICKLOCK is set to TRUE. For more information, see About Mouse Input on MSDN.
            /// Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_GETMOUSECLICKLOCKTIME = 0x2008,

            /// <summary>
            /// Turns the Mouse ClickLock accessibility feature on or off. This feature temporarily locks down the primary mouse button 
            /// when that button is clicked and held down for the time specified by SPI_SETMOUSECLICKLOCKTIME. The uiParam parameter 
            /// specifies TRUE for on, or FALSE for off. The default is off. For more information, see Remarks and About Mouse Input on MSDN.
            /// Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_SETMOUSECLICKLOCKTIME = 0x2009,

            /// <summary>
            /// Retrieves the type of font smoothing. The pvParam parameter must point to a UINT that receives the information.
            /// Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETFONTSMOOTHINGTYPE = 0x200A,

            /// <summary>
            /// Sets the font smoothing type. The pvParam parameter points to a UINT that contains either FE_FONTSMOOTHINGSTANDARD, 
            /// if standard anti-aliasing is used, or FE_FONTSMOOTHINGCLEARTYPE, if ClearType is used. The default is FE_FONTSMOOTHINGSTANDARD. 
            /// When using this option, the fWinIni parameter must be set to SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE; otherwise, 
            /// SystemParametersInfo fails.
            /// </summary>
            SPI_SETFONTSMOOTHINGTYPE = 0x200B,

            /// <summary>
            /// Retrieves a contrast value that is used in ClearType™ smoothing. The pvParam parameter must point to a UINT 
            /// that receives the information.
            /// Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETFONTSMOOTHINGCONTRAST = 0x200C,

            /// <summary>
            /// Sets the contrast value used in ClearType smoothing. The pvParam parameter points to a UINT that holds the contrast value. 
            /// Valid contrast values are from 1000 to 2200. The default value is 1400.
            /// When using this option, the fWinIni parameter must be set to SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE; otherwise, 
            /// SystemParametersInfo fails.
            /// SPI_SETFONTSMOOTHINGTYPE must also be set to FE_FONTSMOOTHINGCLEARTYPE.
            /// Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETFONTSMOOTHINGCONTRAST = 0x200D,

            /// <summary>
            /// Retrieves the width, in pixels, of the left and right edges of the focus rectangle drawn with DrawFocusRect. 
            /// The pvParam parameter must point to a UINT.
            /// Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETFOCUSBORDERWIDTH = 0x200E,

            /// <summary>
            /// Sets the height of the left and right edges of the focus rectangle drawn with DrawFocusRect to the value of the pvParam parameter.
            /// Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETFOCUSBORDERWIDTH = 0x200F,

            /// <summary>
            /// Retrieves the height, in pixels, of the top and bottom edges of the focus rectangle drawn with DrawFocusRect. 
            /// The pvParam parameter must point to a UINT.
            /// Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETFOCUSBORDERHEIGHT = 0x2010,

            /// <summary>
            /// Sets the height of the top and bottom edges of the focus rectangle drawn with DrawFocusRect to the value of the pvParam parameter.
            /// Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETFOCUSBORDERHEIGHT = 0x2011,

            /// <summary>
            /// Not implemented.
            /// </summary>
            SPI_GETFONTSMOOTHINGORIENTATION = 0x2012,

            /// <summary>
            /// Not implemented.
            /// </summary>
            SPI_SETFONTSMOOTHINGORIENTATION = 0x2013,
        }
        #endregion // SPI
        [Flags]
        public enum SPIF
        {
            None = 0x00,
            /// <summary>Writes the new system-wide parameter setting to the user profile.</summary>
            SPIF_UPDATEINIFILE = 0x01,
            /// <summary>Broadcasts the WM_SETTINGCHANGE message after updating the user profile.</summary>
            SPIF_SENDCHANGE = 0x02,
            /// <summary>Same as SPIF_SENDCHANGE.</summary>
            SPIF_SENDWININICHANGE = 0x02
        } 
        /// <summary>
        /// ANIMATIONINFO specifies animation effects associated with user actions. 
        /// Used with SystemParametersInfo when SPI_GETANIMATION or SPI_SETANIMATION action is specified.
        /// </summary>
        /// <remark>
        /// The uiParam value must be set to (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO)) when using this structure.
        /// </remark>
        [StructLayout(LayoutKind.Sequential)]
        public struct ANIMATIONINFO
        {
            /// <summary>
            /// Creates an AMINMATIONINFO structure.
            /// </summary>
            /// <param name="iMinAnimate">If non-zero and SPI_SETANIMATION is specified, enables minimize/restore animation.</param>
            public ANIMATIONINFO(System.Int32 iMinAnimate)
            {
                this.cbSize = (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO));
                this.iMinAnimate = iMinAnimate;
            }

            /// <summary>
            /// Always must be set to (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO)).
            /// </summary>
            public System.UInt32 cbSize;

            /// <summary>
            /// If non-zero, minimize/restore animation is enabled, otherwise disabled.
            /// </summary>
            public System.Int32 iMinAnimate;
        }

        public enum WindowMessages : int
        {
            WM_NULL = 0x00000000,
            WM_CREATE = 0x00000001,
            WM_DESTROY = 0x00000002,
            WM_MOVE = 0x00000003,
            WM_SIZE = 0x00000005,
            WM_ACTIVATE = 0x00000006,
            WM_SETFOCUS = 0x00000007,
            WM_KILLFOCUS = 0x00000008,
            WM_ENABLE = 0x0000000A,
            WM_SETREDRAW = 0x0000000B,
            WM_SETTEXT = 0x0000000C,
            WM_GETTEXT = 0x0000000D,
            WM_GETTEXTLENGTH = 0x0000000E,
            WM_PAINT = 0x0000000F,
            WM_CLOSE = 0x00000010,
            WM_QUERYENDSESSION = 0x00000011,
            WM_QUIT = 0x00000012,
            WM_QUERYOPEN = 0x00000013,
            WM_ERASEBKGND = 0x00000014,
            WM_SYSCOLORCHANGE = 0x00000015,
            WM_ENDSESSION = 0x00000016,
            WM_SHOWWINDOW = 0x00000018,
            WM_WININICHANGE = 0x0000001A,
            WM_DEVMODECHANGE = 0x0000001B,
            WM_ACTIVATEAPP = 0x0000001C,
            WM_FONTCHANGE = 0x0000001D,
            WM_TIMECHANGE = 0x0000001E,
            WM_CANCELMODE = 0x0000001F,
            WM_SETCURSOR = 0x00000020,
            WM_MOUSEACTIVATE = 0x00000021,
            WM_CHILDACTIVATE = 0x00000022,
            WM_QUEUESYNC = 0x00000023,
            WM_GETMINMAXINFO = 0x00000024,
            WM_PAINTICON = 0x00000026,
            WM_ICONERASEBKGND = 0x00000027,
            WM_NEXTDLGCTL = 0x00000028,
            WM_SPOOLERSTATUS = 0x0000002A,
            WM_DRAWITEM = 0x0000002B,
            WM_MEASUREITEM = 0x0000002C,
            WM_DELETEITEM = 0x0000002D,
            WM_VKEYTOITEM = 0x0000002E,
            WM_CHARTOITEM = 0x0000002F,
            WM_SETFONT = 0x00000030,
            WM_GETFONT = 0x00000031,
            WM_SETHOTKEY = 0x00000032,
            WM_GETHOTKEY = 0x00000033,
            WM_QUERYDRAGICON = 0x00000037,
            WM_COMPAREITEM = 0x00000039,
            WM_COMPACTING = 0x00000041,
            WM_COMMNOTIFY = 0x00000044,
            WM_WINDOWPOSCHANGING = 0x00000046,
            WM_WINDOWPOSCHANGED = 0x00000047,
            WM_POWER = 0x00000048,
            WM_COPYDATA = 0x0000004A,
            WM_CANCELJOURNAL = 0x0000004B,
            WM_NOTIFY = 0x0000004E,
            WM_INPUTLANGCHANGEREQUEST = 0x00000050,
            WM_INPUTLANGCHANGE = 0x00000051,
            WM_TCARD = 0x00000052,
            WM_HELP = 0x00000053,
            WM_USERCHANGED = 0x00000054,
            WM_NOTIFYFORMAT = 0x00000055,
            WM_CONTEXTMENU = 0x0000007B,
            WM_STYLECHANGING = 0x0000007C,
            WM_STYLECHANGED = 0x0000007D,
            WM_DISPLAYCHANGE = 0x0000007E,
            WM_GETICON = 0x0000007F,
            WM_SETICON = 0x00000080,
            WM_NCCREATE = 0x00000081,
            WM_NCDESTROY = 0x00000082,
            WM_NCCALCSIZE = 0x00000083,
            WM_NCHITTEST = 0x00000084,
            WM_NCPAINT = 0x00000085,
            WM_NCACTIVATE = 0x00000086,
            WM_GETDLGCODE = 0x00000087,
            WM_SYNCPAINT = 0x00000088,
            WM_NCMOUSEMOVE = 0x000000A0,
            WM_NCLBUTTONDOWN = 0x000000A1,
            WM_NCLBUTTONUP = 0x000000A2,
            WM_NCLBUTTONDBLCLK = 0x000000A3,
            WM_NCRBUTTONDOWN = 0x000000A4,
            WM_NCRBUTTONUP = 0x000000A5,
            WM_NCRBUTTONDBLCLK = 0x000000A6,
            WM_NCMBUTTONDOWN = 0x000000A7,
            WM_NCMBUTTONUP = 0x000000A8,
            WM_NCMBUTTONDBLCLK = 0x000000A9,
            WM_INPUT = 0x000000FF,
            SBM_SETPOS = 0x000000e0,
            SBM_GETPOS = 0x000000e1,
            SBM_SETRANGE = 0x000000e2,
            SBM_GETRANGE = 0x000000e3,
            SBM_ENABLE_ARROWS = 0x000000e4,
            SBM_SETRANGEREDRAW = 0x000000e6,
            SBM_SETSCROLLINFO = 0x000000e9,
            SBM_GETSCROLLINFO = 0x000000ea,
            SBM_GETSCROLLBARINFO = 0x000000eb,
            WM_KEYFIRST = 0x00000100,
            WM_KEYUP = 0x00000101,
            WM_CHAR = 0x00000102,
            WM_DEADCHAR = 0x00000103,
            WM_SYSKEYDOWN = 0x00000104,
            WM_SYSKEYUP = 0x00000105,
            WM_SYSCHAR = 0x00000106,
            WM_SYSDEADCHAR = 0x00000107,
            WM_KEYLAST = 0x00000108,
            WM_IME_STARTCOMPOSITION = 0x0000010D,
            WM_IME_ENDCOMPOSITION = 0x0000010E,
            WM_IME_COMPOSITION = 0x0000010F,
            WM_INITDIALOG = 0x00000110,
            WM_COMMAND = 0x00000111,
            WM_SYSCOMMAND = 0x00000112,
            WM_TIMER = 0x00000113,
            WM_HSCROLL = 0x00000114,
            WM_VSCROLL = 0x00000115,
            WM_INITMENU = 0x00000116,
            WM_INITMENUPOPUP = 0x00000117,
            WM_MENUSELECT = 0x0000011F,
            WM_MENUCHAR = 0x00000120,
            WM_ENTERIDLE = 0x00000121,
            WM_CTLCOLORMSGBOX = 0x00000132,
            WM_CTLCOLOREDIT = 0x00000133,
            WM_CTLCOLORLISTBOX = 0x00000134,
            WM_CTLCOLORBTN = 0x00000135,
            WM_CTLCOLORDLG = 0x00000136,
            WM_CTLCOLORSCROLLBAR = 0x00000137,
            WM_CTLCOLORSTATIC = 0x00000138,
            WM_MOUSEFIRST = 0x00000200,
            WM_LBUTTONDOWN = 0x00000201,
            WM_LBUTTONUP = 0x00000202,
            WM_LBUTTONDBLCLK = 0x00000203,
            WM_RBUTTONDOWN = 0x00000204,
            WM_RBUTTONUP = 0x00000205,
            WM_RBUTTONDBLCLK = 0x00000206,
            WM_MBUTTONDOWN = 0x00000207,
            WM_MBUTTONUP = 0x00000208,
            WM_MBUTTONDBLCLK = 0x00000209,
            WM_MOUSEWHEEL = 0x0000020A,
            WM_PARENTNOTIFY = 0x00000210,
            WM_ENTERMENULOOP = 0x00000211,
            WM_EXITMENULOOP = 0x00000212,
            WM_NEXTMENU = 0x00000213,
            WM_SIZING = 0x00000214,
            WM_CAPTURECHANGED = 0x00000215,
            WM_MOVING = 0x00000216,
            WM_POWERBROADCAST = 0x00000218,
            WM_DEVICECHANGE = 0x00000219,
            WM_MDICREATE = 0x00000220,
            WM_MDIDESTROY = 0x00000221,
            WM_MDIACTIVATE = 0x00000222,
            WM_MDIRESTORE = 0x00000223,
            WM_MDINEXT = 0x00000224,
            WM_MDIMAXIMIZE = 0x00000225,
            WM_MDITILE = 0x00000226,
            WM_MDICASCADE = 0x00000227,
            WM_MDIICONARRANGE = 0x00000228,
            WM_MDIGETACTIVE = 0x00000229,
            WM_MDISETMENU = 0x00000230,
            WM_ENTERSIZEMOVE = 0x00000231,
            WM_EXITSIZEMOVE = 0x00000232,
            WM_DROPFILES = 0x00000233,
            WM_MDIREFRESHMENU = 0x00000234,
            WM_IME_SETCONTEXT = 0x00000281,
            WM_IME_NOTIFY = 0x00000282,
            WM_IME_CONTROL = 0x00000283,
            WM_IME_COMPOSITIONFULL = 0x00000284,
            WM_IME_SELECT = 0x00000285,
            WM_IME_CHAR = 0x00000286,
            WM_IME_KEYDOWN = 0x00000290,
            WM_IME_KEYUP = 0x00000291,
            WM_MOUSEHOVER = 0x000002A1,
            WM_MOUSELEAVE = 0x000002A3,
            WM_CUT = 0x00000300,
            WM_COPY = 0x00000301,
            WM_PASTE = 0x00000302,
            WM_CLEAR = 0x00000303,
            WM_UNDO = 0x00000304,
            WM_RENDERFORMAT = 0x00000305,
            WM_RENDERALLFORMATS = 0x00000306,
            WM_DESTROYCLIPBOARD = 0x00000307,
            WM_DRAWCLIPBOARD = 0x00000308,
            WM_PAINTCLIPBOARD = 0x00000309,
            WM_VSCROLLCLIPBOARD = 0x0000030A,
            WM_SIZECLIPBOARD = 0x0000030B,
            WM_ASKCBFORMATNAME = 0x0000030C,
            WM_CHANGECBCHAIN = 0x0000030D,
            WM_HSCROLLCLIPBOARD = 0x0000030E,
            WM_QUERYNEWPALETTE = 0x0000030F,
            WM_PALETTEISCHANGING = 0x00000310,
            WM_PALETTECHANGED = 0x00000311,
            WM_HOTKEY = 0x00000312,
            WM_PRINT = 0x00000317,
            WM_PRINTCLIENT = 0x00000318,
            WM_APPCOMMAND = 0x00000319,
            WM_THEMECHANGED = 0x0000031A,
            WM_HANDHELDFIRST = 0x00000358,
            WM_HANDHELDLAST = 0x0000035F,
            WM_AFXFIRST = 0x00000360,
            WM_AFXLAST = 0x0000037f,
            WM_PENWINFIRST = 0x00000380,
            WM_PENWINLAST = 0x0000038F,
            WM_DDE_FIRST = 0x000003E0,
            WM_DDE_TERMINATE = 0x000003E1,
            WM_DDE_ADVISE = 0x000003E2,
            WM_DDE_UNADVISE = 0x000003E3,
            WM_DDE_ACK = 0x000003E4,
            WM_DDE_DATA = 0x000003E5,
            WM_DDE_REQUEST = 0x000003E6,
            WM_DDE_POKE = 0x000003E7,
            WM_DDE_EXECUTE = 0x000003E8,
            WM_USER = 0x00000400,
            CBEM_INSERTITEMA = 0x00000401,
            CBEM_SETIMAGELIST = 0x00000402,
            CBEM_GETIMAGELIST = 0x00000403,
            CBEM_GETITEMA = 0x00000404,
            CBEM_SETITEMA = 0x00000405,
            CBEM_GETCOMBOCONTROL = 0x00000406,
            CBEM_GETEDITCONTROL = 0x00000407,
            CBEM_SETEXSTYLE = 0x00000408,
            CBEM_GETEXSTYLE = 0x00000409,
            CBEM_HASEDITCHANGED = 0x0000040a,
            CBEM_INSERTITEMW = 0x0000040b,
            CBEM_SETITEMW = 0x0000040c,
            CBEM_GETITEMW = 0x0000040d,
            CBEM_SETEXTENDEDSTYLE = 0x0000040e,
            SB_SETICON = 0x0000040f,
            RB_IDTOINDEX = 0x00000410,
            RB_GETTOOLTIPS = 0x00000411,
            RB_SETTOOLTIPS = 0x00000412,
            RB_SETBKCOLOR = 0x00000413,
            RB_GETBKCOLOR = 0x00000414,
            RB_SETTEXTCOLOR = 0x00000415,
            RB_GETTEXTCOLOR = 0x00000416,
            RB_SIZETORECT = 0x00000417,
            RB_BEGINDRAG = 0x00000418,
            RB_ENDDRAG = 0x00000419,
            RB_DRAGMOVE = 0x0000041a,
            RB_GETBARHEIGHT = 0x0000041b,
            RB_GETBANDINFOW = 0x0000041c,
            RB_GETBANDINFOA = 0x0000041d,
            RB_MINIMIZEBAND = 0x0000041e,
            RB_MAXIMIZEBAND = 0x0000041f,
            TBM_SETBUDDY = 0x00000420,
            MSG_FTS_JUMP_VA = 0x00000421,
            RB_GETBANDBORDERS = 0x00000422,
            MSG_FTS_JUMP_QWORD = 0x00000423,
            MSG_REINDEX_REQUEST = 0x00000424,
            MSG_FTS_WHERE_IS_IT = 0x00000425,
            RB_GETPALETTE = 0x00000426,
            RB_MOVEBAND = 0x00000427,
            TB_GETROWS = 0x00000428,
            TB_GETBITMAPFLAGS = 0x00000429,
            TB_SETCMDID = 0x0000042a,
            RB_PUSHCHEVRON = 0x0000042b,
            TB_GETBITMAP = 0x0000042c,
            MSG_GET_DEFFONT = 0x0000042d,
            TB_REPLACEBITMAP = 0x0000042e,
            TB_SETINDENT = 0x0000042f,
            TB_SETIMAGELIST = 0x00000430,
            TB_GETIMAGELIST = 0x00000431,
            TB_LOADIMAGES = 0x00000432,
            TB_GETRECT = 0x00000433,
            TB_SETHOTIMAGELIST = 0x00000434,
            TB_GETHOTIMAGELIST = 0x00000435,
            TB_SETDISABLEDIMAGELIST = 0x00000436,
            TB_GETDISABLEDIMAGELIST = 0x00000437,
            TB_SETSTYLE = 0x00000438,
            TB_GETSTYLE = 0x00000439,
            TB_GETBUTTONSIZE = 0x0000043a,
            TB_SETBUTTONWIDTH = 0x0000043b,
            TB_SETMAXTEXTROWS = 0x0000043c,
            TB_GETTEXTROWS = 0x0000043d,
            TB_GETOBJECT = 0x0000043e,
            TB_GETBUTTONINFOW = 0x0000043f,
            TB_SETBUTTONINFOW = 0x00000440,
            TB_GETBUTTONINFOA = 0x00000441,
            TB_SETBUTTONINFOA = 0x00000442,
            TB_INSERTBUTTONW = 0x00000443,
            TB_ADDBUTTONSW = 0x00000444,
            TB_HITTEST = 0x00000445,
            TB_SETDRAWTEXTFLAGS = 0x00000446,
            TB_GETHOTITEM = 0x00000447,
            TB_SETHOTITEM = 0x00000448,
            TB_SETANCHORHIGHLIGHT = 0x00000449,
            TB_GETANCHORHIGHLIGHT = 0x0000044a,
            TB_GETBUTTONTEXTW = 0x0000044b,
            TB_SAVERESTOREW = 0x0000044c,
            TB_ADDSTRINGW = 0x0000044d,
            TB_MAPACCELERATORA = 0x0000044e,
            TB_GETINSERTMARK = 0x0000044f,
            TB_SETINSERTMARK = 0x00000450,
            TB_INSERTMARKHITTEST = 0x00000451,
            TB_MOVEBUTTON = 0x00000452,
            TB_GETMAXSIZE = 0x00000453,
            TB_SETEXTENDEDSTYLE = 0x00000454,
            TB_GETEXTENDEDSTYLE = 0x00000455,
            TB_GETPADDING = 0x00000456,
            TB_SETPADDING = 0x00000457,
            TB_SETINSERTMARKCOLOR = 0x00000458,
            TB_GETINSERTMARKCOLOR = 0x00000459,
            TB_MAPACCELERATORW = 0x0000045a,
            TB_GETSTRINGW = 0x0000045b,
            TB_GETSTRINGA = 0x0000045c,
            TAPI_REPLY = 0x00000463,
            ACM_OPENA = 0x00000464,
            ACM_PLAY = 0x00000465,
            ACM_STOP = 0x00000466,
            ACM_OPENW = 0x00000467,
            BFFM_SETSTATUSTEXTW = 0x00000468,
            CDM_HIDECONTROL = 0x00000469,
            CDM_SETDEFEXT = 0x0000046a,
            PSM_CANCELTOCLOSE = 0x0000046b,
            EM_CONVPOSITION = 0x0000046c,
            MCIWNDM_GETZOOM = 0x0000046d,
            PSM_APPLY = 0x0000046e,
            PSM_SETTITLEA = 0x0000046f,
            PSM_SETWIZBUTTONS = 0x00000470,
            PSM_PRESSBUTTON = 0x00000471,
            PSM_SETCURSELID = 0x00000472,
            PSM_SETFINISHTEXTA = 0x00000473,
            PSM_GETTABCONTROL = 0x00000474,
            PSM_ISDIALOGMESSAGE = 0x00000475,
            MCIWNDM_REALIZE = 0x00000476,
            MCIWNDM_SETTIMEFORMATA = 0x00000477,
            MCIWNDM_GETTIMEFORMATA = 0x00000478,
            MCIWNDM_VALIDATEMEDIA = 0x00000479,
            MCIWNDM_PLAYTO = 0x0000047b,
            MCIWNDM_GETFILENAMEA = 0x0000047c,
            MCIWNDM_GETDEVICEA = 0x0000047d,
            MCIWNDM_GETPALETTE = 0x0000047e,
            MCIWNDM_SETPALETTE = 0x0000047f,
            MCIWNDM_GETERRORA = 0x00000480,
            PSM_HWNDTOINDEX = 0x00000481,
            PSM_INDEXTOHWND = 0x00000482,
            MCIWNDM_SETINACTIVETIMER = 0x00000483,
            PSM_INDEXTOPAGE = 0x00000484,
            DL_BEGINDRAG = 0x00000485,
            DL_DRAGGING = 0x00000486,
            DL_DROPPED = 0x00000487,
            DL_CANCELDRAG = 0x00000488,
            MCIWNDM_GET_SOURCE = 0x0000048c,
            MCIWNDM_PUT_SOURCE = 0x0000048d,
            MCIWNDM_GET_DEST = 0x0000048e,
            MCIWNDM_PUT_DEST = 0x0000048f,
            MCIWNDM_CAN_PLAY = 0x00000490,
            MCIWNDM_CAN_WINDOW = 0x00000491,
            MCIWNDM_CAN_RECORD = 0x00000492,
            MCIWNDM_CAN_SAVE = 0x00000493,
            MCIWNDM_CAN_EJECT = 0x00000494,
            MCIWNDM_CAN_CONFIG = 0x00000495,
            IE_GETINK = 0x00000496,
            IE_SETINK = 0x00000497,
            IE_GETPENTIP = 0x00000498,
            IE_SETPENTIP = 0x00000499,
            IE_GETERASERTIP = 0x0000049a,
            IE_SETERASERTIP = 0x0000049b,
            IE_GETBKGND = 0x0000049c,
            IE_SETBKGND = 0x0000049d,
            IE_GETGRIDORIGIN = 0x0000049e,
            IE_SETGRIDORIGIN = 0x0000049f,
            IE_GETGRIDPEN = 0x000004a0,
            IE_SETGRIDPEN = 0x000004a1,
            IE_GETGRIDSIZE = 0x000004a2,
            IE_SETGRIDSIZE = 0x000004a3,
            IE_GETMODE = 0x000004a4,
            IE_SETMODE = 0x000004a5,
            IE_GETINKRECT = 0x000004a6,
            IE_GETAPPDATA = 0x000004b8,
            IE_SETAPPDATA = 0x000004b9,
            IE_GETDRAWOPTS = 0x000004ba,
            IE_SETDRAWOPTS = 0x000004bb,
            IE_GETFORMAT = 0x000004bc,
            IE_SETFORMAT = 0x000004bd,
            IE_GETINKINPUT = 0x000004be,
            IE_SETINKINPUT = 0x000004bf,
            IE_GETNOTIFY = 0x000004c0,
            IE_SETNOTIFY = 0x000004c1,
            IE_GETRECOG = 0x000004c2,
            IE_SETRECOG = 0x000004c3,
            IE_GETSECURITY = 0x000004c4,
            IE_SETSECURITY = 0x000004c5,
            IE_GETSEL = 0x000004c6,
            IE_SETSEL = 0x000004c7,
            CDM_LAST = 0x000004c8,
            IE_GETCOMMAND = 0x000004c9,
            IE_GETCOUNT = 0x000004ca,
            IE_GETGESTURE = 0x000004cb,
            IE_GETMENU = 0x000004cc,
            IE_GETPAINTDC = 0x000004cd,
            IE_GETPDEVENT = 0x000004ce,
            IE_GETSELCOUNT = 0x000004cf,
            IE_GETSELITEMS = 0x000004d0,
            IE_GETSTYLE = 0x000004d1,
            MCIWNDM_SETTIMEFORMATW = 0x000004db,
            EM_OUTLINE = 0x000004dc,
            EM_GETSCROLLPOS = 0x000004dd,
            EM_SETSCROLLPOS = 0x000004de,
            EM_SETFONTSIZE = 0x000004df,
            MCIWNDM_GETFILENAMEW = 0x000004e0,
            MCIWNDM_GETDEVICEW = 0x000004e1,
            MCIWNDM_GETERRORW = 0x000004e4,
            FM_GETFOCUS = 0x00000600,
            FM_GETDRIVEINFOA = 0x00000601,
            FM_GETSELCOUNT = 0x00000602,
            FM_GETSELCOUNTLFN = 0x00000603,
            FM_GETFILESELA = 0x00000604,
            FM_GETFILESELLFNA = 0x00000605,
            FM_REFRESH_WINDOWS = 0x00000606,
            FM_RELOAD_EXTENSIONS = 0x00000607,
            FM_GETDRIVEINFOW = 0x00000611,
            FM_GETFILESELW = 0x00000614,
            FM_GETFILESELLFNW = 0x00000615,
            WLX_WM_SAS = 0x00000659,
            SM_GETSELCOUNT = 0x000007e8,
            SM_GETSERVERSELA = 0x000007e9,
            SM_GETSERVERSELW = 0x000007ea,
            SM_GETCURFOCUSA = 0x000007eb,
            SM_GETCURFOCUSW = 0x000007ec,
            SM_GETOPTIONS = 0x000007ed,
            UM_GETCURFOCUSW = 0x000007ee,
            UM_GETOPTIONS = 0x000007ef,
            UM_GETOPTIONS2 = 0x000007f0,
            OCM = 0x00002000,
            OCM_CTLCOLOR = 0x00002019,
            OCM_DRAWITEM = 0x0000202b,
            OCM_MEASUREITEM = 0x0000202c,
            OCM_DELETEITEM = 0x0000202d,
            OCM_VKEYTOITEM = 0x0000202e,
            OCM_CHARTOITEM = 0x0000202f,
            OCM_COMPAREITEM = 0x00002039,
            OCM_NOTIFY = 0x0000204e,
            OCM_COMMAND = 0x00002111,
            OCM_HSCROLL = 0x00002114,
            OCM_VSCROLL = 0x00002115,
            OCM_CTLCOLORMSGBOX = 0x00002132,
            OCM_CTLCOLOREDIT = 0x00002133,
            OCM_CTLCOLORLISTBOX = 0x00002134,
            OCM_CTLCOLORBTN = 0x00002135,
            OCM_CTLCOLORDLG = 0x00002136,
            OCM_CTLCOLORSCROLLBAR = 0x00002137,
            OCM_CTLCOLORSTATIC = 0x00002138,
            OCM_PARENTNOTIFY = 0x00002210,
            WM_APP = 0x00008000,
        }
        enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0,
            SMTO_BLOCK = 0x1,
            SMTO_ABORTIFHUNG = 0x2,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x8
        }
    }
}
