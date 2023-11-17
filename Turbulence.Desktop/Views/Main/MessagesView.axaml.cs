using Avalonia.Controls;
using Avalonia.Threading;
using Turbulence.Core.ViewModels;
using Turbulence.Core.ViewModels.Design;

namespace Turbulence.Desktop.Views.Main;

public partial class MessagesView : UserControl
{
    private readonly MessagesViewModel _vm;
    // Saves the previous length of the scrollviewer so we can calculate which item we were looking at
    private double? _beforeFetchLength = null;

    public MessagesView()
    {
        InitializeComponent();
        if (Design.IsDesignMode)
        {
            // Workaround to fix design data context getting overwritten
            DataContext = new DesignMessagesViewModel();
        }
        _vm = (MessagesViewModel)DataContext!;

        Scroll.PropertyChanged += (_, args) =>
        {
            // Listen to length changes and jump to the offset if set
            if (args.Property.Name == nameof(Scroll.Extent))
            {
                if (_beforeFetchLength != null)
                {
                    Scroll.Offset = new(Scroll.Offset.X, Scroll.Extent.Height - _beforeFetchLength.Value);
                    _beforeFetchLength = null;
                    return;
                }
            }
        };

        _vm.CurrentMessages.CollectionChanged += (_, args) =>
        {
            // TODO: This whole thing could use improvement
            // TODO: Is Dispatcher.UIThread.Invoke really necessary?
            Dispatcher.UIThread.Invoke(() =>
            {
                if (Math.Abs(Scroll.Offset.Y - Scroll.ScrollBarMaximum.Y) < 0.05)
                    Scroll.ScrollToEnd();
                else
                    Scroll.LineDown(); //FIXME: a line is not the same height as a message
            });
        };
    }

    public async void ScrollChanged(object sender, ScrollChangedEventArgs args)
    {
        if ((Scroll.Offset.Y + args.OffsetDelta.Y) < 0)
        {
            // Save the length beforehand
            _beforeFetchLength = Scroll.Extent.Height;
            _vm.RequestMoreMessages(true);
        }
        //TODO: also listen for other side
    }
}
