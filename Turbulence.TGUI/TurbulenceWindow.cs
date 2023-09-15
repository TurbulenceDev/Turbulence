using Microsoft.Extensions.Configuration;
using Terminal.Gui;
using Terminal.Gui.Trees;
using Turbulence.API.Discord.Models;
using Turbulence.API.Discord.Models.DiscordChannel;
using Turbulence.API.Discord.Models.DiscordGateway;
using Turbulence.API.Discord.Models.DiscordGuild;
using Turbulence.API.Discord.Models.DiscordUser;
using Turbulence.CLI;
using Turbulence.TGUI.Views;
using static Turbulence.API.Discord.Models.DiscordChannel.ChannelType;
using Channel = Turbulence.API.Discord.Models.DiscordChannel.Channel;

namespace Turbulence.TGUI;

public sealed class TurbulenceWindow : Window
{
    private readonly Discord _discord;
    public ulong CurrentChannel = 0;
    private readonly List<string> _currentMessages = new();
    // TODO: move that class into .Core instead of relying on CLI here :weary:

    private readonly MenuBarView _menuBar = new();
    private readonly TextInputView _textInput;
    private readonly MessagesView _messages = new();
    private readonly ServerListView _serverList = new();

    public TurbulenceWindow()
    {
        Title = "Turbulence";
        Border.BorderStyle = BorderStyle.Rounded;
        Width = Dim.Fill();
        Height = Dim.Fill();

        _textInput = new TextInputView(this);
        
        Add(_menuBar);
        Add(_textInput);
        Add(_messages);
        Add(_serverList);

        // Set component stuff
        _messages.Messages.SetSource(_currentMessages);
        _menuBar.SetStatus("Not connected");
        
        // Hook up events
        // menu bar
        var file = _menuBar.Menus[0];
        file.Children[0].Action = () => Application.RequestStop();
        var discord = _menuBar.Menus[1];
        
        // TODO: set token
        // text input
        
        // server view
        _serverList.ServerTree.SelectionChanged += ServerTree_SelectionChanged;

        // Get Token
        var config = new ConfigurationManager().AddUserSecrets<TurbulenceWindow>().Build();
        if (config["token"] is not { } token)
        {
            MessageBox.ErrorQuery("No token", "No token set. Use 'dotnet user-secrets set token [your token]' to set a token.", "OK");
            throw new Exception("No token set");
        }

        // Start Discord
        _discord = new Discord();
        // Hook events
        Discord.OnReadyEvent += Discord_OnReadyEvent;
        Discord.OnMessageCreate += Discord_OnMessageCreate;
        Task.Run(_discord.Start);
        _menuBar.SetStatus("Connecting...");
    }

    private void Discord_OnReadyEvent(object? sender, Event<Ready> e)
    {
        var ready = e.Data;
        _menuBar.SetStatus("Connected");
        SetServers(ready.PrivateChannels, ready.Users, ready.Guilds);
    }

    private void Discord_OnMessageCreate(object? sender, Event<Message> e)
    {
        // TODO: this isnt called when sending a dm
        var msg = e.Data;
        if (msg.ChannelId == CurrentChannel)
        {
            // TODO: move this into a function?
            // add message
            _currentMessages.Add($"{msg.Author.Username}: {msg.Content}");
            // scroll down 1 message
            _messages.Messages.ScrollDown(1);
        }
    }

    private async void ServerTree_SelectionChanged(object? sender, SelectionChangedEventArgs<ITreeNode> e)
    {
        if (e.NewValue.Tag is ServerNode)
            return; // server => do nothing

        if (e.NewValue.Tag is not ChannelNode node)
            throw new Exception("we shouldnt be here"); // this shouldnt happen

        // channel or dm
        if (node.Type is not (GUILD_TEXT or DM or GROUP_DM))
            return;
            
        CurrentChannel = node.Id;
        _messages.Title = $"Messages: {node.Name}";
        var msgs = await _discord.GetMessages(node.Id);
        _currentMessages.Clear();
        foreach (var msg in msgs.Reverse())
        {
            _currentMessages.Add($"{msg.Author.Username}: {msg.Content}");
        }

        // scroll down to the bottom (also refreshes)
        _messages.Messages.SelectedItem = _currentMessages.Count - 1; // else mouse scrolling will start at the beginning
        _messages.Messages.ScrollDown(_currentMessages.Count);
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
