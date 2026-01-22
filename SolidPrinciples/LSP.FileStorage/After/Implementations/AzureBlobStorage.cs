using LSP.FileStorage.After.Abstractions;

namespace LSP.FileStorage.After.Implementations;

/// <summary>
/// Azure Blob Storage - supports all operations
/// Another example of proper LSP implementation
/// </summary>
public class AzureBlobStorage : IReadableStorage, IWritableStorage, IDeletableStorage
{
    private readonly string _containerName;

    public AzureBlobStorage(string containerName)
    {
        _containerName = containerName;
    }

    public bool FileExists(string fileName)
    {
        Console.WriteLine($"[AZURE] Checking blob existence in container: {_containerName}");
        Console.WriteLine($"[AZURE] Blob name: {fileName}");
        return true;
    }

    public FileMetadata GetMetadata(string fileName)
    {
        Console.WriteLine($"[AZURE] Getting blob properties from container: {_containerName}");
        return new FileMetadata
        {
            FileName = fileName,
            Size = 8192,
            LastModified = DateTime.Now.AddDays(-1),
            StorageType = "Azure Blob Storage"
        };
    }

    public byte[] ReadFile(string fileName)
    {
        Console.WriteLine($"[AZURE] Downloading blob from container: {_containerName}");
        Console.WriteLine($"[AZURE] Blob name: {fileName}");
        Console.WriteLine($"[AZURE] Download complete");
        return new byte[] { 200, 201, 202 };
    }

    public Stream OpenRead(string fileName)
    {
        Console.WriteLine($"[AZURE] Opening blob stream: {fileName}");
        return new MemoryStream(new byte[] { 200, 201, 202 });
    }

    public void SaveFile(string fileName, byte[] content)
    {
        Console.WriteLine($"[AZURE] Uploading blob to container: {_containerName}");
        Console.WriteLine($"[AZURE] Blob name: {fileName}");
        Console.WriteLine($"[AZURE] Size: {content.Length} bytes");
        Console.WriteLine($"[AZURE] Upload complete");
    }

    public void SaveFile(string fileName, Stream content)
    {
        Console.WriteLine($"[AZURE] Uploading blob stream to container: {_containerName}");
        Console.WriteLine($"[AZURE] Blob name: {fileName}");
        Console.WriteLine($"[AZURE] Stream upload complete");
    }

    public void DeleteFile(string fileName)
    {
        Console.WriteLine($"[AZURE] Deleting blob from container: {_containerName}");
        Console.WriteLine($"[AZURE] Blob name: {fileName}");
        Console.WriteLine($"[AZURE] Blob deleted successfully");
    }
}
