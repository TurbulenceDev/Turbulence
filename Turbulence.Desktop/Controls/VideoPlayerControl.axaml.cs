using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Avalonia.Unofficial;

namespace Turbulence.Desktop.Controls;

public partial class VideoPlayerControl : UserControl
{
    public static readonly DirectProperty<VideoPlayerControl, Uri?> MediaProperty =
        AvaloniaProperty.RegisterDirect<VideoPlayerControl, Uri?>(nameof(Media),
            o => o.Media,
            (o, v) => o.Media = v,
            null!,
            BindingMode.TwoWay,
            false);

    private VideoView? _videoViewer;
    public VideoPlayerControlModel viewModel;

    public Uri? Media
    {
        get => viewModel.Media;
        set => viewModel.Media = value;
    }

    public VideoPlayerControl()
    {
        InitializeComponent();

        viewModel = new VideoPlayerControlModel();

        _videoViewer = this.Get<VideoView>("VideoViewer");
        
        _videoViewer.Loaded += VideoViewerLoaded;
    }

    private void VideoViewerLoaded(object? sender, RoutedEventArgs e)
    {
        if (_videoViewer != null && viewModel.MediaPlayer != null)
        {
            _videoViewer.MediaPlayer = viewModel.MediaPlayer;
            _videoViewer.MediaPlayer.Hwnd = _videoViewer.hndl.Handle;
            viewModel.Play();
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}