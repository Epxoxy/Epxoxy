using System;
using System.Globalization;
using System.Windows.Data;

namespace Epxoxy.Converters
{
    [ValueConversion(typeof(double), typeof(int))]
    public class DoubleToInteger : IValueConverter
    {
        public bool UseRound { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double source = System.Convert.ToDouble(value);
            if (UseRound) return Math.Round(source, 0);
            return System.Convert.ToInt32(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
