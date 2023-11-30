using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordEmoji;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;
using Turbulence.Discord.Models;
using Turbulence.Discord.Services;

namespace Turbulence.Discord;

public partial class Client
{
    //TODO: move these into the cache
    public User? CurrentUser { get; set; }
    //public static List<dynamic> MemberInfos = new(); // TODO: should we like put the roles into a simple array?
    //public static List<dynamic> ServerSettings = new(); // TODO: listen to the guild settings update event

    public async Task<User> GetCurrentUser()
    {
        return CurrentUser ?? await Api.GetCurrentUser(HttpClient);
    }

    // TODO: cache this or smth
    public async Task<List<Message>> GetMessages(Snowflake channelId)
    {
        return await Api.GetChannelMessages(HttpClient, channelId);
    }
    public async Task<List<Message>> GetMessagesAround(Snowflake channelId, Snowflake messageId)
    {
        return await Api.GetChannelMessages(HttpClient, channelId, around: messageId);
    }
    public async Task<List<Message>> GetMessagesBefore(Snowflake channelId, Snowflake messageId)
    {
        return await Api.GetChannelMessages(HttpClient, channelId, before: messageId);
    }
    public async Task<List<Message>> GetMessagesAfter(Snowflake channelId, Snowflake messageId)
    {
        return await Api.GetChannelMessages(HttpClient, channelId, after: messageId);
    }

    public async Task<Message> SendMessage(Channel channel, string content, Message? reply = null, bool shouldPing = false)
    {
        return await Api.CreateAndSendMessage(HttpClient, channel, content, reply, shouldPing);
    }

    public async Task<Message> EditMessage(string input, Message original)
    {
        return await Api.EditMessage(HttpClient, input, original);
    }

    public async Task DeleteMessage(Message message)
    {
        await Api.DeleteMessage(HttpClient, message);
    }

    public async Task<GuildMember> GetCurrentUserGuildMember(Snowflake guildId)
    {
        return await Api.GetCurrentUserGuildMember(HttpClient, guildId);
    }

    public async Task<Guild> GetGuild(Snowflake guildId)
    {
        if (_cache.GetGuild(guildId) is { } guild)
            return guild;
        guild = await Api.GetGuild(HttpClient, guildId);
        _logger?.Log($"Requested guild {guild.Name} ({guild.Id})", LogType.Networking, LogLevel.Debug);
        _cache.SetGuild(guild);
        return guild;
    }
    public async Task<User> GetUser(Snowflake userId)
    {
        if (_cache.GetUser(userId) is { } user)
            return user;
        user = await Api.GetUser(HttpClient, userId);
        _logger?.Log($"Requested user {user.Username} ({user.Id})", LogType.Networking, LogLevel.Debug);
        _cache.SetUser(user);
        return user;
    }
    public async Task<Channel> GetChannel(Snowflake channelId)
    {
        if (_cache.GetChannel(channelId) is { } channel)
            return channel;
        channel = await Api.GetChannel(HttpClient, channelId);
        _logger?.Log($"Requested channel {channel.Name} ({channel.Id})", LogType.Networking, LogLevel.Debug);
        _cache.SetChannel(channel);
        return channel;
    }

    public async Task<byte[]> GetAvatarAsync(User user, int size = 128)
    {
        if (_cache.GetAvatar(user.Id, size) is { } avatar)
            return avatar;

        if (user.Avatar == null)
        {
            avatar = await Api.GetDefaultAvatarAsync(CdnClient, user);
        }
        else
        {
            avatar = await Api.GetAvatarAsync(CdnClient, user, size);
        }
        _logger?.Log($"Requested avatar for user {user.Id}", LogType.Images, LogLevel.Debug);

        _cache.SetAvatar(user.Id, size, avatar);
        return avatar;
    }

    public async Task<byte[]> GetEmojiAsync(Emoji emoji, int size = 32)
    {
        if (emoji.Id == null)
            return Array.Empty<byte>(); //TODO: instead get image from a local emoji cache?

        if (_cache.GetEmoji(emoji.Id, size) is { } img)
            return img;

        img = await Api.GetEmojiAsync(CdnClient, emoji, size);
        _logger?.Log($"Requested emoji with ID {emoji.Id}", LogType.Images, LogLevel.Debug);

        _cache.SetEmoji(emoji.Id, size, img);
        return img;
    }

    public async Task<Message[]> GetPinnedMessages(Snowflake channelId)
    {
        return await Api.GetPinnedMessages(HttpClient, channelId);
    }

    public async Task<SearchResult> SearchMessages(SearchRequest request)
    {
        return await Api.SearchMessages(HttpClient, request);
    }
}
