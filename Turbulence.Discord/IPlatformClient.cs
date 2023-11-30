using Turbulence.Discord.Models.DiscordVoice;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordEmoji;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGatewayEvents;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord;

public interface IPlatformClient
{
    public bool Connected { get; }
    public Task<Channel> GetChannel(Snowflake channelId);
    public Task Start(string token);
    public Task<List<Message>> GetMessages(Snowflake channelId);
    public Task<List<Message>> GetMessagesAround(Snowflake channelId, Snowflake messageId);
    public Task<List<Message>> GetMessagesBefore(Snowflake channelId, Snowflake messageId);
    public Task<List<Message>> GetMessagesAfter(Snowflake channelId, Snowflake messageId);
    public Task<Message[]> GetPinnedMessages(Snowflake channelId);
    public Task<SearchResult> SearchMessages(SearchRequest request);
    public Task<Message> SendMessage(Channel channel, string content, Message? reply = null, bool shouldPing = false);
    public Task<Message> EditMessage(string input, Message original);
    public Task DeleteMessage(Message message);
    public Task<GuildMember> GetCurrentUserGuildMember(Snowflake guildId);
    public Task<Guild> GetGuild(Snowflake guildId);
    public event EventHandler<Event<bool>>? OnConnectionStatusChanged;
    public event EventHandler<Event<Ready>>? Ready;
    public event EventHandler<Event<Message>>? MessageCreated;
    public event EventHandler<Event<Message>>? MessageUpdated;
    public event EventHandler<Event<MessageDeleteEvent>>? MessageDeleted;
    public event EventHandler<Event<TypingStartEvent>>? TypingStart;
    public event EventHandler<Event<VoiceState>>? VoiceStateUpdated;

    public User? CurrentUser { get; set; }
    public Task<byte[]> GetImageAsync(string url);
    public Task<byte[]> GetAvatarAsync(User user, int size = 128);
    public Task<byte[]> GetEmojiAsync(Emoji emoji, int size = 32);
    public Task<string> GetChannelName(Channel channel);
    public string GetMessageContent(Message message);

    /// <summary>
	/// Sends a voice state message to the gateway declaring that either the user has joined/left vc or (un)muted themself
	/// </summary>
	/// <param name="guildId">The server id where the vc resides, <c>null</c> meaning no server (disconnected or DMs)</param>
	/// <param name="channelId">The id of the voice channel, <c>null</c> meaning no channel (disconnected)</param>
    public void VoiceStateUpdate(Snowflake? guildId, Snowflake? channelId);

    #region Discord specific shit that should not be here

    public bool SendGatewayMessage<T>(GatewayOpcode opcode, T data);

    #endregion
}
