using Epxoxy.Native;
using System;
using System.Windows;
using System.Windows.Interop;

namespace Epxoxy.Controls
{
    public class WindowHelper
    {
        private const int GwlExstyle = -20;
        private const int SwpFramechanged = 0x0020;
        private const int SwpNomove = 0x0002;
        private const int SwpNosize = 0x0001;
        private const int SwpNozorder = 0x0004;
        private const int WsExDlgmodalframe = 0x0001;

        public static readonly DependencyProperty ShowIconProperty =
          DependencyProperty.RegisterAttached(
            "ShowIcon",
            typeof(bool),
            typeof(WindowHelper),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnShowIconChanged)));
        
        public static Boolean GetShowIcon(UIElement element)
        {
            return (Boolean)element.GetValue(ShowIconProperty);
        }

        public static void SetShowIcon(UIElement element, Boolean value)
        {
            element.SetValue(ShowIconProperty, value);
        }

        private static void OnShowIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if(window != null)
            {
                if (!(bool)e.NewValue)
                {
                    if (window.IsInitialized)
                        RemoveIcon(window);
                    else
                        window.SourceInitialized += delegate { RemoveIcon(window); };
                }
            }
        }

        private static void RemoveIcon(Window window)
        {
            // Get this window's handle
            var hwnd = new WindowInteropHelper(window).Handle;

            // Change the extended window style to not show a window icon
            int extendedStyle = UnsafeNativeMethods.GetWindowLong(hwnd, GwlExstyle);
            UnsafeNativeMethods.SetWindowLong(hwnd, GwlExstyle, extendedStyle | WsExDlgmodalframe);

            // Update the window's non-client area to reflect the changes
            UnsafeNativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SwpNomove |
              SwpNosize | SwpNozorder | SwpFramechanged);
        }

    }
}
