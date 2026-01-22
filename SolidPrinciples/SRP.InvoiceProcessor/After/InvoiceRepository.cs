using SRP.InvoiceProcessor.After.Models;

namespace SRP.InvoiceProcessor.After;

/// <summary>
/// Single Responsibility: Persist invoice data to database
/// Changes only when database schema or data access technology changes
/// </summary>
public class InvoiceRepository
{
    public static void Save(Invoice invoice)
    {
        Console.WriteLine($"[DB] Connecting to database...");
        Console.WriteLine($"[DB] Executing: INSERT INTO Invoices VALUES ('{invoice.InvoiceNumber}', '{invoice.CustomerName}', {invoice.Subtotal}, {invoice.Tax}, {invoice.Total})");

        foreach (var item in invoice.Items)
        {
            Console.WriteLine($"[DB] Executing: INSERT INTO InvoiceItems VALUES ('{invoice.InvoiceNumber}', '{item.Description}', {item.Quantity}, {item.UnitPrice})");
        }

        Console.WriteLine($"[DB] Invoice saved successfully");
    }
}
