using Terminal.Gui;
using Turbulence.Core.ViewModels;

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
    private readonly TurbulenceWindow _window; // TODO: This should probably not be necessary

    public TextInputView(TurbulenceWindow window)
    {
        _window = window;
        
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
        var content = _textInput.Text;
        if (content.IsEmpty)
            return;
        
        // TODO: brain is too molten rn to think about how you should access _currentChannel from here
        var channel = _window.CurrentChannel;
        if (channel == 0)
            return; // TODO: user feedback?
        
        _vm.SendMessageCommand.Execute(new Message(content.ToString(), channel));

        // send
        _textInput.Text = string.Empty;
        // TODO: refresh messages?
    }
}
