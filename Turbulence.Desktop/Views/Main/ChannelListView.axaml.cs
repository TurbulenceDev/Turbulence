using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Turbulence.Core.ViewModels;
using Turbulence.Core.ViewModels.Design;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.Views.Main;

public partial class ChannelListView : UserControl
{
    private ChannelListViewModel _vm;
    public ChannelListView()
    {
        InitializeComponent();
        if (Design.IsDesignMode)
        {
            // Workaround to fix design data context getting overwritten
            DataContext = new DesignChannelListViewModel();
        }
        _vm = (ChannelListViewModel)DataContext!;
    }

    //TODO: use a button with a "checked" class + command to do this
    public void ChannelSelected(object sender, RoutedEventArgs e)
    {
        if (e.Source is ToggleButton control) 
        {
            var dataContext = control.DataContext;
            if (dataContext is Channel)
            {
                if (_vm.SelectedChannel != (Channel)dataContext)
                    _vm.SelectedChannel = (Channel)dataContext;
                else // if its already selected, then pls check this one again
                    control.IsChecked = true;
            }
        }
    }
}
