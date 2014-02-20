using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ImmersiveGaming
{
    class GameImmersifier
    {
        internal GameInfo info;
        Win32Form form;
        static List<BlackoutForm> blackoutForms = new List<BlackoutForm>();

        public GameImmersifier(Win32Form form, GameInfo info)
        {
            this.form = form;
            this.info = info;
        }

        Rectangle Rect
        {
            get
            {
                return info.monitors.Select(a => (Rectangle)a.Bounds).Aggregate(Rectangle.Union);
            }
        }

        private void RemoveBorders(Win32Form form)
        {
            form.WindowStyle = User32Types.WindowStyles.MinimizeBox | User32Types.WindowStyles.Popup | User32Types.WindowStyles.Tiled | User32Types.WindowStyles.Visible;
            form.WindowExStyle = User32Types.WindowExStyles.Left;
        }

        public void Activate()
        {
            if (info.hideMouse)
            {
                MouseHider.HideCursors();
            }
            if (info.monitors.Any())
            {
                if (info.blackoutUnused)
                {
                    Blackout(Rect);
                }
                RemoveBorders(form);
                form.Rect = Rect;
            }
        }

        public void Deactivate()
        {
            if (info.hideMouse)
            {
                MouseHider.ShowCursors();
            }
            if (info.monitors.Any())
            {
                if (info.blackoutUnused)
                {
                    Whitein();
                }
            }
        }

        static public void Blackout(Rectangle rect)
        {
            while (blackoutForms.Count > 0)
            {
                blackoutForms[0].Close();
                blackoutForms.RemoveAt(0);
            }
            var bounds = Screen.AllScreens.Where(a => !a.Bounds.IntersectsWith(rect)).ToArray();
            blackoutForms = bounds.Select(a => { var tmp = new BlackoutForm(a); tmp.Show(); return tmp; }).ToList();
        }

        static public void Whitein()
        {
            while (blackoutForms.Count > 0)
            {
                blackoutForms[0].Close();
                blackoutForms.RemoveAt(0);
            }
        }
    }

    [Serializable]
    public class AllGamesInfo
    {
        public List<GameInfo> games = new List<GameInfo>();

        public AllGamesInfo()
        {
        }

        public void Add(GameInfo info)
        {
            games.Add(info);
        }

        public void Merge(AllGamesInfo other)
        {
            Merge(other, (a, b) => new[] { a });
        }

        public void Merge(AllGamesInfo other, Func<GameInfo, GameInfo, GameInfo[]> handler)
        {
            if (other != null)
            {
                foreach (var game in other.games)
                {
                    if (games.Contains(game, new Comparer<GameInfo>((a,b)=>a.IsSimilar(b))))
                    {
                        games.Add(game);
                    }
                }
            }
        }

        public GameInfo Match(Win32Form form)
        {
            foreach (var game in games)
            {
                if (game.IsMatch(form))
                {
                    return game;
                }
            }
            return null;
        }

        public static AllGamesInfo Load(string filename)
        {
            if (File.Exists(filename))
            {
                using (var fr = File.OpenRead(filename))
                {
                    return (AllGamesInfo)new XmlSerializer(typeof(AllGamesInfo)).Deserialize(fr);
                }
            }
            return null;
        }

        public void Save(string filename)
        {
            using (var wr = File.Create(filename))
            {
                new XmlSerializer(typeof(AllGamesInfo)).Serialize(wr, this);
            }
        }
    }

    class Immersifier
    {
        GameImmersifier current = null;
        AllGamesInfo agi = new AllGamesInfo();

        public List<GameInfo> Games
        {
            get
            {
                return agi.games;
            }
        }

        void SetGame(Win32Form form, GameInfo info)
        {
            if (current != null)
            {
                if (info != current.info)
                {
                    current.Deactivate();
                }
                else
                {
                    return;
                }
            }
            if (info == null)
            {
                current = null;
            }
            else
            {
                current = new GameImmersifier(form, info);
                current.Activate();
            }
        }

        public void Add(GameInfo info)
        {
            agi.Add(info);
        }

        public void Update(Win32Form form)
        {
            if (form != null)
            {
                SetGame(form, agi.Match(form));
            }
        }

        public void Load(string filename)
        {
            agi = AllGamesInfo.Load(filename) ?? agi;
        }

        public void Merge(string filename)
        {
            agi.Merge(AllGamesInfo.Load(filename));
        }

        public void Save(string filename)
        {
            agi.Save(filename);
        }
    }
}
