using Turbulence.Discord;

namespace Turbulence.Core.ViewModels;

public class TurbulenceWindowViewModel
{
    public readonly Client Client = new();
    public ulong CurrentChannel = 0;
}
