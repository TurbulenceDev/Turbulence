using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models;

namespace Turbulence.Core.ViewModels;

public partial class MessagesViewModel : ViewModelBase, IRecipient<ShowChannelMessage>, IRecipient<SendMessageMessage>
{
    [ObservableProperty]
    private List<string> _currentMessages = new();
    
    [ObservableProperty]
    private string _title = "";

    private readonly TurbulenceWindowViewModel _parentVm;

    public event EventHandler? ShowNewChannel; 
    
    public MessagesViewModel(TurbulenceWindowViewModel parentVm)
    {
        _parentVm = parentVm;
    }

    public async void Receive(ShowChannelMessage m)
    {
        Title = $"Messages: {m.Name}";
        var msgs = await _parentVm.Client.GetMessages(m.Id);
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

public record ShowChannelMessage(Snowflake Id, string Name);
public record SendMessageMessage(string Message);
