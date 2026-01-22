namespace SOLID.Combined.Models;

/// <summary>
/// Core domain models for the e-commerce order system
/// </summary>

public class Order
{
    public string OrderId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public List<OrderItem> Items { get; set; } = [];
    public OrderType Type { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Subtotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public string? TrackingNumber { get; set; }
    public PaymentInfo? Payment { get; set; }
}

public class OrderItem
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;
}

public enum OrderType
{
    Standard,
    Express,
    International
}

public enum OrderStatus
{
    Created,
    Validated,
    PaymentProcessed,
    ReadyToShip,
    Shipped,
    Delivered,
    Cancelled
}

public class PaymentInfo
{
    public string PaymentMethod { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public bool IsProcessed { get; set; }
}

public class ShippingInfo
{
    public string Carrier { get; set; } = string.Empty;
    public string TrackingNumber { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public int EstimatedDays { get; set; }
}

public class InventoryItem
{
    public string ProductId { get; set; } = string.Empty;
    public int AvailableQuantity { get; set; }
}
