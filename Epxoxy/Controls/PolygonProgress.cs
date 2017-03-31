using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Epxoxy.Controls
{
    [TemplatePart(Name = "PART_Rect", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Track", Type = typeof(FrameworkElement))]
    public class PolygonProgress : RangeBase
    {
        public enum ProgressStates
        {
            GreenState,
            YellowState,
            RedState
        }

        static PolygonProgress()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PolygonProgress), new FrameworkPropertyMetadata(typeof(PolygonProgress)));
            RangeBase.MaximumProperty.OverrideMetadata(typeof(PolygonProgress), new FrameworkPropertyMetadata(100.0));
        }

        public PolygonProgress()
        {

        }

        public double RedRadio
        {
            get { return (double)GetValue(RedRadioProperty); }
            set { SetValue(RedRadioProperty, value); }
        }
        public double YellowRadio
        {
            get { return (double)GetValue(YellowRadioProperty); }
            set { SetValue(YellowRadioProperty, value); }
        }
        public ProgressStates State
        {
            get { return (ProgressStates)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        public System.Windows.Media.Geometry AroundData
        {
            get { return (System.Windows.Media.Geometry)GetValue(AroundDataProperty); }
            set { SetValue(AroundDataProperty, value); }
        }
        public Brush FontForeground
        {
            get { return (Brush)GetValue(FontForegroundProperty); }
            set { SetValue(FontForegroundProperty, value); }
        }
        public double BarHeight
        {
            get { return (double)GetValue(BarHeightProperty); }
            set { SetValue(BarHeightProperty, value); }
        }
        public string Describe
        {
            get { return (string)GetValue(DescribeProperty); }
            set { SetValue(DescribeProperty, value); }
        }
        public double DescribeWidth
        {
            get { return (double)GetValue(DescribeWidthProperty); }
            set { SetValue(DescribeWidthProperty, value); }
        }
        public double Stroke
        {
            get { return (double)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(ProgressStates), typeof(PolygonProgress), new PropertyMetadata(ProgressStates.RedState));
        public static readonly DependencyProperty RedRadioProperty =
            DependencyProperty.Register("RedRadio", typeof(double), typeof(PolygonProgress), new PropertyMetadata(0.25d));
        public static readonly DependencyProperty YellowRadioProperty =
            DependencyProperty.Register("YellowRadio", typeof(double), typeof(PolygonProgress), new PropertyMetadata(0.5d));
        public static readonly DependencyProperty AroundDataProperty =
            DependencyProperty.Register("AroundData", typeof(System.Windows.Media.Geometry), typeof(PolygonProgress), new PropertyMetadata(null));
        public static readonly DependencyProperty BarHeightProperty =
            DependencyProperty.Register("BarHeight", typeof(double), typeof(PolygonProgress), new PropertyMetadata(12d));
        public static readonly DependencyProperty FontForegroundProperty =
            DependencyProperty.Register("FontForeground", typeof(Brush), typeof(PolygonProgress), new PropertyMetadata(null));
        public static readonly DependencyProperty DescribeProperty =
            DependencyProperty.Register("Describe", typeof(string), typeof(PolygonProgress), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty DescribeWidthProperty =
            DependencyProperty.Register("DescribeWidth", typeof(double), typeof(PolygonProgress), new PropertyMetadata(60d));
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(double), typeof(PolygonProgress), new PropertyMetadata(2d));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            rect = GetTemplateChild("PART_Rect") as FrameworkElement;
            track = GetTemplateChild("PART_Track") as FrameworkElement;
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            setRectWidth();
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            setRectWidth();
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            setRectWidth();
        }

        private void setRectWidth()
        {
            if (rect != null && track != null)
            {
                double min = Minimum;
                double max = Maximum;
                double val = Value;
                double height = track.ActualHeight;
                double width = track.ActualWidth;

                double percent = max <= min ? 1.0 : (val - min) / (max - min);
                rect.Width = percent * width;

                verifyStateChanged(percent > YellowRadio ? GreenState : (percent > RedRadio ? YellowState : RedState));
            }
        }

        private void verifyStateChanged(string stateName)
        {
            VisualStateManager.GoToState(this, stateName, true);
        }

        private FrameworkElement rect;
        private FrameworkElement track;
        private const string GreenState = "GreenState";
        private const string RedState = "RedState";
        private const string YellowState = "YellowState";
    }
}
