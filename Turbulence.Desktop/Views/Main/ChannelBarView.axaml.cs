using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Turbulence.Core.ViewModels;
using Turbulence.Core.ViewModels.Design;

namespace Turbulence.Desktop.Views.Main
{
    public partial class ChannelBarView : UserControl
    {
        private readonly ChannelBarViewModel _vm;

        public ChannelBarView()
        {
            InitializeComponent();
            if (Design.IsDesignMode)
            {
                // Workaround to fix design data context getting overwritten
                DataContext = new DesignChannelBarViewModel();
            }
            _vm = (ChannelBarViewModel)DataContext!;
        }

        public void OnPins(object? sender, RoutedEventArgs? _)
        {
            if (sender is Control control)
            {
                FlyoutBase.ShowAttachedFlyout(control);
                if (!Design.IsDesignMode)
                    ((PinnedMessagesViewModel)Pins.DataContext!).FetchPinnedMessages();
            }
        }

        public void OnSearchKey(object? sender, KeyEventArgs? args)
        {
            if (args == null)
                return;

            if (args.Key != Key.Enter)
                return;

            var input = Search.Text;
            if (string.IsNullOrEmpty(input))
                return;

            _vm.Search(input);
        }
    }
}
