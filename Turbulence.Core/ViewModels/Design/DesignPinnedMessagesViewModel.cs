using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Core.ViewModels.Design;

public class DesignPinnedMessagesViewModel : PinnedMessagesViewModel
{
    public DesignPinnedMessagesViewModel()
    {
        var user1 = new User()
        {
            Id = new(0),
            Username = "User",
            Discriminator = "0",
            Avatar = ""
        };
        var user2 = new User()
        {
            Id = new(0),
            Username = "User2",
            Discriminator = "0",
            Avatar = ""
        };
        var now = DateTimeOffset.Now;
        PinnedMessages.AddRange(new List<Message> {
            CreateMessage("this is a pinned message", MessageType.DEFAULT, user1, now - new TimeSpan(24, 0, 0)),
            CreateMessage("please dont pin this embarassing message", MessageType.DEFAULT, user2, now - new TimeSpan(0, 1, 0)),
        });
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
