using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Epxoxy.Controls
{
    public class NumericTextBox : TextBox
    {
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(NumericTextBox),
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValueChanged));

        public bool AcceptNegative
        {
            get { return (bool)GetValue(AcceptNegativeProperty); }
            set { SetValue(AcceptNegativeProperty, value); }
        }
        public static readonly DependencyProperty AcceptNegativeProperty =
            DependencyProperty.Register("AcceptNegative", typeof(bool), typeof(NumericTextBox),
                new PropertyMetadata(false, OnAcceptNegativeChanged));

        public bool AcceptDecimal
        {
            get { return (bool)GetValue(AcceptDecimalProperty); }
            set { SetValue(AcceptDecimalProperty, value); }
        }
        public static readonly DependencyProperty AcceptDecimalProperty =
            DependencyProperty.Register("AcceptDecimal", typeof(bool), typeof(NumericTextBox),
                new PropertyMetadata(false, OnAcceptDecimalChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as NumericTextBox;
            if (box != null) box.OnNumberChange((double)e.NewValue);
        }

        private static void OnAcceptNegativeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as NumericTextBox;
            if (box != null) box.OnAcceptNegativeChanged((bool)e.NewValue);
        }

        private static void OnAcceptDecimalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as NumericTextBox;
            if (box != null) box.OnAcceptDecimalChanged((bool)e.NewValue);
        }

        public NumericTextBox()
        {
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnLoaded;
            InputMethod.SetIsInputMethodEnabled(this, false);
            InputMethod.SetPreferredImeState(this, InputMethodState.Off);
            DataObject.AddPastingHandler(this, OnPastingEvent);
            RoutedEventHandler unloaded = null;
            unloaded = (obj, args) =>
            {
                this.Unloaded -= unloaded;
                DataObject.RemovePastingHandler(this, OnPastingEvent);
            };
            this.Unloaded += unloaded;
        }

        private void OnPastingEvent(object sender, DataObjectPastingEventArgs e)
        {
            double value;
            if (!double.TryParse((string)e.DataObject.GetData(typeof(string)), out value))
            {
                e.CancelCommand();
            }
        }

        private bool isInternalSync;
        private double newValueCache;
        private void OnNumberChange(double newvalue)
        {
            System.Diagnostics.Debug.WriteLine("OnNumberChange " + newvalue);
            newValueCache = newvalue;
            if (isInternalSync) return;
            string number = newvalue.ToString();
            if (this.Text != number)
            {
                isInternalSync = true;
                this.Text = number;
                this.Select(this.Text.Length, 0);
                isInternalSync = false;
            }
        }

        private bool acceptNegative;
        private void OnAcceptNegativeChanged(bool isAccepted)
        {
            this.acceptNegative = isAccepted;
            if (!isAccepted && newValueCache < 0)
                this.Value = -newValueCache;
        }

        private bool acceptDecimal;
        private void OnAcceptDecimalChanged(bool isAccepted)
        {
            this.acceptDecimal = isAccepted;
            if (!isAccepted && newValueCache != (int)newValueCache)
            {
                this.Value = (int)newValueCache;
            }
        }

        private bool isShiftHolded()
        {
            return (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightShift));
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Back
                || Keyboard.IsKeyDown(Key.LeftCtrl)
                || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                return;
            }
            else
            {
                if ((e.Key > Key.D9 || e.Key < Key.D0)
                    && (e.Key > Key.NumPad9 || e.Key < Key.NumPad0))
                {
                    System.Diagnostics.Debug.WriteLine(e.Key);
                    switch (e.Key)
                    {
                        case Key.Left:
                        case Key.Right:
                        case Key.Up:
                        case Key.Down:
                        case Key.PageUp:
                        case Key.PageDown:
                        case Key.Home:
                        case Key.End:
                        case Key.Insert:
                        case Key.Delete:
                        case Key.Tab:
                        case Key.LeftShift:
                        case Key.RightShift: break;
                        case Key.OemMinus:
                        case Key.Subtract:
                            if (isShiftHolded()
                                || !acceptNegative
                                || newValueCache < 0)
                                e.Handled = true;
                            break;
                        case Key.Decimal:
                        case Key.OemPeriod:
                            if (!acceptDecimal || Text.Contains("."))
                                e.Handled = true;
                            break;
                        default: e.Handled = true; break;
                    }
                }
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!isInternalSync)
            {
                string text = Text;
                if (text.Length == 1)
                {
                    if (doForSpecialKey(text[0])) return;
                }
                else if (text.Length > 1 && text[text.Length - 1] == '.')
                {
                    if(text.Length == 2)
                    text = text.Remove(text.Length - 1, 1);
                }
                TextChange[] changes = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(changes, 0);
                int offset = changes[0].Offset;
                if (changes[0].AddedLength > 0)
                {
                    double value;
                    if (!double.TryParse(text, out value))
                    {
                        System.Diagnostics.Debug.WriteLine("TryParse fail : " + text);
                        if (text.Length >= offset + changes[0].AddedLength)
                        {
                            text = text.Remove(offset, changes[0].AddedLength);
                        }
                        this.Text = text;
                        this.Select(offset, 0);
                    }
                    else updateNumberWithIgnoreText(value);
                }
                else
                {
                    double value;
                    if (double.TryParse(text, out value))
                    {
                        updateNumberWithIgnoreText(value);
                    }
                }
            }
            base.OnTextChanged(e);
        }

        private void updateNumberWithIgnoreText(double value)
        {
            if (this.Value != value)
            {
                isInternalSync = !(!acceptNegative && value < 0) || (!acceptDecimal && value != (int)value);
                double old = this.Value;
                this.Value = value;
                isInternalSync = false;
                bool hasChange = this.Value != old;
                if (!hasChange || (hasChange && this.Value != value))
                {
                    OnNumberChange(this.Value);
                }
            }
        }

        private bool doForSpecialKey(char c)
        {
            if (c == '-')
            {
                isInternalSync = true;
                Value = 0;
                isInternalSync = false;
            }
            else if (c == '.')
            {
                isInternalSync = true;
                Value = 0;
                Text = Text.Insert(0, "0");
                this.Select(this.Text.Length, 0);
                isInternalSync = false;
            }
            else return false;
            return true;
        }

    }
}
