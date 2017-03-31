using Epxoxy.Behaviors;
using Epxoxy.Helpers;
using Epxoxy.Native;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Epxoxy.Controls
{
    public class WindowExt : DependencyObject
    {
        public static object GetTitleBar(DependencyObject obj)
        {
            return (object)obj.GetValue(TitleBarProperty);
        }

        public static void SetTitleBar(DependencyObject obj, object value)
        {
            obj.SetValue(TitleBarProperty, value);
        }

        // Using a DependencyProperty as the backing store for TitleBar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleBarProperty =
            DependencyProperty.RegisterAttached("TitleBar", typeof(object), typeof(WindowExt), new PropertyMetadata(null));



        public static Brush GetTitleBarColor(DependencyObject obj)
        {
            return (Brush)obj.GetValue(TitleBarColorProperty);
        }

        public static void SetTitleBarColor(DependencyObject obj, Brush value)
        {
            obj.SetValue(TitleBarColorProperty, value);
        }

        public static readonly DependencyProperty TitleBarColorProperty =
            DependencyProperty.RegisterAttached("TitleBarColor", typeof(Brush), typeof(WindowExt), new PropertyMetadata(null));

        public static bool GetDraggable(DependencyObject obj)
        {
            return (bool)obj.GetValue(DraggableProperty);
        }
        public static void SetDraggable(DependencyObject obj, bool value)
        {
            obj.SetValue(DraggableProperty, value);
        }

        public static bool GetMinimize(DependencyObject obj)
        {
            return (bool)obj.GetValue(MinimizeProperty);
        }
        public static void SetMinimize(DependencyObject obj, bool value)
        {
            obj.SetValue(MinimizeProperty, value);
        }

        public static bool GetMaximize(DependencyObject obj)
        {
            return (bool)obj.GetValue(MaximizeProperty);
        }
        public static void SetMaximize(DependencyObject obj, bool value)
        {
            obj.SetValue(MaximizeProperty, value);
        }

        public static bool GetCloseable(DependencyObject obj)
        {
            return (bool)obj.GetValue(CloseableProperty);
        }
        public static void SetCloseable(DependencyObject obj, bool value)
        {
            obj.SetValue(CloseableProperty, value);
        }

        public static readonly DependencyProperty DraggableProperty =
            DependencyProperty.RegisterAttached("Draggable", typeof(bool), typeof(WindowExt), new PropertyMetadata(false,
                (o, args) =>
                {
                    o.ApplyBehavior<WindowDraggableBehavior>();
                }));
        public static readonly DependencyProperty MinimizeProperty =
            DependencyProperty.RegisterAttached("Minimize", typeof(bool), typeof(WindowExt), new PropertyMetadata(false,
                (o, args) =>
                {
                    o.ApplyBehavior<WindowMinimizedBehavior>();
                }));
        public static readonly DependencyProperty MaximizeProperty =
            DependencyProperty.RegisterAttached("Maximize", typeof(bool), typeof(WindowExt), new PropertyMetadata(false, (o, args) =>
            {
                o.ApplyBehavior<WindowMaximizedBehavior>();
            }));
        public static readonly DependencyProperty CloseableProperty =
            DependencyProperty.RegisterAttached("Closeable", typeof(bool), typeof(WindowExt), new PropertyMetadata(false,
                (o, args) =>
                {
                    o.ApplyBehavior<WindowClosedBehavior>();
                }));

        public static void SetWindowBackground(Window window)
        {
            ////ensure win32 handle is created
            var handle = new WindowInteropHelper(window).EnsureHandle();
            ////set window background
            if (handle == null) return;
            var result = UnsafeNativeMethods.SetClassLong(handle, GCL_HBRBACKGROUND, UnsafeNativeMethods.GetSysColorBrush(COLOR_WINDOW));
        }

        public const int GCL_HBRBACKGROUND = -10;
        public const int COLOR_WINDOW = 5;
    }
}
