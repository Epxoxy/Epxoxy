using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Epxoxy.Controls
{
    public class Toast : Control, INotified
    {
        static Toast()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Toast), new FrameworkPropertyMetadata(typeof(Toast)));
            initializeCommand();
        }

        private static RoutedCommand removeToastCommand;
        public static RoutedCommand RemoveToastCommand => removeToastCommand;
        private static RoutedCommand notifiedCommand;
        public static RoutedCommand NotifiedCommand => notifiedCommand;

        private static void initializeCommand()
        {
            removeToastCommand = new RoutedCommand("RemoveToastCommand", typeof(Toast));
            CommandManager.RegisterClassCommandBinding(typeof(Toast), new CommandBinding(RemoveToastCommand, ExecuteRemoveNotification));
            notifiedCommand = new RoutedCommand("NotifiedCommand", typeof(Toast));
            CommandManager.RegisterClassCommandBinding(typeof(Toast), new CommandBinding(NotifiedCommand, ExecuteNotified));
        }
        private static void ExecuteRemoveNotification(object sender, ExecutedRoutedEventArgs e)
        {
            var toast = sender as Toast;
            if (toast != null) toast.UpdateToast(true);
        }
        private static void ExecuteNotified(object sender, ExecutedRoutedEventArgs e)
        {
            var toast = sender as Toast;
            if (toast != null)
            {
                var obj = e.Parameter;
                INotified iNotified;
                if (obj is string) iNotified = new ToastItem()
                {
                    ToastContent = obj,
                    Thumb = new Rectangle()
                    {
                        Height = 30,
                        Width = 30,
                        Fill = Brushes.SkyBlue
                    }
                };
                else iNotified = (e.Parameter as INotified);
                toast.AddNotifiedItem(iNotified);
            }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }
        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            private set { SetValue(IsPressedProperty, value); }
        }

        public INotified NotifiedItem
        {
            get { return (INotified)GetValue(NotifiedItemProperty); }
            set { SetValue(NotifiedItemProperty, value); }
        }
        public object ToastContent
        {
            get { return (object)GetValue(ToastContentProperty); }
            set { SetValue(ToastContentProperty, value); }
        }
        public string ToastTitle
        {
            get { return (string)GetValue(ToastTitleProperty); }
            set { SetValue(ToastTitleProperty, value); }
        }
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public object Thumb
        {
            get { return (object)GetValue(ThumbProperty); }
            set { SetValue(ThumbProperty, value); }
        }
        public DateTime NotifiedTime
        {
            get { return (DateTime)GetValue(NotifiedTimeProperty); }
            set { SetValue(NotifiedTimeProperty, value); }
        }
        public BindingList<INotified> DismissList
        {
            get { return (BindingList<INotified>)GetValue(DismissListProperty); }
            private set { SetValue(DismissListProperty, value); }
        }
        public TimeSpan DismissTime
        {
            get { return (TimeSpan)GetValue(DismissTimeProperty); }
            set { SetValue(DismissTimeProperty, value); }
        }
        public int MaxDismissSize
        {
            get { return (int)GetValue(MaxDismissSizeProperty); }
            set { SetValue(MaxDismissSizeProperty, value); }
        }
        
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(Toast), new PropertyMetadata(null));
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(Toast), new PropertyMetadata(null));
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(Toast), new PropertyMetadata(null));
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register("IsPressed", typeof(bool), typeof(Toast), new PropertyMetadata(false));
        public static readonly DependencyProperty ToastContentProperty =
            DependencyProperty.Register("ToastContent", typeof(object), typeof(Toast), new PropertyMetadata(null));
        public static readonly DependencyProperty ToastTitleProperty =
            DependencyProperty.Register("ToastTitle", typeof(string), typeof(Toast), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(Toast), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty ThumbProperty =
            DependencyProperty.Register("Thumb", typeof(object), typeof(Toast), new PropertyMetadata(null));
        public static readonly DependencyProperty NotifiedTimeProperty =
            DependencyProperty.Register("NotifiedTime", typeof(DateTime), typeof(Toast), new PropertyMetadata(null));
        public static readonly DependencyProperty NotifiedItemProperty =
            DependencyProperty.Register("NotifiedItem", typeof(INotified), typeof(Toast), new PropertyMetadata(null, OnNotifiedContentChanged));
        public static readonly DependencyProperty DismissTimeProperty =
            DependencyProperty.Register("DismissTime", typeof(TimeSpan), typeof(Toast), new PropertyMetadata(TimeSpan.FromSeconds(5d)));
        public static readonly DependencyProperty DismissListProperty =
            DependencyProperty.Register("DismissList", typeof(BindingList<INotified>), typeof(Toast), new PropertyMetadata(new BindingList<INotified>()));
        public static readonly DependencyProperty MaxDismissSizeProperty =
            DependencyProperty.Register("MaxDismissSize", typeof(int), typeof(Toast), new PropertyMetadata(10, OnMaxDismissSizeChanged));

        private static void OnMaxDismissSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var toast = d as Toast;
            if (toast != null)
            {
                var size = (int)e.NewValue;
                toast.maxDismissSizeCache = size;
                while(toast.DismissList.Count > size)
                {
                    toast.DismissList.RemoveAt(0);
                }
            }
        }

        private static void OnNotifiedContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var toast = d as Toast;
            if (toast != null) toast.AddNotifiedItem(e.NewValue as INotified);
        }

        #region Timer

        private void EnsureTimer()
        {
            if (!IsTimerUseless && timer == null)
            {
                Helpers.DebugHelper.debugWrite(this, "New Toast Timer");
                timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = DismissTime;
                timer.Tick += OnTimerTick;
                timer.Start();
            }
        }

        private void EnsureReleaseTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                if (IsTimerUseless)
                {
                    timer.Tick -= OnTimerTick;
                    timer = null;
                    Helpers.DebugHelper.debugWrite(this, "Ensure Release Timer");
                }
                else
                {
                    timer.Start();
                }
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            UpdateToast();
            Helpers.DebugHelper.debugWrite(this, "Toast Timer Tick");
        }

        private System.Windows.Threading.DispatcherTimer timer;
        #endregion

        #region Update Toast

        private void AddNotifiedItem(INotified newItem)
        {
            if (newItem == null) return;
            waitingList.Add(newItem);

            if (NotifingItem == null) UpdateToast();
            else EnsureTimer();
        }

        private async void UpdateToast(bool isRead = false)
        {
            var iNotified = NextINotified;
            if (NotifingItem != null)
            {
                VisualStateManager.GoToState(this, "Removing", true);
                if (!isRead && maxDismissSizeCache > 0)
                {
                    DismissList.Add(NotifingItem);
                    if (DismissList.Count > maxDismissSizeCache)
                        DismissList.RemoveAt(0);
                }
            }
            await System.Threading.Tasks.Task.Delay(250);
            NotifingItem = iNotified;
            //Check and update notifing item
            if (NotifingItem != null)
            {
                Helpers.DebugHelper.debugWrite(this, "Updating");
                if (waitingList.Contains(NotifingItem))
                    waitingList.Remove(NotifingItem);

                ToastContent = NotifingItem.ToastContent;
                ToastTitle = NotifingItem.ToastTitle + " (#" + count++ + ")";
                Description = NotifingItem.Description;
                Thumb = NotifingItem.Thumb;
                NotifiedTime = NotifingItem.NotifiedTime;
                VisualStateManager.GoToState(this, "Initilized", true);
                await System.Threading.Tasks.Task.Delay(350);
            }
            else
            {
                EmptyOld();
                VisualStateManager.GoToState(this, "Hide", true);
            }
            if (timer == null) EnsureTimer();
            EnsureReleaseTimer();
        }

        private void EmptyOld()
        {
            ToastContent = null;
            ToastTitle = null;
            Description = null;
            Thumb = null;
            NotifiedTime = DateTime.MinValue;
        }

        private int count;
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            closeBtn = GetTemplateChild("PART_CloseBtn") as System.Windows.Controls.Button;
            VisualStateManager.GoToState(this, "Hide", true);
            this.Unloaded -= OnUnloaded;
            this.Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= OnUnloaded;
            EnsureReleaseTimer();
        }

        protected virtual void OnClick()
        {
            RoutedEventArgs newEvent = new RoutedEventArgs(Toast.ClickEvent, this);
            RaiseEvent(newEvent);
            Command?.Execute(CommandParameter);
            closeBtn?.Command?.Execute(closeBtn.CommandParameter);
            NotifingItem.Command?.Execute(null);
            UpdateToast(true);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            VisualStateManager.GoToState(this, "Pressed", true);
            IsPressed = true;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            VisualStateManager.GoToState(this, "Normal", true);
            if (IsPressed) { OnClick(); IsPressed = false; }
        }


        private int maxDismissSizeCache = 10;
        private INotified NotifingItem { get; set; }
        private System.Windows.Controls.Button closeBtn;
        private bool IsTimerUseless => waitingList.Count < 1 && NotifingItem == null;
        private INotified NextINotified => waitingList.Count > 0 ? waitingList[0] : null;
        private System.Collections.Generic.List<INotified> waitingList = new System.Collections.Generic.List<INotified>();
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Toast));
    }

    public enum ToastState
    {
        New,
        Delete,
        Focus
    }
}
