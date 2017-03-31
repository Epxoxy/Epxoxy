using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Epxoxy.Behaviors
{
    public class NumberOnlyBehavior : Behavior<TextBox>
    {
        private TextBox Box { get; set; }
        protected override void OnAttached()
        {
            base.OnAttached();
            Box = this.AssociatedObject;
            InputMethod.SetIsInputMethodEnabled(Box, false);
            InputMethod.SetPreferredImeState(Box, InputMethodState.Off);
            Box.PreviewKeyDown += PreviewKeyDownHandler;
            Box.TextChanged += TextChangedHandler;
            Helpers.DebugHelper.debugWrite(this, "Attach");
        }

        private void TextChangedHandler(object sender, TextChangedEventArgs e)
        {
            TextChange[] changes = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(changes, 0);
            int offset = changes[0].Offset;
            if (changes[0].AddedLength > 0)
            {
                double num = 0;
                if (!double.TryParse(Box.Text, out num))
                {
                    Box.Text = Box.Text.Remove(offset, changes[0].AddedLength);
                    Box.Select(offset, 0);
                }
            }
        }

        private void PreviewKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Back
                || Keyboard.IsKeyDown(Key.LeftCtrl)
                || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                return;
            }
            else
            {
                //Debug optional
                if (e.Key == Key.Decimal)
                    Helpers.DebugHelper.debugWrite(this, "Decimal");

                if ((e.Key > Key.D9 || e.Key < Key.D0)
                    && (e.Key > Key.NumPad9 || e.Key < Key.NumPad0))
                {
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
                        default: e.Handled = true; break;
                    }
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Box.PreviewKeyDown -= PreviewKeyDownHandler;
            Box.TextChanged -= TextChangedHandler;
            Helpers.DebugHelper.debugWrite(this, "OnDetaching");
        }
    }
}
