using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Core.ViewModels.Design;

public class DesignSearchViewModel : SearchViewModel
{
    public DesignSearchViewModel()
    {
        var user = new User()
        {
            Id = new(0),
            Username = "User",
            Discriminator = "0",
            Avatar = string.Empty,
        };
        TotalSearchResult = 3;
        SearchResults.AddRange(new List<Message>()
        {
            CreateMessage("hello", user, new(0)),
            CreateMessage("message in another channel", user, new(1)),
            CreateMessage("message in that channel again", user, new(1)),
        });
    }

    private static Message CreateMessage(string content, User author, Snowflake channel, Message? reference = null) => new Message()
    {
        Content = content,
        Type = MessageType.DEFAULT,
        Author = author,
        Id = new(0),
        ChannelId = channel,
        Timestamp = DateTimeOffset.Now,
        EditedTimestamp = null,
        Tts = false,
        MentionEveryone = false,
        Mentions = Array.Empty<User>(),
        MentionRoles = Array.Empty<Snowflake>(),
        Attachments = Array.Empty<Attachment>(),
        Embeds = Array.Empty<Embed>(),
        Pinned = false,
        ReferencedMessage = reference,
    };
}
