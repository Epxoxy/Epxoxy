using System.Windows;

namespace Epxoxy.Controls
{
    public class ButtonBaseEx : DependencyObject
    {
        public static double GetCornerRadius(DependencyObject obj)
        {
            return (double)obj.GetValue(CornerRadiusProperty);
        }
        public static void SetCornerRadius(DependencyObject obj, double value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(double), typeof(ButtonBaseEx), new PropertyMetadata(0d));

        public static object GetIcon(DependencyObject obj)
        {
            return (object)obj.GetValue(IconProperty);
        }
        public static void SetIcon(DependencyObject obj, object value)
        {
            obj.SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached("Icon", typeof(object), typeof(ButtonBaseEx), new PropertyMetadata(null));

        public static Style GetIconStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(IconStyleProperty);
        }
        public static void SetIconStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(IconStyleProperty, value);
        }

        public static readonly DependencyProperty IconStyleProperty =
            DependencyProperty.RegisterAttached("IconStyle", typeof(Style), typeof(ButtonBaseEx), new PropertyMetadata(null));


    }
}
