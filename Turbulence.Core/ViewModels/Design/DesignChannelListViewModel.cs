using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels.Design;

public class DesignChannelListViewModel : ChannelListViewModel
{
    public DesignChannelListViewModel()
    {
        Channels.AddRange(new List<Channel>()
        {
            new Channel()
            {
                Id = new(0),
                Type = ChannelType.GUILD_CATEGORY,
                Name = "Category"
            },
            new Channel()
            {
                Id = new(1),
                Type = ChannelType.GUILD_TEXT,
                Name = "Text"
            },
            new Channel()
            {
                Id = new(2),
                Type = ChannelType.GUILD_VOICE,
                Name = "Voice"
            }
        });
    }
}
