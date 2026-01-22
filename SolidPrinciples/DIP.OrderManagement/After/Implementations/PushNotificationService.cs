using DIP.OrderManagement.After.Abstractions;
using DIP.OrderManagement.After.Models;

namespace DIP.OrderManagement.After.Implementations;

/// <summary>
/// Push notification implementation (mobile apps)
/// Another implementation showing extensibility
/// </summary>
public class PushNotificationService : INotificationService
{
    public void SendOrderConfirmation(Order order)
    {
        Console.WriteLine($"[PUSH] Sending push notification...");
        Console.WriteLine($"[PUSH] Device token: {order.CustomerContact}");
        Console.WriteLine($"[PUSH] Title: Order Confirmed!");
        Console.WriteLine($"[PUSH] Body: Your order {order.OrderId} (${order.TotalAmount:F2}) is being processed");
        Console.WriteLine($"[PUSH] Push notification sent successfully");
    }
}
