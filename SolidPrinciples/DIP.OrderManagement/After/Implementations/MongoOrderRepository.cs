using DIP.OrderManagement.After.Abstractions;
using DIP.OrderManagement.After.Models;

namespace DIP.OrderManagement.After.Implementations;

/// <summary>
/// MongoDB implementation of IOrderRepository
/// Can be swapped without changing OrderService
/// </summary>
public class MongoOrderRepository : IOrderRepository
{
    private readonly string _connectionString;

    public MongoOrderRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Save(Order order)
    {
        Console.WriteLine($"[MONGO DB] Connecting to MongoDB: {_connectionString}");
        Console.WriteLine($"[MONGO DB] db.orders.insertOne({{");
        Console.WriteLine($"[MONGO DB]   orderId: '{order.OrderId}',");
        Console.WriteLine($"[MONGO DB]   customerName: '{order.CustomerName}',");
        Console.WriteLine($"[MONGO DB]   totalAmount: {order.TotalAmount},");
        Console.WriteLine($"[MONGO DB]   items: [{order.Items.Count} items]");
        Console.WriteLine($"[MONGO DB] }})");
        Console.WriteLine($"[MONGO DB] Order saved successfully");
    }

    public Order? GetById(string orderId)
    {
        Console.WriteLine($"[MONGO DB] db.orders.findOne({{ orderId: '{orderId}' }})");
        return null; // Simulated
    }
}
