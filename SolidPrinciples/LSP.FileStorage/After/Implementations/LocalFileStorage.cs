using LSP.FileStorage.After.Abstractions;

namespace LSP.FileStorage.After.Implementations;

/// <summary>
/// Local file storage - supports all operations
/// Properly implements all interfaces without throwing exceptions
/// </summary>
public class LocalFileStorage : IReadableStorage, IWritableStorage, IDeletableStorage
{
    private readonly string _basePath;

    public LocalFileStorage(string basePath)
    {
        _basePath = basePath;
    }

    public bool FileExists(string fileName)
    {
        var fullPath = Path.Combine(_basePath, fileName);
        Console.WriteLine($"[LOCAL] Checking if file exists: {fullPath}");
        return true; // Simulated
    }

    public FileMetadata GetMetadata(string fileName)
    {
        Console.WriteLine($"[LOCAL] Getting metadata for: {fileName}");
        return new FileMetadata
        {
            FileName = fileName,
            Size = 1024,
            LastModified = DateTime.Now,
            StorageType = "Local File System"
        };
    }

    public byte[] ReadFile(string fileName)
    {
        var fullPath = Path.Combine(_basePath, fileName);
        Console.WriteLine($"[LOCAL] Reading file from: {fullPath}");
        Console.WriteLine($"[LOCAL] File read successfully");
        return new byte[] { 1, 2, 3, 4, 5 };
    }

    public Stream OpenRead(string fileName)
    {
        Console.WriteLine($"[LOCAL] Opening stream for: {fileName}");
        return new MemoryStream(new byte[] { 1, 2, 3, 4, 5 });
    }

    public void SaveFile(string fileName, byte[] content)
    {
        var fullPath = Path.Combine(_basePath, fileName);
        Console.WriteLine($"[LOCAL] Saving file to: {fullPath}");
        Console.WriteLine($"[LOCAL] File size: {content.Length} bytes");
        Console.WriteLine($"[LOCAL] File saved successfully");
    }

    public void SaveFile(string fileName, Stream content)
    {
        var fullPath = Path.Combine(_basePath, fileName);
        Console.WriteLine($"[LOCAL] Saving stream to: {fullPath}");
        Console.WriteLine($"[LOCAL] Stream saved successfully");
    }

    public void DeleteFile(string fileName)
    {
        var fullPath = Path.Combine(_basePath, fileName);
        Console.WriteLine($"[LOCAL] Deleting file: {fullPath}");
        Console.WriteLine($"[LOCAL] File deleted successfully");
    }
}
