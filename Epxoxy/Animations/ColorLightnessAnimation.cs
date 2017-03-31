using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Epxoxy.Helpers;

namespace Epxoxy.Animations
{
    public class ColorLightnessAnimation : ColorAnimationBase
    {
        private Double[] _keyvalues;
        private bool _isAnimationFunctionValid;
        static ColorLightnessAnimation()
        {
            PropertyChangedCallback propCallback = new PropertyChangedCallback(AnimationFunctionChanged);
            OriginColorProperty = DependencyProperty.Register("OriginColor", typeof(Color), typeof(ColorLightnessAnimation), new PropertyMetadata(null));
            ToProperty = DependencyProperty.Register("To", typeof(double), typeof(ColorLightnessAnimation), new PropertyMetadata(0d, propCallback));
            FromProperty = DependencyProperty.Register("From", typeof(double), typeof(ColorLightnessAnimation), new PropertyMetadata(0d, propCallback));
            ByProperty = DependencyProperty.Register("By", typeof(double), typeof(ColorLightnessAnimation), new PropertyMetadata(0d, propCallback));
            EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(ColorLightnessAnimation), new PropertyMetadata(null));
        }

        private static void AnimationFunctionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorLightnessAnimation a = (ColorLightnessAnimation)d;
            a._isAnimationFunctionValid = false;
        }

        #region Constructors

        public ColorLightnessAnimation() : base()
        {
        }
        public ColorLightnessAnimation(float toValue, Duration duration) : this()
        {
            To = toValue;
            Duration = duration;
        }
        public ColorLightnessAnimation(float toValue, Duration duration, FillBehavior fillBehavior) : this()
        {
            To = toValue;
            Duration = duration;
            FillBehavior = fillBehavior;
        }
        public ColorLightnessAnimation(float fromValue, float toValue, Duration duration) : this()
        {
            From = fromValue;
            To = toValue;
            Duration = duration;
        }
        public ColorLightnessAnimation(float fromValue, float toValue, Duration duration, FillBehavior fillBehavior) : this()
        {
            From = fromValue;
            To = toValue;
            Duration = duration;
            FillBehavior = fillBehavior;
        }

        #endregion

        #region Freezable

        public new ColorLightnessAnimation Clone()
        {
            return (ColorLightnessAnimation)base.Clone();
        }

        protected override Freezable CreateInstanceCore()
        {
            return new ColorLightnessAnimation();
        }

        #endregion

        protected override Color GetCurrentValueCore(Color defaultOriginValue, Color defaultDestinationValue, AnimationClock animationClock)
        {
            System.Diagnostics.Debug.Assert(animationClock.CurrentState != ClockState.Stopped);
            if (!_isAnimationFunctionValid) ValidateAnimationFunction();
            double progress = animationClock.CurrentProgress.Value;
            IEasingFunction easingFunction = EasingFunction;

            if (easingFunction != null)
            {
                progress = easingFunction.Ease(progress);
            }
            double fromVal = _keyvalues[0];
            double toVal = _keyvalues[1];
            double target;
            if (fromVal > toVal)
            {
                target = (1 - progress) * (fromVal - toVal) + toVal;
            }
            else
            {
                target = (toVal - fromVal) * progress + fromVal;
            }
            Color originColor = OriginColor;
            if (target == 0) return originColor;
            return ColorEx.ChangeColorBrightness(originColor, target);
        }

        private void ValidateAnimationFunction()
        {
            _keyvalues = new Double[2];
            //Validate From
            double fromVal = From;
            if (fromVal > 100) _keyvalues[0] = 100;
            else if (fromVal < -100) _keyvalues[0] = -100;
            else _keyvalues[0] = fromVal;
            //Validate TO
            double toVal = To;
            if (toVal > 100) _keyvalues[1] = 100;
            else if (toVal < -100) _keyvalues[1] = -100;
            else _keyvalues[1] = toVal;
            _isAnimationFunctionValid = true;
        }


        public Color OriginColor
        {
            get { return (Color)GetValue(OriginColorProperty); }
            set { SetValue(OriginColorProperty, value); }
        }
        public static readonly DependencyProperty OriginColorProperty;

        public double To
        {
            get { return (double)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }
        public static readonly DependencyProperty ToProperty;

        public double From
        {
            get { return (double)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }
        public static readonly DependencyProperty FromProperty;

        public double By
        {
            get { return (double)GetValue(ByProperty); }
            set { SetValue(ByProperty, value); }
        }
        public static readonly DependencyProperty ByProperty;

        public IEasingFunction EasingFunction
        {
            get
            {
                return (IEasingFunction)GetValue(EasingFunctionProperty);
            }
            set
            {
                SetValue(EasingFunctionProperty, value);
            }
        }
        public static readonly DependencyProperty EasingFunctionProperty;

    }

}
