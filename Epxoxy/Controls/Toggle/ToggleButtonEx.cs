using System.Windows;
using System.Windows.Controls.Primitives;

namespace Epxoxy.Controls
{
    public class ToggleButtonEx : ToggleButton
    {
        static ToggleButtonEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleButtonEx), new FrameworkPropertyMetadata(typeof(ToggleButtonEx)));
        }

        public object OnContent
        {
            get { return (object)GetValue(OnContentProperty); }
            set { SetValue(OnContentProperty, value); }
        }
        public static readonly DependencyProperty OnContentProperty =
            DependencyProperty.Register("OnContent", typeof(object), typeof(ToggleButtonEx), new PropertyMetadata(null));

        public object OffContent
        {
            get { return (object)GetValue(OffContentProperty); }
            set { SetValue(OffContentProperty, value); }
        }
        public static readonly DependencyProperty OffContentProperty =
            DependencyProperty.Register("OffContent", typeof(object), typeof(ToggleButtonEx), new PropertyMetadata(null));

    }
}