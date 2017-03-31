using System.Windows;
using System.Windows.Media;

namespace Epxoxy.Controls
{
    public class ColorSlider : System.Windows.Controls.Slider
    {
        static ColorSlider()
        {
            FromColorProperty = DependencyProperty.Register("FromColor", typeof(Color), typeof(ColorSlider), new PropertyMetadata(null));
            ToColorProperty = DependencyProperty.Register("ToColor", typeof(Color), typeof(ColorSlider), new PropertyMetadata(null));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSlider), new FrameworkPropertyMetadata(typeof(ColorSlider)));
            MaximumProperty.OverrideMetadata(typeof(ColorSlider), new FrameworkPropertyMetadata(255d));
            MinimumProperty.OverrideMetadata(typeof(ColorSlider), new FrameworkPropertyMetadata(0d));
            IsMoveToPointEnabledProperty.OverrideMetadata(typeof(ColorSlider), new FrameworkPropertyMetadata(true));
        }

        public Color FromColor
        {
            get { return (Color)GetValue(FromColorProperty); }
            set { SetValue(FromColorProperty, value); }
        }
        public static readonly DependencyProperty FromColorProperty;

        public Color ToColor
        {
            get { return (Color)GetValue(ToColorProperty); }
            set { SetValue(ToColorProperty, value); }
        }
        public static readonly DependencyProperty ToColorProperty;

    }
}
