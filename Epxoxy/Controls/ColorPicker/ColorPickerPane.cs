using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Epxoxy.Helpers;

namespace Epxoxy.Controls
{
    public class ColorPickerPane : System.Windows.Controls.Control
    {
        static ColorPickerPane()
        {
            AlphaProperty = DependencyProperty.Register("Alpha", typeof(int), typeof(ColorPickerPane), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ComponentChanged));
            RedProperty = DependencyProperty.Register("Red", typeof(int), typeof(ColorPickerPane), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ComponentChanged));
            GreenProperty = DependencyProperty.Register("Green", typeof(int), typeof(ColorPickerPane), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ComponentChanged));
            BlueProperty = DependencyProperty.Register("Blue", typeof(int), typeof(ColorPickerPane), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ComponentChanged));
            HueProperty = DependencyProperty.Register("Hue", typeof(int), typeof(ColorPickerPane), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ComponentChanged));
            BrightnessProperty = DependencyProperty.Register("Brightness", typeof(int), typeof(ColorPickerPane), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ComponentChanged));
            SaturationProperty = DependencyProperty.Register("Saturation", typeof(int), typeof(ColorPickerPane), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ComponentChanged));
            SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color?), typeof(ColorPickerPane), new FrameworkPropertyMetadata(
                Color.FromArgb(0, 0, 0, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SelectedColorChanged));
            ModeProperty = DependencyProperty.Register("Mode", typeof(ColorSelectMode), typeof(ColorPickerPane), new PropertyMetadata(ColorSelectMode.Basic));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerPane), new FrameworkPropertyMetadata(typeof(ColorPickerPane)));
            OnSelectedColorChangedEvent = EventManager.RegisterRoutedEvent("OnSelectedColorChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColorPickerPane));
        }
        public static readonly RoutedEvent OnSelectedColorChangedEvent;

        public event RoutedEventHandler OnSelectedColorChanged;

        private static void ComponentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorpickerpane = d as ColorPickerPane;
            if (colorpickerpane != null) colorpickerpane.OnComponentChanged(e);
        }

        private static void SelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorpickerpane = d as ColorPickerPane;
            if (colorpickerpane != null) colorpickerpane.UpdateOnSelectedColorChanged((Color?)e.NewValue);
        }
        protected virtual void UpdateOnSelectedColorChanged(Color? newColor)
        {
            if (!internalUpdate && newColor != null)
            {
                UpdateARGB(newColor.Value);
                UpdateHSV(newColor.Value);
            }
            RoutedEventArgs args = new RoutedEventArgs(OnSelectedColorChangedEvent, this);
            this.RaiseEvent(args);
        }

        protected virtual void OnComponentChanged(DependencyPropertyChangedEventArgs e)
        {
            if (internalUpdate) return;
            var color = this.SelectedColor.Value;
            var i = Convert.ToInt32(e.NewValue);
            byte x = i <= 255 ? (byte)i : (byte)255;

            //Update selected color when alpha/red/green/blue property changed
            if (e.Property == AlphaProperty)
            {
                this.SelectedColor = Color.FromArgb(x, color.R, color.G, color.B);
            }
            else if (e.Property == RedProperty)
            {
                this.SelectedColor = Color.FromArgb(color.A, x, color.G, color.B);
                this.UpdateHSV(this.SelectedColor.Value);
            }
            else if (e.Property == GreenProperty)
            {
                this.SelectedColor = Color.FromArgb(color.A, color.R, x, color.B);
                this.UpdateHSV(this.SelectedColor.Value);
            }
            else if (e.Property == BlueProperty)
            {
                this.SelectedColor = Color.FromArgb(color.A, color.R, color.G, x);
                this.UpdateHSV(this.SelectedColor.Value);
            }
            else
            {
                //Update RGB when hue/saturation/brightness property changed
                var hsv = color.ColorToHsv();
                double y = Convert.ToDouble(e.NewValue);
                if (e.Property == HueProperty)
                {
                    this.SelectedColor = ColorHelper.HsvToColor(y / 360, hsv[1], hsv[2], color.A / 255.0);
                    this.UpdateARGB(this.SelectedColor.Value);
                }
                else if (e.Property == SaturationProperty)
                {
                    this.SelectedColor = ColorHelper.HsvToColor(hsv[0], y / 100, hsv[2], color.A / 255.0);
                    this.UpdateARGB(this.SelectedColor.Value);
                }
                else if (e.Property == BrightnessProperty)
                {
                    this.SelectedColor = ColorHelper.HsvToColor(hsv[0], hsv[1], y / 100, color.A / 255.0);
                    this.UpdateARGB(this.SelectedColor.Value);
                }
            }
            internalUpdate = false;
        }

        private List<Color> ColorList;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var coloritemsroot = GetTemplateChild("PART_ColorListRoot") as System.Windows.Controls.Primitives.Selector;

            if (coloritemsroot != null)
            {
                if (ColorList == null)
                {
                    ColorList = new List<Color>();
                    var infos = typeof(Colors).GetProperties();
                    for (int i = 0; i < infos.Length; ++i)
                    {
                        ColorList.Add((Color)ColorConverter.ConvertFromString(infos[i].Name));
                    }
                }
                coloritemsroot.ItemsSource = ColorList;
            }
        }

        //Update ARGB
        private void UpdateARGB(Color color)
        {
            internalUpdate = true;
            if (this.Alpha != color.A) this.Alpha = color.A;
            if (this.Red != color.R) this.Red = color.R;
            if (this.Green != color.G) this.Green = color.G;
            if (this.Blue != color.B) this.Blue = color.B;
            internalUpdate = false;
        }
        //Update HSV
        private void UpdateHSV(Color color)
        {
            internalUpdate = true;
            var hsv = color.ColorToHsv();
            this.Hue = (int)(hsv[0] * 360);
            this.Saturation = (int)(hsv[1] * 100);
            this.Brightness = (int)(hsv[2] * 100);
            internalUpdate = false;
        }

        private bool internalUpdate = false;

        public int Alpha
        {
            get { return (int)GetValue(AlphaProperty); }
            set { SetValue(AlphaProperty, value); }
        }
        public static readonly DependencyProperty AlphaProperty;

        public int Red
        {
            get { return (int)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public static readonly DependencyProperty RedProperty;

        public int Green
        {
            get { return (int)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }
        public static readonly DependencyProperty GreenProperty;

        public int Blue
        {
            get { return (int)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }
        public static readonly DependencyProperty BlueProperty;

        public int Hue
        {
            get { return (int)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }
        public static readonly DependencyProperty HueProperty;

        public int Brightness
        {
            get { return (int)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }
        public static readonly DependencyProperty BrightnessProperty;

        public int Saturation
        {
            get { return (int)GetValue(SaturationProperty); }
            set { SetValue(SaturationProperty, value); }
        }
        public static readonly DependencyProperty SaturationProperty;

        public Color? SelectedColor
        {
            get { return (Color?)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty;

        public ColorSelectMode Mode
        {
            get { return (ColorSelectMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
        public static readonly DependencyProperty ModeProperty;

    }

    public enum ColorSelectMode
    {
        Basic,
        Custom
    }
}
