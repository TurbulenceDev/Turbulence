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
    }
}
