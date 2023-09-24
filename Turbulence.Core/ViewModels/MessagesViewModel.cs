using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using static Turbulence.Discord.Models.DiscordChannel.ChannelType;

namespace Turbulence.Core.ViewModels;

public partial class MessagesViewModel : ViewModelBase, IRecipient<MessageCreatedMsg>, IRecipient<ChannelSelectedMsg>
{
    public ObservableList<Message> CurrentMessages { get; } = new();
    
    [ObservableProperty]
    private string _title = "";

    public event EventHandler? ShowNewChannel;

    public async void Receive(ChannelSelectedMsg message)
    {
        Title = message.Channel.Type switch
        {
            DM => $"Messages: {(message.Channel.Recipients is { } recipients
                ? recipients.First().Username
                : (await Api.GetChannel(Client.HttpClient, message.Channel.Id)).Recipients?.First().Username) ?? "unknown"}",
            _ => $"Messages: {message.Channel.Name}",
        };

        var channelMessages = await MainWindowViewModel.Client.GetMessages(message.Channel.Id);
        CurrentMessages.Clear();
         foreach (var channelMessage in channelMessages.Reverse())
         {
             // TODO: Generates a collection changed notification on each Add(), fix?
             CurrentMessages.Add(channelMessage);
         }
        
        ShowNewChannel?.Invoke(this, EventArgs.Empty);
    }

    public void Receive(MessageCreatedMsg message) => CurrentMessages.Add(message.Message);
}

public record MessageCreatedMsg(Message Message);
