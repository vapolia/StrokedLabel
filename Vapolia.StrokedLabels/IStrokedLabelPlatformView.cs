using Color = Microsoft.Maui.Graphics.Color;

namespace Vapolia.StrokedLabels;


internal interface IStrokedLabelPlatformView
{
    Color StrokeColor { get; set; }
    int StrokeWidth { get; set; }

    void Invalidate();
}
