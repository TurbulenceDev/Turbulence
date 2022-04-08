namespace Turbulence.ModelGenerator;

public static class Downloader
{
    /// <summary>
    /// Download files from a List.
    /// </summary>
    public static async Task DownloadFiles(Uri root, List<string> files, Uri outPath)
    {
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
                ?? throw new Exception("Can't get directory name, files or tempPath is most likely malformed"));

            // Write file
            await using var fs = new FileStream(outputFile.AbsolutePath, FileMode.Create);
            await response.Content.CopyToAsync(fs);

            Console.WriteLine(" done");
        }
    }
}