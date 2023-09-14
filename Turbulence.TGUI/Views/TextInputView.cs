using Terminal.Gui;
using static Terminal.Gui.TextAlignment;

namespace Turbulence.TGUI.Views;

public sealed class TextInputView : FrameView
{
    public readonly TextField TextInput = new() { Width = Dim.Fill(10) };
    public readonly Button SendButton = new()
    {
        Width = 9,
        Height = 1,
        X = 81,
        Y = 0,
        Text = "Send",
        IsDefault = true,
    };
    
    public TextInputView()
    {
        Title = "Write Message";
        Y = 25;
        X = 25;
        Width = 93;
        Height = 3;
        Border = new Border { BorderStyle = BorderStyle.Rounded };
        
        Add(TextInput);
        Add(SendButton);
    }
}
