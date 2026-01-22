namespace SRP.InvoiceProcessor.Before;

/// <summary>
/// PROBLEM: This class violates Single Responsibility Principle
/// It has multiple reasons to change:
/// 1. Validation logic changes
/// 2. Tax calculation rules change
/// 3. Database schema or ORM changes
/// 4. Email service provider or notification logic changes
/// </summary>
public class InvoiceProcessor
{
    public static void ProcessInvoice(Invoice invoice)
    {
        // Responsibility 1: Validation
        ArgumentNullException.ThrowIfNull(invoice);

        if (string.IsNullOrWhiteSpace(invoice.CustomerName))
        {
            throw new InvalidOperationException("Customer name is required");
        }

        if (invoice.Items == null || invoice.Items.Count == 0)
        {
            throw new InvalidOperationException("Invoice must contain at least one item");
        }

        // Responsibility 2: Business Logic (Calculation)
        decimal subtotal = 0;
        foreach (var item in invoice.Items)
        {
            subtotal += item.Quantity * item.UnitPrice;
        }

        decimal taxRate = 0.20m; // 20% tax
        decimal tax = subtotal * taxRate;
        invoice.Total = subtotal + tax;

        // Responsibility 3: Data Persistence
        Console.WriteLine($"[DB] Connecting to database...");
        Console.WriteLine($"[DB] Executing: INSERT INTO Invoices VALUES ('{invoice.InvoiceNumber}', '{invoice.CustomerName}', {invoice.Total})");
        Console.WriteLine($"[DB] Invoice saved successfully");

        // Responsibility 4: External Communication (Email)
        Console.WriteLine($"[EMAIL] Connecting to SMTP server...");
        Console.WriteLine($"[EMAIL] Sending invoice to: {invoice.CustomerEmail}");
        Console.WriteLine($"[EMAIL] Subject: Invoice {invoice.InvoiceNumber} - Amount: ${invoice.Total:F2}");
        Console.WriteLine($"[EMAIL] Email sent successfully");
    }
}

public class Invoice
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public List<InvoiceItem> Items { get; set; } = [];
    public decimal Total { get; set; }
}

public class InvoiceItem
{
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
