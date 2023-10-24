using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Core.ViewModels.Design;

public record DesignMessage : Message
{
    public DesignMessage()
    {
        Author = new User()
        {
            Id = new(0),
            Username = "User",
            Discriminator = "0",
            Avatar = null
        };
        Type = MessageType.DEFAULT;
        Content = "This is a test message.";
        Timestamp = DateTime.Now;
        ReferencedMessage = CreateMessage("Reply to this.", MessageType.DEFAULT, Author, DateTimeOffset.Now - new TimeSpan(0, 1, 0));
    }

    private static Message CreateMessage(string content, MessageType type, User author, DateTimeOffset timestamp) => new Message()
    {
        Content = content,
        Type = type,
        Author = author,
        Id = new(0),
        ChannelId = new(0),
        Timestamp = timestamp,
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
