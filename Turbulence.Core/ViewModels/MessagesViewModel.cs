namespace Turbulence.Core.ViewModels;

public class MessagesViewModel
{
    public readonly List<string> CurrentMessages = new();
    public string Title = "";

    private readonly TurbulenceWindowViewModel _parentVm;

    public event EventHandler? ShowNewChannel; 
    
    public MessagesViewModel(TurbulenceWindowViewModel parentVm)
    {
        _parentVm = parentVm;
    }
    
    public async Task ShowChannel(ChannelNode node)
    {
        Title = $"Messages: {node.Name}";
        var msgs = await _parentVm.Client.GetMessages(node.Id);
        CurrentMessages.Clear();
        foreach (var msg in msgs.Reverse())
        {
            CurrentMessages.Add($"{msg.Author.Username}: {msg.Content}");
        }
        
        ShowNewChannel?.Invoke(this, EventArgs.Empty);
    }
}
