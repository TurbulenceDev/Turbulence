using CommunityToolkit.Mvvm.DependencyInjection;
using Terminal.Gui;
using Turbulence.Core.ViewModels;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.TGUI.Views;

public sealed class MessagesView : FrameView
{
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    private readonly ListView _messagesListView = new()
    {
        Width = Dim.Fill(1),
        Height = Dim.Fill(),
    };

    private readonly MessagesViewModel _vm = new();
    private readonly ScrollBarView _scrollbar;
    private readonly List<string> _currentMessagesProcessed = new(); // TODO: Implement IListDataSource?

    public MessagesView()
    {
        Title = "Messages";
        X = 25;
        Y = 1;
        Width = 93;
        Height = 24;
        Border = new Border { BorderStyle = BorderStyle.Rounded };

        Add(_messagesListView);
        _messagesListView.SetSource(_currentMessagesProcessed);
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
            // Scroll down to the bottom (also refreshes)
            _messagesListView.SelectedItem = ((MessagesViewModel)sender!).CurrentMessages.Count - 1; // Else mouse scrolling will start at the beginning
            _messagesListView.ScrollDown(((MessagesViewModel)sender).CurrentMessages.Count);
        };

        _vm.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(MessagesViewModel.Title))
                Title = _vm.Title;
        };

        _vm.CurrentMessages.CollectionChanged += (_, _) =>
        {
            _currentMessagesProcessed.Clear();
            _currentMessagesProcessed.AddRange(_vm.CurrentMessages.Select(m =>
            m.Type == MessageType.DEFAULT ? 
                $"{m.GetBestAuthorName()}: {_client.GetMessageContent(m)}" :
                _client.GetMessageContent(m)));
            _messagesListView.ScrollDown(1);
            _messagesListView.SetNeedsDisplay();
        };
    }
}
