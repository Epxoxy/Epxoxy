using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Epxoxy.Controls
{
    public class PopupBox : ItemsControl
    {
        private static RoutedCommand closeCommand;
        public static RoutedCommand CloseCommand => closeCommand;

        private static void InitializeCommand()
        {
            closeCommand = new RoutedCommand("CloseCommand", typeof(PopupBox));
            CommandManager.RegisterClassCommandBinding(typeof(PopupBox), new CommandBinding(CloseCommand, ExecuteCloseCommand));
        }

        private static void ExecuteCloseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var box = sender as PopupBox;
            if (null != box && box.IsOpen) box.IsOpen = false;
        }

        static PopupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupBox), new FrameworkPropertyMetadata(typeof(PopupBox)));
            ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(PopupBox),
                new PropertyMetadata(null, OnContentChanged));
            IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(PopupBox),
                 new FrameworkPropertyMetadata(false,
                     FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                     OnIsOpenChanged));
            InitializeCommand();
        }

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = (PopupBox)d;
            box.OnContentChanged(e.OldValue, e.NewValue);
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly DependencyProperty IsOpenProperty;

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty;

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = (PopupBox)d;
            box.ChangeOpenState((bool)e.NewValue);
        }

        protected virtual void OnContentChanged(object oldContent, object newContent)
        {
            // Remove the old content child
            RemoveLogicalChild(oldContent);
            // Add the new content child
            AddLogicalChild(newContent);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            popBox = GetTemplateChild("PART_Popup") as Popup;
            itemPresenter = GetTemplateChild("PART_ItemsPresenter") as ItemsPresenter;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            this.IsOpen = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            this.IsOpen = false;
            base.OnMouseLeave(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            if (null != e.Source && Items.Contains(e.Source))
            {
                var element = e.Source as FrameworkElement;
                if (null != element && element.IsMouseOver && element.IsEnabled)
                {
                    var point = Mouse.GetPosition(element);
                    if (point.X < 0 || point.Y < 0 || point.X > element.ActualWidth || point.Y > element.ActualHeight)
                        return;
                    this.IsOpen = false;
                }
            }
        }

        private bool _finalIsOpen = false;
        private void ChangeOpenState(bool isOpen)
        {
            _finalIsOpen = isOpen;
            if (isOpen)
            {
                popBox.IsOpen = isOpen;
                if (itemPresenter != null)
                    AnimationItemView2(this, itemPresenter.ActualHeight);
            }
            else
            {
                if (itemPresenter != null)
                {
                    AnimationItemView2(this, itemPresenter.ActualHeight, false, new EventHandler(OnAnimationOutCompleted));
                }
                else popBox.IsOpen = false;
            }
            System.Diagnostics.Debug.WriteLine("ChangeOpenState " + isOpen);
        }

        private void OnAnimationOutCompleted(object sender, EventArgs e)
        {
            if (!_finalIsOpen)
            {
                System.Diagnostics.Debug.WriteLine("PopBox OnAnimationOutCompleted");
                popBox.IsOpen = false;
            }
        }

        public static void AnimationItemView(ItemsControl itemscontrol, double totalHeight, bool isfadein = true, EventHandler onCompleted = null)
        {
            var next = new Point();
            var items = itemscontrol.ItemContainerGenerator.Items;
            var easingFunc = new CubicEase() { EasingMode = EasingMode.EaseOut };
            var duration = TimeSpan.FromMilliseconds(200d);
            double itemHeight = 0d;
            for (int i = 0; i < items.Count; i++)
            {
                var element = itemscontrol.ItemContainerGenerator.ContainerFromItem(items[i]) as FrameworkElement;
                if (element != null)
                {
                    itemHeight = element.RenderSize.Height;
                    next.Y += itemHeight;
                    var transform = element.RenderTransform as TranslateTransform;
                    if (transform == null)
                        element.RenderTransform = transform = new TranslateTransform();
                    transform.BeginAnimation(TranslateTransform.YProperty, null);
                    //Generate base data
                    var desireY = next.Y;
                    var max = totalHeight - desireY + itemHeight;
                    var origin = isfadein ? max : 0;
                    var target = isfadein ? 0 : max;
                    //Create animation
                    transform.Y = origin;
                    var animation = new DoubleAnimation(target, duration)
                    {
                        EasingFunction = easingFunc
                    };
                    if (i == 0 && onCompleted != null)
                    {
                        animation.Completed += onCompleted;
                    }
                    transform.BeginAnimation(TranslateTransform.YProperty, animation);
                }
            }
        }

        public static void AnimationItemView2(ItemsControl itemscontrol, double totalHeight, bool showItems = true, EventHandler onCompleted = null)
        {
            var next = new Point();
            var translateEase = new CubicEase() { EasingMode = EasingMode.EaseOut };
            var scaleEase = new ExponentialEase() { EasingMode = EasingMode.EaseInOut };
            var wholeduration = TimeSpan.FromMilliseconds(200d);
            var shortduration = TimeSpan.FromMilliseconds(20d);
            var items = itemscontrol.ItemContainerGenerator.Items;
            var shortIndex = items.Count - 4;
            var fromScale = showItems ? 0 : 1;
            var toScale = 1 - fromScale;
            double itemHeight = 0d;
            for (int i = 0; i < items.Count; i++)
            {
                var element = itemscontrol.ItemContainerGenerator.ContainerFromItem(items[i]) as FrameworkElement;
                if (element != null)
                {
                    ScaleTransform scale = null;
                    TranslateTransform translate = null;
                    var transformGroup = element.RenderTransform as TransformGroup;
                    if (transformGroup == null)
                    {
                        scale = new ScaleTransform(1, 1);
                        translate = new TranslateTransform();
                        element.RenderTransform = transformGroup = new TransformGroup()
                        {
                            Children = new TransformCollection(new Transform[] { scale, translate })
                        };
                        element.RenderTransformOrigin = new Point(0.5, 0);
                    }
                    else
                    {
                        scale = transformGroup.Children[0] as ScaleTransform;
                        translate = transformGroup.Children[1] as TranslateTransform;
                    }
                    itemHeight = element.RenderSize.Height;
                    next.Y += itemHeight;
                    //Generate base data
                    var desireY = next.Y;
                    var max = totalHeight - desireY + itemHeight;
                    var origin = showItems ? max : 0;
                    var toTranslate = showItems ? 0 : max;
                    var animation = new DoubleAnimation(toTranslate, wholeduration)
                    {
                        EasingFunction = translateEase
                    };
                    if (i == 0 && onCompleted != null)
                    {
                        animation.Completed += onCompleted;
                    }
                    var scaleX = new DoubleAnimation(fromScale, toScale,
                        showItems && i >= shortIndex ? shortduration : wholeduration)
                    {
                        EasingFunction = scaleEase
                    };
                    var scaleY = new DoubleAnimation(fromScale, toScale,
                        showItems && i >= shortIndex ? shortduration : wholeduration)
                    {
                        EasingFunction = scaleEase
                    };
                    scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleX);
                    scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleY);
                    translate.BeginAnimation(TranslateTransform.YProperty, null);
                    //Create animation
                    translate.Y = origin;
                    translate.BeginAnimation(TranslateTransform.YProperty, animation);
                }
            }
        }

        private ItemsPresenter itemPresenter;
        private Popup popBox;
    }
}
