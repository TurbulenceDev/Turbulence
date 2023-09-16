using CommunityToolkit.Mvvm.Messaging;

namespace Turbulence.Core.ViewModels;

public class MessagesViewModel : IRecipient<ShowChannelMessage>, IRecipient<SendMessageMessage>
{
    public readonly List<string> CurrentMessages = new();
    public string Title = "";

    private readonly TurbulenceWindowViewModel _parentVm;

    public event EventHandler? ShowNewChannel; 
    
    public MessagesViewModel(TurbulenceWindowViewModel parentVm)
    {
        _parentVm = parentVm;
    }

    public async void Receive(ShowChannelMessage message)
    {
        Title = $"Messages: {message.Node.Name}";
        var msgs = await _parentVm.Client.GetMessages(message.Node.Id);
        CurrentMessages.Clear();
        foreach (var msg in msgs.Reverse())
        {
            CurrentMessages.Add($"{msg.Author.Username}: {msg.Content}");
        }
        
        ShowNewChannel?.Invoke(this, EventArgs.Empty);
    }

    public void Receive(SendMessageMessage message)
    {
        CurrentMessages.Add(message.Message);
        
        // TODO: do this somehow? probably c# event
        // scroll down 1 message
        // _messages.MessagesListView.ScrollDown(1);
    }
}

public record ShowChannelMessage(ChannelNode Node);
public record SendMessageMessage(string Message);
