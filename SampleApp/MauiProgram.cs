using Microsoft.Extensions.Logging;
using Vapolia.StrokedLabels;

namespace SampleApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
            })
            .UseStrokedLabelBehavior();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}