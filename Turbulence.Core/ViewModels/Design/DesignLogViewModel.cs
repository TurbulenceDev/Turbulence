namespace Turbulence.Core.ViewModels.Design
{
    public class DesignLogViewModel : LogViewModel
    {
        public DesignLogViewModel()
        {
            Logs.AddRange(new string[]
            {
                "[CDN] Request 1",
                "[API] Request 2",
            });
        }

        public override void Refresh()
        {
            Logs.Add($"[REFRESH] New Request {Logs.Count}");
        }
    }
}
