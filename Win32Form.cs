using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace ImmersiveGaming
{
    public class Win32Form
    {
        IntPtr _handle;
        string _text;
        string _className;

        static Dictionary<IntPtr, Tuple<DateTime, Win32Form>> s_windows = new Dictionary<IntPtr, Tuple<DateTime, Win32Form>>();

        public static Win32Form Fetch(IntPtr handle)
        {
            Cleanup();
            if (s_windows.ContainsKey(handle))
            {
                s_windows[handle] = Tuple.Create(DateTime.Now, s_windows[handle].Item2);
            }
            else
            {
                s_windows[handle] = Tuple.Create(DateTime.Now, new Win32Form(handle));
            }
            return s_windows[handle].Item2;
        }

        private static void Cleanup()
        {
            foreach (var k in s_windows.Keys)
            {
                var tmp = s_windows[k];
                if (!User32.IsWindow(k))
                {
                    s_windows.Remove(k);
                    continue;
                }
            }
        }

        private Win32Form(IntPtr handle)
        {
            //Debug.Assert(handle != IntPtr.Zero);
            this._handle = handle;
        }

        public static Win32Form BottomWindow
        {
            get
            {
                return new Win32Form(new IntPtr(1));
            }
        }

        public static Win32Form TopWindow
        {
            get
            {
                return new Win32Form(new IntPtr(0));
            }
        }

        public static Win32Form TopMostWindow
        {
            get
            {
                return new Win32Form(new IntPtr(-1));
            }
        }

        public static Win32Form NoTopMostWindow
        {
            get
            {
                return new Win32Form(new IntPtr(-2));
            }
        }

        public static Win32Form DesktopWindow
        {
            get
            {
                return new Win32Form(User32.GetDesktopWindow());
            }
        }

        public static Win32Form ForegroundWindow
        {
            get
            {
                var handle = User32.GetForegroundWindow();
                if (handle != IntPtr.Zero)
                {
                    return new Win32Form(handle);
                }
                else
                {
                    return null;
                }
            }
        }

        public static IEnumerable<Win32Form> AllWindows
        {
            get
            {
                List<Win32Form> result = new List<Win32Form>();
                User32.EnumChildWindows(IntPtr.Zero, (hwnd, _) => { result.Add(new Win32Form(hwnd)); return true; }, IntPtr.Zero);
                return result;
            }
        }

        public bool TopMost
        {
            get
            {
                return (WindowExStyle & User32Types.WindowExStyles.Topmost) == User32Types.WindowExStyles.Topmost;
            }
            set
            {
                if (value)
                {
                    WindowExStyle |= User32Types.WindowExStyles.Topmost;
                    User32.SetWindowPos(_handle, TopMostWindow._handle, 0, 0, 0, 0, User32Types.SetWindowPosFlags.IgnoreResize | User32Types.SetWindowPosFlags.IgnoreMove | User32Types.SetWindowPosFlags.DoNotReposition);
                }
                else
                {
                    WindowExStyle &= ~User32Types.WindowExStyles.Topmost;
                    User32.SetWindowLong(_handle, User32Types.WindowLongIndex.ExStyle, User32.GetWindowLong(_handle, User32Types.WindowLongIndex.ExStyle) & ~(int)(User32Types.WindowExStyles.Topmost));
                    User32.SetWindowPos(_handle, TopWindow._handle, 0, 0, 0, 0, User32Types.SetWindowPosFlags.IgnoreResize | User32Types.SetWindowPosFlags.IgnoreMove | User32Types.SetWindowPosFlags.DoNotReposition);
                }
            }
        }

        public User32Types.WindowStyles WindowStyle
        {
            get
            {
                return (User32Types.WindowStyles)User32.GetWindowLong(_handle, User32Types.WindowLongIndex.Style);
            }
            set
            {
                if (WindowStyle != value)
                {
                    if (User32.SetWindowLong(_handle, User32Types.WindowLongIndex.Style, (int)value) == 0)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
            }
        }

        public User32Types.WindowExStyles WindowExStyle
        {
            get
            {
                return (User32Types.WindowExStyles)User32.GetWindowLong(_handle, User32Types.WindowLongIndex.ExStyle);
            }
            set
            {
                if (WindowExStyle != value)
                {
                    if (User32.SetWindowLong(_handle, User32Types.WindowLongIndex.ExStyle, (int)value) == 0)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
            }
        }

        public bool Visible
        {
            get
            {
                return User32.IsWindowVisible(_handle);
            }
            set
            {
                if (value)
                {
                    User32.ShowWindow(_handle, User32Types.ShowWindowCommand.Show);
                }
                else
                {
                    User32.ShowWindow(_handle, User32Types.ShowWindowCommand.Minimize);
                }
            }
        }

        public string Text
        {
            get
            {
                if (String.IsNullOrEmpty(_text))
                {
                    var tmp = new StringBuilder(257);
                    var len = User32.GetWindowTextLength(_handle);
                    if (len == 0)
                    {
                        return String.Empty;
                    }
                    if (len >= tmp.Capacity)
                    {
                        tmp = new StringBuilder(len + 1);
                    }
                    if (tmp.Capacity > 1)
                    {
                        if (User32.GetWindowText(_handle, tmp, tmp.Capacity) == 0)
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                    _text = tmp.ToString();
                }
                return _text;
            }
            set
            {
                if (!User32.SetWindowText(_handle, value))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                else
                {
                    _text = value;
                }
            }
        }

        public Rectangle Rect
        {
            get
            {
                User32Types.Rect rect;
                User32.GetWindowRect(_handle, out rect);
                return rect;
            }
            set
            {
                User32.SetWindowPos(_handle, IntPtr.Zero, value.Left, value.Top, value.Width, value.Height, User32Types.SetWindowPosFlags.AsynchronousWindowPosition | User32Types.SetWindowPosFlags.DoNotActivate | User32Types.SetWindowPosFlags.DoNotChangeOwnerZOrder);
            }
        }

        public Rectangle ClientRect
        {
            get
            {
                User32Types.Rect rect;
                User32.GetClientRect(_handle, out rect);
                return rect;
            }
            set
            {
                if (ClientRect != value)
                {
                    User32Types.Rect rect = value;
                    User32.AdjustWindowRectEx(ref rect, WindowStyle, HasMenu, WindowExStyle);
                    User32.SetWindowPos(_handle, IntPtr.Zero, rect.Left, rect.Top, rect.Width, rect.Height, User32Types.SetWindowPosFlags.AsynchronousWindowPosition | User32Types.SetWindowPosFlags.DoNotActivate | User32Types.SetWindowPosFlags.DoNotChangeOwnerZOrder | User32Types.SetWindowPosFlags.FrameChanged);
                }
            }
        }

        public bool HasMenu
        {
            get
            {
                return User32.GetMenu(_handle) != IntPtr.Zero;
            }
        }

        public Point Location
        {
            get
            {
                User32Types.Rect rect;
                User32.GetWindowRect(_handle, out rect);
                return new Point(rect.Left, rect.Top);
            }
            set
            {
                if (Location != value)
                {
                    User32.SetWindowPos(_handle, IntPtr.Zero, value.X, value.Y, 0, 0, User32Types.SetWindowPosFlags.AsynchronousWindowPosition | User32Types.SetWindowPosFlags.DoNotActivate | User32Types.SetWindowPosFlags.DoNotChangeOwnerZOrder | User32Types.SetWindowPosFlags.IgnoreResize | User32Types.SetWindowPosFlags.FrameChanged);
                }
            }
        }

        public Size Size
        {
            get
            {
                User32Types.Rect rect;
                User32.GetWindowRect(_handle, out rect);
                return new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
            set
            {
                if (Size != value)
                {
                    User32.SetWindowPos(_handle, IntPtr.Zero, 0, 0, value.Width, value.Height, User32Types.SetWindowPosFlags.AsynchronousWindowPosition | User32Types.SetWindowPosFlags.DoNotActivate | User32Types.SetWindowPosFlags.DoNotChangeOwnerZOrder | User32Types.SetWindowPosFlags.IgnoreMove);
                }
            }
        }

        public IEnumerable<Win32Form> ChildWindows
        {
            get
            {
                List<Win32Form> result = new List<Win32Form>();
                User32.EnumChildWindows(_handle, (hwnd, b) => { result.Add(new Win32Form(hwnd)); return true; }, IntPtr.Zero);
                return result;
            }
        }

        public string ClassName
        {
            get
            {
                if (string.IsNullOrEmpty(_className))
                {
                    var tmp = new StringBuilder(257);
                    if (User32.GetClassName(_handle, tmp, tmp.Capacity) == 0)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                    _className = tmp.ToString();
                }
                return _className;
            }
        }

        public Process Process
        {
            get
            {
                try
                {
                    IntPtr pid;
                    User32.GetWindowThreadProcessId(_handle, out pid);
                    return Process.GetProcessById((int)pid);
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }
        }

        public override string ToString()
        {
            return Text;
        }

        public bool Equals(Win32Form other)
        {
            return _handle == other._handle;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is Win32Form)
            {
                var frm = obj as Win32Form;
                return frm._handle == _handle;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _handle.GetHashCode();
        }

        public static bool operator ==(Win32Form a, Win32Form b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            
            // Return true if the fields match:
            return a._handle == b._handle;
        }

        public static bool operator !=(Win32Form a, Win32Form b)
        {
            return !(a == b);
        }
    }
}
