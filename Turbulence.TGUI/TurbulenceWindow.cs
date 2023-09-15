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
    // TODO: move that class into .Core instead of relying on CLI here :weary:

    private readonly TurbulenceWindowViewModel _vm = new();
    private readonly MenuBarView _menuBar = new();
    private readonly TextInputView _textInput;
    private readonly MessagesView _messages;
    private readonly ServerListView _serverList;

    public TurbulenceWindow()
    {
        Title = "Turbulence";
        Border.BorderStyle = BorderStyle.Rounded;
        Width = Dim.Fill();
        Height = Dim.Fill();

        var messagesVm = new MessagesViewModel(_vm);
        _messages = new MessagesView(messagesVm);
        _textInput = new TextInputView(_vm);
        _serverList = new ServerListView(new ServerListViewModel(_vm, messagesVm));
        
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
        
        // TODO: set token

        // Get Token
        var config = new ConfigurationManager().AddUserSecrets<TurbulenceWindow>().Build();
        if (config["token"] is not { } token)
        {
            MessageBox.ErrorQuery("No token", "No token set. Use 'dotnet user-secrets set token [your token]' to set a token.", "OK");
            throw new Exception("No token set");
        }

        // Start Discord
        // Hook events
        Client.Ready += OnReady;
        Client.MessageCreated += OnMessageCreated;
        Task.Run(_vm.Client.Start);
        _menuBar.SetStatus("Connecting...");
    }

    private void OnReady(object? sender, Event<Ready> e)
    {
        var ready = e.Data;
        _menuBar.SetStatus("Connected");
        SetServers(ready.PrivateChannels, ready.Users, ready.Guilds);
    }

    private void OnMessageCreated(object? sender, Event<Message> e)
    {
        // TODO: this isnt called when sending a dm
        var msg = e.Data;
        if (msg.ChannelId == _vm.CurrentChannel)
        {
            // TODO: move this into a function?
            // add message
            _messages.AddMessage($"{msg.Author.Username}: {msg.Content}");
            // scroll down 1 message
            _messages.MessagesListView.ScrollDown(1);
        }
    }

    public void SetServers(Channel[] privateChannels, User[] users, Guild[] servers)
    {
        // TODO: use a treebuilder
        _serverList.ServerTree.ClearObjects();
        // first add the private channels
        var dmNode = new TreeNode("DMs")
        {
            Tag = new ServerNode(new Snowflake(0)),
        };
        // build a user id 2 name dict
        var userNames = users.ToDictionary(u => u.Id, u => u.Username);
        
        // TODO: sort by last message timestamp?
        foreach (var dm in privateChannels)
        {
            // get the channel name by getting the name of the recipients (or the id if the lookup fails)
            var name = string.Join(", ", dm.RecipientIDs!.Select(r => userNames.ContainsKey(r) ? userNames[r].ToString() : r.ToString()));
            var channelNode = new TreeNode(name)
            {
                Tag = new ChannelNode(dm.Id, name, dm.Type)
            };
            dmNode.Children.Add(channelNode);
        }
        _serverList.ServerTree.AddObject(dmNode);
        // then add the remaining servers
        foreach (var server in servers)
        {
            var serverNode = new TreeNode(server.Name)
            {
                Tag = new ServerNode(server.Id)
            };
            // TODO: are there channels without parents that have a position other than 0?
            var ordered = server.Channels.OrderBy(c => c.ParentId == null ? 0 : 1 + c.Position); // prioritize the ones without parents, then add the position
            Dictionary<ulong, TreeNode> channelNodes = new();
            foreach (var channel in ordered)
            {
                // create the node
                var channelNode = new TreeNode(channel.Name)
                {
                    Tag = new ChannelNode(channel.Id, channel.Name, channel.Type)
                };
                // now add to server or parent channel
                if (channel.ParentId == null) // add to root
                {
                    serverNode.Children.Add(channelNode);
                    channelNodes.Add(channel.Id, channelNode);
                }
                else // has a parent
                {
                    if (channelNodes.TryGetValue(channel.ParentId, out var parent))
                        parent.Children.Add(channelNode);
                    else
                        throw new Exception($"Parent channel {channel.ParentId} not found");
                }
            }
            _serverList.ServerTree.AddObject(serverNode);
        }
        // redraw
        _serverList.ServerTree.SetNeedsDisplay();
    }
}
