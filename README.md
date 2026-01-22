# Solid Principles

## Overview

This repository is a **complete resource** for understanding and applying SOLID principles in real-world C# development. Unlike typical tutorials with simplistic examples, this project demonstrates each principle through **realistic scenarios** that mirror actual production code challenges.

---

## Features

- One dedicated project per SOLID principle
- Side-by-side comparison of violations vs. correct implementations
- Identify SOLID principle violations in existing code  
- Techniques to refactor code to follow SOLID principles
- Real-world benefits of each principle
- Understand how SOLID principles work together to create maintainable software

---

## What is SOLID?

SOLID is an acronym for five design principles that make software designs more understandable, flexible, and maintainable:

- **S** - Single Responsibility Principle (SRP)
- **O** - Open/Closed Principle (OCP)
- **L** - Liskov Substitution Principle (LSP)
- **I** - Interface Segregation Principle (ISP)
- **D** - Dependency Inversion Principle (DIP)

## Project Structure

**SRP.InvoiceProcessor**

```
SRP.InvoiceProcessor/
├── Before/
│   └── InvoiceProcessor.cs                    # Violates SRP - handles everything
├── After/
│   ├── Models/
│   │   └── Invoice.cs                         # Data models
│   ├── InvoiceValidator.cs                    # Responsibility: Validation
│   ├── InvoiceCalculator.cs                   # Responsibility: Calculation
│   ├── InvoiceRepository.cs                   # Responsibility: Persistence
│   ├── InvoiceNotifier.cs                     # Responsibility: Notifications
│   └── InvoiceProcessor.cs                    # Responsibility: Orchestration
├── Program.cs                                  # Demo application
└── README.md                                   # Detailed documentation
```

**OCP.PaymentGateway**

```
OCP.PaymentGateway/
├── Before/
│   └── PaymentProcessor.cs                    # Violates OCP - switch statements
├── After/
│   ├── Models/
│   │   └── PaymentModels.cs                   # Request/Result models
│   ├── Processors/
│   │   ├── CreditCardProcessor.cs             # Credit card implementation
│   │   ├── PayPalProcessor.cs                 # PayPal implementation
│   │   ├── StripeProcessor.cs                 # Stripe implementation
│   │   ├── BankTransferProcessor.cs           # Bank transfer implementation
│   │   └── CryptocurrencyProcessor.cs         # Crypto implementation (NEW!)
│   ├── IPaymentProcessor.cs                   # Strategy interface
│   └── PaymentGateway.cs                      # Gateway orchestrator
├── Program.cs                                  # Demo application
└── README.md                                   # Detailed documentation
```

**LSP.FileStorage**

```
LSP.FileStorage/
├── Before/
│   └── FileStorage.cs                         # Violates LSP - throws exceptions
├── After/
│   ├── Abstractions/
│   │   └── IFileStorage.cs                    # Segregated interfaces
│   │       ├── IFileStorage                   # Base interface
│   │       ├── IReadableStorage               # Read capability
│   │       ├── IWritableStorage               # Write capability
│   │       └── IDeletableStorage              # Delete capability
│   └── Implementations/
│       ├── LocalFileStorage.cs                # Full-featured storage
│       ├── ReadOnlyCloudStorage.cs            # Read-only implementation
│       ├── S3Storage.cs                       # AWS S3 implementation
│       └── AzureBlobStorage.cs                # Azure implementation
├── Program.cs                                  # Demo application
└── README.md                                   # Detailed documentation
```

**ISP.OfficeDevices**

```
ISP.OfficeDevices/
├── Before/
│   └── OfficeDevices.cs                       # Violates ISP - fat interface
├── After/
│   ├── Interfaces/
│   │   └── IOfficeDevice.cs                   # Segregated interfaces
│   │       ├── IPrinter                       # Print capability
│   │       ├── IScanner                       # Scan capability
│   │       ├── IFax                           # Fax capability
│   │       ├── ICopier                        # Copy capability
│   │       └── IEmailSender                   # Email capability
│   └── Devices/
│       ├── HighEndMultiFunctionPrinter.cs     # All capabilities
│       ├── BasicPrinter.cs                    # Print only
│       ├── StandaloneScanner.cs               # Scan + Email
│       ├── OldFaxMachine.cs                   # Print + Fax
│       └── WirelessCloudPrinter.cs            # Print + Email
├── Program.cs                                  # Demo application
└── README.md                                   # Detailed documentation
```

