namespace LSP.FileStorage.After.Abstractions;

/// <summary>
/// Core interface that all storage implementations can support
/// Following LSP: Only define operations that all implementations can fulfill
/// </summary>
public interface IFileStorage
{
    bool FileExists(string fileName);
    FileMetadata GetMetadata(string fileName);
}

/// <summary>
/// Interface for storage that supports reading
/// Separated to avoid forcing read-only implementations to support writes
/// </summary>
public interface IReadableStorage : IFileStorage
{
    byte[] ReadFile(string fileName);
    Stream OpenRead(string fileName);
}

/// <summary>
/// Interface for storage that supports writing
/// Only implemented by storage systems that can write
/// </summary>
public interface IWritableStorage : IFileStorage
{
    void SaveFile(string fileName, byte[] content);
    void SaveFile(string fileName, Stream content);
}

/// <summary>
/// Interface for storage that supports deletion
/// Optional capability - not all storage systems allow deletion
/// </summary>
public interface IDeletableStorage : IFileStorage
{
    void DeleteFile(string fileName);
}

public class FileMetadata
{
    public string FileName { get; set; } = string.Empty;
    public long Size { get; set; }
    public DateTime? LastModified { get; set; }
    public string StorageType { get; set; } = string.Empty;
}
