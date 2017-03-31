using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Epxoxy.Controls.Previous
{
    [ContentProperty("Content")]
    [TemplatePart(Name = "PART_ContentLayer", Type = typeof(Control))]
    [TemplatePart(Name = "PART_Content", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "PART_PaneLayer", Type = typeof(Control))]
    [TemplatePart(Name = "PART_PaneContent", Type = typeof(ContentPresenter))]
    public class SplitView : Control
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
            toNextDisplayModeCommand = new RoutedCommand("ToNextDisplayModeCommand", typeof(SplitView));
            CommandManager.RegisterClassCommandBinding(typeof(SplitView),
                new CommandBinding(ToNextDisplayModeCommand, ExecuteToNextDisplayMode));
            toNextPlacementCommand = new RoutedCommand("ToNextPlacementCommand", typeof(SplitView));
            CommandManager.RegisterClassCommandBinding(typeof(SplitView),
                new CommandBinding(ToNextPlacementCommand, ExecuteToNextPlacement));
            changedOpenStateCommand = new RoutedCommand("ChangedOpenStateCommand", typeof(SplitView));
            CommandManager.RegisterClassCommandBinding(typeof(SplitView),
                new CommandBinding(ChangedOpenStateCommand, ExecuteChangedOpenState));
            closeCommand = new RoutedCommand("CloseCommand", typeof(SplitView));
            CommandManager.RegisterClassCommandBinding(typeof(SplitView),
                new CommandBinding(CloseCommand, ExecuteCloseSplitView));
        }

        private static void ExecuteToNextDisplayMode(object sender, ExecutedRoutedEventArgs e)
        {
            SplitView sv = sender as SplitView;
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
            SplitView sv = sender as SplitView;
            if (sv != null)
            {
                if (sv.PanePlacement == PanePlacement.Left) sv.PanePlacement = PanePlacement.Right;
                else sv.PanePlacement = PanePlacement.Left;
            }
        }
        private static void ExecuteChangedOpenState(object sender, ExecutedRoutedEventArgs e)
        {
            SplitView sv = sender as SplitView;
            if (sv != null)
            {
                if (sv.IsPaneOpen) sv.IsPaneOpen = false;
                else sv.IsPaneOpen = true;
            }
        }
        private static void ExecuteCloseSplitView(object sender, ExecutedRoutedEventArgs e)
        {
            SplitView sv = sender as SplitView;
            if (sv != null)
            {
                if (sv.IsPaneOpen) sv.IsPaneOpen = false;
            }
        }

        #endregion

        #region Constructor

        static SplitView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitView), new FrameworkPropertyMetadata(typeof(SplitView)));
            IsPaneOpenChangedEvent = EventManager.RegisterRoutedEvent("IsPaneOpenChanged",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SplitView));
            ClipToBoundsProperty.OverrideMetadata(typeof(SplitView), new FrameworkPropertyMetadata(true));
            PaneProperty = DependencyProperty.Register("Pane", typeof(object), typeof(SplitView),
                new PropertyMetadata(null, new PropertyChangedCallback(OnContentChanged)));
            ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(SplitView),
                new PropertyMetadata(null, new PropertyChangedCallback(OnContentChanged)));
            OpenPaneLengthProperty = DependencyProperty.Register("OpenPaneLength", typeof(double), typeof(SplitView),
                new PropertyMetadata(160d,
                new PropertyChangedCallback(OnMetricsChanged)));
            CompactPaneLengthProperty = DependencyProperty.Register("CompactPaneLength", typeof(double), typeof(SplitView),
                new PropertyMetadata(48d,
                new PropertyChangedCallback(OnMetricsChanged)));
            IsPaneOpenProperty = DependencyProperty.Register("IsPaneOpen", typeof(bool), typeof(SplitView),
                new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnIsPaneOpenChanged)));
            PaneBackgroundProperty = DependencyProperty.Register("PaneBackground", typeof(Brush), typeof(SplitView),
                new PropertyMetadata(null));
            DisplayModeProperty = DependencyProperty.Register("DisplayMode", typeof(SplitViewDisplayMode), typeof(SplitView),
                    new FrameworkPropertyMetadata(SplitViewDisplayMode.Overlay,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnDisplayModeChanged)));
            CurrentStateNameProperty = DependencyProperty.Register("CurrentStateName", typeof(string), typeof(SplitView),
                    new FrameworkPropertyMetadata(string.Empty));
            EnableInlineAnimationProperty = DependencyProperty.Register("EnableInlineAnimation", typeof(bool), typeof(SplitView),
                new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnEnableInlineAnimationChanged));
            PanePlacementProperty = DependencyProperty.Register("PanePlacement", typeof(PanePlacement), typeof(SplitView),
                new PropertyMetadata(PanePlacement.Left, OnPanePlacementChanged));
            IsLightDismissEnabledProperty = DependencyProperty.Register("IsLightDismissEnabled", typeof(bool), typeof(SplitView),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
            IsInlineAdaptEnabledProperty = DependencyProperty.Register("IsInlineAdaptEnabled", typeof(bool), typeof(SplitView),
                new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnIsInlineAdaptEnabledChanged));
            initializeCommand();
        }

        public SplitView()
        {
            TemplateSettings = new SplitViewTemplateSettings(this);
            this.Loaded += (sendr, e) => { this.Focus(); };
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
        
        public bool IsInlineAdaptEnabled
        {
            get { return (bool)GetValue(IsInlineAdaptEnabledProperty); }
            set { SetValue(IsInlineAdaptEnabledProperty, value); }
        }

        public event RoutedEventHandler IsPaneOpenChanged
        {
            add { this.AddHandler(IsPaneOpenChangedEvent, value); }
            remove { this.RemoveHandler(IsPaneOpenChangedEvent, value); }
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
        public static readonly DependencyProperty IsInlineAdaptEnabledProperty;
        public static readonly RoutedEvent IsPaneOpenChangedEvent;

        #region PropertyChanged Callback

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView;
            sv?.UpdateContent(e.OldValue, e.NewValue);
        }
        
        private static void OnIsPaneOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView;
            sv?.UpdateOnIsPaneOpenChanged((bool)e.NewValue);
        }

        private static void OnDisplayModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView;
            if (sv != null)
            {
                var mode = (SplitViewDisplayMode)e.NewValue;
                sv.UpdateDisplayMode((SplitViewDisplayMode)e.NewValue);
                sv.OnDisplayModeChanged(mode);
            }
        }

        private static void OnMetricsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView;
            if (sv != null) sv?.TemplateSettings.Update();
        }

        private static void OnPanePlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView;
            if (sv != null)
            {
                var oldPlacement = (PanePlacement)e.OldValue;
                var newPlacement = (PanePlacement)e.NewValue;
                if (oldPlacement != newPlacement)
                {
                    sv.OnPanePlacementChanged(oldPlacement, newPlacement);
                }
            }
        }

        private static void OnIsInlineAdaptEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView;
            sv?.OnIsInlineAdaptEnabledChanged((bool)e.NewValue);
        }

        private static void OnEnableInlineAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sv = d as SplitView;
            if (sv != null)
            {
                sv.isTransitionsEnabled = (bool)e.NewValue ? true
                    : sv.DisplayMode == SplitViewDisplayMode.CompactOverlay 
                    || sv.DisplayMode == SplitViewDisplayMode.Overlay;
            }
        }

        #endregion

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            if (dismissLayer != null) dismissLayer.MouseDown -= OnDismissLayerMouseDown;
            TemplateSettings.Cleanup();
            base.OnApplyTemplate();
            contentPresenter = GetTemplateChild("PART_Content") as FrameworkElement;
            paneClipRectangle = GetTemplateChild("PaneClipRectangleGeometry") as RectangleGeometry;
            Func<string[], IList<DependencyObject>> getTemplateChildList = names =>
            {
                List<DependencyObject> objsList = new List<DependencyObject>();
                foreach (var name in names)
                {
                    var obj = GetTemplateChild(name) as DependencyObject;
                    if (obj != null) objsList.Add(obj);
                }
                if (objsList.Count == 0) return null;
                return objsList;
            };
            TemplateSettings.SetBinding(
                DoubleKeyFrame.ValueProperty,
                SplitViewTemplateSettings.OpenPaneLengthProperty,
                getTemplateChildList(new string[]
                {
                    "toOpenLength00", "toOpenLength01", "toOpenLength02",
                    "toOpenLength03", "toOpenLength04"
                }));
            TemplateSettings.SetBinding(
                DoubleKeyFrame.ValueProperty,
                SplitViewTemplateSettings.CompactPaneLengthProperty,
                getTemplateChildList(new string[]
                {
                    "toCompactLength00", "toCompactLength01", "toCompactLength02",
                    "toCompactLength03", "toCompactLength04",
                }));
            TemplateSettings.SetBinding(
                DoubleKeyFrame.ValueProperty,
                SplitViewTemplateSettings.TranslateCompactToOpenLengthProperty,
                getTemplateChildList(new string[] { "compactToOpen00", "compactToOpen01", "compactToOpen02", "compactToOpen03" }));
            TemplateSettings.SetBinding(
                DoubleKeyFrame.ValueProperty,
                SplitViewTemplateSettings.TranslateOverlayFromProperty,
                getTemplateChildList(new string[] { "negativeOpen00", "negativeOpen01" }));
            TemplateSettings.SetBinding(
                DoubleKeyFrame.ValueProperty,
                SplitViewTemplateSettings.NegativeTranslateOverlayFromProperty,
                getTemplateChildList(new string[] { "orientedOpenLength00", "orientedOpenLength01", "orientedOpenLength02" }));
            
            dismissLayer = GetTemplateChild("LightDismissLayer") as UIElement;
            if(dismissLayer != null) dismissLayer.MouseDown += OnDismissLayerMouseDown;
            //Update state
            OnIsInlineAdaptEnabledChanged(IsInlineAdaptEnabled);
            UpdateDisplayMode(DisplayMode, false);
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
        
        private void UpdateContent(object oldContent, object newContent)
        {
            // Remove the old content child
            RemoveLogicalChild(oldContent);
            // Add the new content child
            AddLogicalChild(newContent);
        }

        private void UpdateAdaptMetricsChanged()
        {
            if (contentPresenter == null) return;
            var displayMode = DisplayMode;
            if (!IsPaneOpen)
            {
                contentPresenter.Margin = default(Thickness);
            }
            else if (displayMode == SplitViewDisplayMode.Inline)
            {
                contentPresenter.Margin = TemplateSettings.AdaptOpenMargin;
            }
            else if (displayMode == SplitViewDisplayMode.CompactInline)
            {
                contentPresenter.Margin = TemplateSettings.AdaptCompactToOpenMargin;
            }
            else
            {
                contentPresenter.Margin = default(Thickness);
            }
        }

        private void UpdateDisplayMode(SplitViewDisplayMode displayMode, bool autoClose = true)
        {
            switch (displayMode)
            {
                case SplitViewDisplayMode.Inline:
                    curToOpenState = SplitViewState.InlineOpen;
                    curToCloseState = SplitViewState.InlineClose;
                    isTransitionsEnabled = EnableInlineAnimation;
                    break;
                case SplitViewDisplayMode.Overlay:
                    curToOpenState = SplitViewState.OverlayOpen;
                    curToCloseState = SplitViewState.OverlayClose;
                    isTransitionsEnabled = true;
                    break;
                case SplitViewDisplayMode.CompactInline:
                    curToOpenState = SplitViewState.CompactInlineOpen;
                    curToCloseState = SplitViewState.CompactInlineClose;
                    isTransitionsEnabled = EnableInlineAnimation;
                    break;
                case SplitViewDisplayMode.CompactOverlay:
                    curToOpenState = SplitViewState.CompactOverlayOpen;
                    curToCloseState = SplitViewState.CompactOverlayClose;
                    isTransitionsEnabled = true;
                    break;
                default: break;
            }
            //Update state
            if (IsPaneOpen)
            {
                if (autoClose)
                {
                    IsPaneOpen = false;
                }
                else
                {
                    SwitchToState(curToCloseState);
                    UpdateOnIsPaneOpenChanged(IsPaneOpen);
                }
            }
            else SwitchToState(curToCloseState);
            //Adapt margin if need
            if (IsInlineAdaptEnabled) UpdateAdaptMetricsChanged();
        }

        private void UpdateOnIsPaneOpenChanged(bool isPaneOpen)
        {
            SwitchToState(isPaneOpen ? curToOpenState : curToCloseState);
            this.RaiseEvent(new RoutedEventArgs(IsPaneOpenChangedEvent));
            this.OnIsPaneOpenChanged(isPaneOpen);
        }
        
        private void OnIsInlineAdaptEnabledChanged(bool enabled)
        {
            this.IsPaneOpenChanged -= OnThisIsPaneOpenChanged;
            if (contentPresenter != null)
            {
                if (enabled)
                {
                    UpdateAdaptMetricsChanged();
                    this.IsPaneOpenChanged += OnThisIsPaneOpenChanged;
                }
                else
                {
                    contentPresenter.Margin = default(Thickness);
                }
            }
        }

        private void OnThisIsPaneOpenChanged(object sender, RoutedEventArgs e)
        {
            UpdateAdaptMetricsChanged();
        }

        private void OnPanePlacementChanged(PanePlacement oldPlacement, PanePlacement newPlacement)
        {
            TemplateSettings.UpdateForPlacementChanged();
            VisualStateManager.GoToState(this, SplitViewState.NoneState, true);
            SwitchToState(curToCloseState);
        }

        private void SwitchToState(string stateName)
        {
            VisualStateManager.GoToState(this, stateName, isTransitionsEnabled);
            CurrentStateName = stateName;
        }

        #endregion

        protected virtual void OnDisplayModeChanged(SplitViewDisplayMode mode)
        {
        }

        protected virtual void OnIsPaneOpenChanged(bool isPaneOpen)
        {
        }

        private UIElement dismissLayer;
        private FrameworkElement contentPresenter;
        private RectangleGeometry paneClipRectangle;
        private SplitViewTemplateSettings TemplateSettings;
        private string curToOpenState { get; set; } = SplitViewState.InlineOpen;
        private string curToCloseState { get; set; } = SplitViewState.InlineClose;
        private bool isTransitionsEnabled = true;
    }
}
