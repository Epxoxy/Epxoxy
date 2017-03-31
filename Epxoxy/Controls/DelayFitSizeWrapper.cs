using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Epxoxy.Controls
{

    [ContentProperty("Content")]
    public class DelayFitSizeWrapper : System.Windows.Controls.ContentControl
    {

        static DelayFitSizeWrapper()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DelayFitSizeWrapper), 
                new FrameworkPropertyMetadata(typeof(DelayFitSizeWrapper)));
        }

        public DelayFitSizeWrapper()
        {
            this.Loaded += OnThisLoaded;
        }

        private void OnThisLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnThisLoaded;
            this.Unloaded += OnThisUnloaded;
        }

        private void OnThisUnloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= OnThisUnloaded;
            EnsureReleaseTimer();
        }

        public FrameworkElement wrapper;
        public FrameworkElement wrappingElement;
        public override void OnApplyTemplate()
        {
            EnsureReleaseTimer();
            if (wrapper != null) wrapper.SizeChanged -= OnWrapperSizeChanged;
            base.OnApplyTemplate();
            wrapper = GetTemplateChild("PART_WRAPPER") as FrameworkElement;
            wrappingElement = GetTemplateChild("PART_CONTENT") as FrameworkElement;
            if (wrapper != null && wrappingElement != null)
            {
                wrapper.SizeChanged += OnWrapperSizeChanged;
            }
        }

        private void OnWrapperSizeChanged(object sender, SizeChangedEventArgs e)
        {
            newerSize = e.NewSize;
            if (delayTimer == null)
            {
                delayTimer = new DispatcherTimer();
                delayTimer.Interval = TimeSpan.FromMilliseconds(Delay);
                delayTimer.Tick += OnTimerTick;
            }
            else delayTimer.Stop();
            delayTimer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (delayTimer != null) delayTimer.Stop();
            wrappingElement.Height = newerSize.Height;
            wrappingElement.Width = newerSize.Width;
        }

        private void EnsureReleaseTimer()
        {
            if (delayTimer != null)
            {
                delayTimer.Tick -= OnTimerTick;
                delayTimer.Stop();
                delayTimer = null;
            }
        }

        private DispatcherTimer delayTimer { get; set; }
        public int Delay
        {
            get { return delay; }
            set
            {
                if (delay != value)
                {
                    delay = value;
                }
            }
        }
        private int delay = 30;
        public Size newerSize = new Size();
    }
}
