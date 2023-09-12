using Microsoft.Extensions.Configuration;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Turbulence.API.Discord;
using Turbulence.API.Discord.Models.DiscordChannel;
using Turbulence.API.Discord.Models.DiscordGateway;
using Turbulence.API.Discord.Models.DiscordGatewayEvents;
using Turbulence.API.Discord.Models.DiscordGuild;
using Turbulence.API.Discord.Models.DiscordUser;
using static Turbulence.API.Discord.Models.DiscordGateway.GatewayOpcode;

namespace Turbulence.CLI;

public class Cli
{
    private static int? _heartbeatInterval; // Time between heartbeats
    private static int? _lastSequence;
    private static readonly CancellationTokenSource HeartbeatToken = new();

    private const string UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:109.0) Gecko/20100101 Firefox/116.0";

    // TODO: move these cached objects into 1. their own classes (more efficient than keeping json stuff around) 2. into the api
    public static User User = null!;
    //public static List<dynamic> MemberInfos = new(); // TODO: should we like put the roles into a simple array?
    public static List<Guild> Guilds = new();
    //public static List<dynamic> ServerSettings = new(); // TODO: listen to the guild settings update event

    private static HttpClient _client = new();

    private static async Task Main()
    {
        try
        {
            // This uses dotnet user-secrets, saved in a secrets.json; can be configured through VS or CLI
            var config = new ConfigurationManager().AddUserSecrets<Cli>().Build();

            if (config["token"] is not { } token)
            {
                Console.WriteLine("No token set. Use 'dotnet user-secrets set token [your token]' to set a token.");
                return;
            }

            // TODO: according to the docs this should be cached and only re-requested if the cached version doesnt exist/is not reachable
            var gateway = await Api.GetGateway(_client);

            var ws = new ClientWebSocket();
            // TODO: implement zlib (de)compression
            // TODO: additional headers like Accept-Language etc? (also doesnt contain Connection: keep-alive); enable deflate extension (not used)?
            ws.Options.SetRequestHeader("User-Agent", UserAgent);
            ws.Options.SetRequestHeader("Origin", "https://discord.com");
            ws.Options.SetRequestHeader("Accept", "*/*");
            ws.Options.SetRequestHeader("Accept-Encoding", "gzip, deflate, br");
            ws.Options.SetRequestHeader("Sec-Fetch-Dest", "websocket");
            ws.Options.SetRequestHeader("Sec-Fetch-Mode", "websocket");
            ws.Options.SetRequestHeader("Sec-Fetch-Site", "cross-site");
            ws.Options.SetRequestHeader("Pragma", "no-cache");
            ws.Options.SetRequestHeader("Cache-Control", "no-cache");

            await ws.ConnectAsync(new Uri($"{gateway.AbsoluteUri}/?encoding=json&v={Api.Version}"), default);

            //Console.WriteLine(token);

            _client.DefaultRequestHeaders.Add("Authorization", token);
            _client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);

            // Send identify
            GatewayPayload payload = new()
            {
                Opcode = IDENTIFY,
                Data = JsonSerializer.SerializeToNode(new Identify
                {
                    Token = token,
                    //TODO: turn into an bitfield enum
                    Capabilities = 0b11101111111101, //TODO: use official caps, which probably require other models
                    Properties = new IdentifyConnectionProperties
                    {
                        Os = "Linux",
                        Browser = "Firefox",
                        Device = string.Empty,
                        SystemLocale = "en-US",
                        BrowserUserAgent = UserAgent,
                        BrowserVersion = "116.0",
                        OsVersion = "",
                        Referrer = "",
                        ReferringDomain = "",
                        ReferrerCurrent = "",
                        ReferringDomainCurrent = "",
                        ReleaseChannel = "stable",
                        ClientBuildNumber = 226944, //TODO: dynamically get this
                        ClientEventSource = string.Empty,
                    },
                    Presence = new GatewayPresenceUpdate()
                    {
                        Status = "online",
                        Since = 0,
                        Activities = Array.Empty<Activity>(),
                        Afk = false,
                    },
                    Compress = false,
                    ClientState = new ClientState
                    {
                        GuildVersions = new object(),
                        HighestLastMessageId = "0",
                        ReadStateVersion = 0,
                        UserGuildSettingsVersion = -1,
                        UserSettingsVersion = -1,
                        PrivateChannelsVersion = "0",
                        ApiCodeVersion = 0,
                    },
                }),
                SequenceNumber = null,
                EventName = null,
            };

            var seri = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
            
            Console.WriteLine(seri);
            await ws.SendAsync(Encoding.UTF8.GetBytes(seri), default, true, default);
            Console.WriteLine("WS Send: Identify");

            await Task.WhenAll(Receive(ws), Heartbeat(ws)); // TODO: implement a send queue, to issue gateway commands async
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in Main(): {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }

        Console.WriteLine("Finished?");
    }

    private static async Task<bool> IsMentioned(Message message)
    {
        // Was everyone pinged?
        if (message.MentionEveryone)
            return true;

        // Were we directly pinged (this also handles replies)?
        if (message.Mentions.Any(m => m.Id == User.Id))
            return true;
        
        // Was our role pinged?
        if (message.GuildId is not { } guildId)
        {
            // Not a guild
            return false;
        }

        var member = await Api.GetCurrentUserGuildMember(_client, guildId);
        return member.Roles.Any(r => message.MentionRoles.Any(m => m.Id == r.Id));
    }

    // checks if a message should trigger a notification
    private static async Task<bool> ShouldNotify(Message message, bool mentioned)
    {
        //flow:
        // msg comes in
        // is server muted? notif depending on mention
        // if channelOverride exists
        //  is channel muted? no notif
        //  notif depending on channel message notification settings
        // notif depending on server message notification settings

        // each setting can contain guild wide settings and specific channel overrides
        //TODO: make message_notifications (0 = all, 1 = only mention, 2 = none, 3 = inherit server) into a enum
        //TODO: also check ignore @everyone/@here/roles if we disabled it; probably need a MentionType enum instead of a bool
        // foreach (var setting in ServerSettings)
        // {
        //     // is server muted? no ping if not mentioned
        //     if (setting.guild_id == message.guild_id && setting.muted == true)
        //         return mentioned; // mentions still go through mutes //TODO: can we set "ignore @everyone" here?
        //
        //     // check channel specific
        //     foreach (var channelOverride in setting.channel_overrides)
        //     {
        //         if (channelOverride.channel_id == message.ChannelId)
        //         {
        //             // channel muted
        //             if (channelOverride.muted == true)
        //                 return false;
        //
        //             var notification = channelOverride.message_notifications;
        //             if (notification == 3) //inherit from server
        //                 notification = setting.message_notifications;
        //
        //             return notification == 0 || // notify all
        //                 (notification == 1 && mentioned); // only mention
        //         }
        //     }
        //
        //     // then check server wide mute/notif settings
        //     if (setting.guild_id == message.guild_id)
        //     {
        //         return setting.message_notifications == 0 || // notify all
        //                 (setting.message_notifications == 1 && mentioned); // only mention
        //     }
        // }

        // if there is no entry/we are here, it should probably notify
        return true;
    }

    private static async Task Heartbeat(WebSocket webSocket)
    {
        while (_heartbeatInterval == null)
        {
            await Task.Delay(1000);
            if (webSocket.State != WebSocketState.Open)
                return;
        }
    
        // first wait heartbeat interval + jitter (ignoring jitter here)
        await Task.Delay(_heartbeatInterval.Value, HeartbeatToken.Token);
    
        while (webSocket.State == WebSocketState.Open)
        {
            //TODO: probably check if we got a ack (op 11) after the last heartbeat we sent. if not we "should" reconnect
            GatewayPayload heartBeat = new()
            {
                Opcode = HEARTBEAT,
                Data = _lastSequence,
                SequenceNumber = null,
                EventName = null,
            };
            await webSocket.SendAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(heartBeat)), default, true, default);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("WS Send: Heartbeat");
            Console.ForegroundColor = ConsoleColor.White;
    
            // wait the interval
            try
            {
                await Task.Delay(_heartbeatInterval.Value, HeartbeatToken.Token);
            }
            catch (TaskCanceledException)
            {
                // keep on sending i guess
            }
        }
    }
    
    private static async Task Receive(WebSocket webSocket)
    {
        // TODO: something here takes up a lot of ram. may be the json stuff not being gc'ed
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
                var result = await webSocket.ReceiveAsync(arraySegment, default);
                
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    Console.WriteLine($"Closing: {Encoding.UTF8.GetString(buffer)}");
                    Console.WriteLine(result.CloseStatus);
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    continue;
                }
    
                if (result.EndOfMessage)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(buffer));
                    msg = JsonSerializer.Deserialize<GatewayPayload>(Encoding.UTF8.GetString(buffer[..result.Count]));
                }
                else // handle longer messages
                {
                    // create a stream and append the messages till we reach the end of the messages
                    var byteBuffer = new MemoryStream(bufferSize); //TODO: switch to smth other than MemoryStream? apparently has unnecessary overhead
                    byteBuffer.Write(buffer, 0, buffer.Length);
                    var count = result.Count;
                    while (!result.EndOfMessage)
                    {
                        result = await webSocket.ReceiveAsync(arraySegment, CancellationToken.None);
                        if (result.MessageType == WebSocketMessageType.Close)
                            continue;
    
                        byteBuffer.Write(buffer, 0, buffer.Length);
                        count += result.Count;
                        if (result.EndOfMessage)
                        {
                            // parse the whole message from the stream
                            var stream = Encoding.UTF8.GetString(byteBuffer.ToArray(), 0, count);
                            msg = JsonSerializer.Deserialize<GatewayPayload>(stream);
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
                        case DISPATCH:
                        {
                            _lastSequence = msg.SequenceNumber; // save the sequence for the next heartbeat (only set if op 0)
                            Console.WriteLine($"Name: {msg.EventName}, Sequence: {msg.SequenceNumber}");
                            Console.WriteLine(JsonSerializer.Serialize(msg));
                            if (msg.Data == null)
                                continue;

                            //TODO: move this into a custom resolver of the GatewayPayload class
                            //      by dynamically assigning subclasses to the data according to the event name
                            //      then we wouldnt need to do this (dynamic) shit. also needs the exported models
                            switch (msg.EventName)
                            {
                                case "MESSAGE_CREATE":
                                    Console.WriteLine(msg.Data.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
                                    
                                    if (msg.Data.Deserialize<Message>() is not { } message)
                                    {
                                        Console.WriteLine("Invalid message received on MESSAGE_CREATE");
                                        continue;
                                    }
                                    
                                    var mentioned = await IsMentioned(message);
                                    if (mentioned)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("[PING] ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    // check if we should get a notification
                                    if (await ShouldNotify(message, mentioned))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.Write("[NOTIF] ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    // TODO: edit the msg content with mentioned role/user names as well as making it a reply
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"{message.Author.Username}: {message.Content}");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                case "READY":
                                     if (msg.Data.Deserialize<Ready>() is not { } ready)
                                     {
                                         Console.WriteLine("READY event on MESSAGE_CREATE was null");
                                         continue;
                                     }
                                     
                                     // Cache that shit //TODO: cache more/all. probably also need like private channels etc
                                     foreach (var guild in ready.Guilds)
                                         Guilds.Add(guild);
                                     // foreach (var guildSetting in ready.user_guild_settings.entries)
                                     //     ServerSettings.Add(guildSetting);
                                     User = ready.User;
                                     // foreach (var member in ready.)
                                     //     MemberInfos.Add(member);

                                     Console.WriteLine("READY");
                                     Console.WriteLine($"Current User: {User.GlobalName} ({User.Username})");
                                     Console.WriteLine("Servers:");
                                     foreach (var guild in Guilds)
                                         Console.WriteLine($"-{guild.Name} (ID: {guild.Id})");
                                     break;
                                 default:
                                     Console.WriteLine($"Data: {msg.Data.ToJsonString( new JsonSerializerOptions { WriteIndented = true })}");
                                     break;
                            }
                            break;
                        }
                        case HEARTBEAT:
                            // We should send a heartbeat now without waiting so we cancel the delay
                            HeartbeatToken.Cancel();
                            break;
                        case HELLO:
                            if (msg.Data.Deserialize<Hello>() is not { } hello)
                            {
                                Console.WriteLine("Got Hello without heartbeat interval");
                                break;
                            }

                            _heartbeatInterval = hello.HeartbeatInterval;
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