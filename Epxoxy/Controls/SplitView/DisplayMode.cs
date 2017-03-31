using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epxoxy.Controls
{
    public enum PanePlacement
    {
        Left,
        Right
    }

    public enum SplitViewDisplayMode
    {
        Inline,
        Overlay,
        CompactInline,
        CompactOverlay
    }

    internal class SplitViewState
    {
        public const string InlineOpen = "InlineOpenPane";
        public const string InlineClose = "InlineClosePane";
        public const string OverlayOpen = "OverlayOpenPane";
        public const string OverlayClose = "OverlayClosePane";
        public const string CompactInlineOpen = "CompactInlineOpenPane";
        public const string CompactInlineClose = "CompactInlineClosePane";
        public const string CompactOverlayOpen = "CompactOverlayOpenPane";
        public const string CompactOverlayClose = "CompactOverlayClosePane";
        public const string NoneState = "NoneState";
    }
}
