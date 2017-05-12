using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Epxoxy.Controls
{
    public class Toast : Control, ICommandSource
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
                INotifyItem notifyItem;
                if (obj is string) notifyItem = new ToastItem()
                {
                    ToastContent = obj,
                    Thumb = new Rectangle()
                    {
                        Height = 30,
                        Width = 30,
                        Margin = new Thickness(7,0,7,0),
                        Fill = Brushes.SkyBlue
                    }
                };
                else notifyItem = (e.Parameter as INotifyItem);
                toast.NewerNotifyItem = notifyItem;
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
        
        public object ToastContent
        {
            get { return (object)GetValue(ToastContentProperty); }
            private set { SetValue(ToastContentProperty, value); }
        }
        public string ToastTitle
        {
            get { return (string)GetValue(ToastTitleProperty); }
            private set { SetValue(ToastTitleProperty, value); }
        }
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            private set { SetValue(DescriptionProperty, value); }
        }
        public object Thumb
        {
            get { return (object)GetValue(ThumbProperty); }
            private set { SetValue(ThumbProperty, value); }
        }
        public DateTime NotifyTime
        {
            get { return (DateTime)GetValue(NotifyTimeProperty); }
            private set { SetValue(NotifyTimeProperty, value); }
        }
        public BindingList<INotifyItem> DismissList
        {
            get { return (BindingList<INotifyItem>)GetValue(DismissListProperty); }
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
        public INotifyItem NewerNotifyItem
        {
            get { return (INotifyItem)GetValue(NewerNotifyItemProperty); }
            set { SetValue(NewerNotifyItemProperty, value); }
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
        public static readonly DependencyProperty NotifyTimeProperty =
            DependencyProperty.Register("NotifyTime", typeof(DateTime), typeof(Toast), new PropertyMetadata(null));
        public static readonly DependencyProperty DismissTimeProperty =
            DependencyProperty.Register("DismissTime", typeof(TimeSpan), typeof(Toast), new PropertyMetadata(TimeSpan.FromSeconds(5d)));
        public static readonly DependencyProperty DismissListProperty =
            DependencyProperty.Register("DismissList", typeof(BindingList<INotifyItem>), typeof(Toast), new PropertyMetadata(new BindingList<INotifyItem>()));
        public static readonly DependencyProperty MaxDismissSizeProperty =
            DependencyProperty.Register("MaxDismissSize", typeof(int), typeof(Toast), new PropertyMetadata(10, OnMaxDismissSizeChanged));
        public static readonly DependencyProperty NewerNotifyItemProperty =
            DependencyProperty.Register("NewerNotifyItem", typeof(INotifyItem), typeof(Toast), new PropertyMetadata(null, OnNewerNotifyItemChanged));
        
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

        private static void OnNewerNotifyItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var toast = d as Toast;
            if (toast != null) toast.AddNotifyItem(e.NewValue as INotifyItem);
        }

        public void ToastMessage(string title, string message)
        {
            var toastitem = new ToastItem()
            {
                ToastTitle = title,
                ToastContent = new TextBlock()
                {
                    Text = message,
                    TextWrapping = TextWrapping.Wrap
                },
            };
            this.NewerNotifyItem = toastitem;
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

        private void AddNotifyItem(INotifyItem newItem)
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
                ToastTitle = NotifingItem.ToastTitle;
                Description = NotifingItem.Description;
                Thumb = NotifingItem.Thumb;
                NotifyTime = System.DateTime.Now;
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
            NotifyTime = DateTime.MinValue;
        }
        
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
        private INotifyItem NotifingItem { get; set; }
        private System.Windows.Controls.Button closeBtn;
        private bool IsTimerUseless => waitingList.Count < 1 && NotifingItem == null;
        private INotifyItem NextINotified => waitingList.Count > 0 ? waitingList[0] : null;
        private System.Collections.Generic.List<INotifyItem> waitingList = new System.Collections.Generic.List<INotifyItem>();
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Toast));
    }

    public enum ToastState
    {
        New,
        Delete,
        Focus
    }
}
