using Terminal.Gui;
using Turbulence.Core.ViewModels;
using Turbulence.TGUI.Views;

namespace Turbulence.TGUI;

public sealed class MainWindow : Window
{
    public MainWindow()
    {
        Title = "Turbulence";
        Border.BorderStyle = BorderStyle.Rounded;
        Width = Dim.Fill();
        Height = Dim.Fill();

        var vm = new MainWindowViewModel();

        Add(new MenuBarView());
        Add(new TextInputView(vm));
        Add(new MessagesView(new MessagesViewModel(vm)));
        Add(new ServerListView());
    }
}
