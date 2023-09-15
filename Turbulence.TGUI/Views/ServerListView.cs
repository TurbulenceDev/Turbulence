using Terminal.Gui;
using Terminal.Gui.Trees;
using Turbulence.Core.ViewModels;

namespace Turbulence.TGUI.Views;

public sealed class ServerListView : FrameView
{
    public readonly TreeView ServerTree = new()
    {
        Width = Dim.Fill(),
        Height = Dim.Fill(),
    };

    private readonly ServerListViewModel _vm;
    
    public ServerListView(ServerListViewModel vm)
    {
        _vm = vm;
        
        Title = "Servers";
        X = 0;
        Y = 1;
        Width = 25;
        Height = Dim.Fill();
        Border = new Border { BorderStyle = BorderStyle.Rounded };
        
        Add(ServerTree);

        ServerTree.SelectionChanged += SelectionChanged;
    }

    private void SelectionChanged(object? _, SelectionChangedEventArgs<ITreeNode> e)
    {
        _vm.SelectionChangedCommand.Execute(e.NewValue.Tag);
    }
}
