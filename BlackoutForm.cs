using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyrimMouseHider
{
    public partial class BlackoutForm : Form
    {
        public BlackoutForm(Screen screen)
        {
            InitializeComponent();
            Bounds = screen.Bounds;
            new Win32Form(this.Handle).WindowExStyle |= User32Types.WindowExStyles.ToolWindow;
        }
    }
}
