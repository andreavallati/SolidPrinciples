# Dependency Inversion Principle (DIP)

## Principle Definition
**A. High-level modules should not depend on low-level modules. Both should depend on abstractions.**  
**B. Abstractions should not depend on details. Details should depend on abstractions.**

Instead of having high-level business logic depend on low-level implementation details (database, email service, etc.), both should depend on interfaces or abstract classes.

## Real-World Scenario: Order Management System

This example demonstrates an e-commerce order processing system that saves orders to a database and sends confirmation notifications.

### Before (Violation) ?

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

### After (Following DIP) ?

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

## Running the Example

```bash
dotnet run --project DIP.OrderManagement
```

The program demonstrates three different scenarios using different combinations of repository and notification implementations, all without changing the `OrderService` class.

## Key Takeaway

**"Depend on abstractions, not concretions."**

- Design interfaces based on what high-level modules need
- Low-level modules implement those interfaces
- Use **Dependency Injection** to provide implementations at runtime
- This is the foundation of modern frameworks (ASP.NET Core, Spring, etc.)

DIP enables:
- **Testability** - Mock dependencies in tests
- **Flexibility** - Swap implementations easily  
- **Maintainability** - Changes isolated to specific implementations
- **Scalability** - Add features without modifying core logic

This principle is the cornerstone of **Inversion of Control (IoC)** and **Dependency Injection** patterns used throughout modern software development.
