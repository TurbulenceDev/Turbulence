using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordEmoji;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord;

internal static class Api
{
    public const string Version = "9";
    public const string VoiceVersion = "7";
    private const string RootAdress = "https://discord.com/api";
    private const string ApiRoot = $"{RootAdress}/v{Version}";
    private const string CdnRoot = "https://cdn.discordapp.com/";

    private static async Task<HttpResponseMessage> SendRequest(HttpClient client, HttpRequestMessage req)
    {
        var response = await client.SendAsync(req);

        if (!response.IsSuccessStatusCode)
        {
            if (await response.Content.ReadFromJsonAsync<Error>() is not { } error)
            {
                throw new ApiException(
                    $@"Failed to {req.Method} with code {(int)response.StatusCode}:
{await response.Content.ReadAsStringAsync()}");
            }

            if (error.Errors == null)
            {
                throw new ApiException($"API responded with error: {error.Message} ({error.Code?.ToString() ?? "no error code"})");
            }

            throw new ApiException($@"{error.Message} ({error.Code?.ToString() ?? "no error code"}):
{JsonSerializer.Serialize(error.Errors, new { WriteIndented = true })}");

        }

        //TODO: log
        //Console.WriteLine(await response.Content.ReadAsStringAsync());
        return response;
    }

    // Additionally parses the response content from json to the given type
    private static async Task<T> SendRequest<T>(HttpClient client, HttpRequestMessage req)
    {
        var response = await SendRequest(client, req);
        //var res = await response.Content.ReadAsStringAsync();
        return await response.Content.ReadFromJsonAsync<T>() ?? throw new ApiException($"ApiCall to {req.RequestUri!.AbsolutePath} failed");
    }

    private static async Task<T> Get<T>(HttpClient client, string endpoint)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, $"{ApiRoot}{endpoint}");
        return await SendRequest<T>(client, req);
    }

