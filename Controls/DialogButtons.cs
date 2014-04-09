using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Biotronic;

namespace Controls
{
    public partial class DialogButtons : UserControl
    {
        private HorizontalAlignment _alignment = HorizontalAlignment.Center;
        private Button[] _buttonArray = new Button[0];
        private int _buttonWidth = 88;
        private int _buttonSpacing = 4;
        private MessageBoxButtons _buttons = MessageBoxButtons.OKCancel;

        [Flags]
        enum ButtonNames
        {
            Yes = 1,
            No = 2,
            OK = 4,
            Abort = 8,
            Retry = 16,
            Ignore = 32,
            Cancel = 64,
        }

        public DialogButtons()
        {
            InitializeComponent();
            Dock = DockStyle.Bottom;
            UpdateButtons();
        }

        private void CreateButtons(ButtonNames buttons)
        {
            var v = Enums.Values<ButtonNames>().Select(a => ((buttons & a) == a) ? a.ToString() : null).Where(a => !String.IsNullOrEmpty(a));
            CreateButtonsFromNames(Enums.Values<ButtonNames>().Select(a => ((buttons & a) == a) ? a.ToString() : null).Where(a => !String.IsNullOrEmpty(a)));
        }

        private void CreateButtonsFromNames(IEnumerable<string> names)
        {
            foreach (var btn in _buttonArray)
            {
                btn.Click -= btn_Click;
                Controls.Remove(btn);
            }
            _buttonArray = names.Select((name, i) => {
                var btn = CreateButton(name);
                btn.TabIndex = i;
                Controls.Add(btn);
                return btn;
            }).ToArray();
        }

        protected virtual Button CreateButton(string name)
        {
            var btn = new Button();
            btn.Text = name;
            btn.DialogResult = Enums.Parse<DialogResult>(name);
            btn.Click += btn_Click;
            btn.Size = new Size(88, 26);
            return btn;
        }

        void btn_Click(object sender, EventArgs e)
        {
            OnButtonClick(new DialogResultEventArgs(((Button)sender).DialogResult));
        }

        public MessageBoxButtons Buttons
        {
            get
            {
                return _buttons;
            }
            set
            {
                if (value != _buttons)
                {
                    _buttons = value;
                    UpdateButtons();
                }
            }
        }

        public event EventHandler<DialogResultEventArgs> ButtonClick;

        protected virtual void OnButtonClick(DialogResultEventArgs args)
        {
            if (ButtonClick != null)
            {
                ButtonClick(this, args);
            }
        }

        private IEnumerable<Button> VisibleButtons
        {
            get
            {
                return _buttonArray.Where(a => a.Visible);
            }
        }

        private void SetButtonVisibilities()
        {
            switch (_buttons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    CreateButtons(ButtonNames.Abort | ButtonNames.Retry | ButtonNames.Ignore);
                    break;
                case MessageBoxButtons.OK:
                    CreateButtons(ButtonNames.OK);
                    break;
                case MessageBoxButtons.OKCancel:
                    CreateButtons(ButtonNames.OK | ButtonNames.Cancel);
                    break;
                case MessageBoxButtons.RetryCancel:
                    CreateButtons(ButtonNames.Retry | ButtonNames.Cancel);
                    break;
                case MessageBoxButtons.YesNo:
                    CreateButtons(ButtonNames.Yes | ButtonNames.No);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    CreateButtons(ButtonNames.Yes | ButtonNames.No | ButtonNames.Cancel);
                    break;
            }
        }

        private void UpdateButtons()
        {
            SetButtonVisibilities();

            int numButtons = VisibleButtons.Count();
            int totalButtonWidth = (_buttonWidth + _buttonSpacing) * numButtons - _buttonSpacing;

            int buttonOffset = 0;
            AnchorStyles buttonAnchors = AnchorStyles.None;
            switch (_alignment)
            {
                case HorizontalAlignment.Center:
                    buttonAnchors = AnchorStyles.Bottom;
                    buttonOffset = (Width - totalButtonWidth) / 2;
                    break;
                case HorizontalAlignment.Left:
                    buttonAnchors = AnchorStyles.Bottom | AnchorStyles.Left;
                    buttonOffset = _buttonSpacing;
                    break;
                case HorizontalAlignment.Right:
                    buttonAnchors = AnchorStyles.Bottom | AnchorStyles.Right;
                    buttonOffset = Width - totalButtonWidth - _buttonSpacing;
                    break;
                default:
                    Debug.Assert(false, "Alignment value for DialogButton set to invalid value. This should never happpen.");
                    break;
            }
            int buttonIdx = 0;

            foreach (var btn in VisibleButtons)
            {
                btn.Anchor = buttonAnchors;
                btn.Left = buttonOffset + buttonIdx * (_buttonWidth + _buttonSpacing);
                buttonIdx++;
            }
        }
    }

    public class DialogResultEventArgs : EventArgs
    {
        private DialogResult value;

        public DialogResult Result
        {
            get
            {
                return value;
            }
        }

        public DialogResultEventArgs(DialogResult result)
        {
            value = result;
        }
    }
}
