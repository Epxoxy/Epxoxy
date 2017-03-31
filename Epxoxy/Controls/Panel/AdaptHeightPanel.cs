using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Epxoxy.Controls
{
    public class AdaptHeightPanel : Panel
    {
        public double ClipWidth
        {
            get { return (double)GetValue(ClipWidthProperty); }
            set { SetValue(ClipWidthProperty, value); }
        }

        public static readonly DependencyProperty ClipWidthProperty =
            DependencyProperty.Register("ClipWidth", typeof(double), typeof(AdaptHeightPanel),
                new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        protected override Size MeasureOverride(Size availableSize)
        {
            Size childConstraint = new Size(Double.PositiveInfinity, Double.PositiveInfinity);
            Size desireSize = new Size(0, 0);

            foreach (UIElement child in InternalChildren)
            {
                if (child == null) { continue; }
                child.Measure(availableSize);
                desireSize.Height = Math.Max(child.DesiredSize.Height, desireSize.Height);
                desireSize.Width = Math.Max(child.DesiredSize.Width, desireSize.Width);
            }

            if (double.IsInfinity(availableSize.Height) || double.IsInfinity(availableSize.Width))
                return desireSize;
            return availableSize;
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
                    if (InternalChildren[cindex] == null) { continue; }
                    var rect = new Rect(0, 0, InternalChildren[cindex].DesiredSize.Width, finalSize.Height);
                    InternalChildren[cindex].Arrange(rect);
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
                var renderSize = RenderSize;
                renderSize.Width = ClipWidth;
                return new RectangleGeometry(new Rect(renderSize));
            }
            else
                return null;
        }
    }
}
