using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Turbulence.Core;

public class ObservableList<T> : ObservableCollection<T>
{
    public bool SuppressNotification { get; set; } = false;
 
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (!SuppressNotification)
            base.OnCollectionChanged(e);
    }
 
    public void AddRange(IEnumerable<T> list)
    {
        SuppressNotification = true;
        foreach (var item in list)
        {
            Add(item);
        }
        SuppressNotification = false;
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public void ReplaceAll(IEnumerable<T> list)
    {
        SuppressNotification = true;
        Clear();
        AddRange(list);
        SuppressNotification = false;
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
