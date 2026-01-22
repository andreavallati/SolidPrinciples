using SRP.InvoiceProcessor.After.Models;

namespace SRP.InvoiceProcessor.After;

/// <summary>
/// Single Responsibility: Calculate invoice totals and taxes
/// Changes only when calculation or tax rules change
/// </summary>
public class InvoiceCalculator
{
    private const decimal TaxRate = 0.20m; // 20% VAT

    public void Calculate(Invoice invoice)
    {
        decimal subtotal = 0;

        foreach (var item in invoice.Items)
        {
            subtotal += item.Quantity * item.UnitPrice;
        }

        invoice.Subtotal = subtotal;
        invoice.Tax = subtotal * TaxRate;
        invoice.Total = invoice.Subtotal + invoice.Tax;
    }
}
