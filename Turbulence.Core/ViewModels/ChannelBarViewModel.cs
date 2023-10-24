using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels;

public partial class ChannelBarViewModel : ViewModelBase, IRecipient<ChannelSelectedMsg>
{
    [ObservableProperty]
    private Channel? _channel;
    
    public void Receive(ChannelSelectedMsg message)
    {
        Channel = message.Channel;
    }

    [RelayCommand]
    public void Search(string search)
    {
        Messenger.Send(new SearchMsg(search));
    }
}

public record SearchMsg(string Search);