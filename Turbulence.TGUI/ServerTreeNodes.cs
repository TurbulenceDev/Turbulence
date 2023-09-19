using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGuild;

namespace Turbulence.TGUI;

public record ServerTreeNode(string Text)
{
    public sealed override string ToString() => Text;
}

public record DmsNode() : ServerTreeNode("DMs");
public record ServerNode(Guild Server) : ServerTreeNode(Server.Name);
public record ChannelNode(Channel Channel, string Text) : ServerTreeNode(Text);
