using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels;

public partial class PinnedMessagesViewModel : ViewModelBase, IRecipient<ChannelSelectedMsg>
{
    public ObservableList<Message> PinnedMessages { get; } = new();
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;
    private Snowflake? _currentChannel = null;

    public void Receive(ChannelSelectedMsg message)
    {
        PinnedMessages.Clear(); // TODO: cache?
        _currentChannel = message.Channel.Id;
    }

    public async void FetchPinnedMessages()
    {
        if (_currentChannel == null)
            return;
        // Dont double fetch //FIXME: still fetches when no pins exist
        if (PinnedMessages.Count > 0) 
            return;

        var messages = await _client.GetPinnedMessages(_currentChannel);
        PinnedMessages.ReplaceAll(messages);
    }
}
