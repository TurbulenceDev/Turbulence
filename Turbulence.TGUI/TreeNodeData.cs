using Turbulence.API.Discord.Models.DiscordChannel;
using Turbulence.API.Discord.Models;

namespace Turbulence.TGUI
{
    public abstract record TreeNodeData
    {

    }
    public record ServerNode(Snowflake ID) : TreeNodeData
    {
        //TODO: loaded?
    }
    public record ChannelNode(ulong ID, string? Name, ChannelType Type) : TreeNodeData
    {
    }
}
