using System.Drawing;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordEmoji;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord.Services;

public interface ICache
{
    public byte[]? GetAvatar(Snowflake userId, int size);
    public byte[]? GetEmoji(Snowflake emojiId, int size);
    public Guild? GetGuild(Snowflake guildId);
    public User? GetUser(Snowflake userId);
    public Channel? GetChannel(Snowflake channelId);

    public void SetAvatar(Snowflake userId, int size, byte[] avatar);
    public void SetEmoji(Snowflake emojiId, int size, byte[] emoji);
    public void SetGuild(Guild guild);
    public void SetUser(User user);
    public void SetChannel(Channel channel);
}

public class Cache : ICache
{
    private readonly Dictionary<(Snowflake, int), byte[]> _avatars = new();
    private readonly Dictionary<(Snowflake, int), byte[]> _emojis = new();
    private readonly Dictionary<Snowflake, Guild> _guilds = new();
    private readonly Dictionary<Snowflake, User> _users = new();
    private readonly Dictionary<Snowflake, Channel> _channels = new();

    public byte[]? GetAvatar(Snowflake userId, int size)
    {
        return _avatars.TryGetValue((userId, size), out var avatar) ? avatar : null;
    }

    public byte[]? GetEmoji(Snowflake emojiId, int size)
    {
        return _emojis.TryGetValue((emojiId, size), out var emoji) ? emoji : null;
    }
    public Guild? GetGuild(Snowflake guildId)
    {
        return _guilds.TryGetValue(guildId, out var guild) ? guild : null;
    }

    public User? GetUser(Snowflake userId)
    {
        return _users.TryGetValue(userId, out var user) ? user : null;
    }

    public Channel? GetChannel(Snowflake channelId)
    {
        return _channels.TryGetValue(channelId, out var channel) ? channel : null;
    }

    public void SetAvatar(Snowflake userId, int size, byte[] avatar)
    {
        _avatars[(userId, size)] = avatar;
    }

    public void SetEmoji(Snowflake emojiId, int size, byte[] emoji)
    {
        _avatars[(emojiId, size)] = emoji;
    }

    public void SetGuild(Guild guild)
    {
        _guilds[guild.Id] = guild;
    }

    public void SetUser(User user)
    {
        _users[user.Id] = user;
    }

    public void SetChannel(Channel channel)
    {
        _channels[channel.Id] = channel;
    }
}