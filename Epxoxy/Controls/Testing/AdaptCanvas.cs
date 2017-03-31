using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Epxoxy.Controls
{
    class AdaptCanvas : Canvas
    {
        public AdaptProperty AdaptTarget
        {
            get { return (AdaptProperty)GetValue(AdaptTargetProperty); }
            set { SetValue(AdaptTargetProperty, value); }
        }

        public static readonly DependencyProperty AdaptTargetProperty =
            DependencyProperty.Register("AdaptTarget", typeof(AdaptProperty), typeof(AdaptCanvas),
                new FrameworkPropertyMetadata(AdaptProperty.None, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnAdaptTargetChanged));

        private static void OnAdaptTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdaptCanvas panel = d as AdaptCanvas;
            if (panel != null)
            {
                panel.UpdateAdaptTarget((AdaptProperty)e.NewValue);
            }
        }

        private Func<Size, Size, Size> AdaptElementFunc = null;

        private Size AdaptHeight(Size desiredSize, Size finalSize)
        {
            Helpers.DebugHelper.debugWrite(this, "");
            return new Size(desiredSize.Width, finalSize.Height);
        }

        private Size AdaptWidth(Size desiredSize, Size finalSize)
        {
            return new Size(finalSize.Width, desiredSize.Height);
        }

        private Size AdaptBoth(Size desiredSize, Size finalSize)
        {
            return new Size(finalSize.Width, finalSize.Height);
        }

        private Size AdaptNone(Size desiredSize, Size finalSize)
        {
            return desiredSize;
        }

        private void UpdateAdaptTarget(AdaptProperty newTarget)
        {
            switch (newTarget)
            {
                case AdaptProperty.Height:
                    AdaptElementFunc = AdaptHeight;
                    break;
                case AdaptProperty.Width:
                    AdaptElementFunc = AdaptWidth;
                    break;
                case AdaptProperty.Both:
                    AdaptElementFunc = AdaptBoth;
                    break;
                default:
                    AdaptElementFunc = AdaptNone;
                    break;
            }
        }

        private void UpdateClipRect()
        {

        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            UpdateAdaptTarget(AdaptTarget);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateAdaptTarget(AdaptTarget);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size childConstraint = new Size(Double.PositiveInfinity, Double.PositiveInfinity);

            foreach (UIElement child in InternalChildren)
            {
                if (child == null) { continue; }
                child.Measure(childConstraint);
            }

            return new Size();
            //return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (double.IsInfinity(finalSize.Height) || double.IsInfinity(finalSize.Width))
            {
                throw new InvalidOperationException("Infinity height or width.");
            }
            if (Children.Count > 0)
            {
                for (int cindex = 0; cindex < InternalChildren.Count; cindex++)
                {
                    UIElement child = InternalChildren[cindex];
                    if (child == null) { continue; }
                    double x = 0;
                    double y = 0;

                    //Compute offset of the child:
                    //If Left is specified, then Right is ignored
                    //If Left is not specified, then Right is used
                    //If both are not there, then 0
                    double left = GetLeft(child);
                    if (!double.IsNaN(left))
                    {
                        x = left;
                    }
                    else
                    {
                        double right = GetRight(child);

                        if (!double.IsNaN(right))
                        {
                            x = finalSize.Width - child.DesiredSize.Width - right;
                        }
                    }

                    double top = GetTop(child);
                    if (!double.IsNaN(top))
                    {
                        y = top;
                    }
                    else
                    {
                        double bottom = GetBottom(child);

                        if (!double.IsNaN(bottom))
                        {
                            y = finalSize.Height - child.DesiredSize.Height - bottom;
                        }
                    }
                    Size adaptSize = AdaptElementFunc(InternalChildren[cindex].DesiredSize, finalSize);
                    //new Rect(0, 0, InternalChildren[cindex].DesiredSize.Width, finalSize.Height);
                    InternalChildren[cindex].Arrange(new Rect(new Point(x, y), adaptSize));
                }
            }
            return finalSize;
        }

        /// <summary>
        /// Override of <seealso cref="UIElement.GetLayoutClip"/>.
        /// </summary>
        /// <returns>Geometry to use as additional clip if LayoutConstrained=true</returns>
        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            //FillHeightPanel like Canvas only clips to bounds if ClipToBounds is set, 
            //  no automatic clipping
            if (ClipToBounds)
            {
                return new RectangleGeometry(new Rect(0, -layoutSlotSize.Height, layoutSlotSize.Width, layoutSlotSize.Height));
            }
            else
                return null;
        }
    }

    public enum ClipTo
    {
        Increase,
        Decrease
    }

    public enum AdaptProperty
    {
        None,
        Height,
        Width,
        Both
    }
}