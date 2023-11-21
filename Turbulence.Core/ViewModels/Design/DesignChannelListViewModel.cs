using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels.Design;

public class DesignChannelListViewModel : ChannelListViewModel
{
    public DesignChannelListViewModel()
    {
        Channels.AddRange(new List<Channel>()
        {
            new()
            {
                Id = new(0),
                Type = ChannelType.GUILD_CATEGORY,
                Name = "Category"
            },
            new()
            {
                Id = new(1),
                Type = ChannelType.GUILD_TEXT,
                Name = "Text"
            },
            new()
            {
                Id = new(2),
                Type = ChannelType.GUILD_VOICE,
                Name = "Voice"
            },
            new()
            {
                Id = new(3),
                Type = ChannelType.GUILD_STAGE_VOICE,
                Name = "Stage"
            },
            new()
            {
                Id = new(3),
                Type = ChannelType.GUILD_ANNOUNCEMENT,
                Name = "Announcement"
            },
        });
    }
}
