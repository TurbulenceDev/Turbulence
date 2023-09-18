using CommunityToolkit.Mvvm.ComponentModel;

namespace Turbulence.Core;

public class ViewModelBase : ObservableRecipient
{
    protected ViewModelBase()
    {
        IsActive = true;
    }
}
