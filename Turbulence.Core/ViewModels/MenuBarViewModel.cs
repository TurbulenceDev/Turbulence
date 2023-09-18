using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Turbulence.Core.ViewModels;

public partial class MenuBarViewModel : ViewModelBase, IRecipient<SetStatusMessage>
{
    [ObservableProperty]
    private string? _status = "Status";
    
    public void Receive(SetStatusMessage m)
    {
        Status = m.Status;
    }
}

public record SetStatusMessage(string Status);
