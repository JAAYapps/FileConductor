using FileConductor.Core.Models;

namespace FileConductor.Core.Services.ProjectManager;

/// <summary>
/// Represents a single, actively mounted project that you can perform file operations on.
/// </summary>
public interface IMountedProject : IDisposable
{
    public Project Project { get; }
    public string MountPoint { get; }
        
    public Task AddFileAsync(string virtualPath, string sourcePhysicalPath);
    public Task RemoveFileAsync(string virtualPath);
    public bool FileExists(string virtualPath);
        
    public Task CreateDirectoryAsync(string virtualPath);
    public Task DeleteDirectoryAsync(string virtualPath, bool recursive);
    public bool DirectoryExists(string virtualPath);
}