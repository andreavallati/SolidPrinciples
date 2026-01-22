# Single Responsibility Principle (SRP)

## Principle Definition
**A class should have only ONE reason to change.**

Each class should have a single, well-defined responsibility. When a class has multiple responsibilities, changes to one responsibility may affect or break the others.

## Real-World Scenario: Invoice Processing System

This example demonstrates invoice processing in an e-commerce or billing system.

### Before (Violation)

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

### After (Following SRP)

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

## Running the Example

```bash
dotnet run --project SRP.InvoiceProcessor
```

The program demonstrates both approaches and clearly shows the benefits of following SRP.

## Key Takeaway

When you find yourself saying "and" when describing what a class does, it probably violates SRP. 
- "This class validates **and** calculates **and** saves **and** sends emails"
- "This class validates" (single responsibility)

