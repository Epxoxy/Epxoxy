using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Epxoxy.Controls
{
    /// <summary>
    /// Interaction logic for MessageDialog.xaml
    /// </summary>
    public partial class MessageDialog : Window
    {
        public bool HideCloseButton
        {
            get { return (bool)GetValue(HideCloseButtonProperty); }
            set { SetValue(HideCloseButtonProperty, value); }
        }

        public static readonly DependencyProperty HideCloseButtonProperty =
            DependencyProperty.Register("HideCloseButton", typeof(bool), typeof(MessageDialog), new PropertyMetadata(true, OnHideCloseButtonChanged));

        private static void OnHideCloseButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dialog = d as MessageDialog;
            if (dialog != null) dialog.UpdateCloseButtonVisibility();
        }

        public string TypeTitle
        {
            get { return (string)GetValue(TypeTitleProperty); }
            set { SetValue(TypeTitleProperty, value); }
        }

        public static readonly DependencyProperty TypeTitleProperty =
            DependencyProperty.Register("TypeTitle", typeof(string), typeof(MessageDialog), new PropertyMetadata("Message"));

        public string PrimaryButtonText
        {
            get { return (string)GetValue(PrimaryButtonTextProperty); }
            set { SetValue(PrimaryButtonTextProperty, value); }
        }

        public static readonly DependencyProperty PrimaryButtonTextProperty =
            DependencyProperty.Register("PrimaryButtonText", typeof(string), typeof(MessageDialog), new PropertyMetadata("OK"));

        public string SecondaryButtonText
        {
            get { return (string)GetValue(SecondaryButtonTextProperty); }
            set { SetValue(SecondaryButtonTextProperty, value); }
        }

        public static readonly DependencyProperty SecondaryButtonTextProperty =
            DependencyProperty.Register("SecondaryButtonText", typeof(string), typeof(MessageDialog), new PropertyMetadata("Cancel"));

        public new DialogResult DialogResult
        {
            get { return (DialogResult)GetValue(DialogResultProperty); }
            set { SetValue(DialogResultProperty, value); }
        }

        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.Register("DialogResult", typeof(DialogResult), typeof(MessageDialog), new PropertyMetadata(default(DialogResult)));

        public DialogResult ResultOnPressEnter
        {
            get { return (DialogResult)GetValue(ResultOnPressEnterProperty); }
            set { SetValue(ResultOnPressEnterProperty, value); }
        }

        public static readonly DependencyProperty ResultOnPressEnterProperty =
            DependencyProperty.Register("ResultOnPressEnter", typeof(DialogResult), typeof(MessageDialog), new PropertyMetadata(DialogResult.Primary));

        private Label title;
        private Button closeButton;
        private Button primaryButton;
        private Button secondaryButton;
        private Border border;
        private ContentPresenter content;

        public MessageDialog(Window win)
        {
            InitializeComponent();
            if (win != null)
            {
                this.Owner = win;
            }
            this.Loaded += OnLoaded;
        }
        public MessageDialog() : this(Application.Current.MainWindow)
        {

        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            if (border != null)
            {
                border.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                Size desiredSize = border.DesiredSize;
                this.Height = desiredSize.Height >= this.MaxHeight ? MaxHeight : desiredSize.Height;
                this.Width = desiredSize.Width >= this.MaxWidth ? MaxHeight : desiredSize.Width;
                AdjustWindowLocation();
            }
        }

        //For some reason, use sizetocontent case problem
        //so calculate by self
        private void AdjustWindowLocation()
        {
            if (this.Owner != null)
            {
                if (WindowStartupLocation != WindowStartupLocation.Manual)
                    WindowStartupLocation = WindowStartupLocation.Manual;
                this.Top = (this.Owner.ActualHeight - this.ActualHeight) / 2 + this.Owner.Top;
                this.Left = (this.Owner.ActualWidth - this.ActualWidth) / 2 + this.Owner.Left;
                System.Diagnostics.Debug.WriteLine(this.Top + ", " + this.Left);
            }
        }
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //ensure unsubscribe handlers
            this.UnsubscribeHandlers();
            //Get template children
            this.title = GetTemplateChild("PART_Title") as Label;
            this.closeButton = GetTemplateChild("PART_CloseButton") as Button;
            this.primaryButton = GetTemplateChild("PART_PrimaryButton") as Button;
            this.secondaryButton = GetTemplateChild("PART_SecondaryButton") as Button;
            this.border = GetTemplateChild("PART_Border") as Border;
            this.content = GetTemplateChild("PART_Content") as ContentPresenter;
            if (border != null)
            {
                border.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                Size desiredSize = border.DesiredSize;
                this.Height = desiredSize.Height >= this.MaxHeight ? MaxHeight : desiredSize.Height;
                this.Width = desiredSize.Width >= this.MaxWidth ? MaxHeight : desiredSize.Width;
                AdjustWindowLocation();
            }
            //subscribe handlers
            this.SubscribeHandler();
            //Update visibility
            UpdateCloseButtonVisibility();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Activated -= OnDialogActivated;
            this.Deactivated -= OnDialogDeactivated;
            this.UnsubscribeHandlers();
            Keyboard.ClearFocus();
            base.OnClosing(e);
        }

        private void SubscribeHandler()
        {
            if (title != null) title.MouseDown += OnTitleAreaDrag;
            if (closeButton != null) closeButton.Click += OnCloseBtnClick;
            if (primaryButton != null) primaryButton.Click += OnPrimaryButtonClick;
            if (secondaryButton != null) secondaryButton.Click += OnSecondaryButtonClick;
        }

        private void UnsubscribeHandlers()
        {
            if (title != null) title.MouseDown -= OnTitleAreaDrag;
            if (closeButton != null) closeButton.Click -= OnCloseBtnClick;
            if (primaryButton != null) primaryButton.Click -= OnPrimaryButtonClick;
            if (secondaryButton != null) secondaryButton.Click -= OnSecondaryButtonClick;
        }

        private void UpdateCloseButtonVisibility()
        {
            if (this.closeButton != null)
            {
                this.closeButton.Visibility = HideCloseButton ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnLoaded;
            Keyboard.Focus(this);
            this.Activated += OnDialogActivated;
            this.Deactivated += OnDialogDeactivated;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Enter)
            {
                if (ResultOnPressEnter == DialogResult.Primary)
                {
                    if (primaryButton != null)
                        primaryButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, primaryButton));
                    else OnPrimaryButtonClick(this, new RoutedEventArgs());
                }
                else if (ResultOnPressEnter == DialogResult.Secondary)
                {
                    if (secondaryButton != null)
                        secondaryButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, secondaryButton));
                    else OnSecondaryButtonClick(this, new RoutedEventArgs());
                }
                else
                {
                    if (closeButton != null)
                        closeButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, closeButton));
                    else OnCloseBtnClick(this, new RoutedEventArgs());
                }
            }
            else if (e.Key == Key.Escape || e.Key == Key.Back)
            {
                if (closeButton != null)
                    closeButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, closeButton));
                else OnCloseBtnClick(this, new RoutedEventArgs());
            }
        }

        private void OnDialogActivated(object sender, EventArgs e)
        {
            if (title != null) title.Foreground = Brushes.Black;
            //this.BorderBrush.Opacity = 1;
        }

        private void OnDialogDeactivated(object sender, EventArgs e)
        {
            if (title != null) title.Foreground = Brushes.LightGray;
            //this.BorderBrush.Opacity = 0;
        }

        private void OnTitleAreaDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void OnCloseBtnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = DialogResult.Dismiss;
            this.Close();
        }

        private void OnPrimaryButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = DialogResult.Primary;
            this.Close();
        }

        private void OnSecondaryButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = DialogResult.Secondary;
            this.Close();
        }
    }
}
