using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using LibVLCSharp.Shared;
using Microsoft.Extensions.DependencyInjection;
using Turbulence.Discord;
using Turbulence.Discord.Services;

namespace Turbulence.Desktop;

internal class Program : Application
{
    public static void Main(string[] args)
    {
        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception during Main: {e}");
        }
    }

    // INFO: designer directly calls this and not main
    private static AppBuilder BuildAvaloniaApp()
    {
        // Init IoC
        var provider = new ServiceCollection()
                .AddSingleton<IPlatformClient, Client>()
                .AddSingleton<ICache, Cache>()
                .AddSingleton<ITypingStorage, TypingStorage>()
                .AddSingleton<ILogger, Logger>()
                .AddSingleton<LibVLC>(_ =>
                {
                    LibVLCSharp.Shared.Core.Initialize();
                    var libVLC = new LibVLC(
                        enableDebugLogs: true,
                        "--input-repeat=2"
                    );
                    //libVLC.Log += (sender, args) => Console.WriteLine(args.Message);
                    return libVLC;
                })
                .BuildServiceProvider();
        Ioc.Default.ConfigureServices(provider);

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
