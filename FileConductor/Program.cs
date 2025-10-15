using Avalonia;
using System;
using System.IO;
using System.Runtime.InteropServices;
using FileConductor.Core;
using FileConductor.Core.Services.DatabaseManager;
using FileConductor.Core.Services.FileManager;
using FileConductor.Core.Services.ImageManager;
using FileConductor.Core.Services.PathProvider;
using FileConductor.Core.Services.ProjectManager;
using Zio;
using Zio.FileSystems;

namespace FileConductor;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        DiscUtils.Complete.SetupHelper.SetupComplete();
        var pathProvider = new PhysicalPathProvider();
        var imageManager = new VhdxImageManager(new PhysicalFileSystem(), pathProvider.GetAppDataPath());
        var projectManager = new JsonProjectManager();
        var databaseManager = new LiteDbManager(imageManager);
        IFileManager? fileManager = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            fileManager = new FuseFileManager();
        else if  (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            fileManager = new DonkanFileManager();
        if (fileManager != null)
        {
            var conductor = new Conductor(pathProvider, projectManager, imageManager, databaseManager, fileManager);
        }
        imageManager.ReadFile();
        //BuildAvaloniaApp()
        //.StartWithClassicDesktopLifetime(args);
    } 
    
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    // public static void Main(string[] args)
    // {
    //     var physicalProjectPath = UPath.Root / "home/joshua/FileConductor/FileConductor/bin/Debug/net8.0";
    //     var zipArchivePath = UPath.Root / "home/joshua/FileConductor/FileConductor/bin/Debug/net8.0/MyModProject.zip";
    //     
    //     var physicalFileSystem = new PhysicalFileSystem();
    //     var zipFileSystem = new ZipArchiveFileSystem(zipArchivePath.GetName());
    //     
    //     PrintProjectAssets(physicalFileSystem, physicalProjectPath / "MyModProject");
    //     
    //     PrintProjectAssets(zipFileSystem,  UPath.Root / "MyModProject");
    // }
    //
    // static void PrintProjectAssets(IFileSystem fs, UPath projectPath)
    // {
    //     Console.WriteLine($"--- Reading from filesystem: {fs.GetType().Name} ---");
    //     Console.WriteLine("Project path: " + projectPath);
    //     if (!fs.DirectoryExists(projectPath) && !fs.FileExists(projectPath))
    //     {
    //         Console.WriteLine("Project exist with normal IO.Path in Dotnet?: " + Path.Exists(projectPath.GetName() + " Path: " + projectPath));
    //         Console.WriteLine("Project path does not exist.");
    //         return;
    //     }
    //
    //     // Enumerate files in the virtual project directory
    //     foreach (var fileEntry in fs.EnumerateFileEntries(projectPath))
    //     {
    //         Console.WriteLine($"Found file: {fileEntry.Name}");
    //         Console.WriteLine($"  Content: '{fs.ReadAllText(fileEntry.Path)}'");
    //     }
    //     Console.WriteLine();
    // }


}