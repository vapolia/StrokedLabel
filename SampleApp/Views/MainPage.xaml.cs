using System.Reflection;

namespace SampleApp.Views;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
        var version = typeof(MauiApp).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        VersionLabel.Text = $".NET MAUI ver. {version?[..version.IndexOf('+')]}";
    }
}