using Terminal.Gui;
using Turbulence.Core.ViewModels;

namespace Turbulence.TGUI.Views;

public sealed class MessagesView : FrameView
{
    public readonly ListView MessagesListView = new()
    {
        Width = Dim.Fill(1),
        Height = Dim.Fill(),
    };

    private readonly MessagesViewModel _vm;
    private readonly ScrollBarView _scrollbar;

    public MessagesView(MessagesViewModel vm)
    {
        _vm = vm;
        
        Title = "Messages";
        X = 25;
        Y = 1;
        Width = 93;
        Height = 24;
        Border = new Border { BorderStyle = BorderStyle.Rounded };

        Add(MessagesListView);
        MessagesListView.SetSource(_vm.CurrentMessages);
        _scrollbar = new ScrollBarView(MessagesListView, true);

        // Draw scrollbar on
        MessagesListView.DrawContent += _ => {
            _scrollbar.Size = MessagesListView.Source.Count;
            _scrollbar.Position = MessagesListView.TopItem;
            _scrollbar.OtherScrollBarView.Size = MessagesListView.Maxlength;
            _scrollbar.OtherScrollBarView.Position = MessagesListView.LeftItem;
            _scrollbar.Refresh();
        };

        // Vertical set
        _scrollbar.ChangedPosition += () => {
            MessagesListView.TopItem = _scrollbar.Position;
            if (MessagesListView.TopItem != _scrollbar.Position)
            {
                _scrollbar.Position = MessagesListView.TopItem;
            }
            MessagesListView.SetNeedsDisplay();
        };

        // Horizontal set
        _scrollbar.OtherScrollBarView.ChangedPosition += () => {
            MessagesListView.LeftItem = _scrollbar.OtherScrollBarView.Position;
            if (MessagesListView.LeftItem != _scrollbar.OtherScrollBarView.Position)
            {
                _scrollbar.OtherScrollBarView.Position = MessagesListView.LeftItem;
            }
            MessagesListView.SetNeedsDisplay();
        };

        _vm.ShowNewChannel += (sender, _) =>
        {
            // scroll down to the bottom (also refreshes)
            MessagesListView.SelectedItem = ((MessagesViewModel)sender!).CurrentMessages.Count - 1; // else mouse scrolling will start at the beginning
            MessagesListView.ScrollDown(((MessagesViewModel)sender).CurrentMessages.Count);
        };
    }

    public void AddMessage(string message)
    {
        _vm.CurrentMessages.Add(message);
    }
}
