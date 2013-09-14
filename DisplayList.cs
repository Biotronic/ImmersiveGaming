using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImmersiveGaming
{
    public partial class DisplayList : UserControl
    {
        List<Tuple<Screen, Display>> monitors = new List<Tuple<Screen, Display>>();

        public DisplayList()
        {
            InitializeComponent();

            foreach (var screen in Screen.AllScreens)
            {
                var monitor = CreateDisplay(screen);
                monitors.Add(Tuple.Create(screen, monitor));
                Controls.Add(monitor);
                monitor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                monitor.Scroll += monitor_Scroll;
                monitor.Click += monitor_Click;
                monitor.DragDrop += monitor_DragDrop;
                monitor.DragEnter += monitor_DragEnter;
                monitor.DragOver += monitor_DragOver;
                monitor.DragLeave += monitor_DragLeave;
                monitor.DoubleClick += monitor_DoubleClick;
                monitor.MouseClick += monitor_MouseClick;
                monitor.MouseDoubleClick += monitor_MouseDoubleClick;
                monitor.MouseDown += monitor_MouseDown;
                monitor.MouseEnter += monitor_MouseEnter;
                monitor.MouseLeave += monitor_MouseLeave;
                monitor.MouseHover += monitor_MouseHover;
                monitor.MouseMove += monitor_MouseMove;
                monitor.MouseUp += monitor_MouseUp;
                monitor.MouseWheel += monitor_MouseWheel;
            }

            OnResize(EventArgs.Empty);
        }
        void monitor_Scroll(Object sender, ScrollEventArgs e)
        {
            OnMonitorScroll(new MonitorScrollEventArgs(sender as Display, e));
        }
        void monitor_Click(Object sender, EventArgs e)
        {
            OnMonitorClick(new MonitorEventArgs(sender as Display));
        }
        void monitor_DragDrop(Object sender, DragEventArgs e)
        {
            OnMonitorDragDrop(new MonitorDragEventArgs(sender as Display, e));
        }
        void monitor_DragEnter(Object sender, DragEventArgs e)
        {
            OnMonitorDragEnter(new MonitorDragEventArgs(sender as Display, e));
        }
        void monitor_DragOver(Object sender, DragEventArgs e)
        {
            OnMonitorDragOver(new MonitorDragEventArgs(sender as Display, e));
        }
        void monitor_DragLeave(Object sender, EventArgs e)
        {
            OnMonitorDragLeave(new MonitorEventArgs(sender as Display));
        }
        void monitor_DoubleClick(Object sender, EventArgs e)
        {
            OnMonitorDoubleClick(new MonitorEventArgs(sender as Display));
        }
        void monitor_MouseClick(Object sender, MouseEventArgs e)
        {
            OnMonitorMouseClick(new MonitorMouseEventArgs(sender as Display, e));
        }
        void monitor_MouseDoubleClick(Object sender, MouseEventArgs e)
        {
            OnMonitorMouseDoubleClick(new MonitorMouseEventArgs(sender as Display, e));
        }
        void monitor_MouseDown(Object sender, MouseEventArgs e)
        {
            OnMonitorMouseDown(new MonitorMouseEventArgs(sender as Display, e));
        }
        void monitor_MouseEnter(Object sender, EventArgs e)
        {
            OnMonitorMouseEnter(new MonitorEventArgs(sender as Display));
        }
        void monitor_MouseLeave(Object sender, EventArgs e)
        {
            OnMonitorMouseLeave(new MonitorEventArgs(sender as Display));
        }
        void monitor_MouseHover(Object sender, EventArgs e)
        {
            OnMonitorMouseHover(new MonitorEventArgs(sender as Display));
        }
        void monitor_MouseMove(Object sender, MouseEventArgs e)
        {
            OnMonitorMouseMove(new MonitorMouseEventArgs(sender as Display, e));
        }
        void monitor_MouseUp(Object sender, MouseEventArgs e)
        {
            OnMonitorMouseUp(new MonitorMouseEventArgs(sender as Display, e));
        }
        void monitor_MouseWheel(Object sender, MouseEventArgs e)
        {
            OnMonitorMouseWheel(new MonitorMouseEventArgs(sender as Display, e));
        }
        virtual protected void OnMonitorScroll(MonitorScrollEventArgs e)
        {
            if (MonitorScroll != null)
            {
                MonitorScroll(this, e);
            }
        }
        virtual protected void OnMonitorClick(MonitorEventArgs e)
        {
            if (MonitorClick != null)
            {
                MonitorClick(this, e);
            }
        }
        virtual protected void OnMonitorDragDrop(MonitorDragEventArgs e)
        {
            if (MonitorDragDrop != null)
            {
                MonitorDragDrop(this, e);
            }
        }
        virtual protected void OnMonitorDragEnter(MonitorDragEventArgs e)
        {
            if (MonitorDragEnter != null)
            {
                MonitorDragEnter(this, e);
            }
        }
        virtual protected void OnMonitorDragOver(MonitorDragEventArgs e)
        {
            if (MonitorDragOver != null)
            {
                MonitorDragOver(this, e);
            }
        }
        virtual protected void OnMonitorDragLeave(MonitorEventArgs e)
        {
            if (MonitorDragLeave != null)
            {
                MonitorDragLeave(this, e);
            }
        }
        virtual protected void OnMonitorDoubleClick(MonitorEventArgs e)
        {
            if (MonitorDoubleClick != null)
            {
                MonitorDoubleClick(this, e);
            }
        }
        virtual protected void OnMonitorMouseClick(MonitorMouseEventArgs e)
        {
            if (MonitorMouseClick != null)
            {
                MonitorMouseClick(this, e);
            }
        }
        virtual protected void OnMonitorMouseDoubleClick(MonitorMouseEventArgs e)
        {
            if (MonitorMouseDoubleClick != null)
            {
                MonitorMouseDoubleClick(this, e);
            }
        }
        virtual protected void OnMonitorMouseDown(MonitorMouseEventArgs e)
        {
            if (MonitorMouseDown != null)
            {
                MonitorMouseDown(this, e);
            }
        }
        virtual protected void OnMonitorMouseEnter(MonitorEventArgs e)
        {
            if (MonitorMouseEnter != null)
            {
                MonitorMouseEnter(this, e);
            }
        }
        virtual protected void OnMonitorMouseLeave(MonitorEventArgs e)
        {
            if (MonitorMouseLeave != null)
            {
                MonitorMouseLeave(this, e);
            }
        }
        virtual protected void OnMonitorMouseHover(MonitorEventArgs e)
        {
            if (MonitorMouseHover != null)
            {
                MonitorMouseHover(this, e);
            }
        }
        virtual protected void OnMonitorMouseMove(MonitorMouseEventArgs e)
        {
            if (MonitorMouseMove != null)
            {
                MonitorMouseMove(this, e);
            }
        }
        virtual protected void OnMonitorMouseUp(MonitorMouseEventArgs e)
        {
            if (MonitorMouseUp != null)
            {
                MonitorMouseUp(this, e);
            }
        }
        virtual protected void OnMonitorMouseWheel(MonitorMouseEventArgs e)
        {
            if (MonitorMouseWheel != null)
            {
                MonitorMouseWheel(this, e);
            }
        }

        virtual protected Display CreateDisplay(Screen screen)
        {
            return new Display();
        }

        public event ScrollEventHandler MonitorScroll;
        public event EventHandler MonitorClick;
        public event DragEventHandler MonitorDragDrop;
        public event DragEventHandler MonitorDragEnter;
        public event DragEventHandler MonitorDragOver;
        public event EventHandler MonitorDragLeave;
        public event EventHandler MonitorDoubleClick;
        public event MouseEventHandler MonitorMouseClick;
        public event MouseEventHandler MonitorMouseDoubleClick;
        public event MouseEventHandler MonitorMouseDown;
        public event EventHandler MonitorMouseEnter;
        public event EventHandler MonitorMouseLeave;
        public event EventHandler MonitorMouseHover;
        public event MouseEventHandler MonitorMouseMove;
        public event MouseEventHandler MonitorMouseUp;
        public event MouseEventHandler MonitorMouseWheel;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            var totalBounds = Screen.AllScreens.Aggregate(new Rectangle(0, 0, 0, 0), (a, b) => a.UnionBounds(b.Bounds));

            var scaleW = (double)Width / totalBounds.Width;
            var scaleH = (double)Height / totalBounds.Height;

            var scale = Math.Min(scaleW, scaleH);

            var offsetX = (int)((Width - totalBounds.Width * scale) / 2 - totalBounds.Left * scale);
            var offsetY = (int)((Height - totalBounds.Height * scale) / 2 - totalBounds.Top * scale);

            foreach (var item in monitors)
            {
                var screenBounds = item.Item1.Bounds;
                var monitor = item.Item2;
                monitor.Bounds = new Rectangle(
                    (int)(screenBounds.Left * scale) + offsetX,
                    (int)(screenBounds.Top * scale) + offsetY,
                    (int)(screenBounds.Right * scale) - (int)((screenBounds.Left - totalBounds.Left) * scale),
                    (int)(screenBounds.Bottom * scale) - (int)((screenBounds.Top - totalBounds.Top) * scale)
                    );
            }
        }
    }
    public class MonitorDragEventArgs : DragEventArgs
    {
        private Display monitor;
        public Display Monitor
        {
            get
            {
                return monitor;
            }
        }

        public MonitorDragEventArgs(Display monitor, DragEventArgs args)
            : base(args.Data, args.KeyState, args.X, args.Y,args.AllowedEffect, args.Effect)
        {
            this.monitor = monitor;
        }

        public MonitorDragEventArgs(Display monitor, IDataObject data, int keyState, int x, int y, DragDropEffects allowedEffect, DragDropEffects effect)
            : base(data, keyState, x,y,allowedEffect,effect)
        {
            this.monitor = monitor;
        }
    }
    public class MonitorEventArgs : EventArgs
    {
        private Display monitor;
        public Display Monitor
        {
            get
            {
                return monitor;
            }
        }
        public MonitorEventArgs(Display monitor)
        {
            this.monitor = monitor;
        }
    }
    public class MonitorMouseEventArgs : MouseEventArgs
    {
        private Display monitor;
        public Display Monitor
        {
            get
            {
                return monitor;
            }
        }

        public MonitorMouseEventArgs(Display monitor, MouseEventArgs args)
            : base(args.Button, args.Clicks, args.X, args.Y, args.Delta)
        {
            this.monitor = monitor;
        }

        public MonitorMouseEventArgs(Display monitor, MouseButtons button, int clicks, int x, int y, int delta)
            : base(button, clicks, x, y, delta)
        {
            this.monitor = monitor;
        }
    }
    public class MonitorScrollEventArgs : ScrollEventArgs
    {
        private Display monitor;
        public Display Monitor
        {
            get
            {
                return monitor;
            }
        }
        public MonitorScrollEventArgs(Display monitor, ScrollEventArgs args)
            : base(args.Type, args.OldValue, args.NewValue, args.ScrollOrientation)
        {
            this.monitor = monitor;
        }
        public MonitorScrollEventArgs(Display monitor, ScrollEventType type, int oldValue, int newValue, ScrollOrientation scroll)
            : base(type, oldValue, newValue, scroll)
        {
            this.monitor = monitor;
        }
    }

    static partial class Extensions
    {
        static public Rectangle OffsetBy(this Rectangle rect, int x, int y)
        {
            return new Rectangle(rect.Left + x, rect.Top + y, rect.Width, rect.Height);
        }
    }
}
