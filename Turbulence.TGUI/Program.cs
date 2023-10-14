using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Gui;
using Turbulence.Discord;
using Turbulence.TGUI;

var provider = new ServiceCollection()
    .AddSingleton<IPlatformClient, Client>()
    .BuildServiceProvider();

Ioc.Default.ConfigureServices(provider);

Application.Init();

try
{
    Application.Run(new MainWindow());
}
finally
{
    Application.Shutdown();
}
