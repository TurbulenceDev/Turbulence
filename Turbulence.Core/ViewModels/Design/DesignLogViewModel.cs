using Turbulence.Discord.Services;

namespace Turbulence.Core.ViewModels.Design;

public class DesignLogViewModel : LogViewModel
{
    Array types = Enum.GetValues(typeof(LogType));
    Array levels = Enum.GetValues(typeof(LogLevel));
    Random random = new Random();

    public DesignLogViewModel()
    {
        Logs.AddRange(new LogEntry[]
        {
            new("Request 1", LogType.Images, LogLevel.Debug, DateTime.Now),
            new("Request 2", LogType.Networking, LogLevel.Info, DateTime.Now),
        });
    }

    public override void Refresh()
    {
        var type = (LogType)types.GetValue(random.Next(types.Length))!;
        var lvl = (LogLevel)levels.GetValue(random.Next(levels.Length))!;
        Logs.Add(new($"New Request {Logs.Count}", type, lvl, DateTime.Now));
    }
}
