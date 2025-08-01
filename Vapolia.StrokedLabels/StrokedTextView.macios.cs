using Microsoft.Maui.Platform;
using Color = Microsoft.Maui.Graphics.Color;

using UIKit;
using CoreGraphics;
using Microsoft.Maui.Controls;

namespace Vapolia.StrokedLabels;

internal class StrokedTextView : MauiLabel, IStrokedLabelPlatformView
{
    public Color StrokeColor { get; set; } = (Color)StrokedLabel.StrokeColorProperty.DefaultValue;
    public int StrokeWidth { get; set; } = (int)StrokedLabel.StrokeWidthProperty.DefaultValue;

    public void Invalidate()
        => SetNeedsDisplay();

    public override void DrawText(CGRect rect)
    {
        if (Equals(StrokeColor, KnownColor.Default) || Equals(StrokeColor, KnownColor.Transparent))
        {
            base.DrawText(rect);
            return;
        }

        var shadowOffset = ShadowOffset;
        var textColor = TextColor;

        //Stroke outline. Use shadow settings.
        var c = UIGraphics.GetCurrentContext();
        c.SetLineWidth(StrokeWidth);
        c.SetLineJoin(CGLineJoin.Round);
        c.SetTextDrawingMode(CGTextDrawingMode.Stroke);
        TextColor = StrokeColor.ToPlatform();
        base.DrawText(rect);

        //Fill inside. No shadow.
        c.SetTextDrawingMode(CGTextDrawingMode.Fill);
        TextColor = textColor;
        ShadowOffset = CGSize.Empty;
        base.DrawText(rect);
        
        ShadowOffset = shadowOffset; 
    }
}