**DIP.OrderManagement**

```
DIP.OrderManagement/
├── Before/
│   └── OrderService.cs                        # Violates DIP - tight coupling
├── After/
│   ├── Models/
│   │   └── Order.cs                           # Data models
│   ├── Abstractions/
│   │   └── IOrderServices.cs                  # Interface definitions
│   │       ├── IOrderRepository               # Repository abstraction
│   │       ├── INotificationService           # Notification abstraction
│   │       └── ILogger                        # Logger abstraction
│   ├── Implementations/
│   │   ├── SqlOrderRepository.cs              # SQL implementation
│   │   ├── MongoOrderRepository.cs            # MongoDB implementation
│   │   ├── EmailNotificationService.cs        # Email notifications
│   │   ├── SmsNotificationService.cs          # SMS notifications
│   │   ├── PushNotificationService.cs         # Push notifications
│   │   └── ConsoleLogger.cs                   # Console logger
│   └── OrderService.cs                        # Service with DI
├── Program.cs                                  # Demo application
└── README.md                                   # Detailed documentation
```

---

## Principle Details

Each principle is explained in detail below with real-world scenarios, code examples, and practical insights. Individual README files are also available in each project folder.

## Single Responsibility Principle (SRP)

### Principle Definition
**"A class should have only ONE reason to change."**

Each class should have a single, well-defined responsibility. When a class has multiple responsibilities, changes to one responsibility may affect or break the others.

### Project: `SRP.InvoiceProcessor`

**Real-World Scenario:** Invoice processing in an e-commerce or billing system

#### ❌ Before (Violation)

The `InvoiceProcessor` class has **four different responsibilities**:
1. **Validation** - Checking if invoice data is valid
2. **Calculation** - Computing totals and taxes
3. **Persistence** - Saving to database
4. **Notification** - Sending emails

**Problems:**
- If tax rules change, you modify the same class used for database operations
- If you switch email providers, you risk breaking validation logic
- Testing is difficult - you can't test validation without also dealing with database/email code
- Multiple developers working on different features will cause merge conflicts
- High risk of introducing bugs when making any change

#### ✅ After (Following SRP)

Each responsibility is separated into its own class:
- **`InvoiceValidator`** - Only validates invoice data
- **`InvoiceCalculator`** - Only handles calculations
- **`InvoiceRepository`** - Only manages data persistence
- **`InvoiceNotifier`** - Only sends notifications
- **`InvoiceProcessor`** - Orchestrates the workflow (single responsibility: coordination)

**Benefits:**
- Each class has one reason to change
- Easy to test each component in isolation
- Changes are localized - switching from email to SMS only affects `InvoiceNotifier`
- Multiple developers can work on different responsibilities without conflicts
- Code is more maintainable and easier to understand

#### Key Takeaway

When you find yourself saying "and" when describing what a class does, it probably violates SRP.
- ❌ "This class validates **and** calculates **and** saves **and** sends emails"
- ✅ "This class validates" (single responsibility)

[SRP Full Documentation](SolidPrinciples/SRP.InvoiceProcessor/README.md)

---

## Open/Closed Principle (OCP)

### Principle Definition
**"Software entities should be OPEN for extension but CLOSED for modification."**

You should be able to add new functionality without changing existing code. This reduces the risk of breaking existing features when adding new ones.

### Project: `OCP.PaymentGateway`

**Real-World Scenario:** Payment processing system supporting multiple payment methods (credit cards, PayPal, Stripe, bank transfers, cryptocurrency)

#### ❌ Before (Violation)

The `PaymentProcessor` class uses a **switch statement** to handle different payment methods:

```csharp
switch (request.Method)
{
    case PaymentMethod.CreditCard:
        return ProcessCreditCard(request);
    case PaymentMethod.PayPal:
        return ProcessPayPal(request);
    // ... more cases
}
```

**Problems:**
- **Modification Required**: Every new payment method requires modifying the `PaymentProcessor` class
- **Growing Switch Statement**: The switch grows larger and more complex over time
- **Risk of Bugs**: Changes to add new features can break existing functionality
- **Testing Difficulty**: Can't test payment methods independently
- **Violation of OCP**: The class is not closed for modification

