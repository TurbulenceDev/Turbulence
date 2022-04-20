using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

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
                if (!line.StartsWith("###### ")) continue;

                string? name;
                
                /* language=RegEx */
                const string paramsPattern = @"###### (.*?)(Query String Params|JSON Params|JSON/Form Params|Response Body)$";
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
                           .Replace("/", "")
                           .Replace("-", "");

                string? table = await ExtractTable(reader);

                // Not a valid table or not a table we are interested in, continue
                if (table == null) continue;

                // We use the name of the file for the directory name to store all the tables to
                // We assume there are no duplicates
                string dir = Regex.Replace(file, ".md$", "")
                                  .Split(Path.DirectorySeparatorChar)[^1]
                                  .Split('_')
                                  .Select(x => char.ToUpper(x[0]) + x[1..])
                                  .Aggregate("", (s1, s2) => s1 + s2);
                
                dir = Path.Combine(outPath.AbsolutePath, dir);

                string filename = Path.Combine(dir, name) + ".md";
                
                // Create directory recursively
                Directory.CreateDirectory(dir);

                // Throw error if we have already written to this file
                if (writtenFiles.Contains(filename))
                {
                    throw new Exception($"Tried to write duplicate file {filename}.");
                }

                writtenFiles.Add(filename);

                // Write table to file
                await File.WriteAllTextAsync(filename , table);
            }
        }
    }

    private static async Task<string?> ExtractTable(TextReader reader)
    {
        StringBuilder table = new();
        string? line;
        
        // Get rid of any whitespace lines
        do
        {
            line = await reader.ReadLineAsync();
        } while (line != null && Regex.Match(line, @"^\s*$").Success);

        // End of file (trolled)
        if (line == null) return null;

        // Pattern that every table we are interested in starts with
        /* language=RegEx */
        const string tablePattern = @"^\s*\|\s*[Ff]ield\s*\|\s*[Tt]ype\s*\|\s*[Dd]escription\s*\|.*$";
        
        // Not an interesting table
        if (!Regex.Match(line, tablePattern).Success) return null;

        // Read table
        table.AppendLine(line);
        while ((line = await reader.ReadLineAsync()) != null && line.StartsWith('|'))
        {
            table.AppendLine(line);
        }

        return table.ToString();
    }
}