using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Core.ViewModels.Design;

public class DesignTextInputViewModel : TextInputViewModel
{
    public DesignTextInputViewModel()
    {
        var user = new User()
        {
            Id = new(0),
            Username = "User",
            Discriminator = "0",
            Avatar = string.Empty
        };
        ReplyingMessage = CreateMessage("Message to reply to", user);
        EditingMessage = CreateMessage("Message we're editing", user);
    }

    private static Message CreateMessage(string content, User user) => new()
    {
        Content = content,
        Type = MessageType.DEFAULT,
        Author = user,
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
