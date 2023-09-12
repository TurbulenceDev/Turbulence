using Terminal.Gui;
using Turbulence.TGUI;

internal class Program
{
    private static void Main(string[] args)
    {
        Application.Init();

        try
        {
            Application.Run(new Main());
        }
        finally
        {
            Application.Shutdown();
        }
    }
}