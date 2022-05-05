using static Turbulence.ModelGenerator.Downloader;
using static Turbulence.ModelGenerator.Preprocessing;
using static Turbulence.ModelGenerator.Converter;

namespace Turbulence.ModelGenerator;

public static class Generate
{
    public static async Task Main(string[] args)
    {
        Uri downloadPath = new(Path.Combine(Config.TempPath.AbsolutePath, "Download"));
        Uri tablesPath = new(Path.Combine(Config.TempPath.AbsolutePath, "Tables"));

        await DownloadFiles(Config.DocsRoot, Config.MdFiles, downloadPath);
        await ExtractTables(downloadPath, tablesPath);
        Directory.Delete(downloadPath.AbsolutePath, true);

        await Convert(tablesPath, Config.OutPath);
    }
}
