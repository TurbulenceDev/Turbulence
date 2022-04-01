using Newtonsoft.Json;
using Turbulence.API.Models.DiscordGateway;
using Turbulence.API.Models.DiscordUser;

namespace Turbulence.API;

public static class Api
{
    private const string ApiRootAdress = "https://discord.com/api";
    private const string ApiVersion = "/v9";
    private const string ApiRoot = $"{ApiRootAdress}{ApiVersion}";

    private static async Task<T> ApiCall<T>(HttpClient client, string endpoint)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, $"{ApiRoot}{endpoint}");
        var msg = await client.SendAsync(req);
        Console.WriteLine(await msg.Content.ReadAsStringAsync());

        string content = await msg.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(content) ?? throw new Exception($"ApiCall to {endpoint} failed");
    }
    
    // Implements https://discord.com/developers/docs/topics/gateway#get-gateway
    public static async Task<string> GetGateway(HttpClient client)
    {
        var response = await ApiCall<Gateway>(client, "/gateway");
        
        Console.WriteLine(response.ToString());
        
        return response.Url;
    }

    // Implements https://discord.com/developers/docs/resources/user#get-current-user
    public static async Task<User> GetCurrentUser(HttpClient client)
    {
        return await ApiCall<User>(client, "/users/@me");
    }
}
