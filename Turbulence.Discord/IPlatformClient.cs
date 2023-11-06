using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGatewayEvents;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;
using Turbulence.Discord.Services;

namespace Turbulence.Discord;

public interface IPlatformClient
{
    public bool Connected { get; }
    public Task<Channel> GetChannel(Snowflake channelId);
    public Task Start(string token);
    public Task<List<Message>> GetMessages(Snowflake channelId);
    public Task<List<Message>> GetMessagesAround(Snowflake channelId, Snowflake messageId);
    public Task<Message[]> GetPinnedMessages(Snowflake channelId);
    public Task<SearchResult> SearchMessages(SearchRequest request);
    public Task<Guild> GetGuild(Snowflake guildId);
    public event EventHandler<Event<Ready>>? Ready;
    public event EventHandler<Event<Message>>? MessageCreated;
    public event EventHandler<Event<TypingStartEvent>>? TypingStart;

    public User? CurrentUser { get; set; }
    public Task<Image> GetAvatar(User user, int size = 128);
    
    #region Discord specific shit that should not be here

    public bool SendGatewayMessage<T>(GatewayOpcode opcode, T data);

    public HttpClient HttpClient { get; }

    #endregion
}
