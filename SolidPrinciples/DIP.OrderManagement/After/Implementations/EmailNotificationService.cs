using DIP.OrderManagement.After.Abstractions;
using DIP.OrderManagement.After.Models;

namespace DIP.OrderManagement.After.Implementations;

/// <summary>
/// Email notification implementation
/// </summary>
public class EmailNotificationService : INotificationService
{
    public void SendOrderConfirmation(Order order)
    {
        Console.WriteLine($"[EMAIL] Connecting to SMTP server...");
        Console.WriteLine($"[EMAIL] To: {order.CustomerContact}");
        Console.WriteLine($"[EMAIL] Subject: Order Confirmation - {order.OrderId}");
        Console.WriteLine($"[EMAIL] Body: Thank you for your order, {order.CustomerName}!");
        Console.WriteLine($"[EMAIL]       Total: ${order.TotalAmount:F2}");
        Console.WriteLine($"[EMAIL] Email sent successfully");
    }
}
