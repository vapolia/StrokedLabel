using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Hosting;

[assembly: Microsoft.Maui.Controls.XmlnsDefinition("https://vapolia.eu/Vapolia.StrokedLabel", "Vapolia.StrokedLabels")]
[assembly: Microsoft.Maui.Controls.XmlnsPrefix("https://vapolia.eu/Vapolia.StrokedLabel", "stroked")]
[assembly: Microsoft.Maui.Controls.XmlnsPrefix("clr-namespace:Vapolia.StrokedLabels;assembly=Vapolia.StrokedLabels", "segmented")]


namespace Vapolia.StrokedLabels;

/// <summary>
/// Stroked Label Behavior
/// Benjamin Mayrargue - Vapolia.eu - (c) 2024
/// </summary>
/// <example>
///   &lt;Label Text="some text" behaviors:OutlineLabel.StrokeColor="Red" behaviors:OutlineLabel.StrokeWidth="Red" /&gt;
/// &lt;Label Text="Bonjour" FontSize="60" TextColor="LightGreen"
///           behaviors:StrokedLabel.StrokeColor="DarkGreen"
///           behaviors:StrokedLabel.StrokeWidth="4"
/// /&gt;
/// </example>
/// <remarks>
/// Register the custom label handler in your MauiProgram:
/// builder.UseStrokedLabelBehavior()
///
/// Compatibility: iOS, Android
/// Does not work with FormattedText/Spans (at least on Android) !!
/// </remarks>
public static class  StrokedLabel
{
    public static readonly BindableProperty StrokeColorProperty = BindableProperty.CreateAttached("StrokeColor", typeof(Color), typeof(StrokedLabel), KnownColor.Default);
    public static readonly BindableProperty StrokeWidthProperty = BindableProperty.CreateAttached("StrokeWidth", typeof(int), typeof(StrokedLabel), 0);
    
    public static Color GetStrokeColor(BindableObject view) => (Color?)view.GetValue(StrokeColorProperty) ?? (Color)StrokeColorProperty.DefaultValue;
    public static int GetStrokeWidth(BindableObject view) => (int?)view.GetValue(StrokeWidthProperty) ?? (int)StrokeWidthProperty.DefaultValue;

    public static MauiAppBuilder UseStrokedLabelBehavior(this MauiAppBuilder builder) 
        => builder.ConfigureMauiHandlers(handlers => handlers.AddHandler<Label, StrokeLabelHandler>());
}
