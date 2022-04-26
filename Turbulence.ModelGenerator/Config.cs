namespace Turbulence.ModelGenerator;

// TODO: This should probably be a JSON/YAML file ¯\_(ツ)_/¯
public static class Config
{
    /// <summary>
    /// Root directory or URL for the .md files.
    /// </summary>
    public static readonly Uri DocsRoot = new("https://raw.githubusercontent.com/discord/discord-api-docs/main/docs");

    /// <summary>
    /// The path to temporarily store downloadded/generated files at.
    /// </summary>
    public static readonly Uri TempPath = new($"{Directory.GetCurrentDirectory()}/../../../Out/Temp");
    
    /// <summary>
    /// The path to store final models at.
    /// </summary>
    public static readonly Uri OutPath = new($"{Directory.GetCurrentDirectory()}/../../../Out/Models");
    
    /// <summary>
    /// Location of .md files to generate models for, appended to the root directory.
    /// </summary>
    public static readonly List<string> MdFiles = new()
    {
        // Find with:
        // ack -l "\s*\|\s*[Ff]ield\s*\|\s*[Tt]ype\s*\|\s*[Dd]escription\s*\|$"
        "resources/Guild.md",
        "resources/Voice.md",
        "resources/Emoji.md",
        "resources/Audit_Log.md",
        "resources/Invite.md",
        "resources/Guild_Scheduled_Event.md",
        "resources/User.md",
        "resources/Webhook.md",
        "resources/Sticker.md",
        "resources/Guild_Template.md",
        "resources/Stage_Instance.md",
        "resources/Application.md",
        "resources/Channel.md",
        "interactions/Application_Commands.md",
        "interactions/Receiving_and_Responding.md",
        "interactions/Message_Components.md",
        "topics/Gateway.md",
        "topics/Rate_Limits.md",
        "topics/RPC.md",
        "topics/Teams.md",
        "topics/OAuth2.md",
        "topics/Certified_Devices.md",
        "topics/Permissions.md",
    };
}