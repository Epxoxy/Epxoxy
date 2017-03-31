using System.Windows;

namespace Epxoxy.Controls
{
    public class ToggleSwitch : System.Windows.Controls.Primitives.ToggleButton
    {

        #region Constructor

        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }

        #endregion

        #region Properties

        public bool EnabledStateTag
        {
            get { return (bool)GetValue(EnabledStateTagProperty); }
            set { SetValue(EnabledStateTagProperty, value); }
        }
        public string OnText
        {
            get { return (string)GetValue(OnTextProperty); }
            set { SetValue(OnTextProperty, value); }
        }
        public string OffText
        {
            get { return (string)GetValue(OffTextProperty); }
            set { SetValue(OffTextProperty, value); }
        }

        public static readonly DependencyProperty EnabledStateTagProperty =
            DependencyProperty.Register("EnabledStateTag", typeof(bool), typeof(ToggleSwitch), new PropertyMetadata(true));
        public static readonly DependencyProperty OnTextProperty =
            DependencyProperty.Register("OnText", typeof(string), typeof(ToggleSwitch), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty OffTextProperty =
            DependencyProperty.Register("OffText", typeof(string), typeof(ToggleSwitch), new PropertyMetadata(string.Empty));

        #endregion

    }
}
