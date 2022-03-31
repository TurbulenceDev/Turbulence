using Microsoft.Extensions.Configuration;
using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json;
using Turbulence.API;
using Turbulence.API.Models.DiscordGateway;

namespace Turbulence.CLI;

class Cli
{
    private static int? _heartbeatInterval; // Time between heartbeats
    private static int? _lastSequence;
    private static readonly CancellationTokenSource HeartbeatToken = new();
    
    private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0";

    private static async Task Main()
    {
        try
        {
            // This uses dotnet user-secrets, saved in a secrets.json; can be configured through VS or CLI
            var config = new ConfigurationManager().AddUserSecrets<Cli>().Build();
            string? token = config["token"];
                
            if (token == null)
            {
                Console.WriteLine("No token set. Use 'dotnet user-secrets set token [your token]' to set a token.");
                return;
            }
                
            // Set up http client
            var client = new HttpClient();
                
            string gateway = await Api.GetGateway(client);

            ClientWebSocket ws = new();
            //TODO: correct headers?
            await ws.ConnectAsync(new Uri(gateway), default);
            
            Console.WriteLine(token);
            
            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);

            // Print some crap about the user
            var user = await Api.GetCurrentUser(client);
            Console.WriteLine(user.Username);

            // Send identify
            GatewayPayload payload = new()
            {
                Opcode = 2,
                Data = new Identify
                {
                    Token = token,
                    Properties = new IdentifyConnectionProperties
                    {
                        Os = "Windows",
                        Browser = "Firefox",
                        Device = "",
                    },
                    Intents = Convert.ToInt16("1000000011", 2), // GUILD_MESSAGES + GUILD_MEMBERS + GUILDS // TODO: Improve this
                },
            };
            
            Console.WriteLine("WS Send: Identify");
            await ws.SendAsync(payload.ToBytes(), default, true, default);
            await Task.WhenAll(Receive(ws), Heartbeat(ws));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in Main(): {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
            
        Console.WriteLine("Finished?");
    }

    private static async Task Heartbeat(ClientWebSocket webSocket)
    {
        while (webSocket.State == WebSocketState.Open)
        {
            if (_heartbeatInterval == null)
            {
                await Task.Delay(1000);
                continue;
            }

            // we need to send the first heartbeat after interval + jitter (ignore jitter here), then repeat until we die
            try
            {
                await Task.Delay(_heartbeatInterval.Value, HeartbeatToken.Token);
            }
            catch (TaskCanceledException)
            {
                // keep on sending i guess
            }

            //TODO: probably check if we got a ack (op 11) after the last heartbeat we sent. if not we "should" reconnect
            var ident = $@"{{""op"":1,""d"":{_lastSequence}}}";
            await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(ident)), WebSocketMessageType.Text, true, CancellationToken.None);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("WS Send: Heartbeat");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    private static async Task Receive(ClientWebSocket webSocket)
    {
        //TODO: something here takes up a lot of ram. may be the json stuff not being gc'ed
        const int bufferSize = 1024 * 4;
        
        try
        {
            var buffer = new byte[bufferSize];
            var arraySegment = new ArraySegment<byte>(buffer);
            GatewayPayload? msg = null;
            while (webSocket.State == WebSocketState.Open)
            {
                // Clear buffer
                Array.Clear(buffer);
                
                // Read the message
                var result = await webSocket.ReceiveAsync(arraySegment, CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    continue;
                }

                if (result.EndOfMessage)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(buffer));
                    msg = JsonConvert.DeserializeObject<GatewayPayload>(Encoding.UTF8.GetString(buffer));
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
                            msg = JsonConvert.DeserializeObject<GatewayPayload>(byteBuffer.ToString());
                            break;
                        }
                    }
                }

                // We should now have a valid gateway message
                if (msg != null)
                {
                    Console.WriteLine($"WS Receive: {msg.Opcode}");
                    switch (msg.Opcode)
                    {
                        case 0:
                        {
                            _lastSequence = msg.Sequence; // save the sequence for the next heartbeat (only set if op 0)
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
                            break;
                        }
                        case 1: // Heartbeat Request
                            // we should send a heartbeat now without waiting so we cancel the delay
                            HeartbeatToken.Cancel();
                            break;
                        case 10: // Gateway Hello
                            _heartbeatInterval = (int)msg.Data?.heartbeat_interval;
                            Console.WriteLine($"Interval: {_heartbeatInterval}");
                            break;
                        default:
                            Console.WriteLine($"Data: {msg.Data}");
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during Receive: {ex}");
        }
    }
}