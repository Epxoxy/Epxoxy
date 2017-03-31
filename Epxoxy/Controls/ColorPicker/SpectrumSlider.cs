using System.Windows;
using System.Windows.Media;
using Epxoxy.Helpers;

namespace Epxoxy.Controls
{
    public class SpectrumSlider : System.Windows.Controls.Slider
    {
        public double Hue
        {
            get { return (double)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }
        public static readonly DependencyProperty HueProperty;

        static SpectrumSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SpectrumSlider), new FrameworkPropertyMetadata(typeof(SpectrumSlider)));
            HueProperty = DependencyProperty.Register("Hue", typeof(double), typeof(SpectrumSlider), new PropertyMetadata(0d, OnHuePropertyChanged));
            OrientationProperty.OverrideMetadata(typeof(SpectrumSlider), new FrameworkPropertyMetadata(System.Windows.Controls.Orientation.Vertical));
            MaximumProperty.OverrideMetadata(typeof(SpectrumSlider), new FrameworkPropertyMetadata(360d));
            MinimumProperty.OverrideMetadata(typeof(SpectrumSlider), new FrameworkPropertyMetadata(0d));
            IsMoveToPointEnabledProperty.OverrideMetadata(typeof(SpectrumSlider), new FrameworkPropertyMetadata(true));
        }
        public SpectrumSlider()
        {
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnLoaded;
            InitBackground();
        }

        private static void OnHuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var spectrumSlider = d as SpectrumSlider;
            if (spectrumSlider != null)
            {
                var hue = (double)e.NewValue;
                spectrumSlider.Value = hue;
            }
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            Hue = newValue;
        }

        private void InitBackground()
        {
            var backgroundBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0.5, 1),
                EndPoint = new Point(0.5, 0)
            };

            const int SpectrumColorCount = 30;

            Color[] spectrumColors = ColorHelper.GetSpectrumColors(SpectrumColorCount);
            for (int i = 0; i < SpectrumColorCount; ++i)
            {
                double offset = i * 1.0 / SpectrumColorCount;
                var gradientStop = new GradientStop(spectrumColors[i], offset);
                backgroundBrush.GradientStops.Add(gradientStop);
            }

            this.Background = backgroundBrush;
        }
    }

}
