using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmersiveGaming
{
    class SysInfo
    {
        List<GameInfo> Games = new List<GameInfo>();
        Rectangle[] MonitorBounds;

        public SysInfo()
        {
            MonitorBounds = Screen.AllScreens.Select(a => a.Bounds).ToArray();
        }
    }
}
