using Avalonia.Controls;
using Avalonia.Threading;
using Turbulence.Core.ViewModels;

namespace Turbulence.Desktop.Views.Main;

public partial class MessagesView : UserControl
{
    private readonly MessagesViewModel _vm;

    public MessagesView()
    {
        InitializeComponent();
        if (Design.IsDesignMode)
        {
            // Workaround to fix design data context getting overwritten
            DataContext = Design.GetDataContext(this);
        }
        _vm = (MessagesViewModel)DataContext!;

        _vm.CurrentMessages.CollectionChanged += (_, _) =>
        {
            // TODO: This whole thing could use improvement
            // TODO: Is Dispatcher.UIThread.Invoke really necessary?
            Dispatcher.UIThread.Invoke(() =>
            {
                if (Math.Abs(Scroll.Offset.Y - Scroll.ScrollBarMaximum.Y) < 0.05)
                    Scroll.ScrollToEnd();
                else
                    Scroll.LineDown();
            });
        }; 
    }
}
