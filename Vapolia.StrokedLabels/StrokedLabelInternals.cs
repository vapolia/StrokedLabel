using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Color = Microsoft.Maui.Graphics.Color;

#if ANDROID
using Android.Graphics;
using Android.Content;
using AndroidX.AppCompat.Widget;
#elif IOS || MACCATALYST
using UIKit;
using CoreGraphics;
#endif

namespace Vapolia.StrokedLabels;


internal interface IStrokedLabelPlatformView
{
    Color StrokeColor { get; set; }
    int StrokeWidth { get; set; }

    void Invalidate();
}


internal class StrokeLabelHandler : LabelHandler
{
    static StrokeLabelHandler()
    {
        Mapper.Add(StrokedLabel.StrokeColorProperty.PropertyName, MapStrokeColor);
        Mapper.Add(StrokedLabel.StrokeWidthProperty.PropertyName, MapStrokeWidth);
    }

    private static void MapStrokeColor(ILabelHandler handler, ILabel label)
    {
        if (handler is StrokeLabelHandler { PlatformView: IStrokedLabelPlatformView platformView })
        {
            platformView.StrokeColor = StrokedLabel.GetStrokeColor((BindableObject)label);
            platformView.Invalidate();
        }
    }

    private static void MapStrokeWidth(ILabelHandler handler, ILabel label)
    {
        if (handler is StrokeLabelHandler { PlatformView: IStrokedLabelPlatformView platformView })
        {
            platformView.StrokeWidth = StrokedLabel.GetStrokeWidth((BindableObject)label);
            platformView.Invalidate();
        }
    }

#if ANDROID
    protected override AppCompatTextView CreatePlatformView()
        => new StrokedTextView(Context);
#endif

#if IOS || MACCATALYST
    protected override MauiLabel CreatePlatformView()
       => new StrokedTextView();
#endif
}

#if IOS || MACCATALYST
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
#endif

#if ANDROID
internal class StrokedTextView(Context context) : MauiTextView(context), IStrokedLabelPlatformView
{
    private bool isDrawing;
    private int strokeWidth;

    private bool HasStroke => !Equals(StrokeColor, KnownColor.Default) && !Equals(StrokeColor, KnownColor.Transparent) && strokeWidth > 0;

    public Color StrokeColor { get; set; } = (Color)StrokedLabel.StrokeColorProperty.DefaultValue;
    public int StrokeWidth
    {
        get => strokeWidth;
        set
        {
            strokeWidth = value;
            
            //In order tp make base.OnMeasure find the correct size, modify the paint used to compute that size 
            var p = Paint;
            if (p != null)
            {
                if (HasStroke)
                {
                    p.SetStyle(Android.Graphics.Paint.Style.Stroke);
                    p.StrokeWidth = StrokeWidth;
                }
                else
                {
                    p.SetStyle(Android.Graphics.Paint.Style.Fill);
                    p.StrokeWidth = 0;
                }
            }
        }
    }

    public override void Invalidate()
    {
        // Ignore invalidate() calls triggered by setTextColor(color) calls
        if(isDrawing)
            return;
        
        base.RequestLayout();
        base.Invalidate();
    }

    protected override void OnDraw(Canvas canvas)
    {
        if (!HasStroke)
        {
            base.OnDraw(canvas);
            return;
        }
        
        try
        {
            isDrawing = true;
            var p = Paint;

            if (p != null)
            {
                //save the text color
                var currentTextColor = CurrentTextColor;

                //Draw the outline
                p.SetStyle(Android.Graphics.Paint.Style.Stroke);
                p.StrokeWidth = StrokeWidth;
                SetTextColor(StrokeColor.ToPlatform());
                base.OnDraw(canvas);

                //restore the text color
                SetTextColor(new Android.Graphics.Color(currentTextColor));

                //Draw the inside
                p.SetStyle(Android.Graphics.Paint.Style.Fill);
                base.OnDraw(canvas);
            }
        }
        finally
        {
            isDrawing = false;
        }
    }
}
#endif
