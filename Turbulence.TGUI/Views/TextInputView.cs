using Terminal.Gui;
using Turbulence.Core.ViewModels;
using Message = Turbulence.Core.ViewModels.Message;

namespace Turbulence.TGUI.Views;

public sealed class TextInputView : FrameView
{
    private readonly TextField _textInput = new() { Width = Dim.Fill(10) };
    private readonly Button _sendButton = new()
    {
        Width = 9,
        Height = 1,
        X = 81,
        Y = 0,
        Text = "Send",
        IsDefault = true,
    };

    private readonly TextInputViewModel _vm = new();
    private readonly MainWindowViewModel _parentVm; // TODO: This should probably not be necessary

    public TextInputView(MainWindowViewModel parentVm)
    {
        _parentVm = parentVm;
        
        Title = "Write Message";
        Y = 25;
        X = 25;
        Width = 93;
        Height = 3;
        Border = new Border { BorderStyle = BorderStyle.Rounded };
        
        Add(_textInput);
        Add(_sendButton);

        _sendButton.Clicked += SendMessage;
    }
    
    // TODO: Async probably?
    private void SendMessage()
    {
        var message = _textInput.Text.ToString();
        if (string.IsNullOrWhiteSpace(message))
            return;

        _vm.SendMessageCommand.Execute(new Message(message, _parentVm.SelectedChannel!));

        _textInput.Text = string.Empty;
        // TODO: Refresh messages?
    }
}
