using CommunityToolkit.Mvvm.Input;
using Turbulence.Discord;

namespace Turbulence.Core.ViewModels;

public partial class TextInputViewModel : ViewModelBase
{
    [RelayCommand]
    private static async Task SendMessage(Message? message)
    {
        await new Client().SendMessage(message!.Content!, message.Channel!.Value);
    }
}

public record Message(string? Content, ulong? Channel);
