using DIP.OrderManagement.After.Models;

namespace DIP.OrderManagement.After.Abstractions;

/// <summary>
/// Abstraction for order persistence
/// High-level and low-level modules both depend on this abstraction
/// </summary>
public interface IOrderRepository
{
    void Save(Order order);
    Order? GetById(string orderId);
}

/// <summary>
/// Abstraction for notifications
/// Can be implemented by Email, SMS, Push notifications, etc.
/// </summary>
public interface INotificationService
{
    void SendOrderConfirmation(Order order);
}

/// <summary>
/// Optional: Abstraction for logging
/// </summary>
public interface ILogger
{
    void Log(string message);
}
