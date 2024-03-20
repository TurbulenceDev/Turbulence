using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGuild;

namespace Turbulence.Core.ViewModels;

public partial class MainWindowViewModel : ViewModelBase,
    IRecipient<ServerSelectedMsg>,
    IRecipient<ChannelSelectedMsg>,
    IRecipient<SendMessageMsg>,
    IRecipient<EditMessageMsg>,
    IRecipient<DeleteMessageMsg>,
    IRecipient<ConnectMsg>,
    IRecipient<SearchMsg>,
    IRecipient<SearchClosedMsg>
{
    public static bool IsDebug { get; private set; }
    public static Guild? SelectedServer { get; private set; }
    public static Channel? SelectedChannel { get; private set; }
    
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    public event EventHandler<string>? ErrorEvent;

    [ObservableProperty]
    private bool _searchOpen = false;

    public MainWindowViewModel()
    {
#if DEBUG
        IsDebug = true;
#endif
        // TODO: Move all this somewhere else
        //Messenger.Send(new SetStatusMsg("Not connected"));

        _client.Ready += OnReady;
        _client.MessageCreated += OnMessageCreated;

        if (!IsDebug) //TODO: check auto start config setting?
        {
            Connect();
        }
    }

    public void Connect()
    {
        if (_client.Connected) //TODO: rather check if it's disconnected cause this could allow connecting while starting to connect
            return;

        // Get token
        var token = new ConfigurationManager().AddUserSecrets<MainWindowViewModel>().Build()["token"]; //TODO: use other storage
        if (token == null)
        {
            ErrorEvent?.Invoke(this, "No Token set.");
            Messenger.Send(new SetStatusMsg("Error"));
            return;
        }

        Task.Run(() => _client.Start(token));
        Messenger.Send(new SetStatusMsg("Connecting..."));
    }

    private void OnReady(object? sender, Event<Ready> e)
    {
        var ready = e.Data;

        Messenger.Send(new SetServersMsg(ready.PrivateChannels.ToList(), ready.Users.ToList(), ready.Guilds.ToList()));
        Messenger.Send(new SetStatusMsg("Connected"));
    }

    private void OnMessageCreated(object? sender, Event<Message> e)
    {
        // TODO: This isn't called when sending a DM
        var msg = e.Data;
        if (SelectedChannel?.Id is { } id && msg.ChannelId == id)
        {
            Messenger.Send(new MessageCreatedMsg(msg));
        }
    }

    public void Receive(ChannelSelectedMsg message) => SelectedChannel = message.Channel;
    public void Receive(ServerSelectedMsg message)
    {
        if (message.Server == SelectedServer) // probably not needed to do something then
            return;

        SelectedServer = message.Server;
        //TODO: only send if not cached already
        _client.SendGatewayMessage(GatewayOpcode.LAZY_REQUEST, new LazyRequest() 
        {
            Guild = message.Server.Id,
            Activities = false,
            Threads = true,
            Typing = true,
        });
        // Close the search
        SearchOpen = false;
    }

    public void Receive(ConnectMsg message) => Connect();
    public void Receive(SearchMsg message) => SearchOpen = true;
    public void Receive(SearchClosedMsg message) => SearchOpen = false;

    public async void Receive(SendMessageMsg message) =>
        await _client.SendMessage(SelectedChannel!, message.Message, message.Reply, message.ShouldPing);

    public async void Receive(EditMessageMsg message) =>
        await _client.EditMessage(message.Message, message.Original);
    
    public async void Receive(DeleteMessageMsg message) =>
        await _client.DeleteMessage(message.Message);
}

/// <summary>
/// Send a message in the currently selected channel
/// </summary>
public record SendMessageMsg(string Message, Message? Reply = null, bool ShouldPing = false);
public record EditMessageMsg(string Message, Message Original);
public record DeleteMessageMsg(Message Message);
