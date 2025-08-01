using Microsoft.Maui.Platform;
using Color = Microsoft.Maui.Graphics.Color;

using Android.Graphics;
using Android.Content;
using Microsoft.Maui.Controls;

namespace Vapolia.StrokedLabels;

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