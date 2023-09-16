using CommunityToolkit.Mvvm.Messaging;

namespace Turbulence.Core.ViewModels;

public class MenuBarViewModel : IRecipient<SetStatusMessage>
{
    public string Status { get; set; } = "Status"; // TODO: Bind to view

    public void Receive(SetStatusMessage m) => Status = m.Status;
}

public record SetStatusMessage(string Status);
