using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGateway;
using Msg = Turbulence.Discord.Models.DiscordChannel.Message;

namespace Turbulence.Core.ViewModels;

public class TurbulenceWindowViewModel : ViewModelBase, IRecipient<SetCurrentChannel>
{
    public readonly Client Client = new(); // TODO: Move this to model or something I guess
    public Channel? CurrentChannel;

    public TurbulenceWindowViewModel()
    {
        Messenger.Send(new SetStatusMessage("Not connected"));
        Task.Run(Client.Start);
        Messenger.Send(new SetStatusMessage("Connecting..."));
        
        Client.Ready += OnReady;
        Client.MessageCreated += OnMessageCreated;
    }

    private void OnReady(object? sender, Event<Ready> e)
    {
        var ready = e.Data;

        Messenger.Send(new SetServersMessage(ready.PrivateChannels.ToList(), ready.Users.ToList(), ready.Guilds.ToList()));
        Messenger.Send(new SetStatusMessage("Connected"));
    }

    private void OnMessageCreated(object? sender, Event<Msg> e)
    {
        // TODO: this isnt called when sending a dm
        var msg = e.Data;
        if (CurrentChannel?.Id is { } id && msg.ChannelId == id)
        {
            Messenger.Send(new SendMessageMessage($"{msg.Author.Username}: {msg.Content}"));
        }
    }

    public void Receive(SetCurrentChannel m)
    {
        CurrentChannel = m.Channel;
    }
}

public record SetCurrentChannel(Channel Channel);
