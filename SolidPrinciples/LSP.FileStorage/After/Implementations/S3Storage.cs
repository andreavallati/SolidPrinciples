using LSP.FileStorage.After.Abstractions;

namespace LSP.FileStorage.After.Implementations;

/// <summary>
/// AWS S3 storage - supports read, write, and delete operations
/// Properly implements interfaces without unexpected behavior
/// </summary>
public class S3Storage : IReadableStorage, IWritableStorage, IDeletableStorage
{
    private readonly string _bucketName;

    public S3Storage(string bucketName)
    {
        _bucketName = bucketName;
    }

    public bool FileExists(string fileName)
    {
        Console.WriteLine($"[S3] Checking if object exists in bucket: {_bucketName}");
        Console.WriteLine($"[S3] Key: {fileName}");
        return true;
    }

    public FileMetadata GetMetadata(string fileName)
    {
        Console.WriteLine($"[S3] Getting object metadata from bucket: {_bucketName}");
        Console.WriteLine($"[S3] Performing HeadObject API call");
        return new FileMetadata
        {
            FileName = fileName,
            Size = 4096,
            LastModified = DateTime.Now.AddHours(-2),
            StorageType = "AWS S3"
        };
    }

    public byte[] ReadFile(string fileName)
    {
        Console.WriteLine($"[S3] Downloading from bucket: {_bucketName}");
        Console.WriteLine($"[S3] Key: {fileName}");
        Console.WriteLine($"[S3] Download complete");
        return new byte[] { 100, 101, 102 };
    }

    public Stream OpenRead(string fileName)
    {
        Console.WriteLine($"[S3] Opening stream from S3: {fileName}");
        return new MemoryStream(new byte[] { 100, 101, 102 });
    }

    public void SaveFile(string fileName, byte[] content)
    {
        Console.WriteLine($"[S3] Uploading to bucket: {_bucketName}");
        Console.WriteLine($"[S3] Key: {fileName}");
        Console.WriteLine($"[S3] Size: {content.Length} bytes");
        Console.WriteLine($"[S3] Upload complete");
    }

    public void SaveFile(string fileName, Stream content)
    {
        Console.WriteLine($"[S3] Uploading stream to bucket: {_bucketName}");
        Console.WriteLine($"[S3] Key: {fileName}");
        Console.WriteLine($"[S3] Stream upload complete");
    }

    public void DeleteFile(string fileName)
    {
        Console.WriteLine($"[S3] Deleting object from bucket: {_bucketName}");
        Console.WriteLine($"[S3] Key: {fileName}");
        Console.WriteLine($"[S3] Object deleted successfully");
    }
}
