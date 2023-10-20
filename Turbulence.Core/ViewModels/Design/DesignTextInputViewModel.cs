using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Core.ViewModels.Design;

public class DesignTextInputViewModel : TextInputViewModel
{
    public DesignTextInputViewModel()
    {
        ReplyingMessage = new Message()
        {
            Content = "Message to reply to",
            Type = MessageType.DEFAULT,
            Author = new User()
            {
                Id = new(0),
                Username = "User",
                Discriminator = "0",
                Avatar = string.Empty
            },
            Id = new(0),
            ChannelId = new(0),
            Timestamp = DateTimeOffset.UtcNow,
            EditedTimestamp = null,
            Tts = false,
            MentionEveryone = false,
            Mentions = Array.Empty<User>(),
            MentionRoles = Array.Empty<Snowflake>(),
            Attachments = Array.Empty<Attachment>(),
            Embeds = Array.Empty<Embed>(),
            Pinned = false,
        };
    }
}
