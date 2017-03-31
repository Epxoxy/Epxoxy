using Epxoxy.Native;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Interop;

namespace Epxoxy.Behaviors
{
    public class WindowDraggableBehavior : Behavior<FrameworkElement>
    {
        private Window _window;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDown += TitleBarMouseDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseDown -= TitleBarMouseDown;
        }

        protected void TitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_window == null)
            {
                _window = Window.GetWindow(AssociatedObject);
            }
            if (_window == null) return;
            // if UseNoneWindowStyle = true no movement, no maximize please
            if (e.ChangedButton == MouseButton.Left)
            {
                var mPoint = Mouse.GetPosition(AssociatedObject);

                IntPtr windowHandle = new WindowInteropHelper(_window).Handle;
                UnsafeNativeMethods.ReleaseCapture();
                var wpfPoint = _window.PointToScreen(mPoint);
                var x = Convert.ToInt16(wpfPoint.X);
                var y = Convert.ToInt16(wpfPoint.Y);
                var lParam = (int)(uint)x | (y << 16);
                UnsafeNativeMethods.SendMessage(windowHandle, Constants.WM_NCLBUTTONDOWN, Constants.HT_CAPTION, lParam);
                var canResize = _window.ResizeMode == ResizeMode.CanResizeWithGrip || _window.ResizeMode == ResizeMode.CanResize;
                // we can maximize or restore the window if the title bar height is set (also if title bar is hidden)
                //var isMouseOnTitlebar = mPoint.Y <= TitleBarHeight && TitleBarHeight > 0;
                //if (e.ClickCount == 2 && canResize && isMouseOnTitlebar)
                if (e.ClickCount == 2 && canResize)
                {
                    if (_window.WindowState == WindowState.Maximized)
                    {
                        System.Windows.SystemCommands.RestoreWindow(_window);
                    }
                    else
                    {
                        System.Windows.SystemCommands.MaximizeWindow(_window);
                    }
                }
            }
        }

    }
}