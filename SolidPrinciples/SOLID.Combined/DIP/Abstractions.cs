using SOLID.Combined.Models;

namespace SOLID.Combined.DIP;

/// <summary>
/// DIP: Dependency Inversion Principle
/// High-level modules depend on abstractions, not concrete implementations
/// Abstractions defined here, implementations can be swapped
/// </summary>

/// <summary>
/// Abstraction for order repository
/// </summary>
public interface IOrderRepository
{
    void Save(Order order);
    Order? GetById(string orderId);
}

/// <summary>
/// Abstraction for inventory management
/// </summary>
public interface IInventoryService
{
    bool CheckAvailability(List<OrderItem> items);
    void ReserveStock(List<OrderItem> items);
}

/// <summary>
/// Abstraction for notification service
/// </summary>
public interface INotificationService
{
    void SendOrderConfirmation(Order order);
    void SendShippingNotification(Order order);
}

/// <summary>
/// Abstraction for logging
/// </summary>
public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message);
}

// ============================================================================
// CONCRETE IMPLEMENTATIONS
// These can be swapped without changing the high-level OrderFulfillmentService
// ============================================================================

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly Dictionary<string, Order> _orders = new();

    public void Save(Order order)
    {
        _orders[order.OrderId] = order;
        Console.WriteLine($"[REPOSITORY] Order {order.OrderId} saved to in-memory storage");
    }

    public Order? GetById(string orderId)
    {
        _orders.TryGetValue(orderId, out var order);
        return order;
    }
}

public class SqlOrderRepository : IOrderRepository
{
    public void Save(Order order)
    {
        Console.WriteLine($"[SQL REPOSITORY] Connecting to SQL database...");
        Console.WriteLine($"[SQL REPOSITORY] INSERT INTO Orders VALUES ('{order.OrderId}', '{order.CustomerId}', {order.Total})");
        Console.WriteLine($"[SQL REPOSITORY] Order saved to SQL database");
    }

    public Order? GetById(string orderId)
    {
        Console.WriteLine($"[SQL REPOSITORY] SELECT * FROM Orders WHERE OrderId = '{orderId}'");
        return null;
    }
}

public class InventoryService : IInventoryService
{
    public bool CheckAvailability(List<OrderItem> items)
    {
        Console.WriteLine($"[INVENTORY] Checking stock for {items.Count} item types");

        foreach (var item in items)
        {
            Console.WriteLine($"[INVENTORY] {item.ProductName}: {item.Quantity} units available");
        }

        return true; // Simulated - all in stock
    }

    public void ReserveStock(List<OrderItem> items)
    {
        Console.WriteLine($"[INVENTORY] Reserving stock for order");

        foreach (var item in items)
        {
            Console.WriteLine($"[INVENTORY] Reserved {item.Quantity} x {item.ProductName}");
        }
    }
}

public class EmailNotificationService : INotificationService
{
    public void SendOrderConfirmation(Order order)
    {
        Console.WriteLine($"[EMAIL] Sending confirmation to {order.CustomerEmail}");
        Console.WriteLine($"[EMAIL] Subject: Order Confirmation - {order.OrderId}");
        Console.WriteLine($"[EMAIL] Total: ${order.Total:F2}");
    }

    public void SendShippingNotification(Order order)
    {
        Console.WriteLine($"[EMAIL] Sending shipping notification to {order.CustomerEmail}");
        Console.WriteLine($"[EMAIL] Tracking: {order.TrackingNumber}");
    }
}

public class SmsNotificationService : INotificationService
{
    public void SendOrderConfirmation(Order order)
    {
        Console.WriteLine($"[SMS] Sending confirmation SMS to customer");
        Console.WriteLine($"[SMS] Message: Order {order.OrderId} confirmed. Total: ${order.Total:F2}");
    }

    public void SendShippingNotification(Order order)
    {
        Console.WriteLine($"[SMS] Sending shipping SMS to customer");
        Console.WriteLine($"[SMS] Message: Order {order.OrderId} shipped. Track: {order.TrackingNumber}");
    }
}

public class ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine($"[LOG INFO] {message}");
    }

    public void LogError(string message)
    {
        Console.WriteLine($"[LOG ERROR] {message}");
    }
}
