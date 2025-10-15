using FileConductor.Core.Services.ProjectManager;
using Zio;

namespace FileConductor.Core.Services.FileManager;

public interface IFileManager : IFileSystem
{
    void Mount(IMountedProject mountedProject);
    Task<bool> WaitForUnmountAsync();
    void Unmount();
}