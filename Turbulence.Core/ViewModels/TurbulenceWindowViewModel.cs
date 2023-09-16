using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordGateway;
using Msg = Turbulence.Discord.Models.DiscordChannel.Message;

namespace Turbulence.Core.ViewModels;

public class TurbulenceWindowViewModel
{
    public readonly Client Client = new(); // TODO: Move this to model or something I guess
    public ulong CurrentChannel = 0;

    private static readonly WeakReferenceMessenger Messenger = new();

    public TurbulenceWindowViewModel()
    {
        Client.Ready += OnReady;
        Client.MessageCreated += OnMessageCreated;
    }

    private void OnReady(object? sender, Event<Ready> e)
    {
        var ready = e.Data;

        Messenger.Send(new SetServersMessage(ready.PrivateChannels, ready.Users, ready.Guilds));
        Messenger.Send(new SetStatusMessage("Connected"));
    }

    private void OnMessageCreated(object? sender, Event<Msg> e)
    {
        // TODO: this isnt called when sending a dm
        var msg = e.Data;
        if (msg.ChannelId == CurrentChannel)
        {
            Messenger.Send(new SendMessageMessage($"{msg.Author.Username}: {msg.Content}"));
        }
    }
}
