# Interface Segregation Principle (ISP)

## Principle Definition
**Clients should not be forced to depend on interfaces they do not use.**

Many specific interfaces are better than one general-purpose interface. Split large interfaces into smaller, more specific ones so that clients only need to know about the methods that are relevant to them.

## Real-World Scenario: Office Device Management System

This example demonstrates an office device management system for printers, scanners, fax machines, and multifunction devices.

### Before (Violation)

A single `IMultiFunctionDevice` interface defines all possible operations:

```csharp
public interface IMultiFunctionDevice
{
    void Print(string document);
    void Scan(string document);
    void Fax(string document, string phoneNumber);
    void Copy(string document);
    void Email(string document, string emailAddress);
}
```

**All devices must implement ALL methods**, even if they don't support them:

- **`BasicPrinter`** - Only prints, but forced to implement Scan, Fax, Copy, Email ? throws exceptions
- **`StandaloneScanner`** - Only scans, but forced to implement Print, Fax, Copy ? throws exceptions
- **`OldFaxMachine`** - Prints and faxes, but forced to implement Scan, Copy, Email ? throws exceptions

**Problems:**
- **Fat Interface**: Single interface with too many responsibilities
- **NotSupportedException**: Classes throw exceptions for methods they can't support
- **No Compile-Time Safety**: Clients can't trust the interface contract
- **Misleading API**: Interface promises functionality that implementations don't have
- **Hard to Extend**: Adding new device types with different capabilities is awkward

### After (Following ISP)

Uses **segregated interfaces** for each capability:

```csharp
public interface IPrinter { void Print(string document); }
public interface IScanner { void Scan(string document); }
public interface IFax { void SendFax(string document, string phoneNumber); }
public interface ICopier { void Copy(string document); }
public interface IEmailSender { void SendEmail(string document, string emailAddress); }
```

Each device implements **only the interfaces it supports**:

- **`BasicPrinter`**: `IPrinter` only
- **`StandaloneScanner`**: `IScanner`, `IEmailSender`
- **`OldFaxMachine`**: `IPrinter`, `IFax`
- **`HighEndMultiFunctionPrinter`**: All five interfaces
- **`WirelessCloudPrinter`**: `IPrinter`, `IEmailSender`

**Benefits:**
- **Focused Interfaces**: Each interface has a single, clear purpose
- **Compile-Time Safety**: Can't call methods a device doesn't support
- **No Exceptions**: All implemented methods are functional
- **Flexible Composition**: Devices can implement any combination of interfaces
- **Client Clarity**: Clients depend only on the capabilities they need
- **Easy to Extend**: New device types naturally fit the model

**Example:**
```csharp
void ProcessDocument(IPrinter printer, string document)
{
    printer.Print(document); // Works with any printer
}

// Can use BasicPrinter, WirelessCloudPrinter, HighEndMFP, etc.
// Compile-time guarantee that the device can print
```

## Running the Example

```bash
dotnet run --project ISP.OfficeDevices
```

The program demonstrates how fat interfaces lead to exceptions, and how segregated interfaces provide compile-time safety and flexibility.

## Key Takeaway

**Design interfaces from the client's perspective, not the implementer's.**

Instead of creating one large interface with everything, create multiple small interfaces for specific needs. Classes implement only what they can actually do, and clients depend only on what they actually use.

ISP promotes loose coupling, high cohesion, and better API design.

