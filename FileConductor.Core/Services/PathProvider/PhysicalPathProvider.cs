using Zio;

namespace FileConductor.Core.Services.PathProvider;

public class PhysicalPathProvider : IPathProvider
{
    public UPath GetAppDataPath()
    {
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        if (!Directory.Exists(Path.Combine(appData, "FileConductor")))
            Directory.CreateDirectory(Path.Combine(appData, "FileConductor"));
        return (UPath)Path.Combine(appData, "FileConductor");
    }
}