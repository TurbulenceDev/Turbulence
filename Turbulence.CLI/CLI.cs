using Microsoft.Extensions.Configuration;
using SpanJson;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using System.Text;

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

            var ws = new ClientWebSocket();
            //TODO: correct headers?
            ws.ConnectAsync(new Uri("wss://gateway.discord.gg/?encoding=json&v=9"), CancellationToken.None).Wait();
            // send identify //TODO: transform into GatewayPayload class
            var ident = $@"{{""op"":2,""d"":{{""token"":""{token}"",""capabilities"":253,""properties"":{{""os"":""Windows"",""browser"":""Firefox"",""device"":"""",""system_locale"":""de"",""browser_user_agent"":""{userAgent}"",""browser_version"":""96.0"",""os_version"":""10"",""referrer"":"""",""referring_domain"":"""",""referrer_current"":"""",""referring_domain_current"":"""",""release_channel"":""stable"",""client_build_number"":111699,""client_event_source"":null}},""presence"":{{""status"":""online"",""since"":0,""activities"":[],""afk"":false}},""compress"":false,""client_state"":{{""guild_hashes"":{{}},""highest_last_message_id"":""0"",""read_state_version"":0,""user_guild_settings_version"":-1,""user_settings_version"":-1}}}}}}";
            ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(ident)), WebSocketMessageType.Text, true, CancellationToken.None);
            Console.WriteLine("WS Send: Identify");
            Task.WhenAll(Receive(ws), Send(ws)).Wait();
        }

        private static async Task Send(ClientWebSocket webSocket)
        {
            var random = new Random();

            while (webSocket.State == WebSocketState.Open)
            {


                //await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Binary, false, CancellationToken.None);

                await Task.Delay(1000);
            }
        }

        public class GatewayPayload
        {
            [DataMember(Name = "op")]
            public int Opcode { get; set; } // opcode
            [DataMember(Name = "d")]
            public dynamic? Data { get; set; } //event data
            // From docs: s and t are null when op is not 0
            [DataMember(Name = "s")]
            public int? Sequence { get; set; } // sequence number
            [DataMember(Name = "t")]
            public string? Name { get; set; } // event name
        }

        private static async Task Receive(ClientWebSocket webSocket)
        {
            //TODO: something here takes up a lot of ram. may be the json stuff not being gc'ed
            var bufferSize = 1024 * 4;
            try
            {
                byte[] buffer = new byte[bufferSize];
                GatewayPayload? msg = null;
                while (webSocket.State == WebSocketState.Open)
                {
                    // Read the message
                    var arraySegment = new ArraySegment<byte>(buffer);
                    var result = await webSocket.ReceiveAsync(arraySegment, CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                        continue;
                    }

                    if (result.EndOfMessage)
                    {
                        msg = JsonSerializer.Generic.Utf8.Deserialize<GatewayPayload>(buffer);
                    }
                    else // handle longer messages
                    {
                        // create a stream and append the messages till we reach the end of the messages
                        MemoryStream byteBuffer = new MemoryStream(bufferSize);
                        byteBuffer.Write(buffer, 0, buffer.Length);
                        while (!result.EndOfMessage)
                        {
                            result = await webSocket.ReceiveAsync(arraySegment, CancellationToken.None);
                            if (result.MessageType == WebSocketMessageType.Close)
                                continue;

                            byteBuffer.Write(buffer, 0, buffer.Length);
                            if (result.EndOfMessage)
                            {
                                // parse the whole message from the stream
                                msg = JsonSerializer.Generic.Utf8.Deserialize<GatewayPayload>(byteBuffer.ToArray());
                                break;
                            }
                        }
                    }

                    // We should now have a valid gateway message
                    if (msg != null)
                    {
                        Console.WriteLine($"WS Receive: {msg.Opcode}");
                        if (msg.Opcode == 0)
                        {
                            Console.WriteLine($"Name: {msg.Name}, Sequence: {msg.Sequence}");
                            if (msg.Data == null)
                                continue;

                            //TODO: move this into a custom resolver of the GatewayPayload class
                            //      by dynamically assigning subclasses to the data according to the event name
                            //      then we wouldnt need to do this (dynamic) shit. also needs the exported models
                            var data = msg.Data;
                            switch (msg.Name)
                            {
                                case "MESSAGE_CREATE":
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"{data.author.username}: {data.content}");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                case "READY":
                                    Console.WriteLine("READY");
                                    Console.WriteLine($"Current User: {data.user.username}#{data.user.discriminator}");
                                    Console.WriteLine("Servers:");
                                    foreach (var guild in data.guilds)
                                        Console.WriteLine($"-{guild.name} (ID: {guild.id})");
                                    break;
                                default:
                                    Console.WriteLine($"Data: {data}");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Data: {msg.Data}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during Receive: {ex.ToString()}");
            }
}
    }
}