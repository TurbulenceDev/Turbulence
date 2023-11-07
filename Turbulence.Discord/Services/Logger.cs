namespace Turbulence.Discord.Services;

public interface ILogger
{
    public void Log(string message);
    public IEnumerable<string> GetLogs();
}


public class Logger : ILogger
{
    //TODO: add logging severity/level
    //TODO: add logging type
    //TODO: add timestamp
    private readonly List<string> _log = new();

    public IEnumerable<string> GetLogs()
    {
        return _log;
    }

    public void Log(string message)
    {
        _log.Add(message);
    }
}
