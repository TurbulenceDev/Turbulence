using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Turbulence.ModelGenerator;

public static class Preprocessing
{
    /// <summary>
    /// Extract relevant tables from .md files.
    /// </summary>
    public static async Task ExtractTables(Uri inPath, Uri outPath)
    {
        // List to keep track of written files, so we can make sure there are no duplicates
        List<string> writtenFiles = new();

        foreach (string file in Directory.EnumerateFiles(inPath.AbsolutePath, "*.*", SearchOption.AllDirectories))
        {
            using StreamReader reader = new(file);
            
            // Keep track of the last ## header found, because it will be used to name JSON Params, JSON/Form and Query String Params tables
            string? lastH2 = null;

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (line.StartsWith("## "))
                {
                    lastH2 = line;
                }
                
                // Based on the assumption that every table has H6 above it, which it should according to the README
                // Sometimes violated however
                if (!line.StartsWith("###### ")) continue;

                string? name;
                
                const string paramsPattern = /* language=RegEx */ @"###### (.*?)(Query String Params|JSON Params|JSON/Form Params|Response Body)$";
                var match = Regex.Match(line, paramsPattern);
                if (match.Success && lastH2 != null)
                {
                    // Use the last header title instead of [...] Params
                    name = Regex.Match(lastH2, @"## (.*) %.*").Groups[1].Value;

                    if (line.Contains("Query String Params"))
                    {
                        name += "QueryParams";
                    }
                    else if (line.Contains("Response Body"))
                    {
                        name += "Response";
                    }
                    else
                    {
                        name += "Params";
                    }
                }
                else
                {
                    // Markdown heading to pretty class name
                    /* language=RegEx */
                    const string pattern = @"###### (.*?)( Structure| Fields)?$";
                    name = Regex.Match(line, pattern).Groups[1].Value;
                }
                
                name = name.Replace(" ", "")
                           .Replace(Path.DirectorySeparatorChar.ToString(), "")
                           .Replace("-", "")
                           .Replace("(", "")
                           .Replace(")", "");

                TableSource? table = await ExtractTable(reader, file);

                // Not a valid table or not a table we are interested in, continue
                if (table == null) continue;

                // We use the name of the file for the directory name to store all the tables to
                // We assume there are no duplicates
                string dir = Path.ChangeExtension(file, null)
                                  .Split(Path.DirectorySeparatorChar)[^1]
                                  .Split('_')
                                  .Select(x => char.ToUpper(x[0]) + x[1..])
                                  .Aggregate("", (s1, s2) => s1 + s2);

                // Suffix namespaces with Discord so records and namespaces won't have the same name
                dir = $"Discord{dir}";
                
                dir = Path.Combine(outPath.AbsolutePath, dir);

                string filename = Path.Combine(dir, name);

                // Create directory recursively
                Directory.CreateDirectory(dir);

                // Throw error if we have already written to this file
                if (writtenFiles.Contains(filename))
                {
                    throw new Exception($"Tried to write duplicate file {filename}.");
                }

                writtenFiles.Add(filename);

                await using var xml = XmlWriter.Create(filename, new XmlWriterSettings
                {
                    Indent = true,
                    Async = true,
                    OmitXmlDeclaration = true,
                });

                var serializer = new DataContractSerializer(typeof(TableSource));
                serializer.WriteObject(xml, table);
            }
        }
        
        // Post-processing
        try
        {
            // ListActiveThreadsResponse is part of of two namespaces, delete the less-complete one
            File.Delete($"{outPath.AbsolutePath}/DiscordGuild/ListActiveThreadsResponse");

            // For the second one, make field `has_more` optional as it is missing from the Guild version
            var path = $"{outPath.AbsolutePath}/DiscordChannel/ListActiveThreadsResponse";
            string old = await File.ReadAllTextAsync(path);
            await File.WriteAllTextAsync(path, old.Replace("has_more", "has_more?"));
        }
        catch (Exception)
        {
            Console.WriteLine("Discord API docs have changed causing this code to now be invalid, manual investigation required");
            throw;
        }
    }

    private static async Task<TableSource?> ExtractTable(TextReader reader, string path)
    {
        StringBuilder table = new();
        string? line;
        
        // Pattern that every table we are interested in starts with /* language=RegEx */
        const string tablePattern = @"^\s*\|\s*([Ff]ield)\s*\|\s*[Tt]ype\s*\|\s*[Dd]escription\s*\|.*$";
        
        // Get rid of any non-table lines
        do
        {
            if (reader.Peek() > -1 && (char) reader.Peek() == '#')
            {
                // Did not find a table, return
                return null;
            }
            
            line = await reader.ReadLineAsync();
        } while (line != null && !Regex.Match(line, tablePattern).Success);

        // Read table
        table.AppendLine(line);
        while ((line = await reader.ReadLineAsync()) != null && line.StartsWith('|'))
        {
            table.AppendLine(line);
        }
        
        // Extract URLs
        string urlFolder = path.Split(Path.DirectorySeparatorChar)[^2];
        string urlFile = Path.GetFileNameWithoutExtension(path)
                             .ToLower()
                             .Replace("_", "-");
        var discordUrl = $"https://discord.com/developers/docs/{urlFolder}/{urlFile}";
        var githubUrl =
            $"https://github.com/discord/discord-api-docs/blob/main/docs/{urlFolder}/{Path.GetFileName(path)}";

        return new TableSource(discordUrl, githubUrl, table.ToString());
    }

    // Fix inconsistencies in downloaded files
    public static void PreExtract(Uri path)
    {
        Console.WriteLine("Running pre-extract phase...");
        
        var inconsistentHeaderCount = false;
        var nameFieldCount = false;
        
        foreach (string file in Directory.EnumerateFiles(path.AbsolutePath, "*.*", SearchOption.AllDirectories))
        {
            string text = File.ReadAllText(file);

            // Fix inconsistent header
            if (text.Contains(@"#### Client Status Object"))
            {
                inconsistentHeaderCount = true;
                text = text.Replace(@"#### Client Status Object", @"###### Client Status Object");
            }
            
            // Replace "Name" with "Field" wherever "Name" is used
            if (Regex.Match(text, @"(|\s*)[Nn]ame(\s*\|\s*[Tt]ype\s*\|\s*[Dd]escription\s*\|)").Success)
            {
                nameFieldCount = true;
                text = Regex.Replace(text, @"(|\s*)[Nn]ame(\s*\|\s*[Tt]ype\s*\|\s*[Dd]escription\s*\|)", "$1Field$2");
            }

            File.WriteAllText(file, text);
        }

        if (!inconsistentHeaderCount)
        {
            Console.WriteLine("'Fix inconsistent header' fix in pre-extract phase has become obsolete.");
        }
        
        if (!nameFieldCount)
        {
            Console.WriteLine(@"""Replace 'Name' with 'Field'"" fix in pre-extract phase has become obsolete.");
        }
        
        Console.WriteLine("Pre-extract phase complete.");
    }
}
