using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Turbulence.API.Discord.Models;
using Turbulence.API.Discord.Models.DiscordChannel;
using Turbulence.API.Discord.Models.DiscordGateway;
using Turbulence.API.Discord.Models.DiscordGuild;
using Turbulence.API.Discord.Models.DiscordUser;

namespace Turbulence.API.Discord;

public static class Api
{
    public const string Version = "9";
    private const string RootAdress = "https://discord.com/api";
    private const string ApiRoot = $"{RootAdress}/v{Version}";

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
                throw new ApiException($"API responded with error: {error.Message} ({error.Code})");
            }

            throw new ApiException($@"{error.Message} ({error.Code}):
{JsonSerializer.Serialize(error.Errors, new { WriteIndented = true })}");

        }
        
        //Console.WriteLine(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<T>() ?? throw new ApiException($"ApiCall to {endpoint} failed");
    }

    private static async Task<T> Post<T>(HttpClient client, string endpoint, string body)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, $"{ApiRoot}{endpoint}")
        {
            Content = new StringContent(body, Encoding.UTF8, "application/json")
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
                throw new ApiException($"API responded with error: {error.Message} ({error.Code})");
            }

            throw new ApiException($@"{error.Message} ({error.Code}):
{JsonSerializer.Serialize(error.Errors, new { WriteIndented = true })}");

        }

        //Console.WriteLine(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<T>() ?? throw new ApiException($"ApiCall to {endpoint} failed");
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
    // https://discord.com/developers/docs/resources/guild#get-guild-channels
    public static async Task<Channel[]> GetGuildChannels(HttpClient client, Snowflake guild)
    {
        return await Get<Channel[]>(client, $"/guilds/{guild}/channels");
    }

    // https://discord.com/developers/docs/resources/channel#create-message
    public static async Task<Message> CreateAndSendMessage(HttpClient client, ulong channelId, string content)
    {
        var nonce = Snowflake.Now().ToString();
        CreateMessageParams obj = new()
        {
            Content = content,
            Nonce = nonce,
            Tts = false,
            Flags = 0 // TODO: silent
        };
        return await Post<Message>(client, $"/channels/{channelId}/messages", JsonSerializer.Serialize(obj));
    }
}
