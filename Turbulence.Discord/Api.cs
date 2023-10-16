using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord;

public static class Api
{
    public const string Version = "9";
    private const string RootAdress = "https://discord.com/api";
    private const string ApiRoot = $"{RootAdress}/v{Version}";
    private const string CdnRoot = "https://cdn.discordapp.com/";

    private static async Task<T> Get<T>(HttpClient client, string endpoint)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, $"{ApiRoot}{endpoint}");
        var response = await client.SendAsync(req);

        if (!response.IsSuccessStatusCode)
        {
            if (await response.Content.ReadFromJsonAsync<Error>() is not { } error)
            {
                throw new ApiException(
                    $@"Failed to GET {typeof(T).FullName} with code {(int)response.StatusCode}:
{await response.Content.ReadAsStringAsync()}");
            }

            if (error.Errors == null)
            {
                throw new ApiException($"API responded with error: {error.Message} ({error.Code?.ToString() ?? "no error code"})");
            }

            throw new ApiException($@"{error.Message} ({error.Code?.ToString() ?? "no error code"}):
{JsonSerializer.Serialize(error.Errors, new { WriteIndented = true })}");

        }
        
        //Console.WriteLine(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<T>() ?? throw new ApiException($"ApiCall to {endpoint} failed");
    }

    private static async Task<T> Post<T>(HttpClient client, string endpoint, string body)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, $"{ApiRoot}{endpoint}")
        {
            Content = new StringContent(body, Encoding.UTF8, "application/json"),
        };
        var response = await client.SendAsync(req);

        if (!response.IsSuccessStatusCode)
        {
            if (await response.Content.ReadFromJsonAsync<Error>() is not { } error)
            {
                throw new ApiException(
                    $@"Failed to POST {typeof(T).FullName} with code {(int)response.StatusCode}:
{await response.Content.ReadAsStringAsync()}");
            }

            if (error.Errors == null)
            {
                throw new ApiException($"API responded with error: {error.Message} ({error.Code?.ToString() ?? "no error code"})");
            }

            throw new ApiException($@"{error.Message} ({error.Code?.ToString() ?? "no error code"}):
{JsonSerializer.Serialize(error.Errors, new { WriteIndented = true })}");

        }

        //Console.WriteLine(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<T>() ?? throw new ApiException($"ApiCall to {endpoint} failed");
    }

    private static async Task<byte[]> CdnGet(HttpClient client, string endpoint)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, $"{ApiRoot}{endpoint}");
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

    public static async Task<Channel> GetChannel(HttpClient client, ulong channelId)
    {
        return await Get<Channel>(client, $"/channels/{channelId}");
    }

    public static async Task<Guild> GetGuild(HttpClient client, Snowflake guildId)
    {
        return await Get<Guild>(client, $"/guilds/{guildId}");
    }

    // https://discord.com/developers/docs/resources/channel#get-channel-messages
    public static async Task<List<Message>> GetChannelMessages(HttpClient client, ulong channelId, int limit = 50)
    {
        return await Get<List<Message>>(client, $"/channels/{channelId}/messages?limit={limit}");
    }

    // https://discord.com/developers/docs/resources/channel#create-message
    public static async Task<Message> CreateAndSendMessage(HttpClient client, Channel channel, string content)
    {
        var nonce = Snowflake.Now().ToString();
        CreateMessageParams obj = new()
        {
            Content = content,
            Nonce = nonce,
            Tts = false,
            Flags = 0, // TODO: silent
        };
        return await Post<Message>(client, $"/channels/{channel.Id}/messages", JsonSerializer.Serialize(obj));
    }

    public static async Task<Image> GetAvatar(HttpClient client, Snowflake user, string avatar, int size = 32)
    {
        var data = await CdnGet(client, $"avatars/{user}/{avatar}.png?size={size}");
        return new Image(data, size); // TODO: Size could easily be a lie, as the API will just send the largest available instead of given size
    }

    public static async Task<Image> GetDefaultAvatar(HttpClient client, int index)
    {
        var data = await CdnGet(client, $"embed/avatars/{index}.png"); // TODO: Doesn't work
        return new Image(data, 256);
    }

    // https://discord.com/developers/docs/resources/channel#get-pinned-messages
    public static async Task<Message[]> GetPinnedMessages(HttpClient client, Snowflake channelId)
    {
        return await Get<Message[]>(client, $"/channels/{channelId}/pins");
    }

    public static async Task<SearchResult> SearchMessage(HttpClient client, Snowflake guild, string? message = null, User? author = null, User? mentions = null, string? contains = null, Snowflake? maxId = null, Snowflake? minId = null, Snowflake? channel = null, bool? pinned = null, int offset = 0)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        // null elements arent added to the final query string
        query.Add("content", message);
        query.Add("author_id", author?.Id.ToString());
        query.Add("mentions", mentions?.Id.ToString());
        query.Add("has", contains); //TODO: enum-ify this? "link", "embed", "file", "video", "image", "sound", "sticker"
        query.Add("max_id", maxId?.ToString()); // INFO: these are timestamps converted to snowflakes. "during" is handled by both parameters
        query.Add("min_id", minId?.ToString());
        query.Add("channel_id", channel?.ToString());
        query.Add("pinned", pinned?.ToString().ToLowerInvariant());
        var q = query.ToString();
        if (string.IsNullOrEmpty(q))
            throw new Exception("Search has no parameters!");
        var url = $"/guilds/{guild}/messages/search?{q}&include_nsfw=true"; //TODO: what sets the nsfw flag?
        if (offset > 0) // if we have an offset add it to the parameters
        {
            url += $"&offset={offset}";
        }
        return await Get<SearchResult>(client, url); 
    }
}
