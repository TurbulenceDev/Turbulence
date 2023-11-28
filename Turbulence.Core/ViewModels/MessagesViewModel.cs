using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGatewayEvents;

namespace Turbulence.Core.ViewModels;

public partial class MessagesViewModel : ViewModelBase, IRecipient<MessageCreatedMsg>, IRecipient<ChannelSelectedMsg>, IRecipient<JumpToMessageMsg>
{
    public ObservableList<Message> CurrentMessages { get; } = new();
    
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;
    
    [ObservableProperty]
    private string _title = "";

    public event EventHandler? ShowNewChannel;
    private bool _loadingMessages = false;
    private Channel? _currentChannel = null;

    public MessagesViewModel()
    {
        _client.MessageUpdated += Client_MessageUpdated;
        _client.MessageDeleted += Client_MessageDeleted;
    }

    private void Client_MessageUpdated(object? sender, Event<Message> e)
    {
        var msg = e.Data;
        // check if we have the channel loaded
        if (msg.ChannelId != _currentChannel?.Id)
            return;

        //TODO: we could alternatively check the ids of the first and the last msg as they should be chronological
        // find the message and change it
        for (var i = 0; i < CurrentMessages.Count; i++)
        {
            var m = CurrentMessages[i];
            if (m.Id == msg.Id)
            {
                CurrentMessages[i] = msg;
                return;
            }
        }
    }

    private void Client_MessageDeleted(object? sender, Event<MessageDeleteEvent> e)
    {
        var ev = e.Data;
        // check if we have the channel loaded
        if (ev.ChannelId != _currentChannel?.Id)
            return;

        // find the message and delete it
        for (var i = 0; i < CurrentMessages.Count; i++)
        {
            var m = CurrentMessages[i];
            if (m.Id == ev.Id)
            {
                CurrentMessages.RemoveAt(i);
                return;
            }
        }
    }

    public async Task RequestMoreMessages(bool older = true)
    {
        if (_loadingMessages)
            return;

        //TODO: check if we have enough messages to even request?
        _loadingMessages = true;
        if (older)
        {
            
            var first = CurrentMessages.First();
            var messages = await _client.GetMessagesBefore(first.ChannelId, first.Id);
            CurrentMessages.InsertRange(messages, 0);
        }
        else
        {
            var last = CurrentMessages.Last();
            var messages = await _client.GetMessagesAfter(last.ChannelId, last.Id);
            CurrentMessages.AddRange(messages);
        }
        _loadingMessages = false;
    }

    public async void Receive(ChannelSelectedMsg message)
    {
        _currentChannel = message.Channel;
        Title = $"Messages: {await _client.GetChannelName(message.Channel)}";

        var channelMessages = await _client.GetMessages(message.Channel.Id);
        CurrentMessages.Clear();
        CurrentMessages.ReverseAddRange(channelMessages);

        ShowNewChannel?.Invoke(this, EventArgs.Empty);
    }

    public void Receive(MessageCreatedMsg message) => CurrentMessages.Add(message.Message);

    public async void Receive(JumpToMessageMsg message)
    {
        //TODO: also check the channel
        //TODO: check if the message is in the current loaded list and scroll to it
        var channelMessages = await _client.GetMessagesAround(message.Message.ChannelId, message.Message.Id);
        CurrentMessages.Clear();
        CurrentMessages.ReverseAddRange(channelMessages);
    }
}

public record MessageCreatedMsg(Message Message);
public record JumpToMessageMsg(Message Message);