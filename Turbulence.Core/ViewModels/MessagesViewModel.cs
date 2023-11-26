using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using static Turbulence.Discord.Models.DiscordChannel.ChannelType;

namespace Turbulence.Core.ViewModels;

public partial class MessagesViewModel : ViewModelBase, IRecipient<MessageCreatedMsg>, IRecipient<ChannelSelectedMsg>, IRecipient<JumpToMessageMsg>
{
    public ObservableList<Message> CurrentMessages { get; } = new();
    
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;
    
    [ObservableProperty]
    private string _title = "";

    public event EventHandler? ShowNewChannel;
    private bool _loadingMessages = false;

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
            // TODO: insert at the end
        }
        _loadingMessages = false;
    }

    public async void Receive(ChannelSelectedMsg message)
    {
        Title = message.Channel.Type switch
        {
            DM => $"Messages: {(message.Channel.Recipients is { } recipients
                ? recipients.First().Username
                : (await _client.GetChannel(message.Channel.Id)).Recipients?.First().Username) ?? "unknown"}",
            _ => $"Messages: {message.Channel.Name}",
        };

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