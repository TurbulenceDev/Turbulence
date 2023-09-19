using Terminal.Gui;
using Terminal.Gui.Trees;
using Turbulence.Core.ViewModels;
using Turbulence.Discord;
using static Turbulence.Discord.Models.DiscordChannel.ChannelType;

namespace Turbulence.TGUI.Views;

public sealed class ServerListView : FrameView
{
    private readonly TreeView<ServerTreeNode> _serverTree;
    private readonly ServerListViewModel _vm = new();
    
    public ServerListView()
    {
        _serverTree = new TreeView<ServerTreeNode>
        {
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            TreeBuilder = new ServerTreeBuilder(_vm),
        };
        
        Title = "Servers";
        X = 0;
        Y = 1;
        Width = 25;
        Height = Dim.Fill();
        Border = new Border { BorderStyle = BorderStyle.Rounded };
        
        Add(_serverTree);

        _serverTree.SelectionChanged += (_, e) =>
        {
            if (e.NewValue is ChannelNode
                {
                    Channel.Type: GUILD_TEXT or GUILD_FORUM or GROUP_DM or DM or PUBLIC_THREAD or PRIVATE_THREAD
                    or ANNOUNCEMENT_THREAD or GUILD_ANNOUNCEMENT or GUILD_MEDIA, // TODO: this sucks
                } channelNode)
            { 
                _vm.SelectionChangedCommand.Execute(channelNode.Channel);
            }
        };
        _vm.TreeUpdated += (_, _) =>
        {
            _serverTree.ClearObjects();
            _serverTree.AddObject(new DmsNode());
            _serverTree.AddObjects(_vm.Servers.Select(g => new ServerNode(g)));
            _serverTree.SetNeedsDisplay();
        };
    }
}

public class ServerTreeBuilder : ITreeBuilder<ServerTreeNode>
{
    public bool SupportsCanExpand => true;
    private readonly ServerListViewModel _vm;

    public ServerTreeBuilder(ServerListViewModel vm)
    {
        _vm = vm;
    }

    public bool CanExpand(ServerTreeNode node)
    {
        return node is DmsNode or ServerNode or ChannelNode { Channel.Type: GUILD_CATEGORY };
    }

    public IEnumerable<ServerTreeNode> GetChildren(ServerTreeNode node)
    {
        switch (node)
        {
            case DmsNode:
            {
                // build a user id 2 name dict
                var userNames = _vm.Users.ToDictionary(u => u.Id, u => u.Username);
                List<ServerTreeNode> list = new();
            
                // TODO: Sort by last message timestamp?
                foreach (var dm in _vm.PrivateChannels)
                {
                    // get the channel name by getting the name of the recipients (or the id if the lookup fails)
                    var name = string.Join(", ", dm.RecipientIDs!.Select(r => userNames.TryGetValue(r, out var userName) ? userName : r.ToString()));
                    list.Add(new ChannelNode(dm, name));
                }
            
                return list;
            }
            case ServerNode serverNode:
            {
                // Return channels that have no parent ordered by position
                // TODO: Are there channels without parents that have a position other than 0?
                // INFO: https://github.com/Rapptz/discord.py/issues/2392#issuecomment-707455919
                return serverNode.Server.Channels
                    .Where(c => c.ParentId == null) // top level
                    .OrderBy(c => c.Type == GUILD_CATEGORY ? 1 : 0) // channels without categories should be at the top
                    .ThenBy(c => c.Type) // then by channel type
                    .ThenBy(c => c.Position) // then sort by position
                    .ThenBy(c => c.Id) // sort by raw channel id if pos are the same
                    .Select(c => new ChannelNode(c, c.Name ?? throw new Exception("Server channel has no name")));
            }
            case ChannelNode { Channel.Type: GUILD_CATEGORY } categoryNode:
            {
                if (_vm.Servers.Find(g => g.Id == categoryNode.Channel.GuildId) is not { } guild)
                {
                    // TODO: Can we do this another way?
                    var channel = Task.Run(() => Api.GetChannel(Client.HttpClient, categoryNode.Channel.Id)).GetAwaiter().GetResult();

                    if (_vm.Servers.Find(g => g.Id == channel.GuildId) is not { } guild2)
                    {
                        throw new Exception("Can't find guild associated with channel");
                    }

                    // TODO: Need to deduplicate this code somehow but too tired right now
                    return guild2.Channels
                        .Where(c => c.ParentId == categoryNode.Channel.Id)
                        .OrderBy(c => c.Type)
                        .ThenBy(c => c.Position)
                        .ThenBy(c => c.Id)
                        .Select(c => new ChannelNode(c, c.Name ?? throw new Exception("Guild channel has no name")));
                }

                // Return children of this channel category
                return guild.Channels
                    .Where(c => c.ParentId == categoryNode.Channel.Id)
                    .OrderBy(c => c.Type)
                    .ThenBy(c => c.Position)
                    .ThenBy(c => c.Id)
                    .Select(c => new ChannelNode(c, c.Name ?? throw new Exception("Guild channel has no name")));
            }
            default:
                return Enumerable.Empty<ServerTreeNode>();
        }
    }
}
