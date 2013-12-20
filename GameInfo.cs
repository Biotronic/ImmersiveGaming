using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmersiveGaming
{
    class GameInfo
    {
        public enum Comparison
        {
            Equals,
            StartsWith,
            EndsWith,
            Contains,
            Regex
        }

        private string WindowTitle;
        private string ClassName;
        private string ExeName;
        private Comparison WindowTitleCmp;
        private Comparison ClassNameCmp;
        private Comparison ExeNameCmp;
        private bool[] Blackout;
        private bool HideMouse;

        public GameInfo(string windowTitle, Comparison windowTitleCmp, string className, Comparison classNameCmp, string exeName, Comparison exeNameCmp, bool[] blackout, bool hideMouse)
        {
            this.WindowTitle = windowTitle;
            this.ClassName = className;
            this.ExeName = exeName;
            this.WindowTitleCmp = windowTitleCmp;
            this.ClassNameCmp = classNameCmp;
            this.ExeNameCmp = exeNameCmp;
            this.Blackout = blackout;
            this.HideMouse = hideMouse;
        }

        private bool compare(Comparison cmp, string needle, string haystack)
        {
            if (needle != null)
            {
                switch (cmp)
                {
                    case Comparison.Equals:
                        return haystack == needle;
                    case Comparison.StartsWith:
                        return haystack.StartsWith(needle);
                    case Comparison.EndsWith:
                        return haystack.EndsWith(needle);
                    case Comparison.Contains:
                        return haystack.Contains(needle);
                    case Comparison.Regex:
                        return Regex.IsMatch(needle, haystack);
                }
            }
            return true;
        }

        public bool Matches(Win32Form form)
        {
            return compare(WindowTitleCmp, form.Text, WindowTitle) &&
                compare(ClassNameCmp, form.ClassName, ClassName) &&
                compare(ExeNameCmp, form.Process.MainModule.FileName, ExeName);
        }
    }
}
