using System;
using System.Windows;
using System.Windows.Controls;

namespace Epxoxy.Controls
{
    public class FillPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            Size childConstraint = new Size(Double.PositiveInfinity, Double.PositiveInfinity);
            Size desireSize = new Size(0, 0);

            foreach (UIElement child in InternalChildren)
            {
                if (child == null) { continue; }
                child.Measure(availableSize);
                //Calculate the max desire size of children
                desireSize.Height = Math.Max(child.DesiredSize.Height, desireSize.Height);
                desireSize.Width = Math.Max(child.DesiredSize.Width, desireSize.Width);
            }
            return desireSize;
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
                    //Let children fill this panel
                    var rect = new Rect(0, 0, finalSize.Width, finalSize.Height);
                    InternalChildren[cindex].Arrange(rect);
                }
            }
            return finalSize;
        }
    }
}
