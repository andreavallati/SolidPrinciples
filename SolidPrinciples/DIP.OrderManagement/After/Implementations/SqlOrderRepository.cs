using DIP.OrderManagement.After.Abstractions;
using DIP.OrderManagement.After.Models;

namespace DIP.OrderManagement.After.Implementations;

/// <summary>
/// SQL implementation of IOrderRepository
/// Low-level module depending on abstraction
/// </summary>
public class SqlOrderRepository : IOrderRepository
{
    public void Save(Order order)
    {
        Console.WriteLine($"[SQL DB] Connecting to SQL Server database...");
        Console.WriteLine($"[SQL DB] INSERT INTO Orders VALUES ('{order.OrderId}', '{order.CustomerName}', {order.TotalAmount})");
        foreach (var item in order.Items)
        {
            Console.WriteLine($"[SQL DB] INSERT INTO OrderItems VALUES ('{order.OrderId}', '{item.ProductName}', {item.Quantity}, {item.Price})");
        }
        Console.WriteLine($"[SQL DB] Order saved successfully");
    }

    public Order? GetById(string orderId)
    {
        Console.WriteLine($"[SQL DB] SELECT * FROM Orders WHERE OrderId = '{orderId}'");
        return null; // Simulated
    }
}
