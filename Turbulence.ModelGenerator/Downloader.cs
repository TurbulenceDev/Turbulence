using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Turbulence.ModelGenerator;

public static class Downloader
{
    /// <summary>
    /// Download files from a List.
    /// </summary>
    public static async Task DownloadFiles(Uri root, List<string> files, Uri outPath)
    {
        // If downloads directory already exists, give the option to delete it or stop running
        if (Directory.Exists(outPath.AbsolutePath))
        {
            Console.WriteLine($"{outPath.AbsolutePath} already exists. Delete? (y/N)");

            if (Console.ReadKey(true).KeyChar is 'y' or 'Y')
            {
                Directory.Delete(outPath.AbsolutePath, true);
            }
            else
            {
                Console.WriteLine("Aborting...");
                Environment.Exit(0);
            }
        }
        
        using var client = new HttpClient();

        foreach (string file in files)
        {
            Uri toDownload = new(root + "/" + file);
            Uri outputFile = new(Path.Combine(outPath.AbsolutePath, file));

            Console.Write($"Downloading file {Path.GetFileName(file)}...");

            // Download the file
            var response = await client.GetAsync(toDownload);

            // Create a directory for the file
            Directory.CreateDirectory(Path.GetDirectoryName(outputFile.AbsolutePath)
                ?? throw new Exception("Can't get directory name. Files or tempPath are most likely malformed."));

            // Write file
            await using var fs = new FileStream(outputFile.AbsolutePath, FileMode.Create);
            await response.Content.CopyToAsync(fs);

            Console.WriteLine(" done");
        }
    }
}