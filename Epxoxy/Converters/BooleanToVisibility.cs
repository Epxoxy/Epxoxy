using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Epxoxy.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool sourceValue = System.Convert.ToBoolean(value);
            if (sourceValue) return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility sourceValue = (Visibility)value;
            if (sourceValue == Visibility.Visible) return true;
            else return false;
        }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToCollapsed : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool sourceValue = System.Convert.ToBoolean(value);
            if (sourceValue) return Visibility.Collapsed;
            else return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility sourceValue = (Visibility)value;
            if (sourceValue == Visibility.Visible) return false;
            else return true;
        }
    }
}
