using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGuild;

namespace Turbulence.Core.ViewModels;

public partial class ChannelBarViewModel : ViewModelBase, IRecipient<ChannelSelectedMsg>, IRecipient<ServerSelectedMsg>
{
    [ObservableProperty]
    private Channel? _channel;

    private Guild? _currentServer;

    public void Receive(ServerSelectedMsg message)
    {
        //TODO: do we really need to save the server here? cant we just cache get the server from the channel
        _currentServer = message.Server;
    }

    public void Receive(ChannelSelectedMsg message)
    {
        Channel = message.Channel;
    }

    [RelayCommand]
    public void Search(string search)
    {
        Messenger.Send(new SearchMsg(new SearchRequest(_currentServer!, search)));
    }
}

public record SearchMsg(SearchRequest Request);