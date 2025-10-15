namespace FileConductor.Core.Services.ProjectManager;

/// <summary>
/// Manages the collection of all projects.
/// </summary>
public interface IProjectManager
{
    Task CreateProjectAsync(string projectName);
    Task DeleteProjectAsync(string projectName);
    List<string> ListProjects();
    bool ProjectExists(string projectName);

    /// <summary>
    /// Mounts a project and returns an object to interact with it.
    /// </summary>
    Task<IMountedProject> MountProjectAsync(string projectName, string mountPoint);
    
    Task UnmountProjectAsync(string projectName);
}