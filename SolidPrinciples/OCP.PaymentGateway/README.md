# Open/Closed Principle (OCP)

## Principle Definition
**Software entities (classes, modules, functions) should be OPEN for extension but CLOSED for modification.**

You should be able to add new functionality without changing existing code. This reduces the risk of breaking existing features when adding new ones.

## Real-World Scenario: Payment Gateway System

This example demonstrates a payment processing system that supports multiple payment methods (credit cards, PayPal, Stripe, bank transfers, cryptocurrency).

### Before (Violation)

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

### After (Following OCP)

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

## Running the Example

```bash
dotnet run --project OCP.PaymentGateway
```

The program demonstrates how easily a new cryptocurrency payment processor is added without modifying any existing code.

## Key Takeaway

Use **abstraction** (interfaces) and **composition** (dependency injection) to make your code extensible. When you need new functionality, add new classes instead of modifying existing ones.

**"Open for extension, closed for modification"** = Add new code, don't change old code.

