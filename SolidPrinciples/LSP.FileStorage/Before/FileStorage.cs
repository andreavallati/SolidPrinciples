namespace LSP.FileStorage.Before;

/// <summary>
/// Base class for file storage operations
/// </summary>
public class FileStorage
{
    protected string BasePath { get; set; } = string.Empty;

    public FileStorage(string basePath)
    {
        BasePath = basePath;
    }

    public virtual void SaveFile(string fileName, byte[] content)
    {
        var fullPath = Path.Combine(BasePath, fileName);
        Console.WriteLine($"[LOCAL] Saving file to: {fullPath}");
        Console.WriteLine($"[LOCAL] File size: {content.Length} bytes");
        Console.WriteLine($"[LOCAL] File saved successfully");
    }

    public virtual byte[] ReadFile(string fileName)
    {
        var fullPath = Path.Combine(BasePath, fileName);
        Console.WriteLine($"[LOCAL] Reading file from: {fullPath}");
        Console.WriteLine($"[LOCAL] File read successfully");
        return new byte[] { 1, 2, 3, 4, 5 }; // Simulated content
    }

    public virtual void DeleteFile(string fileName)
    {
        var fullPath = Path.Combine(BasePath, fileName);
        Console.WriteLine($"[LOCAL] Deleting file: {fullPath}");
        Console.WriteLine($"[LOCAL] File deleted successfully");
    }

    public virtual long GetFileSize(string fileName)
    {
        var fullPath = Path.Combine(BasePath, fileName);
        Console.WriteLine($"[LOCAL] Getting size for: {fullPath}");
        return 1024; // Simulated size
    }
}

/// <summary>
/// PROBLEM: This class violates Liskov Substitution Principle
/// It inherits from FileStorage but throws exceptions for some operations
/// It cannot be used as a substitute for FileStorage without breaking behavior
/// </summary>
public class ReadOnlyCloudStorage : FileStorage
{
    public ReadOnlyCloudStorage(string cloudPath) : base(cloudPath)
    {
    }

    // This overrides the base behavior but throws exception
    // Violates LSP: Cannot substitute this for FileStorage
    public override void SaveFile(string fileName, byte[] content)
    {
        throw new NotSupportedException("This storage is read-only. Cannot save files.");
    }

    // Also throws exception - violates expected behavior
    public override void DeleteFile(string fileName)
    {
        throw new NotSupportedException("This storage is read-only. Cannot delete files.");
    }

    // These work fine
    public override byte[] ReadFile(string fileName)
    {
        Console.WriteLine($"[CLOUD-RO] Reading file from cloud: {BasePath}/{fileName}");
        Console.WriteLine($"[CLOUD-RO] Downloading...");
        Console.WriteLine($"[CLOUD-RO] File downloaded successfully");
        return new byte[] { 10, 20, 30, 40, 50 };
    }

    public override long GetFileSize(string fileName)
    {
        Console.WriteLine($"[CLOUD-RO] Getting size from cloud: {BasePath}/{fileName}");
        return 2048;
    }
}

/// <summary>
/// Another problematic class - AWS S3 storage that behaves differently
/// </summary>
public class S3Storage : FileStorage
{
    public S3Storage(string bucketName) : base(bucketName)
    {
    }

    public override void SaveFile(string fileName, byte[] content)
    {
        // S3 doesn't use file paths the same way
        Console.WriteLine($"[S3] Uploading to bucket: {BasePath}");
        Console.WriteLine($"[S3] Key: {fileName}");
        Console.WriteLine($"[S3] Size: {content.Length} bytes");
        Console.WriteLine($"[S3] Upload complete");
    }

    public override byte[] ReadFile(string fileName)
    {
        Console.WriteLine($"[S3] Downloading from bucket: {BasePath}");
        Console.WriteLine($"[S3] Key: {fileName}");
        return new byte[] { 100, 101, 102 };
    }

    // S3 uses different deletion mechanism - soft delete with versioning
    public override void DeleteFile(string fileName)
    {
        Console.WriteLine($"[S3] Marking object for deletion (versioned): {fileName}");
        Console.WriteLine($"[S3] Note: Previous versions still accessible");
    }

    // S3 doesn't support GetFileSize the same way - needs API call
    public override long GetFileSize(string fileName)
    {
        // In real S3, this would need HeadObject API call which might fail
        throw new NotImplementedException("S3 requires separate API call for object metadata");
    }
}
