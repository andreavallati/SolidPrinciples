using SOLID.Combined.Models;

namespace SOLID.Combined.OCP;

/// <summary>
/// OCP: Open for extension, closed for modification
/// New discount strategies can be added without modifying existing code
/// </summary>
public interface IDiscountStrategy
{
    string Name { get; }
    decimal CalculateDiscount(Order order);
}

public class NoDiscount : IDiscountStrategy
{
    public string Name => "No Discount";

    public decimal CalculateDiscount(Order order) => 0m;
}

public class PercentageDiscount : IDiscountStrategy
{
    private readonly decimal _percentage;

    public PercentageDiscount(decimal percentage)
    {
        _percentage = percentage;
    }

    public string Name => $"{_percentage}% Off";

    public decimal CalculateDiscount(Order order)
    {
        var discount = order.Subtotal * (_percentage / 100);
        Console.WriteLine($"[DISCOUNT] Applying {_percentage}% discount: ${discount:F2}");
        return discount;
    }
}

public class BulkOrderDiscount : IDiscountStrategy
{
    public string Name => "Bulk Order Discount";

    public decimal CalculateDiscount(Order order)
    {
        var totalItems = order.Items.Sum(i => i.Quantity);

        if (totalItems >= 20)
        {
            var discount = order.Subtotal * 0.15m; // 15% for 20+ items
            Console.WriteLine($"[DISCOUNT] Bulk order ({totalItems} items): ${discount:F2}");
            return discount;
        }

        return 0m;
    }
}

public class VIPCustomerDiscount : IDiscountStrategy
{
    private readonly List<string> _vipCustomers;

    public VIPCustomerDiscount(List<string> vipCustomers)
    {
        _vipCustomers = vipCustomers;
    }

    public string Name => "VIP Customer Discount";

    public decimal CalculateDiscount(Order order)
    {
        if (_vipCustomers.Contains(order.CustomerId))
        {
            var discount = order.Subtotal * 0.10m; // 10% for VIP
            Console.WriteLine($"[DISCOUNT] VIP customer discount: ${discount:F2}");
            return discount;
        }

        return 0m;
    }
}

/// <summary>
/// OCP: Extensible shipping providers
/// New providers can be added without modifying existing code
/// </summary>
public interface IShippingProvider
{
    string Name { get; }
    ShippingInfo CalculateShipping(Order order);
}

public class StandardShipping : IShippingProvider
{
    public string Name => "Standard Shipping";

    public ShippingInfo CalculateShipping(Order order)
    {
        Console.WriteLine($"[SHIPPING] Standard shipping selected");
        return new ShippingInfo
        {
            Carrier = "USPS",
            Cost = 9.99m,
            EstimatedDays = 5
        };
    }
}

public class ExpressShipping : IShippingProvider
{
    public string Name => "Express Shipping";

    public ShippingInfo CalculateShipping(Order order)
    {
        Console.WriteLine($"[SHIPPING] Express shipping selected");
        return new ShippingInfo
        {
            Carrier = "FedEx",
            Cost = 24.99m,
            EstimatedDays = 2
        };
    }
}

public class InternationalShipping : IShippingProvider
{
    public string Name => "International Shipping";

    public ShippingInfo CalculateShipping(Order order)
    {
        Console.WriteLine($"[SHIPPING] International shipping selected");
        return new ShippingInfo
        {
            Carrier = "DHL",
            Cost = 49.99m,
            EstimatedDays = 10
        };
    }
}

/// <summary>
/// OCP: Extensible payment processors
/// New payment methods can be added without modifying existing code
/// </summary>
public interface IPaymentProcessor
{
    string Name { get; }
    PaymentInfo ProcessPayment(Order order);
}

public class CreditCardPayment : IPaymentProcessor
{
    public string Name => "Credit Card";

    public PaymentInfo ProcessPayment(Order order)
    {
        Console.WriteLine($"[PAYMENT] Processing credit card payment: ${order.Total:F2}");
        return new PaymentInfo
        {
            PaymentMethod = "Credit Card",
            TransactionId = $"CC-{Guid.NewGuid().ToString()[..8]}",
            Amount = order.Total,
            IsProcessed = true
        };
    }
}

public class PayPalPayment : IPaymentProcessor
{
    public string Name => "PayPal";

    public PaymentInfo ProcessPayment(Order order)
    {
        Console.WriteLine($"[PAYMENT] Processing PayPal payment: ${order.Total:F2}");
        return new PaymentInfo
        {
            PaymentMethod = "PayPal",
            TransactionId = $"PP-{Guid.NewGuid().ToString()[..8]}",
            Amount = order.Total,
            IsProcessed = true
        };
    }
}
