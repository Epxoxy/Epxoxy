using System.Windows;

namespace Epxoxy.Controls
{
    public class SplitViewTemplateSettings : DependencyObject
    {
        public double CompactToOpenLength
        {
            get { return (double)GetValue(CompactToOpenLengthProperty); }
            set { SetValue(CompactToOpenLengthProperty, value); }
        }
        public static readonly DependencyProperty CompactToOpenLengthProperty =
            DependencyProperty.Register("CompactToOpenLength", typeof(double), typeof(SplitViewTemplateSettings), new PropertyMetadata(0d));

        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }
        public static readonly DependencyProperty OpenPaneLengthProperty =
            DependencyProperty.Register("OpenPaneLength", typeof(double), typeof(SplitViewTemplateSettings), new PropertyMetadata(0d));

        public double NegativePaneLength
        {
            get { return (double)GetValue(NegativePaneLengthProperty); }
            set { SetValue(NegativePaneLengthProperty, value); }
        }
        public static readonly DependencyProperty NegativePaneLengthProperty =
            DependencyProperty.Register("NegativePaneLength", typeof(double), typeof(SplitViewTemplateSettings), new PropertyMetadata(0d));

        public double CompactPaneLength
        {
            get { return (double)GetValue(CompactPaneLengthProperty); }
            set { SetValue(CompactPaneLengthProperty, value); }
        }
        public static readonly DependencyProperty CompactPaneLengthProperty =
            DependencyProperty.Register("CompactPaneLength", typeof(double), typeof(SplitViewTemplateSettings), new PropertyMetadata(0d));

        public Thickness AdaptOpenThickness
        {
            get { return (Thickness)GetValue(AdaptOpenThicknessProperty); }
            set { SetValue(AdaptOpenThicknessProperty, value); }
        }
        public static readonly DependencyProperty AdaptOpenThicknessProperty =
            DependencyProperty.Register("AdaptOpenThickness", typeof(Thickness), typeof(SplitViewTemplateSettings), new PropertyMetadata(default(Thickness)));

        public Thickness AdaptCompactToOpenMargin
        {
            get { return (Thickness)GetValue(AdaptCompactToOpenMarginProperty); }
            set { SetValue(AdaptCompactToOpenMarginProperty, value); }
        }
        public static readonly DependencyProperty AdaptCompactToOpenMarginProperty =
            DependencyProperty.Register("AdaptCompactToOpenMargin", typeof(Thickness), typeof(SplitViewTemplateSettings), new PropertyMetadata(default(Thickness)));

        public double TranslateCompactToOpenLength
        {
            get { return (double)GetValue(TranslateCompactToOpenLengthProperty); }
            set { SetValue(TranslateCompactToOpenLengthProperty, value); }
        }
        public static readonly DependencyProperty TranslateCompactToOpenLengthProperty =
            DependencyProperty.Register("TranslateCompactToOpenLength", typeof(double), typeof(SplitViewTemplateSettings), new PropertyMetadata(0d));

        public double TranslateOverlayFrom
        {
            get { return (double)GetValue(TranslateOverlayFromProperty); }
            set { SetValue(TranslateOverlayFromProperty, value); }
        }
        public static readonly DependencyProperty TranslateOverlayFromProperty =
            DependencyProperty.Register("TranslateOverlayFrom", typeof(double), typeof(SplitViewTemplateSettings), new PropertyMetadata(0d));



        public double NegativeTranslateOverlayFrom
        {
            get { return (double)GetValue(NegativeTranslateOverlayFromProperty); }
            set { SetValue(NegativeTranslateOverlayFromProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NegativeTranslateOverlayFrom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NegativeTranslateOverlayFromProperty =
            DependencyProperty.Register("NegativeTranslateOverlayFrom", typeof(double), typeof(SplitViewTemplateSettings), new PropertyMetadata(0d));



        internal SplitViewTemplateSettings(SplitView02 owner)
        {
            this.Owner = owner;
            this.Update();
        }

        internal SplitView02 Owner { get; }
        internal void Update()
        {
            Helpers.DebugHelper.debugWrite(this, "SplitViewTemplateSettings working");
            this.OpenPaneLength = this.Owner.OpenPaneLength;
            this.CompactPaneLength = this.Owner.CompactPaneLength;

            this.NegativePaneLength = -this.OpenPaneLength;
            this.CompactToOpenLength = this.OpenPaneLength - this.CompactPaneLength;
            this.UpdateForPlacementChanged();
        }

        internal void UpdateForPlacementChanged()
        {
            PanePlacement newPlacement = this.Owner.PanePlacement;
            if (newPlacement == PanePlacement.Left)
            {
                TranslateOverlayFrom = this.NegativePaneLength;
                NegativeTranslateOverlayFrom = this.OpenPaneLength;
                TranslateCompactToOpenLength = this.CompactToOpenLength;
                AdaptOpenThickness = new Thickness(0, 0, this.OpenPaneLength, 0); 
                AdaptCompactToOpenMargin = new Thickness(0, 0, this.CompactToOpenLength, 0);
            }
            else
            {
                TranslateOverlayFrom = this.OpenPaneLength;
                NegativeTranslateOverlayFrom = this.NegativePaneLength;
                TranslateCompactToOpenLength = -this.CompactToOpenLength;
                AdaptOpenThickness = new Thickness(this.OpenPaneLength, 0, 0, 0);
                AdaptCompactToOpenMargin = new Thickness(this.CompactToOpenLength, 0, 0, 0);
            }
        }
    }
}