#### ✅ After (Following OCP)

Uses the **Strategy Pattern** with an `IPaymentProcessor` interface:

```csharp
public interface IPaymentProcessor
{
    string PaymentMethodName { get; }
    PaymentResult ProcessPayment(PaymentRequest request);
}
```

Each payment method is a separate class implementing this interface:
- `CreditCardProcessor`
- `PayPalProcessor`
- `StripeProcessor`
- `BankTransferProcessor`
- `CryptocurrencyProcessor` (NEW - added without modifying existing code!)

The `PaymentGateway` class registers and manages processors:

```csharp
gateway.RegisterProcessor("crypto", new CryptocurrencyProcessor());
```

**Benefits:**
- **Extension without Modification**: Add new payment processors without changing `PaymentGateway`
- **Independent Testing**: Each processor can be tested in isolation
- **Runtime Flexibility**: Payment methods can be added/removed dynamically
- **Clean Architecture**: Follows SOLID principles and design patterns
- **Reduced Risk**: Existing code remains untouched when adding features

#### Key Takeaway

Use **abstraction** (interfaces) and **composition** (dependency injection) to make your code extensible. When you need new functionality, add new classes instead of modifying existing ones.

**"Open for extension, closed for modification"** = Add new code, don't change old code.

[OCP Full Documentation →](SolidPrinciples/OCP.PaymentGateway/README.md)

---

## Liskov Substitution Principle (LSP)

### Principle Definition
**"Objects of a derived class should be substitutable for objects of the base class without breaking the application."**

Subtypes must be substitutable for their base types. If class B is a subtype of class A, you should be able to replace A with B without disrupting the behavior of the program.

### Project: `LSP.FileStorage`

**Real-World Scenario:** File storage system supporting various storage backends (local file system, cloud storage, AWS S3, Azure Blob Storage)

#### ❌ Before (Violation)

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
storage.SaveFile("file.txt", data); // BOOM! NotSupportedException
```

#### ✅ After (Following LSP)

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

#### Key Takeaway

**Don't force derived classes to implement operations they can't support.** Use interface segregation to create precise contracts that all implementations can fulfill without throwing exceptions or changing expected behavior.

The LSP ensures that inheritance hierarchies are logically sound and that polymorphism works correctly.

[LSP Full Documentation →](SolidPrinciples/LSP.FileStorage/README.md)

---

## Interface Segregation Principle (ISP)

### Principle Definition
**"Clients should not be forced to depend on interfaces they do not use."**

Many specific interfaces are better than one general-purpose interface. Split large interfaces into smaller, more specific ones so that clients only need to know about the methods that are relevant to them.

### Project: `ISP.OfficeDevices`

**Real-World Scenario:** Office device management system for printers, scanners, fax machines, and multifunction devices

#### ❌ Before (Violation)

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

- **`BasicPrinter`** - Only prints, but forced to implement Scan, Fax, Copy, Email → throws exceptions
- **`StandaloneScanner`** - Only scans, but forced to implement Print, Fax, Copy → throws exceptions
- **`OldFaxMachine`** - Prints and faxes, but forced to implement Scan, Copy, Email → throws exceptions

**Problems:**
- **Fat Interface**: Single interface with too many responsibilities
- **NotSupportedException**: Classes throw exceptions for methods they can't support
- **No Compile-Time Safety**: Clients can't trust the interface contract
- **Misleading API**: Interface promises functionality that implementations don't have
- **Hard to Extend**: Adding new device types with different capabilities is awkward

#### ✅ After (Following ISP)

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

#### Key Takeaway

**Design interfaces from the client's perspective, not the implementer's.**

Instead of creating one large interface with everything, create multiple small interfaces for specific needs. Classes implement only what they can actually do, and clients depend only on what they actually use.

ISP promotes loose coupling, high cohesion, and better API design.

[ISP Full Documentation →](SolidPrinciples/ISP.OfficeDevices/README.md)

---

## Dependency Inversion Principle (DIP)

### Principle Definition
**A. High-level modules should not depend on low-level modules. Both should depend on abstractions.**  
**B. Abstractions should not depend on details. Details should depend on abstractions.**

Instead of having high-level business logic depend on low-level implementation details (database, email service, etc.), both should depend on interfaces or abstract classes.

### Project: `DIP.OrderManagement`

**Real-World Scenario:** E-commerce order processing system that saves orders to a database and sends confirmation notifications

#### ❌ Before (Violation)

The `OrderService` class directly creates and depends on concrete implementations:

```csharp
public class OrderService
{
    private readonly SqlOrderRepository _repository;
    private readonly EmailService _emailService;

