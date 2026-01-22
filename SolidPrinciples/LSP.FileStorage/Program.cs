using LSP.FileStorage.After.Abstractions;
using LSP.FileStorage.Before;

Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine("SOLID PRINCIPLES: Liskov Substitution Principle (LSP)");
Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine();

Console.WriteLine("PRINCIPLE:");
Console.WriteLine("Objects of a derived class should be substitutable for objects of the base class");
Console.WriteLine("without breaking the application. Subtypes must be substitutable for their base types.");
Console.WriteLine();

// ============================
// BEFORE: Violation of LSP
// ============================
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine("BEFORE: File Storage with Inheritance Issues");
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine();

Console.WriteLine("PROBLEMS:");
Console.WriteLine("ReadOnlyCloudStorage inherits from FileStorage but throws exceptions");
Console.WriteLine("S3Storage has different behavior than expected (versioned deletes)");
Console.WriteLine("Cannot safely substitute derived classes for base class");
Console.WriteLine("Client code must know implementation details to avoid exceptions");
Console.WriteLine("Violates the 'contract' established by the base class");
Console.WriteLine();

void ProcessFiles(FileStorage storage, string fileName)
{
    try
    {
        Console.WriteLine($"\nProcessing with {storage.GetType().Name}:");

        var content = new byte[] { 1, 2, 3 };
        storage.SaveFile(fileName, content); // This might throw!

        var readContent = storage.ReadFile(fileName);
        Console.WriteLine($"Read {readContent.Length} bytes");

        var size = storage.GetFileSize(fileName); // This might also throw!
        Console.WriteLine($"File size: {size} bytes");

        storage.DeleteFile(fileName); // This might throw too!
    }
    catch (NotSupportedException ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
        Console.WriteLine("Cannot use this implementation as a FileStorage substitute!");
    }
    catch (NotImplementedException ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
        Console.WriteLine("Implementation incomplete, violates LSP!");
    }
}

var localStorage = new FileStorage("C:/data");
ProcessFiles(localStorage, "document.txt");

var readOnlyStorage = new LSP.FileStorage.Before.ReadOnlyCloudStorage("cloud://readonly-bucket");
ProcessFiles(readOnlyStorage, "report.pdf"); // Exceptions thrown

var s3Storage = new LSP.FileStorage.Before.S3Storage("my-s3-bucket");
ProcessFiles(s3Storage, "image.jpg"); // GetFileSize throws

Console.WriteLine();
Console.WriteLine();

// ============================
// AFTER: Following LSP
// ============================
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine("AFTER: Properly Segregated Storage Interfaces");
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine();

Console.WriteLine("BENEFITS:");
Console.WriteLine("Interfaces clearly define capabilities (IReadableStorage, IWritableStorage, etc.)");
Console.WriteLine("No unexpected exceptions - contract is clear");
Console.WriteLine("Read-only storage only implements IReadableStorage");
Console.WriteLine("Full-featured storage implements all interfaces");
Console.WriteLine("Client code can safely work with any implementation");
Console.WriteLine();

void ProcessReadableStorage(IReadableStorage storage, string fileName)
{
    Console.WriteLine($"\nReading with {storage.GetType().Name}:");

    if (storage.FileExists(fileName))
    {
        var metadata = storage.GetMetadata(fileName);
        Console.WriteLine($"File: {metadata.FileName}, Size: {metadata.Size} bytes, Type: {metadata.StorageType}");

        var content = storage.ReadFile(fileName);
        Console.WriteLine($"Read {content.Length} bytes successfully");
    }
}

void ProcessWritableStorage(IWritableStorage storage, string fileName, byte[] content)
{
    Console.WriteLine($"\nWriting with {storage.GetType().Name}:");

    storage.SaveFile(fileName, content);
    Console.WriteLine($"Saved {content.Length} bytes successfully");
}

void ProcessDeletableStorage(IDeletableStorage storage, string fileName)
{
    Console.WriteLine($"\nDeleting with {storage.GetType().Name}:");

    storage.DeleteFile(fileName);
    Console.WriteLine($"File deleted successfully");
}

// All implementations can be used for reading
var afterLocal = new LSP.FileStorage.After.Implementations.LocalFileStorage("C:/data");
var afterReadOnly = new LSP.FileStorage.After.Implementations.ReadOnlyCloudStorage("cloud://readonly-bucket");
var afterS3 = new LSP.FileStorage.After.Implementations.S3Storage("my-s3-bucket");
var afterAzure = new LSP.FileStorage.After.Implementations.AzureBlobStorage("my-container");

Console.WriteLine("\nALL storages support reading (IReadableStorage):");
ProcessReadableStorage(afterLocal, "document.txt");
ProcessReadableStorage(afterReadOnly, "report.pdf"); // No exception!
ProcessReadableStorage(afterS3, "image.jpg"); // No exception!
ProcessReadableStorage(afterAzure, "video.mp4");

Console.WriteLine("\nONLY writable storages support writing (IWritableStorage):");
var testContent = new byte[] { 10, 20, 30 };
ProcessWritableStorage(afterLocal, "new-file.txt", testContent);
// afterReadOnly doesn't implement IWritableStorage - compile-time safety!
ProcessWritableStorage(afterS3, "new-file.txt", testContent);
ProcessWritableStorage(afterAzure, "new-file.txt", testContent);

Console.WriteLine("\nONLY deletable storages support deletion (IDeletableStorage):");
ProcessDeletableStorage(afterLocal, "old-file.txt");
// afterReadOnly doesn't implement IDeletableStorage - compile-time safety!
ProcessDeletableStorage(afterS3, "old-file.txt");
ProcessDeletableStorage(afterAzure, "old-file.txt");

Console.WriteLine();
Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine("KEY TAKEAWAY:");
Console.WriteLine("Design interfaces based on client needs, not implementation details.");
Console.WriteLine("Derived classes should enhance, not restrict, base class behavior.");
Console.WriteLine("Use interface segregation to make substitution safe and predictable.");
Console.WriteLine("=".PadRight(80, '='));
