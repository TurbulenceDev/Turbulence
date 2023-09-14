using Microsoft.Extensions.Configuration;
using Terminal.Gui;
using Terminal.Gui.Trees;
using Turbulence.API.Discord.Models;
using Turbulence.API.Discord.Models.DiscordChannel;
using Turbulence.API.Discord.Models.DiscordGateway;
using Turbulence.API.Discord.Models.DiscordGuild;
using Turbulence.API.Discord.Models.DiscordUser;
using Turbulence.CLI;
using Channel = Turbulence.API.Discord.Models.DiscordChannel.Channel;

namespace Turbulence.TGUI;

public partial class TurbulenceWindow : Window
{
    private readonly Discord _discord;
    private ulong _currentChannel = 0;
    private readonly List<string> _currentMessages = new();
    // TODO: move that class into .Core instead of relying on CLI here :weary:

    // Additional components
    private readonly ScrollBarView _messageScrollbar;

    public TurbulenceWindow()
    {
        InitializeComponent();
        // Set component stuff
        _messageScrollbar = new ScrollBarView(messages, true);
        messages.SetSource(_currentMessages);
        statusMenu.Title = "Not connected";
        
        // Hook up events
        // menu bar
        var file = menuBar.Menus[0];
        file.Children[0].Action = () => Application.RequestStop();
        var discord = menuBar.Menus[1];
        
        // TODO: set token
        // text input
        sendButton.Clicked += SendMessage;
        
        // server view
        serverTree.SelectionChanged += ServerTree_SelectionChanged;
        
        // messages
        // Draw scrollbar on
        messages.DrawContent += (e) => {
            _messageScrollbar.Size = messages.Source.Count;
            _messageScrollbar.Position = messages.TopItem;
            _messageScrollbar.OtherScrollBarView.Size = messages.Maxlength;
            _messageScrollbar.OtherScrollBarView.Position = messages.LeftItem;
            _messageScrollbar.Refresh();
        };
        // Vertical set
        _messageScrollbar.ChangedPosition += () => {
            messages.TopItem = _messageScrollbar.Position;
            if (messages.TopItem != _messageScrollbar.Position)
            {
                _messageScrollbar.Position = messages.TopItem;
            }
            messages.SetNeedsDisplay();
        };
        // Horizontal set
        _messageScrollbar.OtherScrollBarView.ChangedPosition += () => {
            messages.LeftItem = _messageScrollbar.OtherScrollBarView.Position;
            if (messages.LeftItem != _messageScrollbar.OtherScrollBarView.Position)
            {
                _messageScrollbar.OtherScrollBarView.Position = messages.LeftItem;
            }
            messages.SetNeedsDisplay();
        };

        // Get Token
        var config = new ConfigurationManager().AddUserSecrets<TurbulenceWindow>().Build();
        if (config["token"] is not { } token)
        {
            MessageBox.ErrorQuery("No token", "No token set. Use 'dotnet user-secrets set token [your token]' to set a token.", "OK");
            return;
        }

        // Start Discord
        _discord = new Discord(token);
        // Hook events
        Discord.OnReadyEvent += Discord_OnReadyEvent;
        Discord.OnMessageCreate += Discord_OnMessageCreate;
        Task.Run(_discord.Start);
        statusMenu.Title = "Connecting...";
    }

    private void Discord_OnReadyEvent(object? sender, Event<Ready> e)
    {
        var ready = e.Data;
        statusMenu.Title = "Connected";
        SetServers(ready.PrivateChannels, ready.Users, ready.Guilds);
    }

    private void Discord_OnMessageCreate(object? sender, Event<Message> e)
    {
        // TODO: this isnt called when sending a dm
        var msg = e.Data;
        if (msg.ChannelId == _currentChannel)
        {
            // TODO: move this into a function?
            // add message
            _currentMessages.Add($"{msg.Author.Username}: {msg.Content}");
            // scroll down 1 message
            messages.ScrollDown(1);
        }
    }

    public async void SendMessage()
    {
        var content = textInput.Text;
        if (content.IsEmpty)
            return;

        var channel = _currentChannel;
        if (channel == 0)
            return; // TODO: user feedback?

        // send
        textInput.Text = string.Empty;
        await _discord.SendMessage(channel, content.ToString()!);
        // TODO: refresh messages?
    }

    private async void ServerTree_SelectionChanged(object? sender, SelectionChangedEventArgs<ITreeNode> e)
    {
        if (e.NewValue.Tag is ServerNode)
            return; // server => do nothing

        if (e.NewValue.Tag is ChannelNode node)
        {
            // channel or dm
            if (node.Type == ChannelType.GUILD_TEXT || node.Type == ChannelType.DM || node.Type == ChannelType.GROUP_DM)
            {
                _currentChannel = node.Id;
                messageView.Title = $"Messages: {node.Name}";
                var msgs = await _discord.GetMessages(node.Id);
                _currentMessages.Clear();
                foreach (var msg in msgs.Reverse())
                {
                    _currentMessages.Add($"{msg.Author.Username}: {msg.Content}");
                }
                // scroll down to the bottom (also refreshes)
                messages.SelectedItem = _currentMessages.Count - 1; // else mouse scrolling will start at the beginning
                messages.ScrollDown(_currentMessages.Count);
            }
            return;
        }

        // this shouldnt happen
        throw new Exception("we shouldnt be here");
    }

    public void SetServers(Channel[] privateChannels, User[] users, Guild[] servers)
    {
        // TODO: use a treebuilder
        serverTree.ClearObjects();
        // first add the private channels
        var dmNode = new TreeNode("DMs")
        {
            Tag = new ServerNode(new(0))
        };
        // build a user id 2 name dict
        Dictionary<Snowflake, string> userNames = new();
        foreach (var user in users)
        {
            userNames.Add(user.Id, user.Username);
        }
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
        serverTree.AddObject(dmNode);
        // then add the remaining servers
        foreach (var server in servers)
        {
            var serverNode = new TreeNode(server.Name)
            {
                Tag = new ServerNode(server.Id)
            };
            // TODO: are there channels without parents that have a position other than 0?
            var ordered = server.Channels.OrderBy(c => (c.ParentId == null) ? 0 : 1 + c.Position); // prioritize the ones without parents, then add the position
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
            serverTree.AddObject(serverNode);
        }
        // redraw
        serverTree.SetNeedsDisplay();
    }
}