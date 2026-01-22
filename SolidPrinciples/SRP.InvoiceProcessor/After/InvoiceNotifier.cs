using SRP.InvoiceProcessor.After.Models;

namespace SRP.InvoiceProcessor.After;

/// <summary>
/// Single Responsibility: Send notifications about invoices
/// Changes only when notification mechanism or email service changes
/// </summary>
public class InvoiceNotifier
{
    public static void SendNotification(Invoice invoice)
    {
        Console.WriteLine($"[EMAIL] Connecting to SMTP server...");
        Console.WriteLine($"[EMAIL] To: {invoice.CustomerEmail}");
        Console.WriteLine($"[EMAIL] Subject: Invoice {invoice.InvoiceNumber}");
        Console.WriteLine($"[EMAIL] Body:");
        Console.WriteLine($"       Dear {invoice.CustomerName},");
        Console.WriteLine($"       Your invoice {invoice.InvoiceNumber} has been generated.");
        Console.WriteLine($"       Subtotal: ${invoice.Subtotal:F2}");
        Console.WriteLine($"       Tax: ${invoice.Tax:F2}");
        Console.WriteLine($"       Total: ${invoice.Total:F2}");
        Console.WriteLine($"[EMAIL] Email sent successfully");
    }
}
