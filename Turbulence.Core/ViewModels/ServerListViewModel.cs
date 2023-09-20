using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Core.ViewModels;

public partial class ServerListViewModel : ViewModelBase, IRecipient<SetServersMessage>
{
    public List<Channel> PrivateChannels = new();
    public List<User> Users = new();
    
    [ObservableProperty]
    private List<Guild> _servers = new();

    public event EventHandler? TreeUpdated;

    [RelayCommand]
    private void SelectionChanged(Channel channel)
    {
        Messenger.Send(new SetCurrentChannel(channel));
        Messenger.Send(new ShowChannelMessage(channel));
    }

    public void Receive(SetServersMessage m)
    {
        (PrivateChannels, Users, Servers) = (m.PrivateChannels, m.Users, m.Guilds);
        
        TreeUpdated?.Invoke(null, EventArgs.Empty);
    }
}

public record SetServersMessage(List<Channel> PrivateChannels, List<User> Users, List<Guild> Guilds);