    private static async Task<T> Post<T>(HttpClient client, string endpoint, string body)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, $"{ApiRoot}{endpoint}")
        {
            Content = new StringContent(body, Encoding.UTF8, "application/json"),
        };
        return await SendRequest<T>(client, req);
    }

    private static async Task<T> Patch<T>(HttpClient client, string endpoint, string? body = null)
    {
        var req = new HttpRequestMessage(HttpMethod.Patch, $"{ApiRoot}{endpoint}");
        if (body != null)
            req.Content = new StringContent(body, Encoding.UTF8, "application/json");
        return await SendRequest<T>(client, req);
    }

    private static async Task Delete(HttpClient client, string endpoint)
    {
        var req = new HttpRequestMessage(HttpMethod.Delete, $"{ApiRoot}{endpoint}");
        await SendRequest(client, req);
    }

    private static async Task<T> Delete<T>(HttpClient client, string endpoint)
    {
        var req = new HttpRequestMessage(HttpMethod.Delete, $"{ApiRoot}{endpoint}");
        return await SendRequest<T>(client, req);
    }

    private static async Task<byte[]> CdnGet(HttpClient client, string endpoint)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, $"{CdnRoot}{endpoint}");
        var response = await client.SendAsync(req);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException($"Got CDN Error: {response.StatusCode}, {response.ReasonPhrase}");
        }
        return await response.Content.ReadAsByteArrayAsync();
    }

    // Implements https://discord.com/developers/docs/topics/gateway#get-gateway
    public static async Task<Uri> GetGateway(HttpClient client)
    {
        var response = await Get<Gateway>(client, "/gateway");
        
        Console.WriteLine(response.ToString());
        
        return response.Url;
    }

    // Implements https://discord.com/developers/docs/resources/user#get-current-user
    public static async Task<User> GetCurrentUser(HttpClient client)
    {
        return await Get<User>(client, "/users/@me");
    }
    
    // Implements https://discord.com/developers/docs/resources/user#get-current-user-guild-member
    public static async Task<GuildMember> GetCurrentUserGuildMember(HttpClient client, ulong guildId)
    {
        return await Get<GuildMember>(client, $"/users/@me/guilds/{guildId}/member");
    }

    public static async Task<User> GetUser(HttpClient client, ulong userId)
    {
        return await Get<User>(client, $"/users/{userId}");
    }

    public static async Task<Channel> GetChannel(HttpClient client, ulong channelId)
    {
        return await Get<Channel>(client, $"/channels/{channelId}");
    }

    public static async Task<Guild> GetGuild(HttpClient client, Snowflake guildId)
    {
        return await Get<Guild>(client, $"/guilds/{guildId}");
    }

    // https://discord.com/developers/docs/resources/channel#get-channel-messages
    public static async Task<List<Message>> GetChannelMessages(HttpClient client, ulong channelId, Snowflake? around = null, Snowflake? before = null, Snowflake? after = null, int limit = 50)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query.Add("before", before?.Id.ToString());
        query.Add("after", after?.Id.ToString());
        query.Add("limit", limit.ToString());
        query.Add("around", around?.Id.ToString());
        return await Get<List<Message>>(client, $"/channels/{channelId}/messages?{query}");
    }

    // https://discord.com/developers/docs/resources/channel#create-message
    public static async Task<Message> CreateAndSendMessage(HttpClient client, Channel channel, string content, Message? reply = null, bool shouldPing = false)
    {
        var nonce = Snowflake.Now().ToString();
        CreateMessageParams obj = new()
        {
            Content = content,
            Nonce = nonce,
            Tts = false,
            Flags = 0, // TODO: silent
        };

        if (reply == null)
            return await Post<Message>(client, $"/channels/{channel.Id}/messages", JsonSerializer.Serialize(obj));
        
        obj.MessageReference = new MessageReference
        {
            GuildId = reply.GuildId, //TODO: null on the message and channel object, get otherwise?
            ChannelId = reply.ChannelId,
            MessageId = reply.Id,
        };
        if (!shouldPing)
        {
            obj.AllowedMentions = new AllowedMentions
            {
                Parse = new[]
                {
                    "roles", "users", "everyone",
                },
                RepliedUser = false,
            };
        }
        return await Post<Message>(client, $"/channels/{channel.Id}/messages", JsonSerializer.Serialize(obj));
    }

    // https://discord.com/developers/docs/resources/channel#edit-message
    public static async Task<Message> EditMessage(HttpClient client, string input, Message original)
    {
        EditMessageParams obj = new()
        {
            Content = input,
        };
        return await Patch<Message>(client, $"/channels/{original.ChannelId}/messages/{original.Id}", JsonSerializer.Serialize(obj));
    }

    // https://discord.com/developers/docs/resources/channel#delete-message
    public static async Task DeleteMessage(HttpClient client, Message message)
    {
        await Delete(client, $"/channels/{message.ChannelId}/messages/{message.Id}");
    }

    //https://discord.com/developers/docs/reference#image-formatting
    public static Task<byte[]> GetAvatarAsync(HttpClient client, User user, int size = 32)
    {
        return CdnGet(client, $"avatars/{user.Id}/{user.Avatar}.png?size={size}");
        // TODO: Size could easily be a lie, as the API will just send the largest available instead of given size
    }

    //https://discord.com/developers/docs/reference#image-formatting
    public static Task<byte[]> GetDefaultAvatarAsync(HttpClient client, User user)
    {
        // index depends on whether the user switched to new username system
        // users with a username have a Discriminator of "0"
        var index = user.Discriminator == "0" ? (int)(user.Id >> 22) % 6 : int.Parse(user.Discriminator) % 5;
        return CdnGet(client, $"embed/avatars/{index}.png");
    }

    //https://discord.com/developers/docs/reference#image-formatting
    public static Task<byte[]> GetEmojiAsync(HttpClient client, Emoji emoji, int size = 32)
    {
        return CdnGet(client, $"emojis/{emoji.Id}.webp?size={size}&quality=lossless");
        // TODO: what quality
    }

    // https://discord.com/developers/docs/resources/channel#get-pinned-messages
    public static async Task<Message[]> GetPinnedMessages(HttpClient client, Snowflake channelId)
    {
        return await Get<Message[]>(client, $"/channels/{channelId}/pins");
    }

    public static async Task<SearchResult> SearchMessages(HttpClient client, SearchRequest request)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        // null elements arent added to the final query string
        query.Add("content", request.Search);
        query.Add("author_id", request.Author?.ToString());
        query.Add("mentions", request.Mentions?.ToString());
        query.Add("has", request.Contains); //TODO: enum-ify this? "link", "embed", "file", "video", "image", "sound", "sticker"
        query.Add("max_id", request.MaxId?.ToString()); // INFO: these are timestamps converted to snowflakes. "during" is handled by both parameters
        query.Add("min_id", request.MinId?.ToString());
        query.Add("channel_id", request.Channel?.ToString());
        query.Add("pinned", request.Pinned?.ToString().ToLowerInvariant());
        if (!query.HasKeys())
            throw new Exception("Search has no parameters!");
        query.Add("include_nsfw", "true"); //TODO: what sets the nsfw flag?
        query.Add("sort_by", request.SortBy); // "timestamp", "relevance"
        query.Add("sort_order", request.SortOrder); // "desc"/"asc"
        if (request.Offset > 0)
            query.Add("offset", request.Offset.ToString());
        var q = query.ToString();
        var url = $"/guilds/{request.Server.Id}/messages/search?{q}"; 
        return await Get<SearchResult>(client, url); 
    }
}
