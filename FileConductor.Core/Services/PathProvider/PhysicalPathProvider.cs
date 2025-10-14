using System;
using Zio;

namespace FileConductor.Services.PathProvider;

public class PhysicalPathProvider : IPathProvider
{
    public UPath GetAppDataPath()
    {
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return (UPath)appData;
    }
}