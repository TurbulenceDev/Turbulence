using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;
using static Turbulence.Discord.Models.DiscordChannel.ChannelType;

namespace Turbulence.Core.ViewModels;

public partial class ServerListViewModel : IRecipient<SetServersMessage>
{
    public readonly List<TreeNodeData> Tree = new();
    public event EventHandler? TreeUpdated;
    private static readonly WeakReferenceMessenger Messenger = new();
    private readonly TurbulenceWindowViewModel _parentVm;

    public ServerListViewModel(TurbulenceWindowViewModel parentVm)
    {
        _parentVm = parentVm;
    }
    
    [RelayCommand]
    private async Task SelectionChanged(TreeNodeData data)
    {
        if (data is ServerNode)
            return; // server => do nothing

        if (data is not ChannelNode node)
            throw new Exception("we shouldnt be here"); // this shouldnt happen

        // channel or dm
        if (node.Type is not (GUILD_TEXT or DM or GROUP_DM))
            return;

        _parentVm.CurrentChannel = node.Id;

        Messenger.Send(new ShowChannelMessage(node));
    }
    public void Receive(SetServersMessage message)
    {
        // TODO: Fix
        
        TreeUpdated?.Invoke(null, EventArgs.Empty);
        // // TODO: use a treebuilder
        // _serverTree.ClearObjects();
        // // first add the private channels
        // var dmNode = new TreeNode("DMs")
        // {
        //     Tag = new ServerNode(new Snowflake(0)),
        // };
        // // build a user id 2 name dict
        // var userNames = users.ToDictionary(u => u.Id, u => u.Username);
        //
        // // TODO: sort by last message timestamp?
        // foreach (var dm in privateChannels)
        // {
        //     // get the channel name by getting the name of the recipients (or the id if the lookup fails)
        //     var name = string.Join(", ", dm.RecipientIDs!.Select(r => userNames.ContainsKey(r) ? userNames[r].ToString() : r.ToString()));
        //     var channelNode = new TreeNode(name)
        //     {
        //         Tag = new ChannelNode(dm.Id, name, dm.Type)
        //     };
        //     dmNode.Children.Add(channelNode);
        // }
        // Tree.Add((TreeNodeData)dmNode.Tag);
        // //_serverTree.AddObject(dmNode);
        // // then add the remaining servers
        // foreach (var server in servers)
        // {
        //     var serverNode = new TreeNode(server.Name)
        //     {
        //         Tag = new ServerNode(server.Id),
        //     };
        //     // TODO: are there channels without parents that have a position other than 0?
        //     var ordered = server.Channels.OrderBy(c => c.ParentId == null ? 0 : 1 + c.Position); // prioritize the ones without parents, then add the position
        //     Dictionary<ulong, TreeNode> channelNodes = new();
        //     foreach (var channel in ordered)
        //     {
        //         // create the node
        //         var channelNode = new TreeNode(channel.Name)
        //         {
        //             Tag = new ChannelNode(channel.Id, channel.Name, channel.Type),
        //         };
        //         // now add to server or parent channel
        //         if (channel.ParentId == null) // add to root
        //         {
        //             serverNode.Children.Add(channelNode);
        //             channelNodes.Add(channel.Id, channelNode);
        //         }
        //         else // has a parent
        //         {
        //             if (channelNodes.TryGetValue(channel.ParentId, out var parent))
        //                 parent.Children.Add(channelNode);
        //             else
        //                 throw new Exception($"Parent channel {channel.ParentId} not found");
        //         }
        //     }
        //     _serverTree.AddObject((TreeNodeData)serverNode.Tag);
        // }
        // // redraw
        // _serverTree.SetNeedsDisplay();
    }
}

public record SetServersMessage(IEnumerable<Channel> PrivateChannels, IEnumerable<User> Users, IEnumerable<Guild> Servers);
