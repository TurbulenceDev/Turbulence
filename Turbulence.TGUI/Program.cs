using Terminal.Gui;
using Turbulence.TGUI;

Application.Init();

try
{
    Application.Run(new TurbulenceWindow());
}
finally
{
    Application.Shutdown();
}