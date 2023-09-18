using Terminal.Gui;
using Turbulence.Core.ViewModels;

namespace Turbulence.TGUI.Views;

public sealed class MessagesView : FrameView
{
    private readonly ListView _messagesListView = new()
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

        Add(_messagesListView);
        _messagesListView.SetSource(_vm.CurrentMessages);
        _scrollbar = new ScrollBarView(_messagesListView, true);

        // Draw scrollbar on
        _messagesListView.DrawContent += _ => {
            _scrollbar.Size = _messagesListView.Source.Count;
            _scrollbar.Position = _messagesListView.TopItem;
            _scrollbar.OtherScrollBarView.Size = _messagesListView.Maxlength;
            _scrollbar.OtherScrollBarView.Position = _messagesListView.LeftItem;
            _scrollbar.Refresh();
        };

        // Vertical set
        _scrollbar.ChangedPosition += () => {
            _messagesListView.TopItem = _scrollbar.Position;
            if (_messagesListView.TopItem != _scrollbar.Position)
            {
                _scrollbar.Position = _messagesListView.TopItem;
            }
            _messagesListView.SetNeedsDisplay();
        };

        // Horizontal set
        _scrollbar.OtherScrollBarView.ChangedPosition += () => {
            _messagesListView.LeftItem = _scrollbar.OtherScrollBarView.Position;
            if (_messagesListView.LeftItem != _scrollbar.OtherScrollBarView.Position)
            {
                _scrollbar.OtherScrollBarView.Position = _messagesListView.LeftItem;
            }
            _messagesListView.SetNeedsDisplay();
        };

        _vm.ShowNewChannel += (sender, _) =>
        {
            // scroll down to the bottom (also refreshes)
            _messagesListView.SelectedItem = ((MessagesViewModel)sender!).CurrentMessages.Count - 1; // else mouse scrolling will start at the beginning
            _messagesListView.ScrollDown(((MessagesViewModel)sender).CurrentMessages.Count);
        };

        _vm.PropertyChanged += (_, args) =>
        {
            switch (args.PropertyName)
            {
                case nameof(MessagesViewModel.Title):
                    Title = _vm.Title;
                    break;
                case nameof(MessagesViewModel.CurrentMessages):
                    _messagesListView.SetNeedsDisplay();
                    break;
            }
        };
    }
}
