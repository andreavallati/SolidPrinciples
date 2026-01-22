# SOLID Principles - Combined Example

## All 5 SOLID Principles Working Together

This project demonstrates how **all 5 SOLID principles work together** in a real-world e-commerce order fulfillment system. Unlike the individual examples that focus on one principle at a time, this combined example shows how these principles complement and reinforce each other in a cohesive application.

## The Scenario: E-Commerce Order Fulfillment System

An online retail system that processes customer orders from creation through shipment. The system must:
- Validate orders and check inventory
- Apply discounts and calculate pricing
- Process payments through various methods
- Handle different order types (standard, express, international)
- Send notifications and track shipments
- Support different order capabilities (cancellation, refunds, modifications)

## How Each SOLID Principle is Applied

### **S** - Single Responsibility Principle (SRP)

Each class has **one reason to change**:

| Class | Single Responsibility |
|-------|----------------------|
| `OrderValidator` | Validates order data only |
| `OrderPricingCalculator` | Calculates totals, taxes, discounts only |
| `OrderFulfillmentService` | Orchestrates the workflow only |

**Files:**
- `SRP/OrderValidatorAndCalculator.cs`

**Why it matters here:** When tax rules change, you only modify `OrderPricingCalculator`. When validation rules change, you only modify `OrderValidator`. No risk of breaking other functionality.

---

### **O** - Open/Closed Principle (OCP)

The system is **open for extension, closed for modification**:

**Extensible Components:**
- **Discount Strategies**: Add new discount types without modifying existing code
  - `VIPCustomerDiscount`, `BulkOrderDiscount`, `PercentageDiscount`
  - Want to add a "First-Time Customer Discount"? Just create a new class implementing `IDiscountStrategy`

- **Shipping Providers**: Add new carriers dynamically
  - `StandardShipping`, `ExpressShipping`, `InternationalShipping`
  - Want to add drone delivery? Create `DroneShipping` implementing `IShippingProvider`

- **Payment Processors**: Support new payment methods
  - `CreditCardPayment`, `PayPalPayment`
  - Want crypto payments? Create `CryptoPayment` implementing `IPaymentProcessor`

**Files:**
- `OCP/ExtensibleStrategies.cs`

**Why it matters here:** Adding a new payment method (e.g., Apple Pay) requires **zero changes** to `OrderFulfillmentService`. Just implement the interface and inject it.

---

### **L** - Liskov Substitution Principle (LSP)

Order handlers are **fully substitutable** without breaking behavior:

```csharp
IOrderHandler handler = orderHandlerFactory.GetHandler(order.Type);
handler.PrepareForShipment(order); // Works for ALL order types
```

**Implementations:**
- `StandardOrderHandler` - Basic processing
- `ExpressOrderHandler` - Priority processing
- `InternationalOrderHandler` - Customs processing

**Files:**
- `LSP/OrderHandlers.cs`

**Why it matters here:** You can process any order type through the same interface. The `OrderFulfillmentService` doesn't need to know which specific handler it's using—polymorphism just works.

---

### **I** - Interface Segregation Principle (ISP)

Orders implement **only the capabilities they support**:

| Interface | Purpose | Implemented By |
|-----------|---------|----------------|
| `ICancellableOrder` | Can be cancelled | Standard orders (before shipment) |
| `ITrackableOrder` | Can be tracked | Shipped and delivered orders |
| `IRefundableOrder` | Can be refunded | Paid orders |
| `IModifiableOrder` | Can be modified | Orders not yet shipped |

**Example:**
```csharp
// Standard order supports cancellation and modification
var standardOrder = new StandardOrder(order);
if (standardOrder is ICancellableOrder cancellable)
{
    cancellable.Cancel(); // Only available if supported
}

// Shipped order only supports tracking
var shippedOrder = new ShippedOrder(order);
if (shippedOrder is ITrackableOrder trackable)
{
    trackable.GetTrackingNumber(); // Safe to call
}
```

**Files:**
- `ISP/OrderCapabilities.cs`

**Why it matters here:** Clients never get `NotSupportedException`. If an order doesn't support an operation, it simply doesn't implement that interface. **Compile-time safety** instead of runtime errors.

