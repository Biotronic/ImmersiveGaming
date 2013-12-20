using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImmersiveGaming
{
    public class Win32Form
    {
        IntPtr handle;

        public Win32Form(IntPtr handle)
        {
            Debug.Assert(handle != IntPtr.Zero);
            this.handle = handle;
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

        public User32Types.WindowStyles WindowStyle
        {
            get
            {
                return (User32Types.WindowStyles)User32.GetWindowLong(handle, User32Types.WindowLongIndex.Style);
            }
            set
            {
                if (WindowStyle != value)
                {
                    if (User32.SetWindowLong(handle, User32Types.WindowLongIndex.Style, (int)value) == 0)
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
                return (User32Types.WindowExStyles)User32.GetWindowLong(handle, User32Types.WindowLongIndex.ExStyle);
            }
            set
            {
                if (WindowExStyle != value)
                {
                    if (User32.SetWindowLong(handle, User32Types.WindowLongIndex.ExStyle, (int)value) == 0)
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
                return User32.IsWindowVisible(handle);
            }
            set
            {
                if (value)
                {
                    User32.ShowWindow(handle, User32Types.ShowWindowCommand.Show);
                }
                else
                {
                    User32.ShowWindow(handle, User32Types.ShowWindowCommand.Minimize);
                }
            }
        }

        public string Text
        {
            get
            {
                var tmp = new StringBuilder(User32.GetWindowTextLength(handle) + 1);
                if (tmp.Capacity > 1)
                {
                    if (User32.GetWindowText(handle, tmp, tmp.Capacity) == 0)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                return tmp.ToString();
            }
            set
            {
                if (!User32.SetWindowText(handle, value))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        public Rectangle Rect
        {
            get
            {
                User32Types.Rect rect;
                User32.GetWindowRect(handle, out rect);
                return rect;
            }
            set
            {
                User32.SetWindowPos(handle, IntPtr.Zero, value.Left, value.Top, value.Width, value.Height, User32Types.SetWindowPosFlags.AsynchronousWindowPosition | User32Types.SetWindowPosFlags.DoNotActivate | User32Types.SetWindowPosFlags.DoNotChangeOwnerZOrder);
            }
        }

        public Rectangle ClientRect
        {
            get
            {
                User32Types.Rect rect;
                User32.GetClientRect(handle, out rect);
                return rect;
            }
            set
            {
                if (ClientRect != value)
                {
                    User32Types.Rect rect = value;
                    User32.AdjustWindowRectEx(ref rect, WindowStyle, HasMenu, WindowExStyle);
                    User32.SetWindowPos(handle, IntPtr.Zero, rect.Left, rect.Top, rect.Width, rect.Height, User32Types.SetWindowPosFlags.AsynchronousWindowPosition | User32Types.SetWindowPosFlags.DoNotActivate | User32Types.SetWindowPosFlags.DoNotChangeOwnerZOrder);
                }
            }
        }

        public bool HasMenu
        {
            get
            {
                return User32.GetMenu(handle) != IntPtr.Zero;
            }
        }

        public Point Location
        {
            get
            {
                User32Types.Rect rect;
                User32.GetWindowRect(handle, out rect);
                return new Point(rect.Left, rect.Top);
            }
            set
            {
                if (Location != value)
                {
                    User32.SetWindowPos(handle, IntPtr.Zero, value.X, value.Y, 0, 0, User32Types.SetWindowPosFlags.AsynchronousWindowPosition | User32Types.SetWindowPosFlags.DoNotActivate | User32Types.SetWindowPosFlags.DoNotChangeOwnerZOrder | User32Types.SetWindowPosFlags.IgnoreResize | User32Types.SetWindowPosFlags.FrameChanged);
                }
            }
        }

        public Size Size
        {
            get
            {
                User32Types.Rect rect;
                User32.GetWindowRect(handle, out rect);
                return new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
            set
            {
                if (Size != value)
                {
                    User32.SetWindowPos(handle, IntPtr.Zero, 0, 0, value.Width, value.Height, User32Types.SetWindowPosFlags.AsynchronousWindowPosition | User32Types.SetWindowPosFlags.DoNotActivate | User32Types.SetWindowPosFlags.DoNotChangeOwnerZOrder | User32Types.SetWindowPosFlags.IgnoreMove);
                }
            }
        }

        public IEnumerable<Win32Form> ChildWindows
        {
            get
            {
                List<Win32Form> result = new List<Win32Form>();
                User32.EnumChildWindows(handle, (hwnd, b) => { result.Add(new Win32Form(hwnd)); return true; }, IntPtr.Zero);
                return result;
            }
        }

        public string ClassName
        {
            get
            {
                var tmp = new StringBuilder(257);
                if (tmp.Capacity > 1)
                {
                    if (User32.GetClassName(handle, tmp, tmp.Capacity) == 0)
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                return tmp.ToString();
            }
        }

        public Process Process
        {
            get
            {
                IntPtr pid;
                User32.GetWindowThreadProcessId(handle, out pid);
                return Process.GetProcessById((int)pid);
            }
        }

        public override string ToString()
        {
            return Text;
        }

        public bool Equals(Win32Form other)
        {
            return handle == other.handle;
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
                return frm.handle == handle;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return handle.GetHashCode();
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
            return a.handle == b.handle;
        }

        public static bool operator !=(Win32Form a, Win32Form b)
        {
            return !(a == b);
        }
    }
}
