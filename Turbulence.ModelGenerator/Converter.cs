using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Turbulence.ModelGenerator;

public static class Converter
{
    // Amount of unmatched types
    private static int _unmatchedCount = 0;
    
    // Mapping from record name to the namespace it belongs to
    private static readonly Dictionary<string, string> Namespaces = new()
    {
        { "MessageComponent", "DiscordMessageComponents" },
    };

    /// <summary>
    /// Convert all markdown tables in a directory to C# records.
    /// </summary>
    /// <param name="tablesPath">The directory with JSON tables.</param>
    /// <param name="modelPath">The directory to put the generated records.</param>
    public static async Task Convert(Uri tablesPath, Uri modelPath)
    {
        Directory.CreateDirectory(modelPath.LocalPath);

        // First, extract all record names and their namespaces
        foreach (var tableDir in Directory.GetDirectories(tablesPath.LocalPath))
        {
            foreach (var tableFile in Directory.GetFiles(tableDir))
            {
                var nameSpace = tableDir.Split(Path.DirectorySeparatorChar)[^1];
                var recordName = tableFile.Split(Path.DirectorySeparatorChar)[^1];

                if (Namespaces.ContainsKey(recordName))
                {
                    // Check if existing namespace matches
                    if (Namespaces[recordName] == nameSpace) continue;

                    throw new Exception($"Record {recordName} is part of multiple namespaces: {Namespaces[recordName]} and {nameSpace}");
                }
                
                Namespaces.Add(recordName, nameSpace);
            }
        }

        // Assumes that the depth level is exactly 1 deep
        foreach (var tableDir in Directory.GetDirectories(tablesPath.LocalPath))
        {
            var modelDir = Path.Combine(modelPath.LocalPath, tableDir.Split(Path.DirectorySeparatorChar)[^1]);
            Directory.CreateDirectory(modelDir);
            
            foreach (var tableFile in Directory.GetFiles(tableDir))
            {
                var modelFile = Path.ChangeExtension(
                    Path.Combine(modelDir, tableFile.Split(Path.DirectorySeparatorChar)[^1]), ".cs");

                // Write generated .cs record to file
                await File.WriteAllTextAsync(
                    modelFile, await TableToRecord(tableFile));
            }
        }

        if (_unmatchedCount > 0) Console.WriteLine($"\nTotal unmatched types: {_unmatchedCount}");
    }

    private static async Task<string> TableToRecord(string path)
    {
        var split = path.Split(Path.DirectorySeparatorChar);
        var dir = split[^2];
        var recordName = split[^1];
        
        using var xml = XmlReader.Create(path);
        var serializer = new DataContractSerializer(typeof(TableSource));
        var tableSrc = (TableSource) (serializer.ReadObject(xml)
                                      ?? throw new Exception($"Failed to deserialize file {path}."));
        xml.Dispose();
        
        var reader = new StringReader(tableSrc.Table);
        await reader.ReadLineAsync(); // Dump signature line
        await reader.ReadLineAsync(); // Dump separator line

        StringBuilder record = new();
        SortedSet<string> imports = new(StringComparer.InvariantCulture);

        List<string[]> propertyValues = new();

        while (await reader.ReadLineAsync() is { } property)
        {
            propertyValues.Add(property.Split("|")
                                              .Where(x => !string.IsNullOrWhiteSpace(x))
                                              .Select(x => x.Trim())
                                              .ToArray());
        }

        var nameSpace = $"{Config.NamespaceBase}.{dir}";
        
        record.AppendLine(  "using System.Text.Json.Serialization;");
        record.AppendLine();
        record.AppendLine( $"namespace {nameSpace};");
        record.AppendLine();
        record.AppendLine(  "/// <summary>");
        record.AppendLine(@$"/// See the <a href=""{tableSrc.DiscordUrl}"">Discord API documentation</a> or <a href=""{tableSrc.GithubUrl}"">GitHub</a>.");
        record.AppendLine(  "/// </summary>");
        record.AppendLine($"public record {recordName} {{");

        for (var i = 0; i < propertyValues.Count; i++)
        {
            var prop = propertyValues[i];
            var field = prop[0];
            var type = prop[1];
            var description = prop[2];

            if (field.Contains("files[n]"))
            {
                record.AppendLine(
                    "    // TODO: Implement files[n] bullshit (https://discord.com/developers/docs/reference#uploading-files)");
                continue;
            }

            // Escape < and > characters in descriptions
            description = Regex.Replace(description, "<(.*?)>", "&lt;$1&gt;");
            
            // Transform ` ` into XML code blocks
            description = Regex.Replace(description, "`(.*?)`", "<c>$1</c>");

            record.AppendLine($"    /// <summary>\n    /// {PrettifyDescription(description)}\n    /// </summary>");
            
            var prettyField = ConvertField(field);
            var cleanField = CleanField(field);
            var convertedType = ConvertType(type, out var convertedNamespace);

            if (convertedNamespace != null)
                imports.Add($"using {Config.NamespaceBase}.{convertedNamespace};");

            var required = !field.Contains('?');
            
            string requiredStr, ignore;
            if (required)
            {
                requiredStr = "required ";
                ignore = "";
            }
            else
            {
                requiredStr = "";
                ignore = "\n    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]";
                
                // Property that isn't required has to be nullable
                if (!convertedType.EndsWith('?'))
                {
                    convertedType += '?';
                }
            }

            var newline = i != propertyValues.Count - 1 ? "\n" : "";

            record.AppendLine(
                $"    [JsonPropertyName(\"{cleanField}\")]{ignore}\n    public {requiredStr}{convertedType} {prettyField} {{ get; init; }}{newline}");
        }

        record.AppendLine("}");

        while (imports.Count > 0)
        {
            var import = imports.Last();
            imports.Remove(import);
        
            if (import != $"using {nameSpace};")
            {
                record.Insert(0, $"{import}\n");
            }
        }

        return record.ToString();
    }

