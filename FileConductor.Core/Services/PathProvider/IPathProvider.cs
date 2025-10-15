using Zio;

namespace FileConductor.Core.Services.PathProvider;

public interface IPathProvider
{
    UPath GetAppDataPath();
}