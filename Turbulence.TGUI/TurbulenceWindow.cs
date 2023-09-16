using Microsoft.Extensions.Configuration;
using Terminal.Gui;
using Terminal.Gui.Trees;
using Turbulence.Core;
using Turbulence.Core.ViewModels;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;
using Turbulence.Discord;
using Turbulence.TGUI.Views;
using Channel = Turbulence.Discord.Models.DiscordChannel.Channel;
using Message = Turbulence.Discord.Models.DiscordChannel.Message;

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
        _serverList = new ServerListView(new ServerListViewModel(_vm));
        
        Add(_menuBar);
        Add(_textInput);
        Add(_messages);
        Add(_serverList);

        // Set component stuff
        _menuBar.SetStatus("Not connected");
        
        // Hook up events
        // menu bar
        var file = _menuBar.Menus[0];
        file.Children[0].Action = () => Application.RequestStop();
        var discord = _menuBar.Menus[1];

        // Start Discord
        // Hook events

        Task.Run(_vm.Client.Start);
        _menuBar.SetStatus("Connecting...");
    }
}
