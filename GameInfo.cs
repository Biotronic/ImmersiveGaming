using Biotronic;
using ImmersiveGaming.User32Types;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ImmersiveGaming
{
    public enum ComparisonOperator
    {
        Equals,
        StartsWith,
        EndsWith,
        Contains,
        Regex,
    }

    public struct Comparer
    {
        [XmlAttribute]
        public bool active;
        [XmlAttribute]
        public ComparisonOperator operation;
        public string pattern;

        public Comparer(bool active, ComparisonOperator op, string value)
        {
            this.active = active;
            this.operation = op;
            this.pattern = value;
        }

        public bool IsMatch(string input)
        {
            if (!active)
            {
                return true;
            }
            switch (operation)
            {
                case ComparisonOperator.Equals:
                    return pattern == input;
                case ComparisonOperator.Contains:
                    return input.Contains(pattern);
                case ComparisonOperator.StartsWith:
                    return input.StartsWith(pattern);
                case ComparisonOperator.EndsWith:
                    return input.EndsWith(pattern);
                case ComparisonOperator.Regex:
                    return Regex.IsMatch(input, pattern);
            }
            Debug.Assert(false, "Invalid value for Comparer.op");
            return false;
        }

        public bool IsComparable
        {
            get
            {
                if (active)
                {
                    if (string.IsNullOrEmpty(pattern))
                    {
                        return operation == ComparisonOperator.Equals;
                    }
                    return true;
                }
                return false;
            }
        }

        public bool IsSimilar(Comparer other)
        {
            if (IsComparable && other.IsComparable)
            {
                return pattern == other.pattern;
            }
            return false;
        }
    }

    public struct ScreenInfo
    {
        public Rect Bounds;

        public ScreenInfo(Screen s)
        {
            Bounds = s.Bounds;
        }
    }

    public sealed class GameInfo
    {
        [XmlAttribute]
        public string name;
        public Comparer title;
        public Comparer className;
        public Comparer file;
        public bool blackoutUnused;
        public bool hideMouse;
        public CheckState alwaysOnTop;
        public ScreenInfo[] monitors;

        public GameInfo()
        {
            name = "New Game";
            title = new Comparer(true, ComparisonOperator.Equals, "");
            className = new Comparer(true, ComparisonOperator.Equals, "");
            file = new Comparer(true, ComparisonOperator.Equals, "");
            blackoutUnused = false;
            hideMouse = false;
            monitors = new ScreenInfo[] { };
        }

        public GameInfo(string name, Comparer wndTitle, Comparer wndClass, Comparer wndFile, bool blackoutUnused, bool hideMouse, bool[] monitors)
        {
            this.name = name;
            this.title = wndTitle;
            this.className = wndClass;
            this.file = wndFile;
            this.blackoutUnused = blackoutUnused;
            this.hideMouse = hideMouse;
            this.monitors = Screen.AllScreens.ZipFilter(monitors).Select(a=>new ScreenInfo(a)).ToArray();
        }

        public bool IsMatch(Win32Form form)
        {
            bool match = false;
            if (className.active)
            {
                if (!className.IsMatch(form.ClassName))
                {
                    return false;
                }
                match = true;
            }
            if (title.active)
            {
                if (!title.IsMatch(form.Text))
                {
                    return false;
                }
                match = true;
            }
            if (file.active)
            {
                if (!file.IsMatch(form.Process.MainModule.FileName))
                {
                    return false;
                }
                match = true;
            }
            return match;
        }

        public bool IsSimilar(GameInfo other)
        {
            if (name == other.name)
            {
                return true;
            }
            if (title.IsComparable)
            {
                return title.IsSimilar(other.title);
            }
            if (className.IsComparable)
            {
                return className.IsSimilar(other.className);
            }
            if (file.IsComparable)
            {
                return file.IsSimilar(other.file);
            }
            return true;
        }
    }
}
