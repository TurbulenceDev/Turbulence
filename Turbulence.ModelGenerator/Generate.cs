using static Turbulence.ModelGenerator.Downloader;
using static Turbulence.ModelGenerator.Preprocessing;
using static Turbulence.ModelGenerator.Converter;

namespace Turbulence.ModelGenerator;

public static class Generate
{
    public static async Task Main(string[] args)
    {
        Uri downloadPath = new(Path.Combine(Config.TempPath.LocalPath, "Download"));
        Uri tablesPath = new(Path.Combine(Config.TempPath.LocalPath, "Tables"));

        await DownloadFiles(Config.DocsRoot, Config.MdFiles, downloadPath);
        PreExtract(downloadPath);
        await ExtractTables(downloadPath, tablesPath);
        Directory.Delete(downloadPath.LocalPath, true);

        await Convert(tablesPath, Config.OutPath);
        PostConvert(Config.OutPath);
    }
}
