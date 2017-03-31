using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Epxoxy.Controls
{
    [TemplatePart(Name = "PART_PrimaryButton", Type = typeof(System.Windows.Controls.Button))]
    [TemplatePart(Name = "PART_SecondaryButton", Type = typeof(System.Windows.Controls.Button))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(System.Windows.Controls.Button))]
    [TemplatePart(Name = "PART_Canvas", Type = typeof(System.Windows.Controls.Canvas))]
    public class ContentDialog : System.Windows.Controls.ContentControl
    {
        #region Constructor

        static ContentDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentDialog), new FrameworkPropertyMetadata(typeof(ContentDialog)));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(ContentDialog), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
        }

        public ContentDialog() { }
        public ContentDialog(object content)
        {
            this.Content = content;
        }
        public ContentDialog(object content, string title)
        {
            this.Content = content;
            this.Title = title;
        }

        #endregion

        #region Properties

        public string TopTitle
        {
            get { return (string)GetValue(TopTitleProperty); }
            set { SetValue(TopTitleProperty, value); }
        }
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public string PrimaryButtonText
        {
            get { return (string)GetValue(PrimaryButtonTextProperty); }
            set { SetValue(PrimaryButtonTextProperty, value); }
        }
        public string SecondaryButtonText
        {
            get { return (string)GetValue(SecondaryButtonTextProperty); }
            set { SetValue(SecondaryButtonTextProperty, value); }
        }
        public ICommand PrimaryButtonCommand
        {
            get { return (ICommand)GetValue(PrimaryButtonCommandProperty); }
            set { SetValue(PrimaryButtonCommandProperty, value); }
        }
        public ICommand SecondaryButtonCommand
        {
            get { return (ICommand)GetValue(SecondaryButtonCommandProperty); }
            set { SetValue(SecondaryButtonCommandProperty, value); }
        }
        public object PrimaryButtonCommandParameter
        {
            get { return (object)GetValue(PrimaryButtonCommandParameterProperty); }
            set { SetValue(PrimaryButtonCommandParameterProperty, value); }
        }
        public object SecondaryButtonCommandParameter
        {
            get { return (object)GetValue(SecondaryButtonCommandParameterProperty); }
            set { SetValue(SecondaryButtonCommandParameterProperty, value); }
        }
        public IInputElement PrimaryButtonCommandTarget
        {
            get { return (IInputElement)GetValue(PrimaryButtonCommandTargetProperty); }
            set { SetValue(PrimaryButtonCommandTargetProperty, value); }
        }
        public IInputElement SecondaryButtonCommandTarget
        {
            get { return (IInputElement)GetValue(SecondaryButtonCommandTargetProperty); }
            set { SetValue(SecondaryButtonCommandTargetProperty, value); }
        }
        public Brush TitleBrush
        {
            get { return (Brush)GetValue(TitleBrushProperty); }
            set { SetValue(TitleBrushProperty, value); }
        }
        public object BottomToolContent
        {
            get { return (object)GetValue(BottomToolContentProperty); }
            set { SetValue(BottomToolContentProperty, value); }
        }


        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ContentDialog), new PropertyMetadata("Message"));
        public static readonly DependencyProperty PrimaryButtonTextProperty =
            DependencyProperty.Register("PrimaryButtonText", typeof(string), typeof(ContentDialog), new PropertyMetadata("OK"));
        public static readonly DependencyProperty SecondaryButtonTextProperty =
            DependencyProperty.Register("SecondaryButtonText", typeof(string), typeof(ContentDialog), new PropertyMetadata("Cancel"));
        public static readonly DependencyProperty PrimaryButtonCommandProperty =
            DependencyProperty.Register("PrimaryButtonCommand", typeof(ICommand), typeof(ContentDialog), new PropertyMetadata(null));
        public static readonly DependencyProperty SecondaryButtonCommandProperty =
            DependencyProperty.Register("SecondaryButtonCommand", typeof(ICommand), typeof(ContentDialog), new PropertyMetadata(null));
        public static readonly DependencyProperty PrimaryButtonCommandParameterProperty =
            DependencyProperty.Register("PrimaryButtonCommandParameter", typeof(object), typeof(ContentDialog), new PropertyMetadata(null));
        public static readonly DependencyProperty SecondaryButtonCommandParameterProperty =
            DependencyProperty.Register("SecondaryButtonCommandParameter", typeof(object), typeof(ContentDialog), new PropertyMetadata(null));
        public static readonly DependencyProperty PrimaryButtonCommandTargetProperty =
            DependencyProperty.Register("PrimaryButtonCommandTarget", typeof(IInputElement), typeof(ContentDialog), new PropertyMetadata(null));
        public static readonly DependencyProperty SecondaryButtonCommandTargetProperty =
            DependencyProperty.Register("SecondaryButtonCommandTarget", typeof(IInputElement), typeof(ContentDialog), new PropertyMetadata(null));
        public static readonly DependencyProperty TopTitleProperty =
            DependencyProperty.Register("TopTitle", typeof(string), typeof(ContentDialog), new PropertyMetadata("Message"));
        public static readonly DependencyProperty BottomToolContentProperty =
            DependencyProperty.Register("BottomToolContent", typeof(object), typeof(ContentDialog), new PropertyMetadata(null));
        public static readonly DependencyProperty TitleBrushProperty =
            DependencyProperty.Register("TitleBrush", typeof(Brush), typeof(ContentDialog), new PropertyMetadata(Brushes.White));

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            primaryButton = GetTemplateChild("PART_PrimaryButton") as System.Windows.Controls.Button;
            secondButton = GetTemplateChild("PART_SecondaryButton") as System.Windows.Controls.Button;
            closeButton = GetTemplateChild("PART_CloseButton") as System.Windows.Controls.Button;
            unloadingStoryboard = GetTemplateChild("UnloadingStoryboard") as Storyboard;
        }
        private Storyboard unloadingStoryboard;
        #endregion

        private Task WaitForLoadedAsync()
        {
            var tcsOfLoaded = new TaskCompletionSource<object>();
            if (this.IsLoaded)
            {
                VisualStateManager.GoToState(this, "Loading", true);
                tcsOfLoaded.TrySetResult(null);
            }
            else
            {
                RoutedEventHandler handler = null;
                handler = (sender, e) =>
                {
                    this.Loaded -= handler;
                    VisualStateManager.GoToState(this, "Loading", true);
                    tcsOfLoaded.TrySetResult(null);
                };
                this.Loaded += handler;
            }
            return tcsOfLoaded.Task;
        }

        private Task WaitForCloseAsync()
        {
            TaskCompletionSource<object> tcsOfClose = new TaskCompletionSource<object>();
            if (unloadingStoryboard == null)
            {
                tcsOfClose.TrySetResult(null);
            }
            else
            {
                EventHandler handler = null;
                unloadingStoryboard.Completed += handler = (sender, e) =>
                {
                    unloadingStoryboard.Completed -= handler;
                    tcsOfClose.TrySetResult(null);
                };
            }
            VisualStateManager.GoToState(this, "Unloading", true);
            return tcsOfClose.Task;
        }

        public Task<DialogResult> ShowAsync()
        {
            var winRoot = (Application.Current.MainWindow.Content as FrameworkElement);
            return ShowAsync(winRoot);
        }

        public Task<DialogResult> ShowAsync(FrameworkElement dialogContainer)
        {
            if (dialogContainer == null)
                throw new ArgumentNullException("Dialog Container Must Be Not Null!");
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(dialogContainer);
            if (layer == null)
                throw new ArgumentNullException("Dialog Container Must Have AdornerLayer!");
            //Add this to adorner
            CustomAdorner adorner = null;
            Dispatcher.BeginInvoke(new Action(() =>
            {
                adorner = new CustomAdorner(dialogContainer, this);
                layer.Add(adorner);
                Keyboard.Focus(this);
            }));
            //Wait for loaded
            return WaitForLoadedAsync().ContinueWith(x =>
            {
                //Wait for ContentDialog button click
                var tcsOfResult = new TaskCompletionSource<DialogResult>();
                //Initilize event handler
                Func<object, RoutedEventArgs, DialogResult, Task> cleanExitWith = null;
                RoutedEventHandler primaryHandler = null, secondaryHandler = null, closeHandler = null;
                cleanExitWith = (sender, e, result) =>
                {
                    var taskOfClosed = WaitForCloseAsync();
                    return taskOfClosed.ContinueWith(a =>
                    {
                        //UnRegister Event Handler
                        if (primaryButton != null) primaryButton.Click -= primaryHandler;
                        if (secondButton != null) secondButton.Click -= secondaryHandler;
                        if (closeButton != null) closeButton.Click -= closeHandler;
                        //Invoke Closing Event Handler
                        Closing?.Invoke(sender, e);
                        dialogContainer.Dispatcher.Invoke(() =>
                        {
                            adorner.DisconnectChild();
                            //Remove ContentDialog
                            if (layer != null) layer.Remove(adorner);
                            Keyboard.ClearFocus();
                        });
                        tcsOfResult.TrySetResult(result);
                    });
                };
                primaryHandler = (sender, e) => { cleanExitWith(sender, e, DialogResult.Primary); };
                secondaryHandler = (sender, e) => { cleanExitWith(sender, e, DialogResult.Secondary); };
                closeHandler = (sender, e) => { cleanExitWith(sender, e, DialogResult.Dismiss); };
                //Delegate
                if (primaryButton != null) primaryButton.Click += primaryHandler;
                if (secondButton != null) secondButton.Click += secondaryHandler;
                if (closeButton != null) closeButton.Click += closeHandler;
                return tcsOfResult.Task;
            }).Unwrap();
        }
        
        private System.Windows.Controls.Button primaryButton;
        private System.Windows.Controls.Button secondButton;
        private System.Windows.Controls.Button closeButton;
        public event EventHandler<RoutedEventArgs> Closing;
    }
}