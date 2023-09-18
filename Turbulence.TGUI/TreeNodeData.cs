using System.Collections.Immutable;
using Terminal.Gui.Trees;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.TGUI;

// TODO: loaded?
public record ServerNode(Snowflake Id) : ITreeNode
{
    public string? Text { get; set; }
    public IList<ITreeNode>? Children { get; }
    public object? Tag { get; set; }
}

public record ChannelNode : ITreeNode
{
    public Snowflake Id;
    public string Text { get; set; }
    public IList<ITreeNode> Children { get; } = new List<ITreeNode>();
    public object? Tag { get; set; }
    
    public ChannelNode(Snowflake id, string name, ChannelType type)
    {
        Id = id;
        Text = name;
    }
}
