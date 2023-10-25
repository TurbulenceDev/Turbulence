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
    public ObservableList<Guild> Servers { get; } = new();

    [ObservableProperty]
    private Guild? _selectedServer;

    public event EventHandler? TreeUpdated;

    public ServerListViewModel()
    {
        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(SelectedServer) && SelectedServer is { } server)
                Messenger.Send(new ServerSelectedMsg(server));
        };
    }
    
    [RelayCommand]
    private void SelectionChanged(Channel channel) => Messenger.Send(new ChannelSelectedMsg(channel));

    public void Receive(SetServersMsg message)
    {
        (PrivateChannels, Users) = (message.PrivateChannels, message.Users);
        Servers.ReplaceAll(message.Guilds);
        
        TreeUpdated?.Invoke(null, EventArgs.Empty);

        // Select the first server at the start
        SelectedServer = Servers.First();
    }
}

public record SetServersMsg(List<Channel> PrivateChannels, List<User> Users, List<Guild> Guilds);
public record ServerSelectedMsg(Guild Server);
