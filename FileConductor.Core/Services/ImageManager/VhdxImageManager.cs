using DiscUtils;
using DiscUtils.Partitions;
using DiscUtils.Streams;
using DiscUtils.Core;
using DiscUtils.Fat;
using DiscUtils.Raw;
using Zio;
using IFileSystem = Zio.IFileSystem;

namespace FileConductor.Core.Services.ImageManager;

public class VhdxImageManager(IFileSystem fs, UPath path) : IImageManager
{
    private readonly UPath _vhdxPath = path / "data.vhdx";

    public bool ImageExists => fs.FileExists(_vhdxPath);

    public void CreateAssetImage(int span)
    {
        long diskSize = 500L * 1024 * 1024 * 1024; //500GB
        // --- Create the VHDX File ---
        Console.WriteLine($"Creating: {_vhdxPath}");
        using (Stream vhdxStream = fs.CreateFile(span > 1 ? path / $"data{span}.vhdx" : _vhdxPath))
        {
            using (var disk = DiscUtils.Vhdx.Disk.InitializeDynamic(vhdxStream, Ownership.None, diskSize))
            {
                GuidPartitionTable.Initialize(disk, WellKnownPartitionType.WindowsFat);
                using (var fat = FatFileSystem.FormatPartition(disk, 0, "ProjectAssets"))
                {
                    fat.CreateDirectory(@"Projects");
                    fat.CreateDirectory(@"Assets");
                    Console.WriteLine("VHDX created and formatted successfully.");
                }
            }
        }
    }

    public bool ExtractFile(string name, UPath destination)
    {
        using (Stream openDiskStream = fs.OpenFile(_vhdxPath, FileMode.Open, FileAccess.Read))
        using (var disk = new DiscUtils.Vhdx.Disk(openDiskStream, Ownership.None))
        {
            var volMgr = new VolumeManager(disk);
            var logicalVolumes = volMgr.GetLogicalVolumes();

            if (logicalVolumes.Length > 0)
            {
                using (Stream partitionStream = logicalVolumes[0].Open())
                using (var fatFs = new FatFileSystem(partitionStream))
                {
                    if (!fatFs.FileExists(name))
                    {
                        Console.WriteLine($"Error: File '{name}' not found inside the VHDX.");
                        return false; 
                    }
                    
                    using (Stream sourceStream = fatFs.OpenFile(name, FileMode.Open, FileAccess.Read))
                    using (Stream destinationStream = fs.CreateFile(destination))
                    {
                        sourceStream.CopyTo(destinationStream);
                    }
                }
            }
            else
            {
                Console.WriteLine("No logical volumes found in the VHDX.");
                return false;
            }
        }
        return true;
    }

    public bool ReplaceFile(string name, UPath source)
    {
        using (Stream openDiskStream = fs.OpenFile(_vhdxPath, FileMode.Open, FileAccess.ReadWrite))
        using (var disk = new DiscUtils.Vhdx.Disk(openDiskStream, Ownership.None))
        {
            var volMgr = new VolumeManager(disk);
            var logicalVolumes = volMgr.GetLogicalVolumes();

            if (logicalVolumes.Length > 0)
            {
                using (Stream partitionStream = logicalVolumes[0].Open())
                using (var fatFs = new FatFileSystem(partitionStream))
                {
                    var directoryPath = Path.GetDirectoryName(name);
                    if (!string.IsNullOrEmpty(directoryPath) && !fatFs.DirectoryExists(directoryPath))
                        fatFs.CreateDirectory(directoryPath);
                    using (Stream destinationStream = fatFs.OpenFile(name, FileMode.Create, FileAccess.Write))
                    using (Stream sourceStream = fs.OpenFile(source, FileMode.Open, FileAccess.Read))
                    {
                        sourceStream.CopyTo(destinationStream);
                    }
                }
            }
            else
            {
                Console.WriteLine("No logical volumes found in the VHDX.");
                return false;
            }
        }
        return true;
    }
    
    public void ReadFile()
    {
        using (Stream openDiskStream = fs.OpenFile(_vhdxPath, FileMode.Open, FileAccess.ReadWrite))
        {
            using (var disk = new DiscUtils.Vhdx.Disk(openDiskStream, Ownership.None))
            {
                var volMgr = new VolumeManager(disk);
                var logicalVolumes = volMgr.GetLogicalVolumes();
                if (logicalVolumes.Length > 0)
                {
                    using (Stream partitionStream = logicalVolumes[0].Open())
                    {
                        using (var fatFs = new FatFileSystem(partitionStream))
                        {
                            Console.WriteLine("Directories found in the root:");
                            foreach (var dir in fatFs.GetDirectories(@""))
                            {
                                Console.WriteLine(dir);
                            }
                            
                            Console.WriteLine("Files found in the root:");
                            foreach (var file in fatFs.GetFiles(@""))
                            {
                                Console.WriteLine($"{file} Size: {file.Length}" );
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No logical volumes found in the VHDX.");
                }
            }
        }
    }
}