    public OrderService()
    {
        _repository = new SqlOrderRepository();  // Direct instantiation
        _emailService = new EmailService();      // Direct instantiation
    }
}
```

**Problems:**
- **Tight Coupling**: `OrderService` is tightly coupled to `SqlOrderRepository` and `EmailService`
- **Hard to Change**: Switching to MongoDB or SMS requires modifying `OrderService`
- **Hard to Test**: Cannot test without real database and email service
- **Violates OCP**: Must modify existing code to change implementations
- **Rigid Design**: Cannot easily add caching, logging, or other cross-cutting concerns

**To change implementations, you must:**
1. Modify the `OrderService` class
2. Recompile
3. Risk breaking existing functionality

#### ✅ After (Following DIP)

Both high-level and low-level modules depend on **abstractions** (interfaces):

```csharp
public interface IOrderRepository { ... }
public interface INotificationService { ... }

public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly INotificationService _notificationService;

    public OrderService(IOrderRepository repository, INotificationService notificationService)
    {
        _repository = repository;              // Injected
        _notificationService = notificationService;  // Injected
    }
}
```

**Implementations:**
- **Repositories**: `SqlOrderRepository`, `MongoOrderRepository`
- **Notifications**: `EmailNotificationService`, `SmsNotificationService`, `PushNotificationService`
- **Logging**: `ConsoleLogger`

**Benefits:**
- **Loose Coupling**: `OrderService` doesn't know or care about concrete implementations
- **Easy to Change**: Swap implementations by injecting different ones (no code changes!)
- **Easy to Test**: Inject mock implementations for unit testing
- **Follows OCP**: Add new implementations without modifying `OrderService`
- **Flexible**: Can combine different implementations (SQL+Email, Mongo+SMS, etc.)

**Example - Swapping implementations:**
```csharp
// SQL + Email
var service1 = new OrderService(new SqlOrderRepository(), new EmailNotificationService(), logger);

// MongoDB + SMS (no changes to OrderService!)
var service2 = new OrderService(new MongoOrderRepository("..."), new SmsNotificationService(), logger);

// SQL + Push Notifications (no changes to OrderService!)
var service3 = new OrderService(new SqlOrderRepository(), new PushNotificationService(), logger);
```

#### Key Takeaway

**"Depend on abstractions, not concretions."**

- Design interfaces based on what high-level modules need
- Low-level modules implement those interfaces
- Use **Dependency Injection** to provide implementations at runtime
- This is the foundation of modern frameworks (ASP.NET Core, Spring, etc.)

**DIP enables:**
- **Testability** - Mock dependencies in tests
- **Flexibility** - Swap implementations easily
- **Maintainability** - Changes isolated to specific implementations
- **Scalability** - Add features without modifying core logic

This principle is the cornerstone of **Inversion of Control (IoC)** and **Dependency Injection** patterns used throughout modern software development.

[DIP Full Documentation →](SolidPrinciples/DIP.OrderManagement/README.md)

---

## Technologies Used

- **.NET 8** - Latest LTS version
- **C# 12** - Modern language features

---

## Installation
### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- Any IDE that supports C# (Visual Studio 2022, VS Code, Rider)

### Steps

1. **Clone the repository:**
   ```bash
   git clone https://github.com/andreavallati/SolidPrinciples.git
   cd SolidPrinciples
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Run the application** 
   ```bash
   #Run individual projects
   dotnet run --project SRP.InvoiceProcessor
   dotnet run --project OCP.PaymentGateway
   dotnet run --project LSP.FileStorage
   dotnet run --project ISP.OfficeDevices
   dotnet run --project DIP.OrderManagement

   #Or build the entire solution
   dotnet build
   ```
---

<div align="center">

**Happy Coding!**

</div>
