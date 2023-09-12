using Turbulence.API.Discord.Models.DiscordChannel;
using Turbulence.API.Discord.Models;

namespace Turbulence.TGUI
{
    public abstract class TreeNodeData
    {

    }
    public class ServerNode : TreeNodeData
    {
        public Snowflake ID;
        //TODO: loaded?
        public ServerNode(Snowflake id) { ID = id; }
    }

    public class ChannelNode : TreeNodeData
    {
        public ulong ID;
        public string? Name;
        public ChannelType Type;
        //TODO: type
        public ChannelNode(ulong id, string? name, ChannelType type) { ID = id; Name = name; Type = type; }
    }
}
