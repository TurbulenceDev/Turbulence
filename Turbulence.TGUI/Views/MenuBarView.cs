using Terminal.Gui;

namespace Turbulence.TGUI.Views;

public class MenuBarView : MenuBar
{
    public readonly MenuBarItem StatusMenu = new() { Title = "Status" };
    
    public MenuBarView()
    {
        Width = Dim.Fill();
        Height = 1;
        Menus = new[]
        {
            new MenuBarItem
            {
                Title = "_File",
                Children = new[]
                {
                    new MenuItem { Title = "_Quit" },
                },
            },
            new MenuBarItem
            {
                Title = "_Discord",
                Children = new[]
                {
                    new MenuItem { Title = "_Set Token" },
                },
            },
            StatusMenu,
        };
    }
}
