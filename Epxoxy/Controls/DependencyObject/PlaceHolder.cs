using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Epxoxy.Controls
{
    public class PlaceHolder : DependencyObject
    {
        public static object GetContent(DependencyObject obj)
        {
            return (object)obj.GetValue(ContentProperty);
        }
        public static void SetContent(DependencyObject obj, object value)
        {
            obj.SetValue(ContentProperty, value);
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.RegisterAttached("Content", typeof(object), typeof(PlaceHolder), new PropertyMetadata(null));



        public static Brush GetUnderlineBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(UnderlineBrushProperty);
        }

        public static void SetUnderlineBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(UnderlineBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for UnderlineBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnderlineBrushProperty =
            DependencyProperty.RegisterAttached("UnderlineBrush", typeof(Brush), typeof(PlaceHolder), new PropertyMetadata(Brushes.SkyBlue));

    }
}
