namespace Turbulence.ModelGenerator;

public static class Downloader
{
    /// <summary>
    /// Download files from a List.
    /// </summary>
    public static async Task DownloadFiles(Uri root, List<string> files, Uri outPath)
    {
        // If downloads directory already exists, give the option to delete it or stop running
        if (Directory.Exists(outPath.LocalPath))
        {
            Console.WriteLine($"{outPath.LocalPath} already exists. Delete? (y/N)");

            if (Console.ReadKey(true).KeyChar is 'y' or 'Y')
            {
                Directory.Delete(outPath.LocalPath, true);
            }
            else
            {
                Console.WriteLine("Aborting...");
                Environment.Exit(0);
            }
        }
        
        using var client = new HttpClient();

        foreach (var file in files)
        {
            Uri toDownload = new(root + "/" + file);
            Uri outputFile = new(Path.Combine(outPath.LocalPath, file));

            Console.Write($"Downloading file {Path.GetFileName(file)}...");

            // Download the file
            var response = await client.GetAsync(toDownload);

            // Create a directory for the file
            Directory.CreateDirectory(Path.GetDirectoryName(outputFile.LocalPath)
                ?? throw new Exception("Can't get directory name. Files or tempPath are most likely malformed."));

            // Write file
            await using var fs = new FileStream(outputFile.LocalPath, FileMode.Create);
            await response.Content.CopyToAsync(fs);

            Console.WriteLine(" done");
        }
    }
}