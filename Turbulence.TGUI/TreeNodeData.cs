using Turbulence.API.Discord.Models.DiscordChannel;
using Turbulence.API.Discord.Models;

namespace Turbulence.TGUI;

public abstract record TreeNodeData;

// TODO: loaded?
public record ServerNode(Snowflake Id) : TreeNodeData;

public record ChannelNode(ulong Id, string? Name, ChannelType Type) : TreeNodeData;