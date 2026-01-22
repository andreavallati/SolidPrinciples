using DIP.OrderManagement.After.Abstractions;
using DIP.OrderManagement.After.Models;

namespace DIP.OrderManagement.After.Implementations;

/// <summary>
/// SMS notification implementation
/// Can be swapped without changing OrderService
/// </summary>
public class SmsNotificationService : INotificationService
{
    public void SendOrderConfirmation(Order order)
    {
        Console.WriteLine($"[SMS] Connecting to SMS gateway...");
        Console.WriteLine($"[SMS] To: {order.CustomerContact}");
        Console.WriteLine($"[SMS] Message: Order {order.OrderId} confirmed!");
        Console.WriteLine($"[SMS]          Total: ${order.TotalAmount:F2}");
        Console.WriteLine($"[SMS]          Thank you, {order.CustomerName}!");
        Console.WriteLine($"[SMS] SMS sent successfully");
    }
}
