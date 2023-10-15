using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
    public static async Task<Message[]> GetChannelMessages(HttpClient client, ulong channelId, int limit = 50)
    {
        return await Get<Message[]>(client, $"/channels/{channelId}/messages?limit={limit}");
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

    public static async Task<byte[]> GetAvatar(HttpClient client, Snowflake user, string avatar, int size = 32)
    {
        return await CdnGet(client, $"avatars/{user}/{avatar}.webp?size={size}");
    }

    public static async Task<byte[]> GetDefaultAvatar(HttpClient client, int index)
    {
        return await CdnGet(client, $"embed/avatars/{index}.png");
    }

    // https://discord.com/developers/docs/resources/channel#get-pinned-messages
    public static async Task<Message[]> GetPinnedMessages(HttpClient client, Snowflake channelId)
    {
        return await Get<Message[]>(client, $"/channels/{channelId}/pins");
    }
}
