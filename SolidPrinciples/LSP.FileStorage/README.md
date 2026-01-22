# Liskov Substitution Principle (LSP)

## Principle Definition
**Objects of a derived class should be substitutable for objects of the base class without breaking the application.**

Subtypes must be substitutable for their base types. If class B is a subtype of class A, you should be able to replace A with B without disrupting the behavior of the program.

## Real-World Scenario: File Storage System

This example demonstrates a file storage system that supports various storage backends (local file system, cloud storage, AWS S3, Azure Blob Storage).

### Before (Violation)

A base `FileStorage` class defines operations like `SaveFile`, `ReadFile`, `DeleteFile`, and `GetFileSize`.

**Problem implementations:**

1. **`ReadOnlyCloudStorage`** inherits from `FileStorage` but throws `NotSupportedException` for write and delete operations
2. **`S3Storage`** throws `NotImplementedException` for `GetFileSize` because S3 requires a separate API call

**Problems:**
- **Broken Substitution**: Cannot safely replace `FileStorage` with derived classes
- **Runtime Exceptions**: Client code must catch exceptions and handle special cases
- **Violated Contract**: Base class promises functionality that derived classes can't deliver
- **Type Checking**: Clients must check implementation type to avoid errors
- **Fragile Code**: Adding new storage types requires updating all client code

**Example:**
```csharp
FileStorage storage = new ReadOnlyCloudStorage("path");
storage.SaveFile("file.txt", data); // NotSupportedException
```

### After (Following LSP)

Uses **interface segregation** to define clear contracts:

- **`IFileStorage`** - Base interface (metadata operations all implementations support)
- **`IReadableStorage`** - For storage that supports reading
- **`IWritableStorage`** - For storage that supports writing  
- **`IDeletableStorage`** - For storage that supports deletion

Each implementation implements **only the interfaces it can properly support**:

- **`LocalFileStorage`**: Implements all interfaces (read, write, delete)
- **`ReadOnlyCloudStorage`**: Implements only `IReadableStorage`
- **`S3Storage`**: Implements all interfaces (read, write, delete)
- **`AzureBlobStorage`**: Implements all interfaces (read, write, delete)

**Benefits:**
- **Safe Substitution**: Any `IReadableStorage` can be used for reading operations
- **Compile-Time Safety**: Can't call write operations on read-only storage
- **Clear Contracts**: Interfaces clearly communicate capabilities
- **No Runtime Exceptions**: No unexpected `NotSupportedException` errors
- **Extensible**: Easy to add new storage types with different capabilities

**Example:**
```csharp
IReadableStorage storage = new ReadOnlyCloudStorage("path");
storage.ReadFile("file.txt"); // Works perfectly!
// storage.SaveFile(...) // Compile error - method doesn't exist
```

## Running the Example

```bash
dotnet run --project LSP.FileStorage
```

The program demonstrates how violating LSP leads to runtime exceptions, and how following LSP with proper interface design creates safe, predictable substitution.

## Key Takeaway

**Don't force derived classes to implement operations they can't support.** Use interface segregation to create precise contracts that all implementations can fulfill without throwing exceptions or changing expected behavior.

The LSP ensures that inheritance hierarchies are logically sound and that polymorphism works correctly.

