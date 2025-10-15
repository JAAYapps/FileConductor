using Zio;

namespace FileConductor.Core.Services.PathProvider;

public class MemoryPathProvider : IPathProvider
{
    public UPath GetAppDataPath()
    {
        return (UPath)"/user/appdata";
    }
}