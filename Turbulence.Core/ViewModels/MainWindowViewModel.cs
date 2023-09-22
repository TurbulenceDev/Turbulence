using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGuild;

namespace Turbulence.Core.ViewModels;

public class MainWindowViewModel : ViewModelBase, IRecipient<ServerSelectedMsg>, IRecipient<ChannelSelectedMsg>
{
    public static readonly Client Client = new(); // TODO: Move this to model or something I guess
    public Guild? SelectedServer;
    public Channel? SelectedChannel;

    public MainWindowViewModel()
    {
        Messenger.Send(new SetStatusMsg("Not connected"));
        Task.Run(Client.Start);
        Messenger.Send(new SetStatusMsg("Connecting..."));
        
        Client.Ready += OnReady;
        Client.MessageCreated += OnMessageCreated;
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
    public void Receive(ServerSelectedMsg message) => SelectedServer = message.Server;
}
