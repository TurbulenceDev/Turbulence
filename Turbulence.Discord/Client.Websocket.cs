using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGatewayEvents;
using Turbulence.Discord.Models.DiscordVoice;

namespace Turbulence.Discord;

public partial class Client
{
	// TODO: move these into a gateway class thingy?
	private static int? _heartbeatInterval; // Time between heartbeats
	private static int? _lastSequence;
	private static readonly CancellationTokenSource HeartbeatToken = new();

	private readonly BlockingCollection<GatewayPayload> _sendQueue = new();
	private static readonly CancellationTokenSource SendQueueToken = new();

	public void SetWebsocketHeaders()
	{
		// TODO: implement zlib (de)compression
		// TODO: additional headers like Accept-Language etc? (also doesnt contain Connection: keep-alive); enable deflate extension (not used)?
		WebSocket.Options.SetRequestHeader("User-Agent", UserAgent);
		WebSocket.Options.SetRequestHeader("Origin", "https://discord.com");
		WebSocket.Options.SetRequestHeader("Accept", "*/*");
		WebSocket.Options.SetRequestHeader("Accept-Encoding", "gzip, deflate, br");
		WebSocket.Options.SetRequestHeader("Sec-Fetch-Dest", "websocket");
		WebSocket.Options.SetRequestHeader("Sec-Fetch-Mode", "websocket");
		WebSocket.Options.SetRequestHeader("Sec-Fetch-Site", "cross-site");
		WebSocket.Options.SetRequestHeader("Pragma", "no-cache");
		WebSocket.Options.SetRequestHeader("Cache-Control", "no-cache");
	}

	public void SetVoiceWebsocketHeaders()
	{
		// TODO: Accept-Language, Connection: keep-alive, Sec-WebSocket-Extentions/Key/Version ?
		WebSocket.Options.SetRequestHeader("User-Agent", UserAgent);
		WebSocket.Options.SetRequestHeader("Origin", "https://discord.com");
		WebSocket.Options.SetRequestHeader("Accept", "*/*");
		WebSocket.Options.SetRequestHeader("Accept-Encoding", "gzip, deflate, br");
		WebSocket.Options.SetRequestHeader("Sec-Fetch-Dest", "empty");
		WebSocket.Options.SetRequestHeader("Sec-Fetch-Mode", "websocket");
		WebSocket.Options.SetRequestHeader("Sec-Fetch-Site", "cross-site");
		WebSocket.Options.SetRequestHeader("Pragma", "no-cache");
		WebSocket.Options.SetRequestHeader("Cache-Control", "no-cache");
	}

