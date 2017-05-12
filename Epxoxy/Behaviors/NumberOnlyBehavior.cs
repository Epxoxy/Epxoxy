using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Epxoxy.Behaviors
{
    public class NumberOnlyBehavior : Behavior<TextBox>
    {
        public bool AcceptNegative
        {
            get { return (bool)GetValue(AcceptNegativeProperty); }
            set { SetValue(AcceptNegativeProperty, value); }
        }
        public static readonly DependencyProperty AcceptNegativeProperty =
            DependencyProperty.Register("AcceptNegative", typeof(bool), typeof(NumberOnlyBehavior), 
                new PropertyMetadata(false));
        
        public bool AcceptDecimal
        {
            get { return (bool)GetValue(AcceptDecimalProperty); }
            set { SetValue(AcceptDecimalProperty, value); }
        }
        public static readonly DependencyProperty AcceptDecimalProperty =
            DependencyProperty.Register("AcceptDecimal", typeof(bool), typeof(NumberOnlyBehavior), 
                new PropertyMetadata(false));

        private static void OnAcceptDecimalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as NumberOnlyBehavior;
            if(behavior != null)
            {
                behavior.acceptDecimal = (bool)e.NewValue;
            }
        }

        private static void OnAcceptNegativeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as NumberOnlyBehavior;
            if (behavior != null)
            {
                behavior.acceptNegative = (bool)e.NewValue;
            }
        }

        private TextBox box;
        private bool acceptNegative;
        private bool acceptDecimal;
        protected override void OnAttached()
        {
            base.OnAttached();
            box = this.AssociatedObject;
            //Do something for input method
            InputMethod.SetIsInputMethodEnabled(box, false);
            InputMethod.SetPreferredImeState(box, InputMethodState.Off);
            //Add event handler
            DataObject.AddPastingHandler(box, OnPastingEvent);
            box.PreviewKeyDown += PreviewKeyDownHandler;
            box.TextChanged += TextChangedHandler;
            RoutedEventHandler unloaded = null;
            unloaded = (obj, args) =>
            {
                box.Unloaded -= unloaded;
                DataObject.RemovePastingHandler(box, OnPastingEvent);
            };
            box.Unloaded += unloaded;
            Helpers.DebugHelper.debugWrite(this, "Attach");
        }

        private void OnPastingEvent(object sender, DataObjectPastingEventArgs e)
        {
            double value;
            if (!double.TryParse((string)e.DataObject.GetData(typeof(string)), out value))
            {
                e.CancelCommand();
            }
        }

        private void TextChangedHandler(object sender, TextChangedEventArgs e)
        {
            TextChange[] changes = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(changes, 0);
            int offset = changes[0].Offset;
            if (changes[0].AddedLength > 0)
            {
                double num = 0;
                if (!double.TryParse(box.Text, out num))
                {
                    box.Text = box.Text.Remove(offset, changes[0].AddedLength);
                    box.Select(offset, 0);
                }
            }
        }

        private void PreviewKeyDownHandler(object sender, KeyEventArgs e)
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
                            if ((Keyboard.IsKeyDown(Key.LeftCtrl) 
                                || Keyboard.IsKeyDown(Key.RightShift))
                                || !acceptNegative)
                                e.Handled = true;
                            break;
                        case Key.Decimal:
                        case Key.OemPeriod:
                            if (!acceptDecimal || box.Text.Contains("."))
                                e.Handled = true;
                            break;
                        default: e.Handled = true; break;
                    }
                }
            }
        }
        
        protected override void OnDetaching()
        {
            base.OnDetaching();
            box.PreviewKeyDown -= PreviewKeyDownHandler;
            box.TextChanged -= TextChangedHandler;
            Helpers.DebugHelper.debugWrite(this, "OnDetaching");
        }
    }
}
