using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using static Turbulence.Discord.Models.DiscordChannel.ChannelType;

namespace Turbulence.Core.ViewModels;

public partial class MessagesViewModel : ViewModelBase, IRecipient<MessageCreatedMsg>, IRecipient<ChannelSelectedMsg>
{
    public ObservableList<Message> CurrentMessages { get; } = new();
    
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;
    
    [ObservableProperty]
    private string _title = "";

    public event EventHandler? ShowNewChannel;

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
        channelMessages.Reverse();
        CurrentMessages.Clear();
         foreach (var channelMessage in channelMessages)
         {
             // TODO: Generates a collection changed notification on each Add(), fix?
             CurrentMessages.Add(channelMessage);
         }
        
        ShowNewChannel?.Invoke(this, EventArgs.Empty);
    }

    public void Receive(MessageCreatedMsg message) => CurrentMessages.Add(message.Message);
}

public record MessageCreatedMsg(Message Message);
