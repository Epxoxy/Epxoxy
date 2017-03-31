using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Collections;

namespace Epxoxy.Controls
{
    public class CustomAdorner : Adorner
    {
        public CustomAdorner(FrameworkElement adornedElement) : base(adornedElement)
        {
            _visuals = new VisualCollection(this);
        }
        public CustomAdorner(FrameworkElement adornedElement, FrameworkElement child) : this(adornedElement)
        {
            Child = child;
        }

        #region Override

        protected override Size MeasureOverride(Size constraint)
        {
            Child.Measure(constraint);
            return _child.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = determineX();
            double y = determineY();
            double width = determineWidth();
            double height = determineHeight();
            Child.Arrange(new Rect(x, y, width, height));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }

        protected override int VisualChildrenCount
        {
            get { return _visuals.Count; }
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                ArrayList list = new ArrayList();
                list.Add(this._child);
                return (IEnumerator)list.GetEnumerator();
            }
        }

        #endregion

        #region Determine

        private double determineX()
        {
            switch (Child.HorizontalAlignment)
            {
                case HorizontalAlignment.Left: { return offsetX; }
                case HorizontalAlignment.Right:
                    {
                        double adornerWidth = Child.DesiredSize.Width;
                        double adornedWidth = AdornedElement.ActualWidth;
                        return adornedWidth - adornerWidth + offsetX;
                    }
                case HorizontalAlignment.Center:
                    {
                        double adornerWidth = Child.DesiredSize.Width;
                        double adornedWidth = AdornedElement.ActualWidth;
                        return adornedWidth / 2 - adornerWidth / 2 + offsetX;
                    }
                case HorizontalAlignment.Stretch: { return 0.0; }
            }
            return 0.0;
        }
        private double determineY()
        {
            switch (Child.VerticalAlignment)
            {
                case VerticalAlignment.Top: { return offsetY; }
                case VerticalAlignment.Bottom:
                    {
                        double adornerHeight = Child.DesiredSize.Height;
                        double adornedHeight = AdornedElement.ActualHeight;
                        return adornedHeight - adornerHeight + offsetY;
                    }
                case VerticalAlignment.Center:
                    {
                        double adornerHeight = Child.DesiredSize.Height;
                        double adornedHeight = AdornedElement.ActualHeight;
                        return adornedHeight / 2 - adornerHeight / 2 + offsetY;
                    }
                case VerticalAlignment.Stretch: { return 0.0; }
            }
            return 0.0;
        }

        private double determineWidth()
        {
            switch (Child.HorizontalAlignment)
            {
                case HorizontalAlignment.Left: { return Child.DesiredSize.Width; }
                case HorizontalAlignment.Right: { return Child.DesiredSize.Width; }
                case HorizontalAlignment.Center: { return Child.DesiredSize.Width; }
                case HorizontalAlignment.Stretch: { return AdornedElement.ActualWidth; }
            }
            return 0.0;
        }
        private double determineHeight()
        {
            switch (Child.VerticalAlignment)
            {
                case VerticalAlignment.Top: { return Child.DesiredSize.Height; }
                case VerticalAlignment.Bottom: { return Child.DesiredSize.Height; }
                case VerticalAlignment.Center: { return Child.DesiredSize.Height; }
                case VerticalAlignment.Stretch: { return AdornedElement.ActualHeight; }
            }
            return 0.0;
        }

        #endregion

        public void DisconnectChild()
        {
            Child = null;
        }

        private double offsetX = 0.0;
        private double offsetY = 0.0;

        public FrameworkElement Child
        {
            get { return _child; }
            set
            {
                if (_child != null && _visuals != null) _visuals.Remove(_child);
                _child = value;
                if (_child != null) _visuals.Add(_child);
            }
        }
        private FrameworkElement _child;
        protected VisualCollection _visuals;
        public new FrameworkElement AdornedElement
        {
            get
            {
                return (FrameworkElement)base.AdornedElement;
            }
        }
    }

}
