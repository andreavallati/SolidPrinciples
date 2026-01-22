using SRP.InvoiceProcessor.After.Models;

namespace SRP.InvoiceProcessor.After;

/// <summary>
/// Single Responsibility: Validate invoice data
/// Changes only when validation rules change
/// </summary>
public class InvoiceValidator
{
    public static void Validate(Invoice invoice)
    {
        ArgumentNullException.ThrowIfNull(invoice);

        if (string.IsNullOrWhiteSpace(invoice.CustomerName))
        {
            throw new InvalidOperationException("Customer name is required");
        }

        if (string.IsNullOrWhiteSpace(invoice.CustomerEmail))
        {
            throw new InvalidOperationException("Customer email is required");
        }

        if (invoice.Items == null || invoice.Items.Count == 0)
        {
            throw new InvalidOperationException("Invoice must contain at least one item");
        }

        foreach (var item in invoice.Items)
        {
            if (item.Quantity <= 0)
            {
                throw new InvalidOperationException($"Invalid quantity for item: {item.Description}");
            }

            if (item.UnitPrice < 0)
            {
                throw new InvalidOperationException($"Invalid price for item: {item.Description}");
            }
        }
    }
}
