using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Core.ViewModels;

public partial class ServerListViewModel : ViewModelBase, IRecipient<SetServersMsg>
{
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    public List<Channel> PrivateChannels = new();
    public List<User> Users = new();
    public ObservableList<Guild> Servers { get; } = new();

    [ObservableProperty]
    private Guild? _selectedServer;

    [ObservableProperty]
    private bool _DMsSelected = false;

    [ObservableProperty]
    private bool _connected = false;

    public event EventHandler? TreeUpdated;

    public ServerListViewModel()
    {
        _client.OnConnectionStatusChanged += (_, args) => Connected = args.Data;
        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(SelectedServer) && SelectedServer is { } server)
                Messenger.Send(new ServerSelectedMsg(server));
            else if (args.PropertyName == nameof(DMsSelected))
                Messenger.Send(new DMsSelectedMsg(PrivateChannels));
        };
    }

    //TODO: selectdms
    [RelayCommand]
    public void SelectDMs()
    {
        DMsSelected = true;
        SelectedServer = null;
    }

    [RelayCommand]
    public void SelectServer(Guild server)
    {
        DMsSelected = false;
        SelectedServer = server;
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
public record DMsSelectedMsg(List<Channel> Channels);
