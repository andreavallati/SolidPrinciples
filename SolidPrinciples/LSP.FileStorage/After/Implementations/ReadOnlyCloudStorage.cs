using LSP.FileStorage.After.Abstractions;

namespace LSP.FileStorage.After.Implementations;

/// <summary>
/// Read-only cloud storage - only implements read operations
/// Following LSP: Doesn't claim to support operations it can't perform
/// No exceptions thrown - contract is clear from interfaces
/// </summary>
public class ReadOnlyCloudStorage : IReadableStorage
{
    private readonly string _cloudPath;

    public ReadOnlyCloudStorage(string cloudPath)
    {
        _cloudPath = cloudPath;
    }

    public bool FileExists(string fileName)
    {
        Console.WriteLine($"[CLOUD-RO] Checking existence in cloud: {_cloudPath}/{fileName}");
        return true;
    }

    public FileMetadata GetMetadata(string fileName)
    {
        Console.WriteLine($"[CLOUD-RO] Getting metadata from cloud: {fileName}");
        return new FileMetadata
        {
            FileName = fileName,
            Size = 2048,
            LastModified = DateTime.Now.AddDays(-7),
            StorageType = "Read-Only Cloud Storage"
        };
    }

    public byte[] ReadFile(string fileName)
    {
        Console.WriteLine($"[CLOUD-RO] Reading file from cloud: {_cloudPath}/{fileName}");
        Console.WriteLine($"[CLOUD-RO] Downloading...");
        Console.WriteLine($"[CLOUD-RO] File downloaded successfully");
        return new byte[] { 10, 20, 30, 40, 50 };
    }

    public Stream OpenRead(string fileName)
    {
        Console.WriteLine($"[CLOUD-RO] Opening read stream from cloud: {fileName}");
        return new MemoryStream(new byte[] { 10, 20, 30, 40, 50 });
    }
}
