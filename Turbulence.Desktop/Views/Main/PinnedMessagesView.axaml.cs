using Avalonia.Controls;
using Turbulence.Core.ViewModels.Design;

namespace Turbulence.Desktop.Views.Main;

public partial class PinnedMessagesView : UserControl
{
    public PinnedMessagesView()
    {
        InitializeComponent();
        if (Design.IsDesignMode)
        {
            // Workaround to fix design data context getting overwritten
            DataContext = new DesignPinnedMessagesViewModel();
        }
    }
}
