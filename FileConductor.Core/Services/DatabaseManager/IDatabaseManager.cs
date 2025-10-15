using LiteDB;

namespace FileConductor.Core.Services.DatabaseManager;

/// <summary>
/// Manages the SQLite database that acts as the manifest for the virtual file system.
/// </summary>
public interface IDatabaseManager : IDisposable
{
    /// <summary>
    /// Initializes the database connection and creates the necessary tables if they don't exist.
    /// </summary>
    Task InitializeDatabaseAsync();

    // --- File Operations ---

    /// <summary>
    /// Adds a new file entry to the database and updates the reference count for its content hash.
    /// </summary>
    Task AddFileAsync(VirtualFile file);

    /// <summary>
    /// Retrieves a file entry from the database by its virtual path.
    /// </summary>
    Task<VirtualFile?> GetFileAsync(string virtualPath);

    /// <summary>
    /// Removes a file entry from the database and updates the reference count for its content hash.
    /// </summary>
    Task RemoveFileAsync(string virtualPath);

    // --- Directory Operations ---

    /// <summary>
    /// Adds a new directory entry to the database.
    /// </summary>
    Task AddDirectoryAsync(string virtualPath);

    /// <summary>
    /// Removes a directory entry from the database.
    /// </summary>
    Task RemoveDirectoryAsync(string virtualPath);

    /// <summary>
    /// Checks if a directory exists at the given virtual path.
    /// </summary>
    Task<bool> DirectoryExistsAsync(string virtualPath);

    // --- Query and Maintenance Operations ---

    /// <summary>
    /// Lists all files and subdirectories directly within a given virtual directory path.
    /// </summary>
    Task<List<FileSystemItem>> ListDirectoryContentsAsync(string virtualPath);

    /// <summary>
    /// Finds all content hashes that are no longer referenced by any file in the database.
    /// This is used for garbage collection to free up space in the VHDX.
    /// </summary>
    Task<List<string>> GetUnreferencedHashesAsync();
}

public class VirtualFile
{
    [BsonId]
    public string VirtualPath { get; set; } = string.Empty;
    public string ContentHash { get; set; } = string.Empty;
    public int PartNumber { get; set; } = 1;
}

public class VirtualDirectory
{
    [BsonId]
    public string Path { get; set; } = string.Empty;
}

public class HashInfo
{
    [BsonId]
    public string ContentHash { get; set; } = string.Empty;
    public int ReferenceCount { get; set; }
}

// This record is used by the interface, so it's included for completeness.
public record FileSystemItem(string Name, bool IsDirectory);