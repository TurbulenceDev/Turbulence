using Terminal.Gui;

namespace Turbulence.TGUI.Views;

public sealed class MessagesView : FrameView
{
    public readonly ListView Messages = new()
    {
        Width = Dim.Fill(1),
        Height = Dim.Fill(),
    };
    
    public MessagesView()
    {
        Title = "Messages";
        X = 25;
        Y = 1;
        Width = 93;
        Height = 24;
        Border = new Border { BorderStyle = BorderStyle.Rounded };

        Add(Messages);
    }
}
