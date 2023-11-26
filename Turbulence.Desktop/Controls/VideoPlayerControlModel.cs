using CommunityToolkit.Mvvm.DependencyInjection;
using LibVLCSharp.Shared;
using Turbulence.Core;

namespace Turbulence.Desktop.Controls;

public class VideoPlayerControlModel : ViewModelBase
{
    private readonly LibVLC _libVLC = Ioc.Default.GetService<LibVLC>()!;
    public MediaPlayer? MediaPlayer;
    public Uri? Media;// = new Uri("https://media.tenor.com/rIZ4kijzR18AAAPo/turbulence.mp4");

    public VideoPlayerControlModel()
    {
        if (!Avalonia.Controls.Design.IsDesignMode)
        {
            MediaPlayer = new MediaPlayer(_libVLC);
        }
    }
    
    public void Play()
    {
        if (MediaPlayer != null && Media != null)
        {
            using var media = new Media(_libVLC, Media);
            MediaPlayer.Play(media);
            media.Dispose();
        }
    }

    public void Stop()
    {
        if (MediaPlayer != null)
        {
            MediaPlayer.Stop();
        }
    }
}