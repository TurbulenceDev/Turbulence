using CommunityToolkit.Mvvm.Input;
using Turbulence.CLI;

namespace Turbulence.Core.ViewModels;

public partial class TextInputViewModel
{
    [RelayCommand]
    private static async Task SendMessage(Message? message)
    {
        await new Discord().SendMessage(message!.Content!, message.Channel!.Value);
    }
}

public record Message(string? Content, ulong? Channel);
