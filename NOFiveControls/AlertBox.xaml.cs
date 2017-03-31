using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace NOFiveControls
{
    /// <summary>
    /// Interaction logic for AlertBox.xaml
    /// </summary>
    public partial class AlertBox : Window
    {
        #region Properities

        public string AlertTitle
        {
            get { return (string)GetValue(AlertTitleProperty); }
            set { SetValue(AlertTitleProperty, value); }
        }
        public object AlertContent
        {
            get { return (object)GetValue(AlertContentProperty); }
            set { SetValue(AlertContentProperty, value); }
        }
        public bool AutoClose
        {
            get { return (bool)GetValue(AutoCloseProperty); }
            set { SetValue(AutoCloseProperty, value); }
        }
        public Visibility OkCancelButtonVisibility
        {
            get { return (Visibility)GetValue(OkCancelButtonVisibilityProperty); }
            set { SetValue(OkCancelButtonVisibilityProperty, value); }
        }
        public List<string> OKMenus
        {
            get { return (List<string>)GetValue(OKMenusProperty); }
            set { SetValue(OKMenusProperty, value); }
        }

        public static readonly DependencyProperty AlertContentProperty =
            DependencyProperty.Register("AlertContent", typeof(object), typeof(AlertBox), new PropertyMetadata("Alert Content"));
        public static readonly DependencyProperty AlertTitleProperty =
            DependencyProperty.Register("AlertTitle", typeof(string), typeof(AlertBox), new PropertyMetadata("Alert"));
        public static readonly DependencyProperty AutoCloseProperty =
            DependencyProperty.Register("AutoClose", typeof(bool), typeof(AlertBox), new PropertyMetadata(false, OnAutoCloseChanged));
        public static readonly DependencyProperty OKMenusProperty =
            DependencyProperty.Register("OKMenus", typeof(List<string>), typeof(AlertBox), new PropertyMetadata(null));
        public static readonly DependencyProperty OkCancelButtonVisibilityProperty =
            DependencyProperty.Register("OkCancelButtonVisibility", typeof(Visibility), typeof(AlertBox),
                new PropertyMetadata(Visibility.Visible));

        private static void OnAutoCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AlertBox alertbox = d as AlertBox;
            if (alertbox != null) alertbox.setAutoClose((bool)e.NewValue);
        }

        #endregion

        public AlertResult Result { get; private set; }
        public int SelectedIndex { get; private set; } = -1;
        private Lazy<System.Windows.Threading.DispatcherTimer> closeTimer = new Lazy<System.Windows.Threading.DispatcherTimer>();
        private System.Windows.Controls.Button okBtn;
        private System.Windows.Controls.Button cancelBtn;
        private FrameworkElement titleArea;
        private System.Windows.Controls.ListBox okMenusLayer;
        private Popup okMenuPopup;
        private FrameworkElement contentLayer;
        private FrameworkElement bottomCanvas;

        public AlertBox()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnLoaded;
            VisualStateManager.GoToState(this, "Loading", true);
        }

        private void setAutoClose(bool isAutoClose)
        {
            if (isAutoClose)
            {
                closeTimer.Value.Interval = TimeSpan.FromSeconds(5d);
                EventHandler handler = null;
                handler = (o, arg0) =>
                {
                    if (AutoClose)
                    {
                        closeTimer.Value.Tick -= handler;
                        closeTimer.Value.Stop();
                        gotoExit();
                    }
                };
                closeTimer.Value.Tick += handler;
                closeTimer.Value.Start();
            }
            else
            {
                if (closeTimer.Value.IsEnabled)
                {
                    closeTimer.Value.Stop();
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ensureUnSubcribe();
            okBtn = GetTemplateChild("PART_OKBtn") as System.Windows.Controls.Button;
            cancelBtn = GetTemplateChild("PART_CancelBtn") as System.Windows.Controls.Button;
            titleArea = GetTemplateChild("PART_TitleArea") as FrameworkElement;
            okMenusLayer = GetTemplateChild("PART_OkMenusLayer") as System.Windows.Controls.ListBox;
            okMenuPopup = GetTemplateChild("PART_OKMenuPopup") as Popup;
            contentLayer = GetTemplateChild("PART_ContentLayer") as FrameworkElement;
            bottomCanvas = GetTemplateChild("PART_BottomCanvas") as FrameworkElement;
            ensureSubcribe();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                okBtnClick(this, new RoutedEventArgs());
                e.Handled = true;
            }
        }

        private void ensureSubcribe()
        {
            if (okBtn != null) okBtn.Click += okBtnClick;
            if (cancelBtn != null) cancelBtn.Click += cancelBtnClick;
            if (titleArea != null) titleArea.PreviewMouseDown += titleAreaPreviewMouseDown;
            if (okMenusLayer != null) okMenusLayer.SelectionChanged += okMenusSelectionChanged;
            if (contentLayer != null && AutoClose) contentLayer.PreviewMouseDown += onPreviewContentMouseDown;
        }

        private void ensureUnSubcribe()
        {
            if (okBtn != null) okBtn.Click -= okBtnClick;
            if (cancelBtn != null) cancelBtn.Click -= cancelBtnClick;
            if (titleArea != null) titleArea.PreviewMouseDown -= titleAreaPreviewMouseDown;
            if (okMenusLayer != null) okMenusLayer.SelectionChanged -= okMenusSelectionChanged;
            if (contentLayer != null) contentLayer.PreviewMouseDown -= onPreviewContentMouseDown;
        }

        #region Event handelr

        private void titleAreaPreviewMouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void onPreviewContentMouseDown(object sender, MouseButtonEventArgs e)
        {
            Result = AlertResult.Click;
            gotoExit();
            e.Handled = true;
        }

        private void okBtnClick(object sender, RoutedEventArgs e)
        {
            if (OKMenus != null && OKMenus.Count > 0)
            {
                okMenuPopup.IsOpen = true;
            }
            else
            {
                Result = AlertResult.OK;
                gotoExit();
            }
        }

        private void cancelBtnClick(object sender, RoutedEventArgs e)
        {
            Result = AlertResult.Cancel;
            gotoExit();
        }

        private void okMenusSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedIndex = okMenusLayer.SelectedIndex;
            Result = AlertResult.OK;
            okMenuPopup.IsOpen = false;
            gotoExit();
        }

        private async void gotoExit()
        {
            ensureUnSubcribe();
            VisualStateManager.GoToState(this, "UnLoading", true);
            await System.Threading.Tasks.Task.Delay(300);
            this.Close();
        }

        #endregion

    }
}
