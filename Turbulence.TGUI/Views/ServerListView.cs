using Terminal.Gui;
using Terminal.Gui.Trees;
using Turbulence.Core.ViewModels;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.TGUI.Views;

public sealed class ServerListView : FrameView
{
    private readonly TreeView _serverTree;
    private readonly ServerListViewModel _vm;
    
    public ServerListView(ServerListViewModel vm)
    {
        _vm = vm;
        _serverTree = new TreeView
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
        
        _serverTree.AddObject(new TreeNode("DMs"));

        _serverTree.SelectionChanged += (_, e) =>
        {
            if (e.NewValue is ChannelNode channelNode)
            { 
                _vm.SelectionChangedCommand.Execute((channelNode.Id, channelNode.Text));
            }
        };
        _vm.TreeUpdated += (_, _) => _serverTree.SetNeedsDisplay();
    }
}

public class ServerTreeBuilder : ITreeBuilder<ITreeNode>
{
    public bool SupportsCanExpand => true;
    private readonly ServerListViewModel _vm;

    public ServerTreeBuilder(ServerListViewModel vm)
    {
        _vm = vm;
    }

    public bool CanExpand(ITreeNode node)
    {
        return node is { Text: "DMs" };
    }

    public IEnumerable<ITreeNode> GetChildren(ITreeNode node)
    {
        if (node is { Text: "DMs" })
        {
            // build a user id 2 name dict
            var userNames = _vm.Users.ToDictionary(u => u.Id, u => u.Username);
            List<ITreeNode> list = new();
            
            
            // TODO: sort by last message timestamp?
            foreach (var dm in _vm.PrivateChannels)
            {
                // get the channel name by getting the name of the recipients (or the id if the lookup fails)
                var name = string.Join(", ", dm.RecipientIDs!.Select(r => userNames.TryGetValue(r, out var userName) ? userName : r.ToString()));
                list.Add(new ChannelNode(dm.Id, name, ChannelType.DM));
            }
            
            return list;
        }

        return new List<ITreeNode>();

        // // // TODO: use a treebuilder
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
        //
        // if (node is TreeNode tree)
        // {
        //     
        // }
        //
        // if (node is ServerNode server)
        // {
        //     return new List<TreeNodeData> { server };
        // }
        //
        // if (node is ChannelNode channel)
        // {
        //     return new List<TreeNodeData> { channel };
        // }
        //
        // throw new NotImplementedException();
    }
}
