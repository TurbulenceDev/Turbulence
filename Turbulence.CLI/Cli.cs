﻿using Microsoft.Extensions.Configuration;
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

    //TODO: move these cached objects into 1. their own classes (more efficient than keeping json stuff around) 2. into the api
    public static List<dynamic> Servers = new();
    public static List<dynamic> ServerSettings = new(); //TODO: listen to the guild settings update event


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
            //TODO: correct headers? //TODO: implement zlib (de)compression
            await ws.ConnectAsync(new Uri($"{gateway}/?encoding=json&v=9"), default);

            //Console.WriteLine(token);

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
                    Capabilities = 253,
                    Properties = new IdentifyConnectionProperties
                    {
                        OS = "Windows",
                        Browser = "Firefox",
                        Device = "",
                        Locale = "de",
                        UserAgent = UserAgent,
                        BrowserVersion = "96.0",
                        OSVersion = "10",
                        Referrer = "",
                        ReferringDomain = "",
                        ReferrerCurrent = "",
                        ReferringDomainCurrent = "",
                        ReleaseChannel = "stable",
                        ClientBuildNumber = 111699,
                        ClientEventSource = null,
                    },
                    Presence = new GatewayPresenceUpdate()
                    {
                        Status = "online",
                        Since = 0,
                        Activities = Array.Empty<Activity>(),
                        Afk = false,
                    },
                    Compress = false,
                    ClientState = new GatewayClientState()
                    {
                        GuildHashes = new object(),
                        highestLastMessageID = "0",
                        ReadStateVersion = 0,
                        UserGuildSettingsVersion = -1,
                        UserSettingsVersion = -1
                    }
                },
            };
            var a = JsonConvert.SerializeObject(payload);
            await ws.SendAsync(payload.ToBytes(), default, true, default);
            Console.WriteLine("WS Send: Identify");
            await Task.WhenAll(Receive(ws), Heartbeat(ws)); //TODO: implement a send queue, to issue gateway commands async
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in Main(): {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }

        Console.WriteLine("Finished?");
    }

    // checks if a message should trigger a notification
    private static bool ShouldNotify(dynamic msg)
    {
        //flow:
        // msg comes in
        // is server muted + no mention? no notif
        // if channelOverride exists
        //  is channel muted? no notif
        //  notif depending on channel message notification settings
        // notif depending on server message notification settings

        // each setting can contain guild wide settings and specific channel overrides
        //TODO: check message_notifications (0 = all, 1 = only mention, 2 = none, 3 = inherit server?, etc) properly
        foreach (var setting in ServerSettings)
        {
            // is server muted? no ping if not mentioned
            if (setting.guild_id == msg.guild_id && setting.muted == true) //TODO: check mention
                return false;

            // check channel specific
            foreach (var channelOverride in setting.channel_overrides)
            {
                if (channelOverride.channel_id == msg.channel_id)
                {
                    // channel muted
                    if (channelOverride.muted == true)
                        return false;

                    if (channelOverride.message_notifications == 0 || // notify all
                            (channelOverride.message_notifications == 3 && setting.message_notifications == 0)) // inherit server
                        return true;
                    else
                        return false;
                }
            }

            // then check server wide mute/notif settings
            if (setting.guild_id == msg.guild_id)
            {
                if (setting.message_notifications != 0) // not notify all //TODO: also check mentions
                    return false;
            }
        }

        // if there is no entry/we are here, it should probably notify
        return true;
    }

    private static async Task Heartbeat(ClientWebSocket webSocket)
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
                Opcode = 1,
                Data = _lastSequence
            };
            await webSocket.SendAsync(heartBeat.ToBytes(), WebSocketMessageType.Text, true, CancellationToken.None);
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
                            msg = JsonConvert.DeserializeObject<GatewayPayload>(stream);
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
                                        // check if the author is muted
                                        if (data.member.mute == true)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("[MUTED] ");
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                        // check if we should get a notification
                                        if (ShouldNotify(data))
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.Write("[NOTIF] ");
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                        // TODO: check if we/one of our roles got mentioned
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"{data.author.username}: {data.content}");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        break;
                                    case "READY":
                                        // Cache that shit //TODO: cache more/all. probably also need like private channels, user info etc
                                        foreach (var guild in data.guilds)
                                            Servers.Add(guild);
                                        foreach (var guildSetting in data.user_guild_settings.entries)
                                            ServerSettings.Add(guildSetting);

                                        Console.WriteLine("READY");
                                        Console.WriteLine($"Current User: {data.user.username}#{data.user.discriminator}");
                                        Console.WriteLine("Servers:");
                                        foreach (var guild in Servers)
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