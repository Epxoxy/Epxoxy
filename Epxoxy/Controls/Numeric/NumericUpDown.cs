using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Epxoxy.Controls
{
    public class NumericUpDown : RangeBase
    {
        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
        }

        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }
        public Type ValueType
        {
            get { return (Type)GetValue(ValueTypeProperty); }
            set { SetValue(ValueTypeProperty, value); }
        }

        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register("Increment", typeof(double), typeof(NumericUpDown), new PropertyMetadata(1d));
        public static readonly DependencyProperty ValueTypeProperty =
            DependencyProperty.Register("ValueType", typeof(Type), typeof(NumericUpDown),
                new PropertyMetadata(typeof(double), OnValueTypeChanged));

        private static void OnValueTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (NumericUpDown)d;
            ctl.updateTypeConverter();
        }

        public NumericUpDown()
        {
            updateTypeConverter();
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            if (increaseBtn != null) increaseBtn.IsEnabled = Maximum > Value;
            if (decreaseBtn != null) decreaseBtn.IsEnabled = Minimum < Value;
            if (!_isInnerChanged)
            {
                var v = validateFunc(newValue);
                if (v != newValue) this.Value = v;
            }
            else base.OnValueChanged(oldValue, newValue);
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            if (increaseBtn != null) increaseBtn.IsEnabled = newMaximum > Value;
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            if (decreaseBtn != null) decreaseBtn.IsEnabled = newMinimum < Value;
            if (numericTb != null) numericTb.AcceptNegative = newMinimum < 0;
        }

        private RepeatButton increaseBtn = null;
        private RepeatButton decreaseBtn = null;
        private NumericTextBox numericTb;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (increaseBtn != null) increaseBtn.Click -= OnIncreaseBtnClick;
            if (decreaseBtn != null) decreaseBtn.Click -= OnDecreaseBtnClick;
            decreaseBtn = GetTemplateChild("PART_Decrease") as RepeatButton;
            increaseBtn = GetTemplateChild("PART_Increase") as RepeatButton;
            if (increaseBtn != null) increaseBtn.Click += OnIncreaseBtnClick;
            if (decreaseBtn != null) decreaseBtn.Click += OnDecreaseBtnClick;
            numericTb = GetTemplateChild("PART_TextBox") as NumericTextBox;
            if (numericTb != null)
            {
                numericTb.AcceptNegative = Minimum < 0;
                numericTb.AcceptDecimal = ValueType == typeof(double);
            }
        }

        private void OnIncreaseBtnClick(object sender, RoutedEventArgs e)
        {
            this.increase();
        }

        private void OnDecreaseBtnClick(object sender, RoutedEventArgs e)
        {
            this.decrease();
        }

        private void increase()
        {
            if (this.Value >= this.Maximum) return;
            this._isInnerChanged = true;
            if (Maximum - this.Value < Increment)
                this.Value = this.Maximum;
            else this.Value += this.Increment;
            this._isInnerChanged = false;
        }

        private void decrease()
        {
            if (this.Value <= this.Minimum) return;
            this._isInnerChanged = true;
            if (this.Value - Minimum < Increment)
                this.Value = this.Minimum;
            else this.Value -= this.Increment;
            this._isInnerChanged = false;
        }

        private bool _isInnerChanged;
        private Func<double, double> validateFunc;

        private void updateTypeConverter()
        {
            validateFunc = getTypeConverter(ValueType);
            if (numericTb != null)
            {
                numericTb.AcceptDecimal = ValueType == typeof(double);
            }
            var value = this.Value;
            OnValueChanged(value, value);
        }

        private Func<double, double> getTypeConverter(Type type)
        {
            if (type == typeof(double)) return new Func<double, double>(value =>
            {
                return value;
            });
            else if (type == typeof(Int16)) return new Func<double, double>(value =>
            {
                return Convert.ToInt16(value);
            });
            else if (type == typeof(Int32)) return new Func<double, double>(value =>
            {
                return Convert.ToInt32(value);
            });
            else if (type == typeof(Int64)) return new Func<double, double>(value =>
            {
                return Convert.ToInt64(value);
            });
            throw new ArgumentException("Not a validate type of " + type);
        }
    }
}
