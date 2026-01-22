using SRP.InvoiceProcessor.Before;
using AfterInvoice = SRP.InvoiceProcessor.After.Models.Invoice;

Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine("SOLID PRINCIPLES: Single Responsibility Principle (SRP)");
Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine();

Console.WriteLine("PRINCIPLE:");
Console.WriteLine("A class should have only ONE reason to change.");
Console.WriteLine("Each class should have only one responsibility or job.");
Console.WriteLine();

// ============================
// BEFORE: Violation of SRP
// ============================
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine("BEFORE: Invoice Processor with Multiple Responsibilities");
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine();

Console.WriteLine("PROBLEMS:");
Console.WriteLine("InvoiceProcessor class has 4 different responsibilities:");
Console.WriteLine("  1. Validation logic");
Console.WriteLine("  2. Tax calculation");
Console.WriteLine("  3. Database operations");
Console.WriteLine("  4. Email notifications");
Console.WriteLine("Any change in validation, calculation, DB, or email requires modifying this class");
Console.WriteLine("Hard to test individual responsibilities in isolation");
Console.WriteLine("High coupling - everything is mixed together");
Console.WriteLine();

var beforeInvoice = new Invoice
{
    InvoiceNumber = "INV-2024-001",
    CustomerName = "Acme Corporation",
    CustomerEmail = "billing@acme.com",
    Items =
    [
        new() { Description = "Software License", Quantity = 5, UnitPrice = 100.00m },
        new() { Description = "Support Package", Quantity = 1, UnitPrice = 250.00m }
    ]
};

var beforeProcessor = new InvoiceProcessor();

try
{
    InvoiceProcessor.ProcessInvoice(beforeInvoice);
    Console.WriteLine();
    Console.WriteLine($"Invoice processed. Total: ${beforeInvoice.Total:F2}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine();

// ============================
// AFTER: Following SRP
// ============================
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine("AFTER: Separated Responsibilities");
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine();

Console.WriteLine("BENEFITS:");
Console.WriteLine("Each class has a single, well-defined responsibility:");
Console.WriteLine("  - InvoiceValidator: Only validates");
Console.WriteLine("  - InvoiceCalculator: Only calculates");
Console.WriteLine("  - InvoiceRepository: Only handles persistence");
Console.WriteLine("  - InvoiceNotifier: Only sends notifications");
Console.WriteLine("  - InvoiceProcessor: Only orchestrates the workflow");
Console.WriteLine("Easy to test each component independently");
Console.WriteLine("Changes are isolated to specific classes");
Console.WriteLine("Low coupling, high cohesion");
Console.WriteLine();

var afterInvoice = new AfterInvoice
{
    InvoiceNumber = "INV-2024-002",
    CustomerName = "Tech Innovations Inc",
    CustomerEmail = "accounts@techinnovations.com",
    Items =
    [
        new() { Description = "Cloud Services", Quantity = 10, UnitPrice = 150.00m },
        new() { Description = "Consulting Hours", Quantity = 20, UnitPrice = 120.00m }
    ]
};

var validator = new SRP.InvoiceProcessor.After.InvoiceValidator();
var calculator = new SRP.InvoiceProcessor.After.InvoiceCalculator();
var repository = new SRP.InvoiceProcessor.After.InvoiceRepository();
var notifier = new SRP.InvoiceProcessor.After.InvoiceNotifier();

var afterProcessor = new SRP.InvoiceProcessor.After.InvoiceProcessor(
    validator,
    calculator,
    repository,
    notifier);

try
{
    SRP.InvoiceProcessor.After.InvoiceProcessor.ProcessInvoice(afterInvoice);
    Console.WriteLine();
    Console.WriteLine($"Invoice processed. Subtotal: ${afterInvoice.Subtotal:F2}, Tax: ${afterInvoice.Tax:F2}, Total: ${afterInvoice.Total:F2}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine("KEY TAKEAWAY:");
Console.WriteLine("By separating concerns, each class is easier to understand, test, and maintain.");
Console.WriteLine("Changes in one area (e.g., switching from email to SMS) don't affect other areas.");
Console.WriteLine("=".PadRight(80, '='));