    private static string ConvertType(string type, out string? nameSpace)
    {
        nameSpace = null;
        
        type = Regex.Replace(type, @"\[(.*?)\]\(.*?\)", "$1");
        type = Regex.Replace(type, @"\(.*\)", "");
        type = type.Replace("*", "")
                   .Replace("_", " ")
                   .Replace(@"\", "")
                   // https://github.com/discord/discord-api-docs/blob/main/docs/resources/Guild.md#get-guild-prune-count--get-guildsguildiddocs_resources_guildguild-objectprune
                   .Replace("; comma-delimited array of snowflakes", "")
                   .Trim();
        
        var nullable = type.StartsWith("?");
        type = type.Replace("?", "")
                   .Trim();

        string? convertedType = null;
        var found = false;

        // First, see if the type matches a collection type
        foreach (var (pattern, replacement) in CollectionTypeMapping)
        {
            var match = Regex.Match(type, pattern);

            if (!match.Success) continue;

            if (match.Groups[1].Value == string.Empty) return replacement;

            var innerType = MapInnerType(match.Groups[1].Value, out var nSpace);
            nameSpace = nSpace;

            if (innerType == null)
            {
                _unmatchedCount++;
                Console.WriteLine(
                    $@"Could not match type ""{type}"", using ""{replacement.Replace("$1", "dynamic")}"" instead");
                innerType = "dynamic";
            }

            found = true;
            convertedType = replacement.Replace("$1", innerType);
            break;
        }

        // Else, assume it is an inner type
        if (!found)
        {
            convertedType = MapInnerType(type, out var nSpace);
            nameSpace = nSpace;
        }

        if (convertedType == null)
        {
            _unmatchedCount++;
            convertedType = $"/* {type} */ dynamic";
            Console.WriteLine($@"Could not match type ""{type}"", using ""dynamic"" instead");
        }
        
        convertedType = convertedType.Trim();
        convertedType += nullable ? "?" : "";

        return convertedType;
    }

    private static readonly (string typePattern, string replacementPattern)[] CollectionTypeMapping = 
    {
        ( @"^array of up to 10 (.*?)$", @"$1[]"),
        ( @"^(?:a|A)rray of (.*?)s?$", @"$1[]" ),
        ( @"^list of (.*?)s?$", @"$1[]" ),
        ( @"^Map of Snowflakes to .*$", @"Snowflake[]"),
    };

    private static string ToPascalCase(string input)
    {
        return input.Split(" ")
                    .Select(x => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(x))
                    .Aggregate("", (s1, s2) => s1 + s2.Trim());
    }

    // An "inner type" is a type that is explicitly not a collection type
    private static string? MapInnerType(string type, out string? nameSpace)
    {
        // Manually overwritten cases where a namespace has to be supplied
        switch (type)
        {
            // https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-guild-application-command-permissions-structure
            case "application command permission":
                nameSpace = "DiscordApplicationCommands";
                return "ApplicationCommandPermissions";
            // https://discord.com/developers/docs/topics/gateway#update-presence
            case "update presence object":
                nameSpace = "DiscordGateway";
                return "GatewayPresenceUpdate";
            // https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-entity-metadata
            case "entity metadata":
                nameSpace = "DiscordGuildScheduledEvent";
                return "GuildScheduledEventEntityMetadata";
            // https://discord.com/developers/docs/topics/gateway#message-reaction-add-message-reaction-add-event-fields
            case "partial emoji":
                nameSpace = "DiscordEmoji";
                return "Emoji";
            // https://discord.com/developers/docs/resources/guild#unavailable-guild-object
            case "Unavailable Guild object":
                nameSpace = "DiscordGuild";
                return "Guild";
            // https://discord.com/developers/docs/resources/channel#start-thread-in-forum-channel-jsonform-params
            case "a forum thread message params object":
                nameSpace = "DiscordChannel";
                return "ForumThreadMessageParamsObject";
            // https://discord.com/developers/docs/topics/gateway#activity-object-activity-timestamps
            case "timestamps object":
                nameSpace = "DiscordGateway";
                return "ActivityTimestamps";
            // https://discord.com/developers/docs/topics/gateway#guild-members-chunk-guild-members-chunk-event-fields
            case "presence object":
                nameSpace = "DiscordGateway";
                return "PresenceUpdateEvent";
            // https://discord.com/developers/docs/topics/gateway#message-reaction-add-message-reaction-add-event-fields
            case "member object":
                nameSpace = "DiscordGuild";
                return "GuildMember";
            // https://discord.com/developers/docs/topics/gateway#activity-object-activity-structure
            case "party object":
                nameSpace = "DiscordGateway";
                return "ActivityParty";
            // https://discord.com/developers/docs/topics/gateway#activity-object-activity-structure
            case "assets object":
                nameSpace = "DiscordGateway";
                return "ActivityAssets";
            // https://discord.com/developers/docs/topics/gateway#activity-object-activity-structure
            case "secrets object":
                nameSpace = "DiscordGateway";
                return "ActivitySecrets";
            // https://discord.com/developers/docs/resources/channel#create-message-jsonform-params
            case "allowed mention object":
                nameSpace = "DiscordChannel";
                return "AllowedMentions";
            // https://discord.com/developers/docs/resources/guild#integration-object-integration-structure
            case "account object":
                nameSpace = "DiscordGuild";
                return "IntegrationAccount";
        }
        
        // Case where PascalCase conversion already exists
        if (Namespaces.TryGetValue(ToPascalCase(type), out var nSpace))
        {
            nameSpace = nSpace;
            return ToPascalCase(type);
        }
        
        var match = Regex.Match(type, @"(?:(?:a )?partial |a )?(.*?)(?:s)? object");
        if (match.Success)
        {
            var convertedType = ToPascalCase(match.Groups[1].Value);

            if (Namespaces.TryGetValue(convertedType, out string? nSpace2))
            {
                nameSpace = nSpace2;
                return convertedType;
            }

            _unmatchedCount++;
            Console.WriteLine($@"Object type ""{type}"" not found, you should explicitly convert it. Using ""dynamic"" instead.");
            nameSpace = null;
            return "dynamic";
        }

        // For overrides that don't need a special namespace
        nameSpace = null;
        return type switch
        {
            "string" => "string",
            "boolean" => "bool",
            "integer" => "int",
            "int" => "int",
            "float" => "float",
            "object" => "dynamic",
            "snowflake" => "Snowflake",
            "mixed" => "dynamic",
            "ISO8601 timestamp" => "DateTimeOffset",
            "array" => "dynamic",
            "dict" => "dynamic",
            "two integer" => "int",
            "image data" => "string",
            "number" => "int",
            "binary" => "string",
            "guild feature string" => "string",
            "file contents" => "string",
            "audit log event" => "string",
            "channel type" => "/* TODO: Make enum */ int",
            "one of application command type" => "/* TODO: Make enum */ int",
            "event status" => "/* TODO: Make enum */ int",
            "entity type" => "/* TODO: Make enum */ int",
            "event entity type" => "/* TODO: Make enum */ int",
            "scheduled entity type" => "/* TODO: Make enum */ int",
            "application command permission type" => "/* TODO: Make enum */ int",
            "allowed mention type" => "/* TODO: Make enum */ string",
            "privacy level" => "/* 1: PUBLIC, 2: GUILD_ONLY */ int",
            "integer for `INTEGER` options, double for `NUMBER` options" => "dynamic",
            "integer or string" => "/* integer or string */ dynamic",
            "string, integer, or double" => "dynamic",
            "dictionary with keys in available locales" => "string[]",
            "snowflake or array of snowflakes" => "Snowflake[]",
            "null" => "bool?",
            _ => null,
        };
    }

    /// <summary>
    /// Cleans a field, only removing garbage like * and ?
    /// </summary>
    /// <seealso cref="ConvertField"/>
    private static string CleanField(string field)
    {
        field = field.Replace("?", "");
        field = field.Replace("*", "");
        field = field.Replace(@"\", "");
        
        field = Regex.Replace(field, @"\(.*\)", "");

        return field.Trim();
    }
    
    /// <summary>
    /// Converts a field from Discord documentation style to C# conventional style
    /// </summary>
    /// <seealso cref="CleanField"/>
    private static string ConvertField(string field)
    {
        field = CleanField(field);
        
        field = field.Replace("$", "")
                     .Split('_')
                     .Select(x => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(x))
                     .ToArray()
                     .Aggregate("", (s1, s2) => s1 + s2.Trim());

        return field.Trim();
    }
    
    private static readonly Dictionary<string, string> DescriptionReplacement = new()
    {
        // TODO: Add more replacement strings
        { "url", "URL" },
        { "youtube", "YouTube" },
        { "twitch", "Twitch" },
        { "oauth2", "OAuth2 " },
        { "rpc", "RPC" },
        { " id ", " ID " },
    };

    private static string PrettifyDescription(string description)
    {
        // Convert the first real letter to uppercase
        var chars = description.ToCharArray();
        for (var i = 0; i < chars.Length; i++)
        {
            if (!char.IsLetter(description[i])) continue;
            
            chars[i] = char.ToUpper(chars[i]);
            description = new string(chars);
            break;
        }

        description = description[0].ToString().ToUpper() + description[1..];

        if (!description.EndsWith('.')) description += '.';

        // Replace strings in the description to make it prettier
        description = DescriptionReplacement.Aggregate(description, (curr, map) => 
                                                           curr.Replace(map.Key, map.Value));

        // Convert markdown hyperlinks
        const string baseUrl = "https://discord.com/developers/docs";
        description = Regex.Replace(description, @"\[(.*?)\]\((.*?)\)", @"<a href=""$2"">$1</a>")
                           .Replace("#DOCS_REFERENCE/", $"{baseUrl}/reference#")
                           .Replace("#DOCS_RESOURCES_APPLICATION/", $"{baseUrl}/resources/application#")
                           .Replace("#DOCS_RESOURCES_AUDIT_LOG/", $"{baseUrl}/resources/audit-log#")
                           .Replace("#DOCS_RESOURCES_CHANNEL/", $"{baseUrl}/resources/channel#")
                           .Replace("#DOCS_RESOURCES_EMOJI/", $"{baseUrl}/resources/emoji#")
                           .Replace("#DOCS_RESOURCES_GUILD/", $"{baseUrl}/resources/guild#")
                           .Replace("#DOCS_RESOURCES_GUILD_SCHEDULED_EVENT/", $"{baseUrl}/resources/guild-template#")
                           .Replace("#DOCS_RESOURCES_INVITE/", $"{baseUrl}/resources/invite#")
                           .Replace("#DOCS_RESOURCES_STAGE_INSTANCE/", $"{baseUrl}/resources/stage-instance#")
                           .Replace("#DOCS_RESOURCES_STICKER/", $"{baseUrl}/resources/sticker#")
                           .Replace("#DOCS_RESOURCES_USER/", $"{baseUrl}/resources/user#")
                           .Replace("#DOCS_RESOURCES_VOICE/", $"{baseUrl}/resources/voice#")
                           .Replace("#DOCS_RESOURCES_WEBHOOK/", $"{baseUrl}/resources/webhook#");

        return description;
    }
    
    // Ran after conversion is done, for example to add missing classes
    public static void PostConvert(Uri modelsPath)
    {
        File.WriteAllText(Path.Combine(modelsPath.LocalPath, "DiscordMessageComponents", "MessageComponent.cs"), 
@"using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordMessageComponents;

/// <summary>
/// <b>Source:</b> <a href=""https://github.com/discord/discord-api-docs/blob/main/docs/interactions/Message_Components.md"">GitHub</a>, <a href=""https://discord.com/developers/docs/interactions/message-components"">Discord API</a>
/// </summary>
public record MessageComponent {
    /// <summary>
    /// 1: Action Row, 2: Button, 3: Select Menu, 4: Text Input
    /// </summary>
    [JsonPropertyName(""type"")]
    public required int Type { get; init; }

    /// <summary>
    /// Array of components.
    /// </summary>
    [JsonPropertyName(""components"")]
    public required dynamic[] Components { get; init; }
}");

        File.WriteAllText(Path.Combine(modelsPath.LocalPath, "DiscordGateway", "Gateway.cs"), 
            @"using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGateway;

/// <summary>
/// <b>Source:</b> <a href=""https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway.md#get-gateway--get-gateway"">GitHub</a>, <a href=""https://discord.com/developers/docs/topics/gateway#get-gateway"">Discord API</a>
/// </summary>
public record Gateway {
    /// <summary>
    /// An object with a valid WSS URL which the app can use when
    /// <a href=""https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway.md#DOCS_TOPICS_GATEWAY/connecting"">Connecting</a>
    /// to the Gateway. Apps should cache this value and only call this endpoint to retrieve a new URL when they are
    /// unable to properly establish a connection using the cached one.
    /// </summary>
    [JsonPropertyName(""url"")]
    public required Uri Url { get; init; }
}");

        File.WriteAllText(Path.Combine(modelsPath.LocalPath, "Error.cs"), 
            @"using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models;

/// <summary>
/// <b>Source:</b> <a href=""https://discord.com/developers/docs/reference#error-messages"">GitHub</a>, <a href=""https://github.com/discord/discord-api-docs/blob/main/docs/Reference.md#error-messages"">Discord API</a>
/// </summary>
public record Error {
    /// <summary>
    /// The <a href=""https://discord.com/developers/docs/topics/opcodes-and-status-codes#json"">JSON error code</a>.
    /// </summary>
    [JsonPropertyName(""code"")]
    public required int Code { get; init; }

    /// <summary>
    /// The error message.
    /// </summary>
    [JsonPropertyName(""message"")]
    public required string Message { get; init; }

    /// <summary>
    /// The errors. ""We will be frequently adding new error messages, so a complete list of errors is not feasible and
    /// would be almost instantly out of date."" Thanks, Discord. Probably best to just JSON pretty print this if it
    /// exists.
    /// </summary>
    [JsonPropertyName(""errors"")]
    public dynamic? Errors { get; init; }
}");
        
        File.WriteAllText(Path.Combine(modelsPath.LocalPath, "DiscordGatewayEvents", "GatewayPayload.cs"), 
            @"using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGateway;

/// <summary>
/// <b>Source:</b> <a href=""https://discord.com/developers/docs/topics/gateway-events#payload-structure"">GitHub</a>, <a href=""https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#payload-structure"">Discord API</a>
/// </summary>
public record GatewayPayload {
    /// <summary>
    /// <a href=""https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-opcodes"">Gateway opcode</a>, which indicates the payload type.
    /// </summary>
    [JsonPropertyName(""op"")]
    public required int Opcode { get; init; }

    /// <summary>
    /// Event data.
    /// </summary>
    [JsonPropertyName(""d"")]
    public required dynamic? Data { get; init; }

    /// <summary>
    /// Sequence number of event used for
    /// <a href=""https://discord.com/developers/docs/topics/gateway#resuming"">resuming sessions</a> and
    /// <a href=""https://discord.com/developers/docs/topics/gateway#sending-heartbeats"">heartbeating</a>.
    /// Null if <see cref=""Opcode"">Opcode</see> is not 0.
    /// </summary>
    [JsonPropertyName(""s"")]
    public required int? SequenceNumber { get; init; }

    /// <summary>
    /// Event name. Null if <see cref=""Opcode"">Opcode</see> is not 0.
    /// </summary>
    [JsonPropertyName(""t"")]
    public required string? EventName { get; init; }
}");
    }
}
