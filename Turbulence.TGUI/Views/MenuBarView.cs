using Terminal.Gui;
using Turbulence.Core.ViewModels;

namespace Turbulence.TGUI.Views;

public class MenuBarView : MenuBar
{
    private readonly MenuBarViewModel _vm = new();

    public MenuBarView()
    {
        var statusMenu = new MenuBarItem { Title = _vm.Status };

        Width = Dim.Fill();
        Height = 1;
        Menus = new[]
        {
            new MenuBarItem
            {
                Title = "_File",
                Children = new[]
                {
                    new MenuItem
                    {
                        Title = "_Quit",
                        Action = () => Application.RequestStop(),
                    },
                },
            },
            new MenuBarItem
            {
                Title = "_Discord",
                Children = new[]
                {
                    new MenuItem { Title = "_Set Token" },
                    new MenuItem 
                    { 
                        Title = "_Connect",
                        Action = _vm.Connect,
                    },
                },
            },
            statusMenu,
        };

        _vm.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(MenuBarViewModel.Status))
                statusMenu.Title = _vm.Status;
        };
    }
}
