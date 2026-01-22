using SOLID.Combined.Models;

namespace SOLID.Combined.SRP;

/// <summary>
/// SRP: Single Responsibility - Validates order data
/// Only one reason to change: validation rules change
/// </summary>
public class OrderValidator
{
    public ValidationResult Validate(Order order)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(order.CustomerId))
        {
            errors.Add("Customer ID is required");
        }

        if (string.IsNullOrWhiteSpace(order.CustomerEmail))
        {
            errors.Add("Customer email is required");
        }

        if (string.IsNullOrWhiteSpace(order.ShippingAddress))
        {
            errors.Add("Shipping address is required");
        }

        if (order.Items == null || order.Items.Count == 0)
        {
            errors.Add("Order must contain at least one item");
        }

        foreach (var item in order.Items ?? [])
        {
            if (item.Quantity <= 0)
            {
                errors.Add($"Invalid quantity for {item.ProductName}");
            }

            if (item.UnitPrice <= 0)
            {
                errors.Add($"Invalid price for {item.ProductName}");
            }
        }

        return new ValidationResult
        {
            IsValid = errors.Count == 0,
            Errors = errors
        };
    }
}

/// <summary>
/// SRP: Single Responsibility - Calculates order pricing
/// Only one reason to change: pricing rules change
/// </summary>
public class OrderPricingCalculator
{
    private const decimal TaxRate = 0.20m; // 20%

    public void Calculate(Order order)
    {
        // Calculate subtotal
        order.Subtotal = order.Items.Sum(i => i.Total);

        // Tax calculation
        order.TaxAmount = order.Subtotal * TaxRate;

        // Total
        order.Total = order.Subtotal + order.TaxAmount + order.ShippingCost - order.DiscountAmount;

        Console.WriteLine($"[PRICING] Subtotal: ${order.Subtotal:F2}");
        Console.WriteLine($"[PRICING] Tax: ${order.TaxAmount:F2}");
        Console.WriteLine($"[PRICING] Shipping: ${order.ShippingCost:F2}");
        Console.WriteLine($"[PRICING] Discount: -${order.DiscountAmount:F2}");
        Console.WriteLine($"[PRICING] Total: ${order.Total:F2}");
    }
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = [];
}
