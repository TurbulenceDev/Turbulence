using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Avalonia;
using LibVLCSharp.Shared;

namespace Turbulence.Desktop.Controls;

public partial class VideoPlayerControl : UserControl
{
    public static readonly DirectProperty<VideoPlayerControl, MediaPlayer?> MediaPlayerProperty =
        AvaloniaProperty.RegisterDirect<VideoPlayerControl, MediaPlayer?>(nameof(MediaPlayer),
            o => o.MediaPlayer,
            (o, v) => o.MediaPlayer = v,
            null!,
            BindingMode.TwoWay,
            false);

    private readonly VideoView _videoViewer;
    private MediaPlayer? _mediaPlayer;
    
    public MediaPlayer? MediaPlayer
    {
        get => _mediaPlayer;
        set
        {
            _mediaPlayer = value;
            if (_videoViewer.IsLoaded || _mediaPlayer == null)
            {
                SetMediaPlayer();
            }
        }
    }

    public VideoPlayerControl()
    {
        InitializeComponent();
        
        _videoViewer = this.Get<VideoView>("VideoViewer");
        _videoViewer.Loaded += VideoViewerLoaded;
    }

    private void VideoViewerLoaded(object? sender, RoutedEventArgs e)
    {
        SetMediaPlayer();
    }

    private void SetMediaPlayer()
    {
        _videoViewer.MediaPlayer = _mediaPlayer;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}