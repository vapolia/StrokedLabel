using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

#if ANDROID
using AndroidX.AppCompat.Widget;
#endif

namespace Vapolia.StrokedLabels;

internal class StrokeLabelHandler : LabelHandler
{
#if !WINDOWS
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
#endif

#if ANDROID
    protected override AppCompatTextView CreatePlatformView()
        => new StrokedTextView(Context);
#endif

#if IOS || MACCATALYST
    protected override MauiLabel CreatePlatformView()
       => new StrokedTextView();
#endif
    
#if WINDOWS
    //TextBlock is sealed, and we can't return anything else than a TextBlock !
    //protected override Microsoft.UI.Xaml.Controls.TextBlock CreatePlatformView()
    //    => new StrokedTextView();
#endif
}
