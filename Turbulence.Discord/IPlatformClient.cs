using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord;

public interface IPlatformClient
{
    public Task<Channel> GetChannel(Snowflake channelId);
    public Task Start();
    public Task<Message[]> GetMessages(Snowflake channelId);
    public Task<Guild> GetGuild(Snowflake guildId);
    public event EventHandler<Event<Ready>>? Ready;
    public event EventHandler<Event<Message>>? MessageCreated;
    public User? CurrentUser { get; set; }
    
    #region Discord specific shit that should not be here

    public bool SendGatewayMessage<T>(GatewayOpcode opcode, T data);
    public HttpClient HttpClient { get; }

    #endregion
}
