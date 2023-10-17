using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord;

public interface IPlatformClient
{
    public Task<Channel> GetChannel(Snowflake id);
    public Task Start();
    public Task<List<Message>> GetMessages(Snowflake id);
    public Task<Message[]> GetPinnedMessages(Snowflake id);
    public Task<Guild> GetGuild(Snowflake id);
    public event EventHandler<Event<Ready>>? Ready;
    public event EventHandler<Event<Message>>? MessageCreated;
    public User? CurrentUser { get; set; }
    public Task<Image> GetAvatar(User user, int size = 128);
    
    #region Discord specific shit that should not be here

    public bool SendGatewayMessage<T>(GatewayOpcode opcode, T data);
    public HttpClient HttpClient { get; }

    #endregion
}
