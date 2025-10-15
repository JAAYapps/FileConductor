using System.Runtime.InteropServices;
using FileConductor.Core.Services.ProjectManager;
using FuseDotNet;
using FuseDotNet.Logging;
using LTRData.Extensions.Native.Memory;
using Zio;

namespace FileConductor.Core.Services.FileManager;

public class FuseFileManager : IFileManager, IFuseOperations
{
    private IMountedProject? _mountedProject;
    private Task? _mountTask;
    private CancellationTokenSource? _cancellationTokenSource;
    
    #region FuseDotNet
    public PosixResult OpenDir(ReadOnlyNativeMemory<byte> fileNamePtr, ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public PosixResult GetAttr(ReadOnlyNativeMemory<byte> fileNamePtr, out FuseFileStat stat, ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public PosixResult Read(ReadOnlyNativeMemory<byte> fileNamePtr, NativeMemory<byte> buffer, long position, out int readLength,
        ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public PosixResult ReadDir(ReadOnlyNativeMemory<byte> fileNamePtr, out IEnumerable<FuseDirEntry> entries, ref FuseFileInfo fileInfo, long offset,
        FuseReadDirFlags flags)
    {
        throw new NotImplementedException();
    }

    public PosixResult Open(ReadOnlyNativeMemory<byte> fileNamePtr, ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public void Init(ref FuseConnInfo fuse_conn_info)
    {
        throw new NotImplementedException();
    }

    public PosixResult Access(ReadOnlyNativeMemory<byte> fileNamePtr, PosixAccessMode mask)
    {
        throw new NotImplementedException();
    }

    public PosixResult StatFs(ReadOnlyNativeMemory<byte> fileNamePtr, out FuseVfsStat statvfs)
    {
        throw new NotImplementedException();
    }

    public PosixResult FSyncDir(ReadOnlyNativeMemory<byte> fileNamePtr, bool datasync, ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public PosixResult ReadLink(ReadOnlyNativeMemory<byte> fileNamePtr, NativeMemory<byte> target)
    {
        throw new NotImplementedException();
    }

    public PosixResult ReleaseDir(ReadOnlyNativeMemory<byte> fileNamePtr, ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public PosixResult Link(ReadOnlyNativeMemory<byte> from, ReadOnlyNativeMemory<byte> to)
    {
        throw new NotImplementedException();
    }

    public PosixResult MkDir(ReadOnlyNativeMemory<byte> fileNamePtr, PosixFileMode mode)
    {
        throw new NotImplementedException();
    }

    public PosixResult Release(ReadOnlyNativeMemory<byte> fileNamePtr, ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public PosixResult RmDir(ReadOnlyNativeMemory<byte> fileNamePtr)
    {
        throw new NotImplementedException();
    }

    public PosixResult FSync(ReadOnlyNativeMemory<byte> fileNamePtr, bool datasync, ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public PosixResult Unlink(ReadOnlyNativeMemory<byte> fileNamePtr)
    {
        throw new NotImplementedException();
    }

    public PosixResult Write(ReadOnlyNativeMemory<byte> fileNamePtr, ReadOnlyNativeMemory<byte> buffer, long position, out int writtenLength,
        ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public PosixResult SymLink(ReadOnlyNativeMemory<byte> from, ReadOnlyNativeMemory<byte> to)
    {
        throw new NotImplementedException();
    }

    public PosixResult Flush(ReadOnlyNativeMemory<byte> fileNamePtr, ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public PosixResult Rename(ReadOnlyNativeMemory<byte> from, ReadOnlyNativeMemory<byte> to)
    {
        throw new NotImplementedException();
    }

    public PosixResult Truncate(ReadOnlyNativeMemory<byte> fileNamePtr, long size)
    {
        throw new NotImplementedException();
    }

    public PosixResult UTime(ReadOnlyNativeMemory<byte> fileNamePtr, TimeSpec atime, TimeSpec mtime, ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public PosixResult Create(ReadOnlyNativeMemory<byte> fileNamePtr, int mode, ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }

    public PosixResult IoCtl(ReadOnlyNativeMemory<byte> fileNamePtr, int cmd, IntPtr arg, ref FuseFileInfo fileInfo,
        FuseIoctlFlags flags, IntPtr data)
    {
        throw new NotImplementedException();
    }

    public PosixResult ChMod(NativeMemory<byte> fileNamePtr, PosixFileMode mode)
    {
        throw new NotImplementedException();
    }

    public PosixResult ChOwn(NativeMemory<byte> fileNamePtr, int uid, int gid)
    {
        throw new NotImplementedException();
    }

    public PosixResult FAllocate(NativeMemory<byte> fileNamePtr, FuseAllocateMode mode, long offset, long length,
        ref FuseFileInfo fileInfo)
    {
        throw new NotImplementedException();
    }
    #endregion
    
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
        throw new NotImplementedException();
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

    public Stream OpenFile(UPath path, FileMode mode, FileAccess access, FileShare share = FileShare.None)
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

    /// <summary>
    /// Mounts the project. This is a BLOCKING call that will run until the filesystem is unmounted (e.g., by Ctrl+C).
    /// </summary>
    public void Mount(IMountedProject project)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Console.WriteLine("Only supported on Linux.");
            return;
        }
        _mountedProject = project;
        _cancellationTokenSource = new CancellationTokenSource();
        
        _mountTask = Task.Run(() =>
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.WriteLine("Only supported on Linux.");
                return;
            }
            var fuseArgs = new[] { "FileConductor", project.MountPoint };
            try
            {
                this.Mount(fuseArgs, new ConsoleLogger());
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("FUSE mount cancelled.");
            }
        }, _cancellationTokenSource.Token);
    }

    /// <summary>
    /// Returns a Task that completes when the filesystem is unmounted.
    /// </summary>
    public async Task<bool> WaitForUnmountAsync()
    {
        if (_cancellationTokenSource == null) 
            return false;
        await _mountTask?.WaitAsync(_cancellationTokenSource.Token)!;
        return _cancellationTokenSource.Token.IsCancellationRequested;
    }

    /// <summary>
    /// Programmatically unmounts the filesystem.
    /// </summary>
    public void Unmount()
    {
        _cancellationTokenSource?.Cancel();
    }
    
    public void Dispose()
    {
        // TODO release managed resources here
    }
}