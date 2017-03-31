using System.Windows;
using System.Windows.Interactivity;

namespace Epxoxy.Behaviors
{
    public class WindowMinimizedBehavior : Behavior<System.Windows.Controls.Primitives.ButtonBase>
    {
        private Window _window;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += OnButtonClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= OnButtonClick;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (_window == null)
            {
                _window = Window.GetWindow(AssociatedObject);
            }

            if (_window == null)
            {
                return;
            }

            SystemCommands.MinimizeWindow(_window);
        }
    }
}