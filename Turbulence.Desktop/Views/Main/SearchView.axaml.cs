using Avalonia.Controls;
using Turbulence.Core.ViewModels;
using Turbulence.Core.ViewModels.Design;

namespace Turbulence.Desktop.Views.Main
{
    public partial class SearchView : UserControl
    {
        private SearchViewModel _vm;
        public SearchView()
        {
            InitializeComponent();
            if (Design.IsDesignMode)
            {
                DataContext = new DesignSearchViewModel();
            }
            _vm = (SearchViewModel)DataContext!;
        }

        public void OnPageChanged(object? _1, NumericUpDownValueChangedEventArgs? args)
        {
            if (args != null && args.NewValue != null)
                _vm.OnPageChanged((int)args.NewValue);
        }
    }
}
