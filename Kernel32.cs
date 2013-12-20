using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ImmersiveGaming
{
    static class Kernel32
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(Kernel32Types.ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, IntPtr dwProcessId);
    }

    static class Kernel32Types
    {
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }
    }
}
