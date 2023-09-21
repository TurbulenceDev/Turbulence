using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGateway;
using Turbulence.Discord.Models.DiscordGuild;
using Msg = Turbulence.Discord.Models.DiscordChannel.Message;

namespace Turbulence.Core.ViewModels;

public class MainWindowViewModel : ViewModelBase, IRecipient<SelectChannelMsg>, IRecipient<SelectServerMsg>
{
    public readonly Client Client = new(); // TODO: Move this to model or something I guess
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

    private void OnMessageCreated(object? sender, Event<Msg> e)
    {
        // TODO: this isnt called when sending a dm
        var msg = e.Data;
        if (SelectedChannel?.Id is { } id && msg.ChannelId == id)
        {
            Messenger.Send(new SendMessageMsg($"{msg.Author.Username}: {msg.Content}"));
        }
    }

    public void Receive(SelectChannelMsg message)
    {
        SelectedChannel = message.Channel;
    }

    public void Receive(SelectServerMsg message)
    {
        SelectedServer = message.Server;
    }
}

public record SelectChannelMsg(Channel Channel);
public record SelectServerMsg(Guild Server);
