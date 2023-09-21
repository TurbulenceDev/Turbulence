using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using static Turbulence.Discord.Models.DiscordChannel.ChannelType;
using static Turbulence.Discord.Models.DiscordChannel.MessageType;

namespace Turbulence.Core.ViewModels;

public partial class MessagesViewModel : ViewModelBase, IRecipient<ShowChannelMsg>, IRecipient<SendMessageMsg>
{
    [ObservableProperty]
    private List<string> _currentMessages = new();
    
    [ObservableProperty]
    private string _title = "";

    private readonly MainWindowViewModel _parentVm;

    public event EventHandler? ShowNewChannel; 
    
    public MessagesViewModel(MainWindowViewModel parentVm)
    {
        _parentVm = parentVm;
    }

    public async void Receive(ShowChannelMsg message)
    {
        Title = message.Channel.Type switch
        {
            DM => $"Messages: {(message.Channel.Recipients is { } recipients
                ? recipients.First().Username
                : (await Api.GetChannel(Client.HttpClient, message.Channel.Id)).Recipients?.First().Username) ?? "unknown"}",
            _ => $"Messages: {message.Channel.Name}",
        };

        var channelMessages = await _parentVm.Client.GetMessages(message.Channel.Id);
        CurrentMessages.Clear();
        foreach (var msg in channelMessages.Reverse())
        {
            // TODO: let view handle the actual frontend stuff and only give it the messages?
            var messageType = msg.Type switch
            {
                THREAD_CREATED => $"{msg.Author.Username} created Thread \"{msg.Content}\"",
                CALL => $"{msg.Author.Username} called",
                _ => $"{msg.Author.Username}: {msg.Content}",
            };
            CurrentMessages.Add(messageType);
        }
        
        ShowNewChannel?.Invoke(this, EventArgs.Empty);
    }

    public void Receive(SendMessageMsg message)
    {
        CurrentMessages.Add(message.Message);
        
        // TODO: do this somehow? probably c# event
        // scroll down 1 message
        // _messages.MessagesListView.ScrollDown(1);
    }
}

public record ShowChannelMsg(Channel Channel);
public record SendMessageMsg(string Message);
