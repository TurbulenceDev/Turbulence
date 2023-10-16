using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Turbulence.Desktop.Views.Main
{
    public partial class ChannelBarView : UserControl
    {
        public ChannelBarView()
        {
            InitializeComponent();
            if (Design.IsDesignMode)
            {
                // Workaround to fix design data context getting overwritten
                DataContext = Design.GetDataContext(this);
            }
        }

        public void OnPins(object? sender, RoutedEventArgs? _)
        {
            if (sender is Control control)
            {
                FlyoutBase.ShowAttachedFlyout(control);
            }
        }
    }
}
