using Zio;

namespace FileConductor.Services.PathProvider;

public class MemoryPathProvider : IPathProvider
{
    public UPath GetAppDataPath()
    {
        return (UPath)"/user/appdata";
    }
}