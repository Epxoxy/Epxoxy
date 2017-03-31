using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Epxoxy.Converters
{
    public class BackgroundToForegroundConverter : IValueConverter
    {
        public Brush ForegroundOnLight { get; set; }
        public Brush ForegroundOnDark { get; set; }
        public Brush FailBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color? color = null;
            if (value is SolidColorBrush)
            {
                SolidColorBrush scb = value as SolidColorBrush;
                color = scb.Color;
            }
            else if (value is LinearGradientBrush)
            {
                LinearGradientBrush brush = value as LinearGradientBrush;
                if (brush.GradientStops != null && brush.GradientStops.Count > 0)
                {
                    color = brush.GradientStops[(int)(brush.GradientStops.Count / 2)].Color;
                }
            }
            else if (value is RadialGradientBrush)
            {
                RadialGradientBrush brush = value as RadialGradientBrush;
                if (brush.GradientStops != null && brush.GradientStops.Count > 0)
                {
                    color = brush.GradientStops[(int)(brush.GradientStops.Count / 2)].Color;
                }
            }
            else if (value is Color)
            {
                color = (Color)value;
            }
            if (color.HasValue)
            {
                int brightness = GetBrightness(color.Value);
                return brightness < 130 ? ForegroundOnLight : ForegroundOnDark;
            }
            return FailBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private int GetBrightness(Color c)
        {
            if (c.A < 130) return 255 - c.A;
            return (int)Math.Sqrt(c.R * c.R * .241 + c.G * c.G * .691 + c.B * c.B * .068);
        }
    }
}
