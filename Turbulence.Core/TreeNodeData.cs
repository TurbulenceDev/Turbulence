using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core;

public abstract record TreeNodeData;

// TODO: loaded?
public record ServerNode(Snowflake Id) : TreeNodeData;

public record ChannelNode(ulong Id, string? Name, ChannelType Type) : TreeNodeData;
