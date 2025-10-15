namespace FileConductor.Core.Services.ProjectManager;

public class JsonProjectManager : IProjectManager
{
    public Task CreateProjectAsync(string projectName)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProjectAsync(string projectName)
    {
        throw new NotImplementedException();
    }

    public List<string> ListProjects()
    {
        throw new NotImplementedException();
    }

    public bool ProjectExists(string projectName)
    {
        throw new NotImplementedException();
    }

    public Task<IMountedProject> MountProjectAsync(string projectName, string mountPoint)
    {
        throw new NotImplementedException();
    }

    public Task UnmountProjectAsync(string projectName)
    {
        throw new NotImplementedException();
    }
}