using System;
using System.Globalization;
using System.Windows.Data;

namespace Epxoxy.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    public class NegateDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;
            if (value is double) return -System.Convert.ToDouble(value);
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;
            if (value is double) return -System.Convert.ToDouble(value);
            return -System.Convert.ToDouble(value);
        }
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class NegateBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            if (value is bool) return !(bool)value;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            if (value is bool) return !(bool)value;
            return false;
        }
    }
}
