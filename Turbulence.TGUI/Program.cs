using Terminal.Gui;
using Turbulence.TGUI;

Application.Init();

try
{
    Application.Run(new MainWindow());
}
finally
{
    Application.Shutdown();
}
