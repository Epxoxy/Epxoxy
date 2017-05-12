using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Epxoxy.Converters
{
    public class BooleanSelectorConverter : IValueConverter
    {
        public object TrueValue { get; set; }
        public object FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool)
            {
                return ((bool)value) ? TrueValue : FalseValue;
            }
            return FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null == value ? false : (value == TrueValue);
        }
    }
}
