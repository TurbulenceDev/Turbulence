using Terminal.Gui;

namespace Turbulence.TGUI.Views;

public class MenuBarView : MenuBar
{
    private readonly MenuBarItem _statusMenu = new() { Title = "Status" };
    
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
            _statusMenu,
        };
    }

    // TODO: Move this to viewmodel
    public void SetStatus(string status)
    {
        _statusMenu.Title = status;
    }
}
