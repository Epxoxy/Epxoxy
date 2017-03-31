using System;
using System.Globalization;
using System.Windows.Data;

namespace Epxoxy.Converters
{
    public class ReversalConverter : IValueConverter
    {
        public bool Reversal { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return reversal(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return reversal(value);
        }

        private object reversal(object value)
        {
            if (value == null) return null;
            if (Reversal)
            {
                if (value is double) return (-1) * (double)value;
                else if (value is int) return (-1) * (int)value;
                else if (value is float) return (-1) * (float)value;
                else if (value is decimal) return (-1) * (decimal)value;
                else return value;
            }
            else return value;
        }
    }
}
