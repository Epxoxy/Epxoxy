using System.Windows;
using System.Windows.Controls;

namespace Epxoxy.Behaviors
{
    public class DatePickerExtension
    {
        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }
        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(DatePickerExtension), new PropertyMetadata(""));

        public static bool GetUseCustomPlaceholder(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseCustomPlaceholderProperty);
        }
        public static void SetUseCustomPlaceholder(DependencyObject obj, bool value)
        {
            obj.SetValue(UseCustomPlaceholderProperty, value);
        }
        public static readonly DependencyProperty UseCustomPlaceholderProperty =
            DependencyProperty.RegisterAttached("UseCustomPlaceholder", typeof(bool), typeof(DatePickerExtension), new PropertyMetadata(false, OnUseCustomPlaceholderChanged));

        public static void OnUseCustomPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var datePicker = d as DatePicker;
            if (datePicker != null)
            {
                if ((bool)e.NewValue)
                {
                    datePicker.Loaded += OnDatePickerLoaded;
                }
                else
                {
                    datePicker.Loaded -= OnDatePickerLoaded;
                }
            }
        }
        
        private static void OnDatePickerLoaded(object sender, RoutedEventArgs e)
        {
            var dp = sender as DatePicker;
            if (dp == null) return;

            var tb = Helpers.VisualTreeExtension.GetChildOfType<System.Windows.Controls.Primitives.DatePickerTextBox>(dp);
            if (tb == null) return;

            var wm = tb.Template.FindName("PART_Watermark", tb) as ContentControl;
            if (wm == null) return;

            wm.Content = GetPlaceholder(dp); ;
        }
    }
}
