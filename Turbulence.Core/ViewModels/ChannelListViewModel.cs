using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels;

public partial class ChannelListViewModel : ViewModelBase, IRecipient<SetChannelsMsg>
{
    [ObservableProperty]
    private List<Channel> _channels = new();

    public void Receive(SetChannelsMsg m) => Channels = m.Channels;
}

public record SetChannelsMsg(List<Channel> Channels);
