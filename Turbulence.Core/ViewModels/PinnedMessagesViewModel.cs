using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels;

public partial class PinnedMessagesViewModel : ViewModelBase
{
    public ObservableList<Message> PinnedMessages { get; } = new();
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;
    private Snowflake? _currentChannel = null;

    public async void FetchPinnedMessages()
    {
        if (_currentChannel == null)
            return;

        var messages = await _client.GetPinnedMessages(_currentChannel);
        PinnedMessages.ReplaceAll(messages);
    }
}
