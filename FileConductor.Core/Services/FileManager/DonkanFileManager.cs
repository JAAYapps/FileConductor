using System.Runtime.InteropServices;
using System.Security.AccessControl;
using DokanNet;
using DokanNet.Logging;
using FileConductor.Core.Services.ProjectManager;
using LTRData.Extensions.Native.Memory;
using Zio;
using FileAccess = DokanNet.FileAccess;

namespace FileConductor.Core.Services.FileManager;

public class DonkanFileManager : IFileManager, IDokanOperations2
{
    private readonly ConsoleLogger _logger = new("[Dokan]");
    private IMountedProject? _mountedProject;
    private Dokan? _dokan;
    private DokanInstance? _dokanInstance;
    
    #region Dokan Operations

    public NtStatus CreateFile(ReadOnlyNativeMemory<char> fileNamePtr, FileAccess access, FileShare share, FileMode mode,
        FileOptions options, FileAttributes attributes, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public void Cleanup(ReadOnlyNativeMemory<char> fileNamePtr, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public void CloseFile(ReadOnlyNativeMemory<char> fileNamePtr, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus ReadFile(ReadOnlyNativeMemory<char> fileNamePtr, NativeMemory<byte> buffer, out int bytesRead, long offset,
        ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus WriteFile(ReadOnlyNativeMemory<char> fileNamePtr, ReadOnlyNativeMemory<byte> buffer, out int bytesWritten, long offset,
        ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus FlushFileBuffers(ReadOnlyNativeMemory<char> fileNamePtr, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus GetFileInformation(ReadOnlyNativeMemory<char> fileNamePtr, out ByHandleFileInformation fileInfo,
        ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus FindFiles(ReadOnlyNativeMemory<char> fileNamePtr, out IEnumerable<FindFileInformation> files, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus FindFilesWithPattern(ReadOnlyNativeMemory<char> fileNamePtr, ReadOnlyNativeMemory<char> searchPatternPtr,
        out IEnumerable<FindFileInformation> files, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus SetFileAttributes(ReadOnlyNativeMemory<char> fileNamePtr, FileAttributes attributes, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus SetFileTime(ReadOnlyNativeMemory<char> fileNamePtr, DateTime? creationTime, DateTime? lastAccessTime,
        DateTime? lastWriteTime, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus DeleteFile(ReadOnlyNativeMemory<char> fileNamePtr, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus DeleteDirectory(ReadOnlyNativeMemory<char> fileNamePtr, ref DokanFileInfo info)
    {
        try
        {
            var path = (UPath)fileNamePtr.Span.ToString();
            
            this.DeleteDirectory(path, false);
        
            return DokanResult.Success;
        }
        catch (IOException ex)
        {
            return DokanResult.DirectoryNotEmpty;
        }
        catch (Exception ex)
        {
            return DokanResult.Error;
        }
    }

    public NtStatus MoveFile(ReadOnlyNativeMemory<char> oldNamePtr, ReadOnlyNativeMemory<char> newNamePtr, bool replace,
        ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus SetEndOfFile(ReadOnlyNativeMemory<char> fileNamePtr, long length, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus SetAllocationSize(ReadOnlyNativeMemory<char> fileNamePtr, long length, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus LockFile(ReadOnlyNativeMemory<char> fileNamePtr, long offset, long length, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus UnlockFile(ReadOnlyNativeMemory<char> fileNamePtr, long offset, long length, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus GetDiskFreeSpace(out long freeBytesAvailable, out long totalNumberOfBytes, out long totalNumberOfFreeBytes,
        ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus GetVolumeInformation(NativeMemory<char> volumeLabel, out FileSystemFeatures features, NativeMemory<char> fileSystemName,
        out uint maximumComponentLength, ref uint volumeSerialNumber, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus GetFileSecurity(ReadOnlyNativeMemory<char> fileNamePtr, out FileSystemSecurity? security,
        AccessControlSections sections, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus SetFileSecurity(ReadOnlyNativeMemory<char> fileNamePtr, FileSystemSecurity security, AccessControlSections sections,
        ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus Mounted(ReadOnlyNativeMemory<char> mountPoint, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus Unmounted(ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    public NtStatus FindStreams(ReadOnlyNativeMemory<char> fileNamePtr, out IEnumerable<FindFileInformation> streams, ref DokanFileInfo info)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Zio Operations

    public int DirectoryListingTimeoutResetIntervalMs { get; }
    
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void CreateDirectory(UPath path)
    {
        throw new NotImplementedException();
    }

    public bool DirectoryExists(UPath path)
    {
        throw new NotImplementedException();
    }

    public void MoveDirectory(UPath srcPath, UPath destPath)
    {
        throw new NotImplementedException();
    }

    public void DeleteDirectory(UPath path, bool isRecursive)
    {
        Console.WriteLine($"ZIO: Deleting directory at {path}. Please implement code from the projects Service");
    }

    public void CopyFile(UPath srcPath, UPath destPath, bool overwrite)
    {
        throw new NotImplementedException();
    }

    public void ReplaceFile(UPath srcPath, UPath destPath, UPath destBackupPath, bool ignoreMetadataErrors)
    {
        throw new NotImplementedException();
    }

    public long GetFileLength(UPath path)
    {
        throw new NotImplementedException();
    }

    public bool FileExists(UPath path)
    {
        throw new NotImplementedException();
    }

    public void MoveFile(UPath srcPath, UPath destPath)
    {
        throw new NotImplementedException();
    }

    public void DeleteFile(UPath path)
    {
        throw new NotImplementedException();
    }

    public Stream OpenFile(UPath path, FileMode mode, System.IO.FileAccess access, FileShare share = FileShare.None)
    {
        throw new NotImplementedException();
    }

    public FileAttributes GetAttributes(UPath path)
    {
        throw new NotImplementedException();
    }

    public void SetAttributes(UPath path, FileAttributes attributes)
    {
        throw new NotImplementedException();
    }

    public DateTime GetCreationTime(UPath path)
    {
        throw new NotImplementedException();
    }

    public void SetCreationTime(UPath path, DateTime time)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastAccessTime(UPath path)
    {
        throw new NotImplementedException();
    }

    public void SetLastAccessTime(UPath path, DateTime time)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastWriteTime(UPath path)
    {
        throw new NotImplementedException();
    }

    public void SetLastWriteTime(UPath path, DateTime time)
    {
        throw new NotImplementedException();
    }

    public void CreateSymbolicLink(UPath path, UPath pathToTarget)
    {
        throw new NotImplementedException();
    }

    public bool TryResolveLinkTarget(UPath linkPath, out UPath resolvedPath)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UPath> EnumeratePaths(UPath path, string searchPattern, SearchOption searchOption, SearchTarget searchTarget)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<FileSystemItem> EnumerateItems(UPath path, SearchOption searchOption, SearchPredicate? searchPredicate = null)
    {
        throw new NotImplementedException();
    }

    public bool CanWatch(UPath path)
    {
        throw new NotImplementedException();
    }

    public IFileSystemWatcher Watch(UPath path)
    {
        throw new NotImplementedException();
    }

    public string ConvertPathToInternal(UPath path)
    {
        throw new NotImplementedException();
    }

    public UPath ConvertPathFromInternal(string systemPath)
    {
        throw new NotImplementedException();
    }

    public (IFileSystem FileSystem, UPath Path) ResolvePath(UPath path)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Mounting
    /// <summary>
    /// Mounts the project. This method starts the filesystem in a background thread and returns immediately.
    /// </summary>
    public void Mount(IMountedProject project)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Only supported on Windows.");
            return;
        }
        
        if (_mountedProject != null)
        {
            throw new InvalidOperationException("A project is already mounted.");
        }

        _mountedProject = project;
        
        _dokan = new Dokan(_logger);
        
        var dokanBuilder = new DokanInstanceBuilder(_dokan)
            .ConfigureLogger(() => _logger)
            .ConfigureOptions(options =>
            {
                options.Options = DokanOptions.DebugMode | DokanOptions.StderrOutput;
                options.MountPoint = project.MountPoint;
            });
        
        _dokanInstance = dokanBuilder.Build(this);
    }

    public async Task<bool> WaitForUnmountAsync()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Only supported on Windows.");
            return false;
        }
        if (_dokanInstance == null)
        {
            return false;
        }
        
        return await _dokanInstance.WaitForFileSystemClosedAsync(uint.MaxValue);
    }
    
    public void Unmount()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Only supported on Windows.");
            return;
        }
        if (_dokan == null || _mountedProject == null)
        {
            return;
        }
        
        _dokan.RemoveMountPoint(_mountedProject.MountPoint);
        
        _dokanInstance?.Dispose();
        _dokan?.Dispose();
        
        _dokanInstance = null;
        _dokan = null;
        _mountedProject = null;
    }
    #endregion
}