using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels.Design;

public class DesignChannelListViewModel : ChannelListViewModel
{
    public DesignChannelListViewModel()
    {
        var text = new Channel()
        {
            Id = new(1),
            Type = ChannelType.GUILD_TEXT,
            Name = "Text"
        };
        Channels.AddRange(new List<Channel>()
        {
            new()
            {
                Id = new(0),
                Type = ChannelType.GUILD_CATEGORY,
                Name = "Category"
            },
            text,
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
                Id = new(4),
                Type = ChannelType.GUILD_ANNOUNCEMENT,
                Name = "Announcement"
            },
            new()
            {
                Id = new(0),
                Type = ChannelType.GUILD_CATEGORY,
                Name = "DMs"
            },
            new()
            {
                Id = new(0),
                RecipientIDs = new Snowflake[]
                {
                    new(12345678)
                },
                Type = ChannelType.DM,
                Name = "DM"
            },
            new()
            {
                Id = new(0),
                RecipientIDs = new Snowflake[]
                {
                    new(12345678),
                    new(12345679)
                },
                Type = ChannelType.GROUP_DM,
                Name = "Group DM"
            },
        });
        SelectedChannel = text;
    }
}
