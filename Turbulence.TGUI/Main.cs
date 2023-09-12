using Microsoft.Extensions.Configuration;
using Terminal.Gui;
using Terminal.Gui.Trees;
using Turbulence.API.Discord.Models.DiscordChannel;
using Turbulence.API.Discord.Models.DiscordGateway;
using Turbulence.API.Discord.Models.DiscordGuild;
using Turbulence.CLI;

namespace Turbulence.TGUI
{
    public partial class Main
    {
        public Discord Discord;
        //TODO: move that class into .Core instead of relying on CLI here :weary:

        public Main()
        {
            InitializeComponent();
            // Hook up events
            /// menu bar
            var file = menuBar.Menus[0];
            file.Children[0].Action = () => Application.RequestStop();
            var discord = menuBar.Menus[1];
            //TODO: set token
            /// text input
            sendButton.Clicked += () => SendMessage();
            /// server view
            serverTree.SelectionChanged += ServerTree_SelectionChanged;
            //TODO: use something other than a textfield for messages

            // Get Token
            var config = new ConfigurationManager().AddUserSecrets<Main>().Build();
            if (config["token"] is not { } token)
            {
                MessageBox.ErrorQuery("No token", "No token set. Use 'dotnet user-secrets set token [your token]' to set a token.", "OK");
                return;
            }

            // Start Discord
            Discord = new(token);
            // Hook events
            Discord.OnReadyEvent += Discord_OnReadyEvent;
            Task.Run(Discord.Start);
            statusMenu.Title = "Connecting...";
        }

        private void Discord_OnReadyEvent(object? sender, Event<Ready> e)
        {
            var ready = e.Data;
            statusMenu.Title = "Connected";
            SetServers(ready.Guilds);
        }

        public void SendMessage()
        {
            var content = textInput.Text;
            if (content.IsEmpty)
                return;

            //TODO: send
        }

        private async void ServerTree_SelectionChanged(object? sender, SelectionChangedEventArgs<ITreeNode> e)
        {
            if (e.NewValue.Tag is ServerNode)
                return; // server => do nothing

            if (e.NewValue.Tag is ChannelNode node)
            {
                // channel
                if (node.Type == ChannelType.GUILD_TEXT)
                {
                    messageView.Title = $"Messages: {node.Name}";
                    var msgs = await Discord.GetMessages(node.ID);
                    messages.Text = string.Join("\n", msgs.Reverse().Select(m => $"{m.Author.Username}: {m.Content}"));
                    //TODO: scroll down to the bottom
                }
                return;
            }

            //this shouldnt happen
            throw new Exception("we shouldnt be here");
        }

        public void SetServers(Guild[] servers)
        {
            //TODO: use a treebuilder
            serverTree.ClearObjects();
            foreach (var server in servers)
            {
                var serverNode = new TreeNode(server.Name)
                {
                    Tag = new ServerNode(server.Id)
                };
                //TODO: are there channels without parents that have a position other than 0?
                var ordered = server.Channels.OrderBy(c => (c.ParentId == null) ? 0 : 1 + c.Position); // prioritize the ones without parents, then add the position
                Dictionary<ulong, TreeNode> channelNodes = new();
                foreach (var channel in ordered)
                {
                    // create the node
                    var channelNode = new TreeNode(channel.Name);
                    channelNode.Tag = new ChannelNode(channel.Id, channel.Name, channel.Type);
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
        }
    }
}