---

### **D** - Dependency Inversion Principle (DIP)

High-level `OrderFulfillmentService` depends on **abstractions**, not concrete implementations:

**Abstractions:**
- `IOrderRepository` - Data persistence
- `IInventoryService` - Stock management
- `INotificationService` - Customer notifications
- `ILogger` - Application logging

**Concrete Implementations (Easily Swappable):**
- Repository: `InMemoryOrderRepository` ? `SqlOrderRepository`
- Notifications: `EmailNotificationService` ? `SmsNotificationService`

**Example - Switching notification methods:**
```csharp
// Use Email notifications
var service1 = new OrderFulfillmentService(
    validator, calculator, repository, inventory,
    new EmailNotificationService(), // ? Email
    logger, handlerFactory
);

// Use SMS notifications (same service, different implementation)
var service2 = new OrderFulfillmentService(
    validator, calculator, repository, inventory,
    new SmsNotificationService(), // ? SMS
    logger, handlerFactory
);
```

**Files:**
- `DIP/Abstractions.cs`

**Why it matters here:** You can swap from SQL to MongoDB, or from Email to SMS, **without changing a single line** in `OrderFulfillmentService`. Perfect for testing with mocks too.

---

## Running the Example

```bash
dotnet run --project SOLID.Combined
```

### What You'll See

The program demonstrates **3 complete order scenarios**:

1. **Standard Order - VIP Customer**
   - Uses VIP discount strategy
   - Standard shipping
   - Credit card payment
   - Demonstrates ISP with cancellable/modifiable capabilities

2. **Express Order - Bulk Discount**
   - Uses bulk order discount (25 items)
   - Express shipping
   - PayPal payment
   - Shows OCP extensibility

3. **International Order**
   - Uses percentage discount
   - International shipping with customs
   - Credit card payment
   - Uses SMS notifications instead of Email (demonstrates DIP)
   - Shows ISP with tracking capabilities

## The Power of Combined SOLID Principles

### Before SOLID (Typical Problems):
- One giant `OrderProcessor` class with 1000+ lines
- Switch statements everywhere for different order types
- Can't change payment method without modifying core logic
- Testing requires real database, real email server, real payment gateway
- NotSupportedException thrown when calling unsupported operations

### After SOLID (This Example):
- Small, focused classes (each under 100 lines)
- New features added by creating new classes, not modifying existing ones
- Swap implementations at runtime (Email ? SMS, SQL ? MongoDB)
- Easy to test with mock implementations
- Compile-time safety for supported operations

## Key Takeaways

1. **SOLID principles work together** - They're not isolated rules, but a cohesive design philosophy

2. **Real-world applicability** - This isn't academic theory; it's how production systems should be built

3. **Maintainability** - Changes are isolated, reducing risk of breaking existing functionality

4. **Testability** - Each component can be tested independently with mocks/stubs

5. **Extensibility** - New features (payment methods, shipping providers, discounts) can be added without modifying existing code

6. **Flexibility** - Swap implementations at runtime or configuration time

## Extending This Example

Want to add new features? Here's how:

### Add a New Discount Strategy
```csharp
public class FirstTimeBuyerDiscount : IDiscountStrategy
{
    public string Name => "First Time Buyer";
    
    public decimal CalculateDiscount(Order order)
    {
        // Your logic here
        return order.Subtotal * 0.20m;
    }
}
```

### Add a New Shipping Provider
```csharp
public class DroneShipping : IShippingProvider
{
    public string Name => "Drone Delivery";
    
    public ShippingInfo CalculateShipping(Order order)
    {
        return new ShippingInfo
        {
            Carrier = "DroneExpress",
            Cost = 15.99m,
            EstimatedDays = 1
        };
    }
}
```

### Add a New Order Capability
```csharp
public interface IGiftWrappable : IOrder
{
    void AddGiftWrap(string message);
}

public class GiftOrder : StandardOrder, IGiftWrappable
{
    public void AddGiftWrap(string message)
    {
        Console.WriteLine($"Adding gift wrap with message: {message}");
    }
}
```

**No modification of existing code required!** That's the power of SOLID.
