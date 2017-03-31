using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Epxoxy.Controls
{
    public class SimpleSplitView : ContentControl
    {
        public object Pane
        {
            get { return (object)GetValue(PaneProperty); }
            set { SetValue(PaneProperty, value); }
        }
        public static readonly DependencyProperty PaneProperty =
            DependencyProperty.Register("Pane", typeof(object), typeof(SimpleSplitView), new PropertyMetadata(null));

        public Brush PaneBackground
        {
            get { return (Brush)GetValue(PaneBackgroundProperty); }
            set { SetValue(PaneBackgroundProperty, value); }
        }
        public static readonly DependencyProperty PaneBackgroundProperty =
            DependencyProperty.Register("PaneBackground", typeof(Brush), typeof(SimpleSplitView), new PropertyMetadata(null));

        public object Preview
        {
            get { return (object)GetValue(PreviewProperty); }
            set { SetValue(PreviewProperty, value); }
        }
        public static readonly DependencyProperty PreviewProperty =
            DependencyProperty.Register("Preview", typeof(object), typeof(SimpleSplitView), new PropertyMetadata(null));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(SimpleSplitView),
                new PropertyMetadata(false, OnComponentsChanged));

        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }
        public static readonly DependencyProperty OpenPaneLengthProperty =
            DependencyProperty.Register("OpenPaneLength", typeof(double), typeof(SimpleSplitView),
                new PropertyMetadata(280d, OnComponentsChanged, CoreOpenPaneLengthChanged), ValidateDoubleCallBack);

        public double CompactPaneLength
        {
            get { return (double)GetValue(CompactPaneLengthProperty); }
            set { SetValue(CompactPaneLengthProperty, value); }
        }
        public static readonly DependencyProperty CompactPaneLengthProperty =
            DependencyProperty.Register("CompactPaneLength", typeof(double), typeof(SimpleSplitView),
                new PropertyMetadata(40d, OnCompactPaneLengthChanged, CoreCompactPaneLengthChanged), ValidateDoubleCallBack);

        private static bool ValidateDoubleCallBack(object value)
        {
            var v = (double)value;
            return !(double.IsNaN(v) || double.IsInfinity(v));
        }

        private static object CoreCompactPaneLengthChanged(DependencyObject d, object baseValue)
        {
            var ctl = (SimpleSplitView)d;
            double value = (double)baseValue;
            if (value >= ctl.OpenPaneLength)
                throw new ArgumentException("CompactPaneLength can't large than OpenPaneLength");
            return value;
        }

        private static object CoreOpenPaneLengthChanged(DependencyObject d, object baseValue)
        {
            var ctl = (SimpleSplitView)d;
            double value = (double)baseValue;
            if (value <= ctl.CompactPaneLength)
                throw new ArgumentException("OpenPaneLength can't less than CompactPaneLength");
            return value;
        }

        private static void OnComponentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (SimpleSplitView)d;
            ctl.OnComponentsChanged();
        }

        private static void OnCompactPaneLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (SimpleSplitView)d;
            ctl.updateTargetChange();
            ctl.OnComponentsChanged();
        }

        private void OnComponentsChanged()
        {
            if (_pane == null) return;
            if (targetVisibility) updateByVisibility();
            else updateByPaneWidth();
        }

        private bool targetVisibility;
        private void updateTargetChange()
        {
            targetVisibility = CompactPaneLength == 0;
            if (!targetVisibility && _pane != null)
                _pane.Visibility = Visibility.Visible;
        }

        private void updateByVisibility()
        {
            if (IsOpen)
            {
                if (_pane.Visibility != Visibility.Visible)
                    _pane.Visibility = Visibility.Visible;
            }
            else
            {
                if (_pane.Visibility != Visibility.Collapsed)
                    _pane.Visibility = Visibility.Collapsed;
            }
        }

        private void updateByPaneWidth()
        {
            if (IsOpen)
            {
                double openLength = OpenPaneLength;
                if (_pane.Width != openLength)
                    _pane.Width = openLength;
            }
            else
            {
                double compactLength = CompactPaneLength;
                if (_pane.Width != compactLength)
                    _pane.Width = compactLength;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _pane = GetTemplateChild("PART_Pane") as Grid;
            OnComponentsChanged();
        }

        private Grid _pane;
    }
}
