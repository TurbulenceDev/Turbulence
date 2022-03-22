using Microsoft.Extensions.Configuration;

namespace Turbulence.CLI
{
    class CLI
    {
        static void Main()
        {
            const string apiRootAdress = "https://discord.com/api/";
            const string apiVersion = "v9";
            const string apiRoot = $"{apiRootAdress}{apiVersion}/";

            const string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0";

            // this uses dotnet user-secrets, saved in a secrets.json; can be configured through vs or cli
            var config = new ConfigurationManager().AddUserSecrets<CLI>().Build();
            var token = config["token"];

            // set up http client
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

            var userApi = $"{apiRoot}users/";
            // gets the current user
            async Task<HttpContent> getCurrentUser()
            {
                var req = new HttpRequestMessage(HttpMethod.Get, $"{userApi}/@me");
                var msg = await client.SendAsync(req);
                return msg.Content;
            }
            Console.WriteLine(token);
            var user = getCurrentUser().Result;
            Console.WriteLine(user.ReadAsStringAsync().Result);
        }
    }
}