#if DISABLED

using System;
using Color = Microsoft.Maui.Graphics.Color;

namespace Vapolia.StrokedLabels;

//TODO: Use Win2D with a CanvasTextLayout
internal class StrokedTextView : Microsoft.UI.Xaml.Controls.TextBlock, IStrokedLabelPlatformView
{
    public Color StrokeColor { get; set; } = (Color)StrokedLabel.StrokeColorProperty.DefaultValue;
    public int StrokeWidth { get; set; } = (int)StrokedLabel.StrokeWidthProperty.DefaultValue;

    public void Invalidate()
        => throw new NotImplementedException();
}

#endif
