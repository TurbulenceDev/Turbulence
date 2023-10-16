using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels.Design;

public class DesignChannelBarViewModel : ChannelBarViewModel
{
    public DesignChannelBarViewModel()
    {
        Channel = new()
        {
            Id = new(0),
            Type = ChannelType.GUILD_TEXT,
            Name = "Channel",
            Topic = "Description",
        };
    }
}
