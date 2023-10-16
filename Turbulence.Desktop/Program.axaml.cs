using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Turbulence.Discord;

namespace Turbulence.Desktop;

internal class Program : Application
{
    public static void Main(string[] args)
    {
        var provider = new ServiceCollection()
            .AddSingleton<IPlatformClient, Client>()
            .AddSingleton<ICache, Cache>()
            .BuildServiceProvider();
        Ioc.Default.ConfigureServices(provider);
    
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);
    }

    private static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<Program>().UsePlatformDetect();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
