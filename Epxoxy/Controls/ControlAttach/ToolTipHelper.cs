using System.Windows;
using System.Windows.Controls.Primitives;

namespace Epxoxy.Controls
{
    public static class ToolTipHelper
    {
        public static CustomPopupPlacementCallback CustomPopupPlacementCallback => OnCustomPopupPlacementCallback;

        public static CustomPopupPlacement[] OnCustomPopupPlacementCallback(Size popupSize, Size dstSize, Point offset)
        {
            return new[]
            {
                new CustomPopupPlacement(new Point(dstSize.Width/2 - popupSize.Width/2, dstSize.Height + 14), PopupPrimaryAxis.Horizontal)
            };
        }
    }
}
