using Terminal.Gui;

namespace Turbulence.TGUI.Views;

public sealed class MessagesView : FrameView
{
    public readonly ListView Messages = new()
    {
        Width = Dim.Fill(1),
        Height = Dim.Fill(),
    };
    
    private readonly ScrollBarView _scrollbar;

    public MessagesView()
    {
        Title = "Messages";
        X = 25;
        Y = 1;
        Width = 93;
        Height = 24;
        Border = new Border { BorderStyle = BorderStyle.Rounded };

        Add(Messages);
        _scrollbar = new ScrollBarView(Messages, true);

        // Draw scrollbar on
        Messages.DrawContent += _ => {
            _scrollbar.Size = Messages.Source.Count;
            _scrollbar.Position = Messages.TopItem;
            _scrollbar.OtherScrollBarView.Size = Messages.Maxlength;
            _scrollbar.OtherScrollBarView.Position = Messages.LeftItem;
            _scrollbar.Refresh();
        };

        // Vertical set
        _scrollbar.ChangedPosition += () => {
            Messages.TopItem = _scrollbar.Position;
            if (Messages.TopItem != _scrollbar.Position)
            {
                _scrollbar.Position = Messages.TopItem;
            }
            Messages.SetNeedsDisplay();
        };

        // Horizontal set
        _scrollbar.OtherScrollBarView.ChangedPosition += () => {
            Messages.LeftItem = _scrollbar.OtherScrollBarView.Position;
            if (Messages.LeftItem != _scrollbar.OtherScrollBarView.Position)
            {
                _scrollbar.OtherScrollBarView.Position = Messages.LeftItem;
            }
            Messages.SetNeedsDisplay();
        };
    }
}
