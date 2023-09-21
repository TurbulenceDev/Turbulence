using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Core.ViewModels;

public partial class ServerListViewModel : ViewModelBase, IRecipient<SetServersMsg>
{
    public List<Channel> PrivateChannels = new();
    public List<User> Users = new();
    
    [ObservableProperty]
    private List<Guild> _servers = new();

    public event EventHandler? TreeUpdated;

    [RelayCommand]
    private void SelectionChanged(Channel channel)
    {
        Messenger.Send(new SelectChannelMsg(channel));
        Messenger.Send(new ShowChannelMsg(channel));
    }

    public void Receive(SetServersMsg message)
    {
        (PrivateChannels, Users, Servers) = (message.PrivateChannels, message.Users, message.Guilds);
        
        TreeUpdated?.Invoke(null, EventArgs.Empty);

        Messenger.Send(new SetChannelsMsg(Servers.First().Channels.ToList()));
    }
}

public record SetServersMsg(List<Channel> PrivateChannels, List<User> Users, List<Guild> Guilds);
