using Terminal.Gui;

namespace Turbulence.TGUI.Views;

public sealed class ServerView : FrameView
{
    public readonly TreeView ServerTree = new()
    {
        Width = Dim.Fill(),
        Height = Dim.Fill(),
    };
    
    public ServerView()
    {
        Title = "Servers";
        X = 0;
        Y = 1;
        Width = 25;
        Height = Dim.Fill();
        Border = new Border { BorderStyle = BorderStyle.Rounded };
        
        Add(ServerTree);
    }
}
