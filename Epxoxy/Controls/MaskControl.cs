using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Epxoxy.Controls
{
    public class MaskControl : System.Windows.Controls.ContentControl
    {
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(MaskControl), new PropertyMetadata(0d));

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(MaskControl), new PropertyMetadata(null));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(MaskControl),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsOpenChanged));

        public Brush MaskFill
        {
            get { return (Brush)GetValue(MaskFillProperty); }
            set { SetValue(MaskFillProperty, value); }
        }
        public static readonly DependencyProperty MaskFillProperty =
            DependencyProperty.Register("MaskFill", typeof(Brush), typeof(MaskControl), new PropertyMetadata(null));

        public bool IsLightDismissEnabled
        {
            get { return (bool)GetValue(IsLightDismissEnabledProperty); }
            set { SetValue(IsLightDismissEnabledProperty, value); }
        }
        public static readonly DependencyProperty IsLightDismissEnabledProperty =
            DependencyProperty.Register("IsLightDismissEnabled", typeof(bool), typeof(MaskControl), new PropertyMetadata(true));
        
        static MaskControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaskControl), new FrameworkPropertyMetadata(typeof(MaskControl)));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(MaskControl), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
            VisibilityProperty.AddOwner(typeof(MaskControl), new PropertyMetadata(Visibility.Collapsed, OnVisibilityPropertyChanged));
        }

        private static void OnVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MaskControl control = d as MaskControl;
            if (control != null) control.UpdateOnVisibilityChanged((Visibility)e.NewValue);
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MaskControl control = d as MaskControl;
            if (control != null) control.UpdateIsOpenState((bool)e.NewValue);
        }

        public MaskControl()
        {
            this.Loaded += OnThisLoaded;
        }

        private void OnThisLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnThisLoaded;
            this.Unloaded += OnThisUnloaded;
        }

        private void OnThisUnloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= OnThisUnloaded;
            if (dismiss != null)
                dismiss.MouseDown -= OnMaskRectMouseDown;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (dismiss != null) dismiss.MouseDown -= OnMaskRectMouseDown;
            dismiss = GetTemplateChild("PART_LightDismiss") as FrameworkElement;
            if (dismiss != null) dismiss.MouseDown += OnMaskRectMouseDown;
            this.UpdateIsOpenState(IsOpen);

        }

        private void OnMaskRectMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.IsOpen && IsLightDismissEnabled) this.IsOpen = false;
        }

        private void UpdateOnVisibilityChanged(Visibility newVisibility)
        {
            if (Visibility == Visibility.Visible)
            {
                this.IsOpen = true;
            }
            else
            {
                this.IsOpen = false;
            }
        }

        private void UpdateIsOpenState(bool isOpen)
        {
            if (isOpen)
            {
                if (this.Visibility != Visibility.Visible)
                    this.Visibility = Visibility.Visible;
                Keyboard.Focus(this);
            }
            else
            {
                if (this.Visibility != Visibility.Collapsed)
                    this.Visibility = Visibility.Collapsed;
                Keyboard.ClearFocus();
            }
            OnIsOpenChanged(isOpen);
            RoutedEventArgs args = new RoutedEventArgs(IsOpenChangedEvent);
            this.RaiseEvent(args);
        }

        protected virtual void OnIsOpenChanged(bool isOpen)
        {

        }

        private FrameworkElement dismiss;
        public static readonly RoutedEvent IsOpenChangedEvent =
            EventManager.RegisterRoutedEvent("IsOpenChanged", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(MaskControl));
        public event RoutedEventHandler IsOpenChanged
        {
            add { this.AddHandler(IsOpenChangedEvent, value); }
            remove { this.RemoveHandler(IsOpenChangedEvent, value); }
        }
    }

}
