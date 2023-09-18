using Terminal.Gui;
using Turbulence.Core.ViewModels;
using Turbulence.TGUI.Views;

namespace Turbulence.TGUI;

public sealed class TurbulenceWindow : Window
{
    private readonly TurbulenceWindowViewModel _vm;
    private readonly MenuBarView _menuBar;
    private readonly TextInputView _textInput;
    private readonly MessagesView _messages;
    private readonly ServerListView _serverList;

    public TurbulenceWindow()
    {
        Title = "Turbulence";
        Border.BorderStyle = BorderStyle.Rounded;
        Width = Dim.Fill();
        Height = Dim.Fill();

        _vm = new TurbulenceWindowViewModel();
        _menuBar = new MenuBarView();
        _messages = new MessagesView(new MessagesViewModel(_vm));
        _textInput = new TextInputView(_vm);
        _serverList = new ServerListView(new ServerListViewModel());
        
        Add(_menuBar);
        Add(_textInput);
        Add(_messages);
        Add(_serverList);
        
        // Hook up events
        // menu bar
        var file = _menuBar.Menus[0];
        file.Children[0].Action = () => Application.RequestStop();
        var discord = _menuBar.Menus[1];

    }
}
