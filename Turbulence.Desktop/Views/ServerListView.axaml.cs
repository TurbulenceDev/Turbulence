using Avalonia.Controls;

namespace Turbulence.Desktop.Views;

public partial class ServerListView : UserControl
{
    public ServerListView()
    {
        InitializeComponent();
        if (Design.IsDesignMode)
        {
            // Workaround to fix design data context getting overwritten
            DataContext = Design.GetDataContext(this);
        }
    }
}
