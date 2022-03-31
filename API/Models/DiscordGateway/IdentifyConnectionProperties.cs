using System.Runtime.Serialization;

namespace Turbulence.API.Models.DiscordGateway;

public class IdentifyConnectionProperties
{
    /// <summary>
    /// Your operating system.
    /// </summary>
    [DataMember(Name = "$os", IsRequired = true)]
    public string Os { get; set; }

    /// <summary>
    /// Your browser name.
    /// </summary>
    [DataMember(Name = "$browser", IsRequired = true)]
    public string Browser { get; set; }

    /// <summary>
    /// Your device name.
    /// </summary>
    [DataMember(Name = "$device", IsRequired = true)]
    public string Device { get; set; }
}