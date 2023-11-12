namespace Turbulence.Discord.Services;

public interface ILogger
{
    public void Log(string message, LogType type, LogLevel level = LogLevel.Info);
    public IEnumerable<LogEntry> GetLogs();
}

public enum LogType
{
    Any = -1,
    Application,
    Networking,
    Images
}

public enum LogLevel
{
    Debug,
    Info,
    Warning,
}

public record LogEntry(string Message, LogType Type, LogLevel Level, DateTime Timestamp);

public class Logger : ILogger
{
    private readonly List<LogEntry> _log = new();

    public Logger()
    {
        Log("Logger started", LogType.Application, LogLevel.Info);
    }

    public IEnumerable<LogEntry> GetLogs()
    {
        return _log;
    }

    public void Log(string message, LogType type, LogLevel level = LogLevel.Info)
    {
        _log.Add(new(message, type, level, DateTime.Now));
    }
}
