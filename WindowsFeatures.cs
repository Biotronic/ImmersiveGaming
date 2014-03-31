using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmersiveGaming
{
    static class WindowsFeatures
    {
        static RegistryKey cuRunKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        static RegistryKey lmRunKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public enum BootSegment
        {
            user,
            global
        }

        private static RegistryKey getSegmentKey(BootSegment segment)
        {
            return segment == BootSegment.global ? lmRunKey : cuRunKey;
        }

        public static void AddToBoot(BootSegment segment, string valueName)
        {
            AddToBoot(segment, valueName, System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase);
        }

        public static void AddToBoot(BootSegment segment, string valueName, string exePath)
        {
            getSegmentKey(segment).SetValue(valueName, exePath);
        }

        public static void RemoveFromBoot(BootSegment segment, string valueName)
        {
            getSegmentKey(segment).DeleteValue(valueName);
        }

        public static bool StartsOnBoot(BootSegment segment, string valueName)
        {
            return getSegmentKey(segment).GetValue(valueName) != null;
        }
    }
}
