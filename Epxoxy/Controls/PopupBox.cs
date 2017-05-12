using Epxoxy.Helpers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Epxoxy.Controls
{
    public enum PopMode
    {
        MouseEnterLeave,
        Custom
    }

    public class PopupBox : ContentControl
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
            PopupButtonProperty = DependencyProperty.Register("PopupButton", typeof(ButtonBase), typeof(PopupBox),
                new PropertyMetadata(null, OnPopupButtonChanged));
            IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(PopupBox),
                 new FrameworkPropertyMetadata(false,
                     FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                     OnIsOpenChanged));
            PopModeProperty = DependencyProperty.Register("PopMode", typeof(PopMode), typeof(PopupBox),
                new PropertyMetadata(PopMode.MouseEnterLeave, OnPopModeChanged));
            InitializeCommand();
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly DependencyProperty IsOpenProperty;

        public ButtonBase PopupButton
        {
            get { return (ButtonBase)GetValue(PopupButtonProperty); }
            set { SetValue(PopupButtonProperty, value); }
        }
        public static readonly DependencyProperty PopupButtonProperty;

        public PopMode PopMode
        {
            get { return (PopMode)GetValue(PopModeProperty); }
            set { SetValue(PopModeProperty, value); }
        }
        public static readonly DependencyProperty PopModeProperty;

        private static void OnPopupButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = (PopupBox)d;
            box.RemoveLogicalChild(e.OldValue);
            box.AddLogicalChild(e.NewValue);
        }
        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = (PopupBox)d;
            box.ChangeOpenState((bool)e.NewValue);
        }
        private static void OnPopModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = (PopupBox)d;
            box.popMode = (PopMode)e.NewValue;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            popup = GetTemplateChild("PART_Popup") as Popup;
            contentPresenter = GetTemplateChild("PART_ContentPresenter") as ContentPresenter;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (popMode == PopMode.MouseEnterLeave)
                this.IsOpen = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (popMode == PopMode.MouseEnterLeave)
                this.IsOpen = false;
            base.OnMouseLeave(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            /*if (null != e.Source && Items.Contains(e.Source))
            {
                var element = e.Source as FrameworkElement;
                if (null != element && element.IsMouseOver && element.IsEnabled)
                {
                    var point = Mouse.GetPosition(element);
                    if (point.X < 0 || point.Y < 0 || point.X > element.ActualWidth || point.Y > element.ActualHeight)
                        return;
                    this.IsOpen = false;
                }
            } */
        }

        private void ChangeOpenState(bool isOpen)
        {
            _finalIsOpen = isOpen;
            if (isOpen)
            {
                popup.IsOpen = isOpen;
                if (contentPresenter != null)
                    AnimationItemIn(contentPresenter);
            }
            else
            {
                if (contentPresenter != null)
                {
                    AnimationItemIn(contentPresenter, true, new EventHandler(OnAnimationOutCompleted));
                }
                else popup.IsOpen = false;
            }
            System.Diagnostics.Debug.WriteLine("ChangeOpenState " + isOpen);
        }

        private void OnAnimationOutCompleted(object sender, EventArgs e)
        {
            if (!_finalIsOpen)
            {
                System.Diagnostics.Debug.WriteLine("PopBox OnAnimationOutCompleted");
                popup.IsOpen = false;
            }
        }

        private void AnimationItemIn(ContentPresenter contentPresenter, bool reverse = false, EventHandler onCompleted = null)
        {
            if (contentPresenter == null || VisualTreeHelper.GetChildrenCount(contentPresenter) != 1)
            {
                onCompleted?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                var controls = contentPresenter.VisualDepthFirstTraversal().OfType<ButtonBase>().Reverse();
                var last = controls.Count() - 1;
                var sineEase = new SineEase { EasingMode = EasingMode.EaseOut };
                int translateFrom = 80;
                var i = 0;
                var zeroKeyTime = KeyTime.FromPercent(0.0);
                foreach (var uiElement in controls)
                {
                    if (uiElement != null)
                    {
                        var deferredStart = i++ * 20;
                        var deferredEnd = deferredStart + 200.0;
                        var deferredStartKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(deferredStart));
                        var deferredEndKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(deferredEnd));
                        var elementTranslateFrom = translateFrom * i;
                        ScaleTransform scaleTransform = null;
                        TranslateTransform translateTransform = null;
                        var transformGroup = uiElement.RenderTransform as TransformGroup;
                        if (transformGroup != null && transformGroup.Children.Count == 2)
                        {
                            scaleTransform = transformGroup.Children[0] as ScaleTransform;
                            translateTransform = transformGroup.Children[1] as TranslateTransform;
                        }
                        //Ensure transform created
                        if (translateTransform == null || scaleTransform == null)
                        {
                            scaleTransform = new ScaleTransform(0, 0);
                            translateTransform = new TranslateTransform { Y = elementTranslateFrom };
                            uiElement.RenderTransform = transformGroup = new TransformGroup()
                            {
                                Children = new TransformCollection(new Transform[] { scaleTransform, translateTransform })
                            };
                            uiElement.SetCurrentValue(RenderTransformOriginProperty, new Point(.5, .5));
                        }
                        //Opacity
                        var opacityAnimation = new DoubleAnimationUsingKeyFrames();
                        opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, zeroKeyTime, sineEase));
                        opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredStartKeyTime, sineEase));
                        opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame((double)uiElement.GetAnimationBaseValue(OpacityProperty), deferredEndKeyTime, sineEase));
                        //ScaleX
                        var scalex = new DoubleAnimationUsingKeyFrames();
                        scalex.KeyFrames.Add(new EasingDoubleKeyFrame(0, zeroKeyTime, sineEase));
                        scalex.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredStartKeyTime, sineEase));
                        scalex.KeyFrames.Add(new EasingDoubleKeyFrame(1, deferredEndKeyTime, sineEase));
                        //ScaleY
                        var scaley = new DoubleAnimationUsingKeyFrames();
                        scaley.KeyFrames.Add(new EasingDoubleKeyFrame(0, zeroKeyTime, sineEase));
                        scaley.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredStartKeyTime, sineEase));
                        scaley.KeyFrames.Add(new EasingDoubleKeyFrame(1, deferredEndKeyTime, sineEase));
                        //TranslateY
                        var translatey = new DoubleAnimationUsingKeyFrames();
                        translatey.KeyFrames.Add(new EasingDoubleKeyFrame(elementTranslateFrom, zeroKeyTime, sineEase));
                        translatey.KeyFrames.Add(new EasingDoubleKeyFrame(elementTranslateFrom, deferredStartKeyTime, sineEase));
                        translatey.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredEndKeyTime, sineEase));
                        //Begin animation
                        Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
                        Storyboard.SetTargetProperty(scalex, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
                        Storyboard.SetTargetProperty(scaley, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
                        Storyboard.SetTargetProperty(translatey, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)"));
                        Storyboard.SetTarget(opacityAnimation, uiElement);
                        Storyboard.SetTarget(scalex, uiElement);
                        Storyboard.SetTarget(scaley, uiElement);
                        Storyboard.SetTarget(translatey, uiElement);
                        //Init storyboard
                        var storyboard = new Storyboard();
                        storyboard.Children.Add(opacityAnimation);
                        storyboard.Children.Add(scalex);
                        storyboard.Children.Add(scaley);
                        storyboard.Children.Add(translatey);
                        //Add handler
                        if (i == last && onCompleted != null)
                        {
                            EventHandler unsubscribe = null;
                            unsubscribe = (sender, e) =>
                            {
                                storyboard.Completed -= unsubscribe;
                                storyboard.Completed -= onCompleted;
                            };
                            storyboard.Completed += onCompleted;
                            storyboard.Completed += unsubscribe;
                        }
                        if (reverse)
                        {
                            storyboard.AutoReverse = true;
                            storyboard.Begin();
                            storyboard.Seek(TimeSpan.FromMilliseconds(deferredEnd));
                            storyboard.Resume();
                        }
                        else
                            storyboard.Begin();
                    }
                }
            }
        }

        private PopMode popMode = PopMode.MouseEnterLeave;
        private ContentPresenter contentPresenter;
        private bool _finalIsOpen = false;
        private Popup popup;
    }
}
