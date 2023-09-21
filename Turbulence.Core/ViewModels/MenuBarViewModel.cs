using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Turbulence.Core.ViewModels;

public partial class MenuBarViewModel : ViewModelBase, IRecipient<SetStatusMsg>
{
    [ObservableProperty]
    private string? _status = "Status";
    
    public void Receive(SetStatusMsg message)
    {
        Status = message.Status;
    }
}

public record SetStatusMsg(string Status);
