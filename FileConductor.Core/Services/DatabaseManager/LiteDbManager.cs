using FileConductor.Core.Services.ImageManager;
using LiteDB;

namespace FileConductor.Core.Services.DatabaseManager;

/// <summary>
/// An implementation of IDatabaseManager using the embedded NoSQL database LiteDB.
/// </summary>
public class LiteDbManager : IDatabaseManager
{
    private readonly IImageManager _imageManager; // Your service for accessing the VHDX
    private readonly string _tempDbPath;
    private readonly LiteDatabase _db;

    public LiteDbManager(IImageManager imageManager)
    {
        _imageManager = imageManager;
        _tempDbPath = Path.Combine(Path.GetTempPath(), $"fc_{Guid.NewGuid()}.db");
        var dbExist = _imageManager.ExtractFile("index.db", _tempDbPath);
        if (!dbExist)
            Console.WriteLine("Creating index.db");
        _db = new LiteDatabase(_tempDbPath);
        if (!dbExist)
        {
            _db.Dispose();
            _imageManager.ReplaceFile("index.db", _tempDbPath);
            _db = new LiteDatabase(_tempDbPath);
        }
    }
    
    public Task InitializeDatabaseAsync()
    {
        var files = _db.GetCollection<VirtualFile>("files");
        var directories = _db.GetCollection<VirtualDirectory>("directories");
        var hashes = _db.GetCollection<HashInfo>("hashes");
        
        files.EnsureIndex(x => x.VirtualPath);
        directories.EnsureIndex(x => x.Path);
        
        if (!directories.Exists(d => d.Path == "/"))
        {
            directories.Insert(new VirtualDirectory { Path = "/" });
        }
        
        return Task.CompletedTask;
    }

    public Task AddFileAsync(VirtualFile file)
    {
        var files = _db.GetCollection<VirtualFile>("files");
        var hashes = _db.GetCollection<HashInfo>("hashes");
        
        _db.BeginTrans();
        try
        {
            var existingFile = files.FindById(file.VirtualPath);
            if (existingFile != null)
            {
                var oldHashInfo = hashes.FindById(existingFile.ContentHash);
                if (oldHashInfo != null)
                {
                    oldHashInfo.ReferenceCount--;
                    hashes.Update(oldHashInfo);
                }
            }
            
            files.Upsert(file.VirtualPath, file);
            
            var newHashInfo = hashes.FindById(file.ContentHash);
            if (newHashInfo != null)
            {
                newHashInfo.ReferenceCount++;
                hashes.Update(newHashInfo);
            }
            else
            {
                hashes.Insert(new HashInfo { ContentHash = file.ContentHash, ReferenceCount = 1 });
            }
            
            _db.Commit();
        }
        catch
        {
            _db.Rollback();
            throw;
        }

        return Task.CompletedTask;
    }

    public Task<VirtualFile?> GetFileAsync(string virtualPath)
    {
        var files = _db.GetCollection<VirtualFile>("files");
        var file = files.FindById(virtualPath);
        return Task.FromResult<VirtualFile?>(file);
    }
    
    public async Task RemoveFileAsync(string virtualPath)
    {
        var file = await GetFileAsync(virtualPath);
        if (file == null) return;

        var files = _db.GetCollection<VirtualFile>("files");
        var hashes = _db.GetCollection<HashInfo>("hashes");
        
        _db.BeginTrans();
        try
        {
            var hashInfo = hashes.FindById(file.ContentHash);
            if (hashInfo != null)
            {
                hashInfo.ReferenceCount--;
                hashes.Update(hashInfo);
            }
            
            files.Delete(virtualPath);
            
            _db.Commit();
        }
        catch
        {
            _db.Rollback();
            throw;
        }
    }

    public Task AddDirectoryAsync(string virtualPath)
    {
        var directories = _db.GetCollection<VirtualDirectory>("directories");
        directories.Upsert(new VirtualDirectory { Path = virtualPath });
        return Task.CompletedTask;
    }
    
    public Task RemoveDirectoryAsync(string virtualPath)
    {
        var directories = _db.GetCollection<VirtualDirectory>("directories");
        directories.Delete(virtualPath);
        return Task.CompletedTask;
    }

    public Task<bool> DirectoryExistsAsync(string virtualPath)
    {
        var directories = _db.GetCollection<VirtualDirectory>("directories");
        return Task.FromResult(directories.Exists(d => d.Path == virtualPath));
    }
    
    public Task<List<FileSystemItem>> ListDirectoryContentsAsync(string virtualPath)
    {
        var files = _db.GetCollection<VirtualFile>("files");
        var directories = _db.GetCollection<VirtualDirectory>("directories");

        var results = new List<FileSystemItem>();
        
        var subDirs = directories.Find(d => d.Path.StartsWith(virtualPath) && d.Path != virtualPath)
            .Where(d => Path.GetDirectoryName(d.Path.TrimEnd('/')) == virtualPath.TrimEnd('/'));
        
        var childFiles = files.Find(f => Path.GetDirectoryName(f.VirtualPath) == virtualPath.TrimEnd('/'));
        
        results.AddRange(subDirs.Select(d => new FileSystemItem(Path.GetFileName(d.Path.TrimEnd('/')), true)));
        results.AddRange(childFiles.Select(f => new FileSystemItem(Path.GetFileName(f.VirtualPath), false)));
        
        return Task.FromResult(results);
    }

    public Task<List<string>> GetUnreferencedHashesAsync()
    {
        var hashes = _db.GetCollection<HashInfo>("hashes");
        var unreferenced = hashes.Find(h => h.ReferenceCount <= 0)
                                 .Select(h => h.ContentHash)
                                 .ToList();
        return Task.FromResult(unreferenced);
    }

    public void Dispose()
    {
        _db.Dispose();
        
        _imageManager.ReplaceFile("index.db", _tempDbPath);
        
        File.Delete(_tempDbPath);
    }
}