	public Task SendIdentify(string token)
	{
		GatewayPayload payload = new()
		{
			Opcode = GatewayOpcode.IDENTIFY,
			Data = JsonSerializer.SerializeToNode(new Identify
			{
				Token = token,
				// TODO: turn into an bitfield enum
				Capabilities = 0b11101111111101, // TODO: use official caps, which probably require other models
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
					ClientBuildNumber = 226944, // TODO: dynamically get this
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

		//Console.WriteLine(seri);
		return WebSocket.SendAsync(Encoding.UTF8.GetBytes(seri), default, true, default);
	}

	private async Task HeartbeatTask()
	{
		while (_heartbeatInterval == null)
		{
			await Task.Delay(1000);
			if (WebSocket.State != WebSocketState.Open)
				return;
		}

		// first wait heartbeat interval + jitter (ignoring jitter here)
		await Task.Delay(_heartbeatInterval.Value, HeartbeatToken.Token);

		while (WebSocket.State == WebSocketState.Open)
		{
			// TODO: probably check if we got a ack (op 11) after the last heartbeat we sent. if not we "should" reconnect
			GatewayPayload heartBeat = new()
			{
				Opcode = GatewayOpcode.HEARTBEAT,
				Data = _lastSequence,
				SequenceNumber = null,
				EventName = null,
			};
			await WebSocket.SendAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(heartBeat)), default, true, default);
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

	private async Task SendTask()
	{
		while (WebSocket.State == WebSocketState.Open)
		{
			try
			{
				// waits till a message is available and sends it
				var msg = _sendQueue.Take(SendQueueToken.Token);
				await WebSocket.SendAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(msg)), default, true, default);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"WS Send: OP: {msg.Opcode}, Event: {msg.EventName}");
				if (msg.Data != null)
					Console.WriteLine(msg.Data.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
				Console.ForegroundColor = ConsoleColor.White;
			}
			catch (TaskCanceledException)
			{
				// done?
				return;
			}
		}
	}

	private async Task ReceiveTask()
	{
		// TODO: something here takes up a lot of ram. may be the json stuff not being gc'ed
		const int bufferSize = 1024 * 4;

		try
		{
			var buffer = new byte[bufferSize];
			var arraySegment = new ArraySegment<byte>(buffer);

			GatewayPayload? msg = null;
			while (WebSocket.State == WebSocketState.Open)
			{
				// Clear buffer
				Array.Clear(buffer);

				// Read the message
				var result = await WebSocket.ReceiveAsync(arraySegment, default);

				if (result.MessageType == WebSocketMessageType.Close)
				{
					Console.WriteLine($"Closing: {Encoding.UTF8.GetString(buffer)}");
					Console.WriteLine(result.CloseStatus);
					await WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
					continue;
				}

				if (result.EndOfMessage)
				{
					//Console.WriteLine(Encoding.UTF8.GetString(buffer));
					msg = JsonSerializer.Deserialize<GatewayPayload>(Encoding.UTF8.GetString(buffer[..result.Count]));
				}
				else // handle longer messages
				{
					// create a stream and append the messages till we reach the end of the messages
					var byteBuffer = new MemoryStream(bufferSize); // TODO: switch to smth other than MemoryStream? apparently has unnecessary overhead
					byteBuffer.Write(buffer, 0, buffer.Length);
					var count = result.Count;
					while (!result.EndOfMessage)
					{
						result = await WebSocket.ReceiveAsync(arraySegment, CancellationToken.None);
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
					HandleGatewayMessage(msg);
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Exception during Receive: {ex}");
		}
	}

	public async void HandleGatewayMessage(GatewayPayload msg)
	{
		//Console.WriteLine($"WS Receive: {msg.Opcode}");
		switch (msg.Opcode)
		{
			case GatewayOpcode.DISPATCH:
				{
					_lastSequence = msg.SequenceNumber; // save the sequence for the next heartbeat (only set if op 0)
														//Console.WriteLine($"Name: {msg.EventName}, Sequence: {msg.SequenceNumber}");
														//Console.WriteLine(JsonSerializer.Serialize(msg));
					if (msg.Data == null)
						return;

					// TODO: move this into a custom resolver of the GatewayPayload class
					//      by dynamically assigning subclasses to the data according to the event name
					//      then we wouldnt need to do this (dynamic) shit. also needs the exported models
					switch (msg.EventName)
					{
						// TODO: enumify these (nuts), https://discord.com/developers/docs/topics/rpc#commands-and-events-rpc-events
						case "MESSAGE_CREATE":
							if (msg.Data.Deserialize<Message>() is not { } message)
							{
								Console.WriteLine("Invalid message received on MESSAGE_CREATE");
								return;
							}

							MessageCreated?.Invoke(this, new Event<Message>(message));
							break;
						case "READY":
							if (msg.Data.Deserialize<Ready>() is not { } ready)
							{
								Console.WriteLine("READY event on MESSAGE_CREATE was null");
								return;
							}

							// Cache that shit // TODO: cache more/all.
							foreach (var guild in ready.Guilds)
								_cache.SetGuild(guild);
							foreach (var user in ready.Users)
								_cache.SetUser(user);
							foreach (var channel in ready.PrivateChannels)
								_cache.SetChannel(channel);
							// foreach (var guildSetting in ready.user_guild_settings.entries)
							//     ServerSettings.Add(guildSetting);
							CurrentUser = ready.User;
							// foreach (var member in ready.)
							//     MemberInfos.Add(member);

							Ready?.Invoke(this, new Event<Ready>(ready));
							break;
						case "THREAD_LIST_SYNC":
							if (msg.Data.Deserialize<ThreadListSyncEvent>() is not { } threadSync)
							{
								Console.WriteLine("Invalid message received on THREAD_LIST_SYNC");
								return;
							}
							ThreadListSync?.Invoke(this, new Event<ThreadListSyncEvent>(threadSync));
							//TODO: listen to event + add data to guilds
							break;
						case "TYPING_START":
							if (msg.Data.Deserialize<TypingStartEvent>() is not { } typingStart)
							{
								Console.WriteLine("Invalid message received on TYPING_START");
								return;
							}
							TypingStart?.Invoke(this, new Event<TypingStartEvent>(typingStart));
							break;
						case "MESSAGE_UPDATE":
							//TODO: should we use the message class here? according to the docs:
							//> Unlike creates, message updates may contain only a subset of the full message object payload (but will always contain an ID and channel_id).
							if (msg.Data.Deserialize<Message>() is not { } messageUpdate)
							{
								Console.WriteLine("Invalid message received on MESSAGE_UPDATE");
								return;
							}
							MessageUpdated?.Invoke(this, new Event<Message>(messageUpdate));
							//TODO: listen to this
							break;
						case "MESSAGE_DELETE":
							if (msg.Data.Deserialize<MessageDeleteEvent>() is not { } messageDelete)
							{
								Console.WriteLine("Invalid message received on MESSAGE_DELETE");
								return;
							}
							MessageDeleted?.Invoke(this, new Event<MessageDeleteEvent>(messageDelete));
							//TODO: listen to this
							break;
						case "VOICE_STATE_UPDATE":
							if (msg.Data.Deserialize<VoiceState>() is not { } voiceState)
							{
								Console.WriteLine("Invalid message received on VOICE_STATE_UPDATE");
								return;
							}
							VoiceStateUpdated?.Invoke(this, new Event<VoiceState>(voiceState));
							break;
						case "VOICE_SERVER_UPDATE":
							if (msg.Data.Deserialize<VoiceServerUpdateEvent>() is not { } voiceServerEv)
							{
								Console.WriteLine("Invalid message received on MESSAGE_DELETE");
								return;
							}

							if (voiceServerEv.Endpoint == null)
							{
								//TODO: handle this case
								Console.WriteLine("VOICE_SERVER_UPDATE: Endpoint is null.");
								return;
							}

							// Connect to the voice gateway
							var host = voiceServerEv.Endpoint;
							if (!host.Contains(Uri.SchemeDelimiter))
								host = $"{Uri.UriSchemeWss}{Uri.SchemeDelimiter}{host}";
							//TODO: do we need to remove the port?
							ConnectVoice(host);
							break;
						default:
							Console.WriteLine($"[Event: {msg.EventName}] Data: {msg.Data.ToJsonString(new JsonSerializerOptions { WriteIndented = true })}");
							break;
					}
					break;
				}
			case GatewayOpcode.HEARTBEAT:
				// We should send a heartbeat now without waiting so we cancel the delay
				HeartbeatToken.Cancel();
				break;
			case GatewayOpcode.HEARTBEAT_ACK: // received a heartbeat, do nothing
				break;
			case GatewayOpcode.HELLO:
				if (msg.Data.Deserialize<Hello>() is not { } hello)
				{
					Console.WriteLine("Got Hello without heartbeat interval");
					break;
				}

				_heartbeatInterval = hello.HeartbeatInterval;
				//Console.WriteLine($"Interval: {_heartbeatInterval}");
				break;
			default:
				Console.WriteLine($"[OP: {msg.Opcode}] Data: {msg.Data}");
				break;
		}
	}

	public bool SendGatewayMessage<T>(GatewayOpcode opcode, T data)
	{
		var payload = new GatewayPayload()
		{
			Opcode = opcode,
			Data = JsonSerializer.SerializeToNode(data),
		};
		return _sendQueue.TryAdd(payload);
	}

	public void VoiceStateUpdate(Snowflake? guildId, Snowflake? channelId)
	{
		//TODO: mute, dead, video
		SendGatewayMessage(GatewayOpcode.VOICE_STATE_UPDATE, new GatewayVoiceStateUpdate()
		{
			GuildId = guildId,
			ChannelId = channelId,
			SelfMute = false,
			SelfDeaf = false,
			SelfVideo = false,
		});
	}
}
