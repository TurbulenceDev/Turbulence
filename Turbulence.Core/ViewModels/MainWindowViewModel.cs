using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGuild;

namespace Turbulence.Core.ViewModels;

public class MainWindowViewModel : ViewModelBase,
    IRecipient<ServerSelectedMsg>,
    IRecipient<ChannelSelectedMsg>,
    IRecipient<SendMessageMsg>,
    IRecipient<EditMessageMsg>,
    IRecipient<DeleteMessageMsg>
{
    public static bool IsDebug { get; private set; }
    public static Guild? SelectedServer { get; private set; }
    public static Channel? SelectedChannel { get; private set; }
    
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    public MainWindowViewModel()
    {
#if DEBUG
        IsDebug = true;
#endif
        // TODO: Move all this somewhere else
        Messenger.Send(new SetStatusMsg("Not connected"));
        Task.Run(_client.Start);
        Messenger.Send(new SetStatusMsg("Connecting..."));

        _client.Ready += OnReady;
        _client.MessageCreated += OnMessageCreated;
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
        SelectedServer = message.Server;
        //TODO: only send if not cached already
        _client.SendGatewayMessage(GatewayOpcode.LAZY_REQUEST, new LazyRequest() 
        {
            Guild = message.Server.Id,
            Activities = false,
            Threads = true,
            Typing = true,
        });
    }

    public async void Receive(SendMessageMsg message) =>
        await Api.CreateAndSendMessage(_client.HttpClient, SelectedChannel!, message.Message, message.Reply, message.ShouldPing);

    public async void Receive(EditMessageMsg message) =>
        await Api.EditMessage(_client.HttpClient, message.Message, message.Original);
    
    public async void Receive(DeleteMessageMsg message) =>
        await Api.DeleteMessage(_client.HttpClient, message.Message);
}

/// <summary>
/// Send a message in the currently selected channel
/// </summary>
public record SendMessageMsg(string Message, Message? Reply = null, bool ShouldPing = false);
public record EditMessageMsg(string Message, Message Original);
public record DeleteMessageMsg(Message Message);
