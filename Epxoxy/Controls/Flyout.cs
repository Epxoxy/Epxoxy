using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Epxoxy.Controls
{
    [TemplatePart(Name = "PART_ROOT", Type = typeof(FrameworkElement))]
    [ContentProperty("Content")]
    public class Flyout : Control
    {
        #region Command

        private static RoutedCommand closeCommand;
        public static RoutedCommand CloseCommand => closeCommand;
        private static RoutedCommand changedPlacement;
        public static RoutedCommand ChangedPlacement => changedPlacement;
        private static RoutedCommand changedOpenState;
        public static RoutedCommand ChangedOpenState => changedOpenState;

        private static void initializeCommand()
        {
            closeCommand = new RoutedCommand("CloseCommand", typeof(Flyout));
            CommandManager.RegisterClassCommandBinding(typeof(Flyout), new CommandBinding(CloseCommand, ExecuteCloseCommand));

            changedPlacement = new RoutedCommand("ChangedPlacement", typeof(Flyout));
            CommandManager.RegisterClassCommandBinding(typeof(Flyout), new CommandBinding(ChangedPlacement, ExecuteChangedPlacementCommand));

            changedOpenState = new RoutedCommand("ChangedOpenState", typeof(Flyout));
            CommandManager.RegisterClassCommandBinding(typeof(Flyout), new CommandBinding(ChangedOpenState, ExecuteChangeOpenStateCommand));
        }

        private static void ExecuteCloseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var Flyout = sender as Flyout;
            if (Flyout != null && Flyout.IsOpen) Flyout.IsOpen = false;
        }
        private static void ExecuteChangedPlacementCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var Flyout = sender as Flyout;
            if (Flyout != null)
            {
                var old = (int)Flyout.Placement;
                if (old > 2) old = -1;
                Dock dock = (Dock)(++old);
                Flyout.Placement = dock;
            }
        }
        private static void ExecuteChangeOpenStateCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var Flyout = sender as Flyout;
            if (Flyout != null)
            {
                Flyout.IsOpen = Flyout.IsOpen ? false : true;
            }
        }

        #endregion

        #region Member

        private DoubleKeyFrame closeXFrame;
        private DoubleKeyFrame closeYFrame;
        private DoubleKeyFrame openXFrame;
        private DoubleKeyFrame openYFrame;
        private Size layerDesiredSize;
        private Size LayerDesiredSize
        {
            get
            {
                if (layerDesiredSize == null) layerDesiredSize = new Size();
                return layerDesiredSize;
            }
            set
            {
                layerDesiredSize = value;
                updateAnimation();
            }
        }

        #endregion

        #region DependencyProperty

        #region Properties

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public double OpenLength
        {
            get { return (double)GetValue(OpenLengthProperty); }
            set { SetValue(OpenLengthProperty, value); }
        }
        public Dock Placement
        {
            get { return (Dock)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }
        public Orientation Target
        {
            get { return (Orientation)GetValue(TargetProperty); }
            private set { SetValue(TargetProperty, value); }
        }

        public static readonly DependencyProperty OpenLengthProperty =
            DependencyProperty.Register("OpenLength", typeof(double), typeof(Flyout), new PropertyMetadata(0d, OnOpenLengthChanged));
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(Flyout), new PropertyMetadata(null, new PropertyChangedCallback(OnContentChanged)));
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(Flyout), new PropertyMetadata(false, OnIsOpenChanged));
        private static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(Dock), typeof(Flyout), new PropertyMetadata(Dock.Right, OnPlacementChanged));
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(Orientation), typeof(Flyout), new PropertyMetadata(Orientation.Horizontal));

        #endregion

        #region Properties ChangedCallback

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Flyout dc = d as Flyout;
            dc.OnContentChanged(e.OldValue, e.NewValue);
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Flyout dc = d as Flyout;
            if (dc != null) dc.updateContentState();
        }

        private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Flyout dc = d as Flyout;
            if (dc != null) dc.updatePlacement(e.OldValue);
        }

        private static void OnOpenLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void updateContentState()
        {
            if (IsOpen)
            {
                if (Visibility != Visibility.Visible)
                    Visibility = Visibility.Visible;
                VisualStateManager.GoToState(this, FlyoutState.OpenContent, true);
                Keyboard.Focus(this);
            }
            else
            {
                VisualStateManager.GoToState(this, FlyoutState.CloseContent, true);
                Keyboard.ClearFocus();
            }
            RoutedEventArgs args = new RoutedEventArgs(IsOpenChangedEvent);
            this.RaiseEvent(args);
            this.OnIsOpenChanged(args);
        }

        private static void OnVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Flyout ctl = d as Flyout;
            if(ctl != null)
            {
                Visibility visibility = (Visibility)e.NewValue;
                if(visibility == Visibility.Visible)
                {
                    if (!ctl.IsOpen) ctl.IsOpen = true;
                }
                else
                {
                    if (ctl.IsOpen) ctl.IsOpen = false;
                }
            }
        }

        protected virtual void OnIsOpenChanged(RoutedEventArgs args)
        {

        }
        
        private Task<bool> WaitForLoadedAsync()
        {
            var tcsOfResult = new TaskCompletionSource<bool>();
            if (this.IsLoaded)
            {
                tcsOfResult.TrySetResult(true);
            }
            else
            {
                RoutedEventHandler loadedHandler = null;
                loadedHandler = (sender, args) =>
                {
                    this.Loaded -= loadedHandler;
                    tcsOfResult.TrySetResult(true);
                };
                this.Loaded += loadedHandler;
            }
            return tcsOfResult.Task;
        }

        public Task<bool> WaitForCloseAsync()
        {
            var tcsOfResult = new TaskCompletionSource<bool>();
            RoutedEventHandler handlder = null;
            handlder = (obj, args) =>
            {
                if (this.IsOpen == false)
                {
                    tcsOfResult.TrySetResult(true);
                    this.IsOpenChanged -= handlder;
                }
            };
            WaitForLoadedAsync().ContinueWith(result =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.IsOpen = true;
                    this.IsOpenChanged += handlder;
                });
            });
            return tcsOfResult.Task;
        }

        protected virtual void updatePlacement(object oldPlacement)
        {
            switch (Placement)
            {
                case Dock.Left:
                case Dock.Right:
                    Target = Orientation.Horizontal;
                    break;
                case Dock.Top:
                case Dock.Bottom:
                    Target = Orientation.Vertical;
                    break;
            }
            updateAnimation();
        }
        protected virtual void updateAnimation()
        {
            updateAnimation(LayerDesiredSize);
        }

        protected virtual void updateAnimation(Size targetSize)
        {
            double tarValue, finalValue;
            if (Target == Orientation.Horizontal)
            {
                tarValue = targetSize.Width + 4;
                finalValue = Placement == Dock.Right ? tarValue : -tarValue;
                if (flyoutLayer != null) flyoutLayer.RenderTransform = new TranslateTransform(finalValue, 0);
                if (closeXFrame != null) closeXFrame.Value = finalValue;
                if (closeYFrame != null) closeYFrame.Value = 0;
                if (openXFrame != null) openXFrame.Value = finalValue;
                if (openYFrame != null) openYFrame.Value = 0;
            }
            else
            {
                tarValue = targetSize.Height + 4;
                finalValue = Placement == Dock.Bottom ? tarValue : -tarValue;
                if (flyoutLayer != null) flyoutLayer.RenderTransform = new TranslateTransform(0, finalValue);
                if (closeXFrame != null) closeXFrame.Value = 0;
                if (closeYFrame != null) closeYFrame.Value = finalValue;
                if (openXFrame != null) openXFrame.Value = 0;
                if (openYFrame != null) openYFrame.Value = finalValue;
            }
            if (IsOpen)
            {
                VisualStateManager.GoToState(this, FlyoutState.CloseContent, false);
                VisualStateManager.GoToState(this, FlyoutState.OpenContent, false);
            }
        }
        protected virtual void OnContentChanged(object oldContent, object newContent)
        {
            // Remove the old content child
            RemoveLogicalChild(oldContent);

            // Add the new content child
            AddLogicalChild(newContent);
        }

        #endregion

        #endregion

        #region Override

        private FrameworkElement rootlayer;
        private FrameworkElement flyoutLayer;
        private System.Windows.Shapes.Rectangle dismissLayer;
        public override void OnApplyTemplate()
        {
            if (flyoutLayer != null) flyoutLayer.SizeChanged -= OnFlyoutLayerSizeChanged;
            if (dismissLayer != null) dismissLayer.MouseDown -= OnLightDismissLayerMouseDown;
            if (rootlayer != null) rootlayer.IsVisibleChanged -= OnRootIsVisibleChanged;

            base.OnApplyTemplate();
            closeXFrame = GetTemplateChild("CloseXFrame") as DoubleKeyFrame;
            closeYFrame = GetTemplateChild("CloseYFrame") as DoubleKeyFrame;
            openXFrame = GetTemplateChild("OpenXFrame") as DoubleKeyFrame;
            openYFrame = GetTemplateChild("OpenYFrame") as DoubleKeyFrame;
            flyoutLayer = GetTemplateChild("PART_Content") as FrameworkElement;
            dismissLayer = GetTemplateChild("LightDismissLayer") as System.Windows.Shapes.Rectangle;
            rootlayer = GetTemplateChild("PART_ROOT") as FrameworkElement;

            if (flyoutLayer != null) flyoutLayer.SizeChanged += OnFlyoutLayerSizeChanged;
            if (dismissLayer != null) dismissLayer.MouseDown += OnLightDismissLayerMouseDown;
            if(rootlayer != null) rootlayer.IsVisibleChanged += OnRootIsVisibleChanged;

            VisualStateManager.GoToState(this, FlyoutState.OpenContent, false);
            updateContentState();
        }

        private void OnRootIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsOpen || (bool)e.NewValue) return;
            this.Visibility = Visibility.Collapsed;
        }

        private void OnLightDismissLayerMouseDown(object sender, MouseButtonEventArgs e)
        {
            IsOpen = false;
            e.Handled = true;
        }

        private void OnFlyoutLayerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayerDesiredSize = e.NewSize;
        }

        #endregion

        #region Contructor

        static Flyout()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Flyout), new FrameworkPropertyMetadata(typeof(Flyout)));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(Flyout), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
            ClipToBoundsProperty.OverrideMetadata(typeof(Flyout), new FrameworkPropertyMetadata(true));
            VisibilityProperty.AddOwner(typeof(Flyout), new PropertyMetadata(Visibility.Collapsed, OnVisibilityPropertyChanged));
            initializeCommand();
        }

        #endregion

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
        }

        public static readonly RoutedEvent IsOpenChangedEvent =
            EventManager.RegisterRoutedEvent("IsOpenChanged", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(Flyout));
        public static readonly RoutedEvent IsClosedEvent =
            EventManager.RegisterRoutedEvent("IsClosed", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(Flyout));

        public event RoutedEventHandler IsOpenChanged
        {
            add { this.AddHandler(IsOpenChangedEvent, value); }
            remove { this.RemoveHandler(IsOpenChangedEvent, value); }
        }
        public event RoutedEventHandler IsClosed
        {
            add { this.AddHandler(IsClosedEvent, value); }
            remove { this.RemoveHandler(IsClosedEvent, value); }
        }

        private class FlyoutState
        {
            public const string OpenContent = "OpenContent";
            public const string CloseContent = "CloseContent";
        }
    }
}
