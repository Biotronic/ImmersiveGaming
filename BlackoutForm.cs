using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmersiveGaming
{
    public partial class BlackoutForm : Form
    {
        public BlackoutForm(Screen screen)
        {
            InitializeComponent();
            Bounds = screen.Bounds;
            Win32Form.Fetch(this.Handle).WindowExStyle |= User32Types.WindowExStyles.ToolWindow;
        }
    }
}
