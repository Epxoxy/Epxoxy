using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Epxoxy.Controls
{
    [ContentProperty("Content")]
    [TemplatePart(Name = "PART_ContentLayer", Type = typeof(Control))]
    [TemplatePart(Name = "PART_Content", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "PART_PaneLayer", Type = typeof(Control))]
    [TemplatePart(Name = "PART_PaneContent", Type = typeof(ContentPresenter))]
    public class SplitView02 : Control
    {
        #region Command
        private static RoutedCommand toNextDisplayModeCommand;
        private static RoutedCommand toNextPlacementCommand;
        private static RoutedCommand changedOpenStateCommand;
        private static RoutedCommand closeCommand;
        public static RoutedCommand ToNextDisplayModeCommand => toNextDisplayModeCommand;
        public static RoutedCommand ToNextPlacementCommand => toNextPlacementCommand;
        public static RoutedCommand ChangedOpenStateCommand => changedOpenStateCommand;
        public static RoutedCommand CloseCommand => closeCommand;

        private static void initializeCommand()
        {
            toNextDisplayModeCommand = new RoutedCommand("ToNextDisplayModeCommand", typeof(SplitView02));
            CommandManager.RegisterClassCommandBinding(typeof(SplitView02),
                new CommandBinding(ToNextDisplayModeCommand, ExecuteToNextDisplayMode));
            toNextPlacementCommand = new RoutedCommand("ToNextPlacementCommand", typeof(SplitView02));
            CommandManager.RegisterClassCommandBinding(typeof(SplitView02),
                new CommandBinding(ToNextPlacementCommand, ExecuteToNextPlacement));
            changedOpenStateCommand = new RoutedCommand("ChangedOpenStateCommand", typeof(SplitView02));
            CommandManager.RegisterClassCommandBinding(typeof(SplitView02),
                new CommandBinding(ChangedOpenStateCommand, ExecuteChangedOpenState));
            closeCommand = new RoutedCommand("CloseCommand", typeof(SplitView02));
            CommandManager.RegisterClassCommandBinding(typeof(SplitView02),
                new CommandBinding(CloseCommand, ExecuteCloseSplitView));
        }

        private static void ExecuteToNextDisplayMode(object sender, ExecutedRoutedEventArgs e)
        {
            var sv = sender as SplitView02;
            if (sv != null)
            {
                if (sv.DisplayMode < SplitViewDisplayMode.CompactOverlay)
                {
                    sv.DisplayMode += 1;
                }
                else
                {
                    sv.DisplayMode = SplitViewDisplayMode.Inline;
                }
            }
        }
        private static void ExecuteToNextPlacement(object sender, ExecutedRoutedEventArgs e)
        {
            var sv = sender as SplitView02;
            if (sv != null)
            {
                if (sv.PanePlacement == PanePlacement.Left) sv.PanePlacement = PanePlacement.Right;
                else sv.PanePlacement = PanePlacement.Left;
            }
        }
        private static void ExecuteChangedOpenState(object sender, ExecutedRoutedEventArgs e)
        {
            var sv = sender as SplitView02;
            if (sv != null)
            {
                if (sv.IsPaneOpen) sv.IsPaneOpen = false;
                else sv.IsPaneOpen = true;
            }
        }
        private static void ExecuteCloseSplitView(object sender, ExecutedRoutedEventArgs e)
        {
            var sv = sender as SplitView02;
            if (sv != null)
            {
                if (sv.IsPaneOpen) sv.IsPaneOpen = false;
            }
        }

        #endregion

        #region Properties

        public object Pane
        {
            get { return (object)GetValue(PaneProperty); }
            set { SetValue(PaneProperty, value); }
        }
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }
        public double CompactPaneLength
        {
            get { return (double)GetValue(CompactPaneLengthProperty); }
            set { SetValue(CompactPaneLengthProperty, value); }
        }

        [Bindable(true), Category("Appearance")]
        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }

        [Description("#f4f4f4"), Category("Brush")]
        public Brush PaneBackground
        {
            get { return (Brush)GetValue(PaneBackgroundProperty); }
            set { SetValue(PaneBackgroundProperty, value); }
        }

        [Description("Overlay"), Category("Behavior")]
        public SplitViewDisplayMode DisplayMode
        {
            get { return (SplitViewDisplayMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }
        public string CurrentStateName
        {
            get { return (string)GetValue(CurrentStateNameProperty); }
            private set { SetValue(CurrentStateNameProperty, value); }
        }

        [Bindable(false), Category("Behavior")]
        public bool EnableInlineAnimation
        {
            get { return (bool)GetValue(EnableInlineAnimationProperty); }
            set { SetValue(EnableInlineAnimationProperty, value); }
        }

        public PanePlacement PanePlacement
        {
            get { return (PanePlacement)GetValue(PanePlacementProperty); }
            set { SetValue(PanePlacementProperty, value); }
        }

        [Bindable(true), Category("Appearance")]
        public bool IsLightDismissEnabled
        {
            get { return (bool)GetValue(IsLightDismissEnabledProperty); }
            set { SetValue(IsLightDismissEnabledProperty, value); }
        }

        public SplitViewTemplateSettings TemplateSettings
        {
            get { return (SplitViewTemplateSettings)GetValue(TemplateSettingsProperty); }
            private set { SetValue(TemplateSettingsProperty, value); }
        }

        public static readonly DependencyProperty PaneProperty;
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty OpenPaneLengthProperty;
        public static readonly DependencyProperty CompactPaneLengthProperty;
        public static readonly DependencyProperty IsPaneOpenProperty;
        public static readonly DependencyProperty PaneBackgroundProperty;
        public static readonly DependencyProperty DisplayModeProperty;
        public static readonly DependencyProperty CurrentStateNameProperty;
        public static readonly DependencyProperty EnableInlineAnimationProperty;
        public static readonly DependencyProperty PanePlacementProperty;
        public static readonly DependencyProperty IsLightDismissEnabledProperty;
        public static readonly DependencyProperty TemplateSettingsProperty;

        #region PropertyChanged Callback

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView02;
            sv?.UpdateContent(e.OldValue, e.NewValue);
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView02;
            if (sv != null) sv.UpdateOpenState();
        }

        private static void OnDisplayModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView02;
            if (sv != null) sv.UpdateDisplayMode();
        }

        private static void OnMetricsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView02;
            if (sv != null && sv.TemplateSettings != null) sv.TemplateSettings.Update();
        }

        private static void OnPanePlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView02;
            if (sv != null)
            {
                PanePlacement oldPlacement = (PanePlacement)e.OldValue;
                PanePlacement newPlacement = (PanePlacement)e.NewValue;
                if (oldPlacement != newPlacement)
                {
                    sv.TemplateSettings.UpdateForPlacementChanged();
                    sv.OnPlacementChanged(oldPlacement, newPlacement);
                }
            }
        }

        #endregion

        #endregion

        #region Override

        RectangleGeometry paneClipRectangle;
        UIElement dismissLayer;
        public override void OnApplyTemplate()
        {
            if(dismissLayer != null) dismissLayer.MouseDown -= OnDismissLayerMouseDown;
            base.OnApplyTemplate();
            paneClipRectangle = GetTemplateChild("PaneClipRectangleGeometry") as RectangleGeometry;

            //Add event handler
            dismissLayer = GetTemplateChild("LightDismissLayer") as UIElement;
            if (dismissLayer != null) dismissLayer.MouseDown += OnDismissLayerMouseDown;
            //Update state
            Helpers.DebugHelper.debugWrite(this, "OnApplyTemplate working");
            UpdateDisplayMode(false);
        }

        private void OnDismissLayerMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsPaneOpen)
            {
                IsPaneOpen = false;
            }
            e.Handled = true;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (paneClipRectangle != null) paneClipRectangle.Rect = new Rect(0, 0, this.OpenPaneLength, (double)this.ActualHeight);
        }

        #endregion

        #region Private Method

        private string ToOpenStateName { get; set; } = SplitViewState.NoneState;
        private string ToCloseStateName { get; set; } = SplitViewState.NoneState;
        private bool UseTransitions => EnableInlineAnimation ? true : isOverlayMode;
        private bool isOverlayMode;

        protected virtual void UpdateDisplayMode(bool autoClose = true)
        {
            if (TemplateSettings == null) return;
            isOverlayMode = false;
            switch (DisplayMode)
            {
                case SplitViewDisplayMode.Inline:
                    ToOpenStateName = SplitViewState.InlineOpen;
                    ToCloseStateName = SplitViewState.InlineClose;
                    break;
                case SplitViewDisplayMode.Overlay:
                    ToOpenStateName = SplitViewState.OverlayOpen;
                    ToCloseStateName = SplitViewState.OverlayClose;
                    isOverlayMode = true;
                    break;
                case SplitViewDisplayMode.CompactInline:
                    ToOpenStateName = SplitViewState.CompactInlineOpen;
                    ToCloseStateName = SplitViewState.CompactInlineClose;
                    break;
                case SplitViewDisplayMode.CompactOverlay:
                    ToOpenStateName = SplitViewState.CompactOverlayOpen;
                    ToCloseStateName = SplitViewState.CompactOverlayClose;
                    isOverlayMode = true;
                    break;
                default: break;
            }
            VisualStateManager.GoToState(this, SplitViewState.NoneState, true);
            //Update state
            if (IsPaneOpen)
            {
                if (autoClose)
                {
                    IsPaneOpen = false;
                }
                else
                {
                    UpdateOpenState();
                }
            }
            else SwitchToState(ToCloseStateName);

            Helpers.DebugHelper.debugWrite(this, "UpdateDisplayMode working");
        }
        private void UpdateOpenState()
        {
            Helpers.DebugHelper.debugWrite(this, "UpdateOpenState working");
            SwitchToState(IsPaneOpen ? ToOpenStateName : ToCloseStateName);
        }

        protected virtual void OnPlacementChanged(PanePlacement oldPlacement, PanePlacement newPlacement)
        {
            VisualStateManager.GoToState(this, SplitViewState.NoneState, true);
            SwitchToState(ToCloseStateName);
        }

        private void UpdateContent(object oldContent, object newContent)
        {
            // Remove the old content child
            RemoveLogicalChild(oldContent);

            // Add the new content child
            AddLogicalChild(newContent);
        }

        private void SwitchToState(string stateName)
        {
            VisualStateManager.GoToState(this, stateName, UseTransitions);
            CurrentStateName = stateName;
        }
        
        #endregion

        #region Constructor

        static SplitView02()
        {
            PaneProperty = DependencyProperty.Register("Pane", typeof(object), typeof(SplitView02),
                new PropertyMetadata(null,
                new PropertyChangedCallback(OnContentChanged)));
            ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(SplitView02),
                new PropertyMetadata(null, new PropertyChangedCallback(OnContentChanged)));
            OpenPaneLengthProperty = DependencyProperty.Register("OpenPaneLength", typeof(double), typeof(SplitView02),
                new PropertyMetadata(160d,
                new PropertyChangedCallback(OnMetricsChanged)));
            CompactPaneLengthProperty = DependencyProperty.Register("CompactPaneLength", typeof(double), typeof(SplitView02),
                new PropertyMetadata(48d,
                new PropertyChangedCallback(OnMetricsChanged)));
            IsPaneOpenProperty = DependencyProperty.Register("IsPaneOpen", typeof(bool), typeof(SplitView02),
                    new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnIsOpenChanged)));
            PaneBackgroundProperty = DependencyProperty.Register("PaneBackground", typeof(Brush), typeof(SplitView02),
                new PropertyMetadata(null));
            DisplayModeProperty = DependencyProperty.Register("DisplayMode", typeof(SplitViewDisplayMode), typeof(SplitView02),
                    new FrameworkPropertyMetadata(SplitViewDisplayMode.Overlay,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnDisplayModeChanged)));
            CurrentStateNameProperty = DependencyProperty.Register("CurrentStateName", typeof(string), typeof(SplitView02),
                    new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
            EnableInlineAnimationProperty = DependencyProperty.Register("EnableInlineAnimation", typeof(bool), typeof(SplitView02),
                new PropertyMetadata(false));
            PanePlacementProperty = DependencyProperty.Register("PanePlacement", typeof(PanePlacement), typeof(SplitView02),
                new PropertyMetadata(PanePlacement.Left, OnPanePlacementChanged));
            IsLightDismissEnabledProperty = DependencyProperty.Register("IsLightDismissEnabled", typeof(bool), typeof(SplitView02),
                new PropertyMetadata(true));
            TemplateSettingsProperty = DependencyProperty.Register("TemplateSettings", typeof(SplitViewTemplateSettings), typeof(SplitView02),
                new FrameworkPropertyMetadata(null) { BindsTwoWayByDefault = false });

            ClipToBoundsProperty.OverrideMetadata(typeof(SplitView02), new FrameworkPropertyMetadata(true));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitView02), new FrameworkPropertyMetadata(typeof(SplitView02)));
            initializeCommand();
        }

        public SplitView02()
        {
            TemplateSettings = new SplitViewTemplateSettings(this);
            this.Loaded += (sendr, e) => 
            {
                this.Focus();
                UpdateDisplayMode(false);
            };
        }

        #endregion
        
    }
}
