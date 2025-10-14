using Zio;

namespace FileConductor.Services.PathProvider;

public interface IPathProvider
{
    UPath GetAppDataPath();
}