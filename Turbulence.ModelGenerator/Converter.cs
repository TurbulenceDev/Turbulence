using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Turbulence.ModelGenerator;

public static class Converter
{
    /// <summary>
    /// Convert all markdown tables in a directory to C# records.
    /// </summary>
    /// <param name="tablesPath">The directory with JSON tables.</param>
    /// <param name="modelPath">The directory to put the generated records.</param>
    public static async Task Convert(Uri tablesPath, Uri modelPath)
    {
        Directory.CreateDirectory(modelPath.AbsolutePath);
        
        // Assumes that the depth level is exactly 1 deep
        foreach (string tableDir in Directory.GetDirectories(tablesPath.AbsolutePath))
        {
            string modelDir = Path.Combine(modelPath.AbsolutePath, tableDir.Split("/")[^1]);
            Directory.CreateDirectory(modelDir);
            
            foreach (string tableFile in Directory.GetFiles(tableDir))
            {
                string modelFile = Path.ChangeExtension(
                    Path.Combine(modelDir, tableFile.Split("/")[^1]), ".cs");

                // Write generated .cs record to file
                await File.WriteAllTextAsync(
                    modelFile, await TableToRecord(tableFile));
            }
        }
    }

    private static async Task<string> TableToRecord(string path)
    {
        string[] split = path.Split("/");
        string dir = split[^2];
        string recordName = split[^1];
        
        var reader = new StreamReader(path);
        
        // Extract URL and table from file
        string url = await reader.ReadLineAsync() ?? throw new Exception($"Table file {path} is empty.");
        string tableSignatureString = await reader.ReadLineAsync()
                                      ?? throw new Exception("Table file {path} is missing table");
        await reader.ReadLineAsync(); // Dump separator line

        string[] tableSignature = tableSignatureString.Split("|")
                                                      .Where(x => !string.IsNullOrEmpty(x))
                                                      .Select(x => x.Trim())
                                                      .ToArray();

        StringBuilder record = new();

        List<string[]> propertyValues = new();
        
        string? property;
        while ((property = await reader.ReadLineAsync()) != null)
        {
            propertyValues.Add(property.Split("|")
                                              .Where(x => !string.IsNullOrWhiteSpace(x))
                                              .Select(x => x.Trim())
                                              .ToArray());
        }

        record.AppendLine( "using Newtonsoft.Json;");
        record.AppendLine();
        record.AppendLine($"namespace Turbulence.API.Models.{dir};");
        record.AppendLine();
        record.AppendLine( "/// <summary>");
        record.AppendLine($"/// Taken from {url}");
        record.AppendLine( "/// </summary>");
        
        foreach (string[] prop in propertyValues)
        {
            string field = prop[0];
            string description = prop[2];

            record.AppendLine($"/// <param name=\"{ConvertField(field)}\">{PrettifyDescription(description)}</param>");
        }
        
        record.AppendLine($"public record {recordName} (");

        for (var i = 0; i < propertyValues.Count; i++)
        {
            string[] prop = propertyValues[i];
            string field = prop[0];
            string type = prop[1];
            
            if (field.Contains("files[n]"))
            {
                record.AppendLine("\t// TODO: Implement files[n] bullshit (https://discord.com/developers/docs/reference#uploading-files)");
                continue;
            }

            string prettyField = ConvertField(field);
            string cleanField = CleanField(field);
            string convertedType = ConvertType(type);
            bool optional = field.EndsWith('?');
            bool nullable = type.StartsWith('?');

            string requirement = (optional, nullable) switch
            {
                (false, false) => ", Required = Required.Always",
                (false, true) => ", Required = Required.AllowNull",
                (true, false) => ", Required = Required.DisallowNull",
                (true, true) => "",
            };

            // Don't add comma if last property
            string comma = i != propertyValues.Count - 1 ? "," : "";

            record.AppendLine(
                $"\t[property: JsonProperty(\"{cleanField}\"{requirement})]\n\t{convertedType} {prettyField}{comma}");
        }

        record.AppendLine( ");");
        
        return record.ToString();
    }

    private static string ConvertType(string type)
    {
        type = Regex.Replace(type, @"\[(.*)\]\(.*\)", "$1");
        type = Regex.Replace(type, @"\(.*\)", "");
        type = type.Replace("*", "");
        type = type.Replace(@"\", "");
        type = type.Trim();
        
        bool nullable = type.StartsWith("?");
        type = type.Replace("?", "");
        
        type = type switch
        {
            "string" => "string",
            "boolean" => "bool",
            "integer" => "int",
            "float" => "float",
            "object" => "dynamic",
            "snowflake" => "ulong",
            "mixed" => "dynamic",
            "ISO8601 timestamp" => "string", // TODO: Not sure, check
            "array" => "dynamic", // TODO: Maybe set type manually depending on field name or record name
            "dict" => "dynamic", // TODO: Maybe set type manually depending on field name or record name
             _ => new Func<string>(() =>
            {
                Console.WriteLine($@"Type ""{type}"" was not matched, using `dynamic`");
                return $"/*{type}*/ dynamic";
            })(),
        };

        type += nullable ? "?" : "";

        return type.Trim();
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
        
        field = field.Replace("$", "");
        field = field.Split('_')
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
        { "oauth2", "OAuth2 "},
        { "rpc", "RPC" },
    };

    private static string PrettifyDescription(string description)
    {
        description = description[0].ToString().ToUpper() + description[1..];

        if (!description.EndsWith('.')) description += '.';

        // Replace strings in the description to make it prettier
        description = DescriptionReplacement.Aggregate(description, (curr, map) => 
                                                           curr.Replace(map.Key, map.Value));

        // Remove markdown hyperlinks
        // TODO: These can probably become actual hyperlinks in the XML documentation
        description = Regex.Replace(description, @"\[(.*?)\]\(.*?\)", "$1");
        
        return description;
    }
}