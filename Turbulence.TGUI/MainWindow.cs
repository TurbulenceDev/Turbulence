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

        _ = new MainWindowViewModel();

        Add(new MenuBarView());
        Add(new TextInputView());
        Add(new MessagesView());
        Add(new ServerListView());
    }
}
