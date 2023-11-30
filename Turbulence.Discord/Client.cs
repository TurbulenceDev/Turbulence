using System.Net.WebSockets;
using CommunityToolkit.Mvvm.DependencyInjection;
using Turbulence.Discord.Models.DiscordGatewayEvents;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordUser;
using Turbulence.Discord.Services;
using Turbulence.Discord.Models.DiscordVoice;

namespace Turbulence.Discord;

public class Event<T>
{
    public Event(T data)
    {
        Data = data;
    }

    public T Data { get; set; }
}

public partial class Client : IPlatformClient
{
    //TODO: save token here for possible reconnecting?
    // Events
    public event EventHandler<Event<Ready>>? Ready;
    public event EventHandler<Event<Message>>? MessageCreated;
    public event EventHandler<Event<TypingStartEvent>>? TypingStart;
    public event EventHandler<Event<Message>>? MessageUpdated;
    public event EventHandler<Event<MessageDeleteEvent>>? MessageDeleted;
    public event EventHandler<Event<ThreadListSyncEvent>>? ThreadListSync;
    public event EventHandler<Event<bool>>? OnConnectionStatusChanged;
    public event EventHandler<Event<VoiceState>>? VoiceStateUpdated;

    public bool Connected => WebSocket.State == WebSocketState.Open;
    private HttpClient HttpClient { get; } = new();
    private static readonly HttpClient CdnClient = new();

    private readonly ICache _cache = Ioc.Default.GetService<ICache>()!;
    private readonly ILogger? _logger = Ioc.Default.GetService<ILogger>();

    private ClientWebSocket WebSocket { get; set; }
    private ClientWebSocket VoiceWebSocket { get; set; }
    private const string UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:109.0) Gecko/20100101 Firefox/116.0"; // idk where to move this

    public Client()
    {
        // Set up http client
        HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        CdnClient.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        // WS
        WebSocket = new();
        VoiceWebSocket = new();
    }

    public async Task Start(string token)
    {
        // TODO: according to the docs this should be cached and only re-requested if the cached version doesnt exist/is not reachable
        var gateway = await Api.GetGateway(HttpClient);

        HttpClient.DefaultRequestHeaders.Add("Authorization", token);

        SetWebsocketHeaders();
        SetVoiceWebsocketHeaders();
        await WebSocket.ConnectAsync(new Uri($"{gateway.AbsoluteUri}/?encoding=json&v={Api.Version}"), default);
        OnConnectionStatusChanged?.Invoke(this, new(true));
        await SendIdentify(token);
        // Start the tasks // TODO: save the tasks?
        _ = Task.Run(ReceiveTask);
        _ = Task.Run(HeartbeatTask);
        _ = Task.Run(SendTask);
    }

    public async Task ConnectVoice(string host)
    {
        if (VoiceWebSocket.State == WebSocketState.Connecting || VoiceWebSocket.State == WebSocketState.Open) // already connected?
            return;
        await VoiceWebSocket.ConnectAsync(new Uri($"{host}/?v={Api.VoiceVersion}"), default);
    }

    // Util functions
    public async Task<string> GetChannelName(Channel channel)
    {
        async Task<IEnumerable<string>> GetChannelUsers(Channel channel)
        {
            if (channel.Recipients == null && channel.RecipientIDs == null)
                channel = await GetChannel(channel.Id);
            if (channel.Recipients != null)
                return channel.Recipients.Select(u => u.GetBestName());
            else if (channel.RecipientIDs != null)
                return channel.RecipientIDs.Select(id => (GetUser(id).Result).GetBestName());
            return Array.Empty<string>();
        }

        return channel.Type switch
        {
            //TODO: alternatively use RecipientIDs
            ChannelType.DM or ChannelType.GROUP_DM => string.Join(", ", await GetChannelUsers(channel)),
            _ => $"{channel.Name}",
        };
    }

    public string GetMessageContent(Message message)
    {
        var author = message.GetBestAuthorName();
        return message.Type switch
        {
            MessageType.THREAD_CREATED => $"{author} created thread \"{message.Content}\"",
            MessageType.CALL => $"{author} started a voice call",
            MessageType.CHANNEL_PINNED_MESSAGE => $"{author} pinned a message.",
            MessageType.USER_JOIN => $"{author} joined the server.",
            MessageType.RECIPIENT_ADD => $"{author} added {message.Mentions[0].GetBestName()}.",
            _ => message.Content,
        };
    }

    public async Task<byte[]> GetImageAsync(string url)
    {
        //TODO: cache
        /*if (_cache.GetAvatar(user.Id, size) is { } avatar)
            return avatar;*/

        var req = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await CdnClient.SendAsync(req);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException($"Got error while fetching image: {response.StatusCode}, {response.ReasonPhrase}");
        }
        var image = await response.Content.ReadAsByteArrayAsync();
        _logger?.Log($"Requested media image {url}", LogType.Images, LogLevel.Debug);

        //_cache.SetAvatar(user.Id, size, avatar);
        return image;
    }
}
