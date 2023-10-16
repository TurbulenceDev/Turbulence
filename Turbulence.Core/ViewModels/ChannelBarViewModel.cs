using CommunityToolkit.Mvvm.ComponentModel;
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
}