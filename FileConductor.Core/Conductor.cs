using DiscUtils;
using DiscUtils.Fat;
using DiscUtils.Partitions;
using DiscUtils.Streams;
using DiscUtils.Vhdx;
using FileConductor.Core.Services.DatabaseManager;
using FileConductor.Core.Services.FileManager;
using FileConductor.Core.Services.ImageManager;
using FileConductor.Core.Services.PathProvider;
using FileConductor.Core.Services.ProjectManager;

namespace FileConductor.Core;

public class Conductor(
    IPathProvider pathProvider,
    IProjectManager projectManager,
    IImageManager imageManager,
    IDatabaseManager databaseManager,
    IFileManager fileManager)
{
    private readonly IPathProvider _pathProvider = pathProvider;

    /// <summary>
    /// Performs initial setup, creating the VHDX and database if they don't exist.
    /// Should be called once at application startup.
    /// </summary>
    public async Task InitializeAsync()
    {
        if (!imageManager.ImageExists)
            imageManager.CreateAssetImage(0);
        await databaseManager.InitializeDatabaseAsync();
    }

    /// <summary>
    /// Creates a new project manifest.
    /// </summary>
    public async Task CreateProjectAsync(string projectName)
    {
        if (projectManager.ProjectExists(projectName))
        {
            Console.WriteLine($"Error: Project '{projectName}' already exists.");
            return;
        }
        await projectManager.CreateProjectAsync(projectName);
        Console.WriteLine($"Project '{projectName}' created successfully.");
    }

    /// <summary>
    /// Mounts a project, making it accessible as a virtual folder.
    /// </summary>
    public async Task MountProjectAsync(string projectName, string mountPoint)
    {
        if (!projectManager.ProjectExists(projectName))
        {
            Console.WriteLine($"Error: Project '{projectName}' not found.");
            return;
        }
        
        var mountedProject = await projectManager.MountProjectAsync(projectName, mountPoint);

        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true; // Prevent the app from terminating immediately
            Console.WriteLine("Ctrl+C detected. Unmounting...");
            fileManager.Unmount();
        };
        fileManager.Mount(mountedProject);
        
        Console.WriteLine($"Project '{projectName}' mounted at '{mountPoint}'. Press Ctrl+C to unmount.");
        
        await fileManager.WaitForUnmountAsync(); 
        
        Console.WriteLine("Unmount complete. Application will now exit.");
    }
}