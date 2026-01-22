namespace DIP.OrderManagement.Before;

public class Order
{
    public string OrderId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public List<OrderItem> Items { get; set; } = [];
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
}

public class OrderItem
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

/// <summary>
/// Concrete implementation - tightly coupled to OrderService
/// </summary>
public class SqlOrderRepository
{
    public static void Save(Order order)
    {
        Console.WriteLine($"[SQL DB] Connecting to SQL Server database...");
        Console.WriteLine($"[SQL DB] INSERT INTO Orders VALUES ('{order.OrderId}', '{order.CustomerName}', {order.TotalAmount})");
        foreach (var item in order.Items)
        {
            Console.WriteLine($"[SQL DB] INSERT INTO OrderItems VALUES ('{order.OrderId}', '{item.ProductName}', {item.Quantity}, {item.Price})");
        }
        Console.WriteLine($"[SQL DB] Order saved successfully");
    }
}

/// <summary>
/// Concrete implementation - tightly coupled to OrderService
/// </summary>
public class EmailService
{
    public static void SendConfirmation(Order order)
    {
        Console.WriteLine($"[EMAIL] Connecting to SMTP server...");
        Console.WriteLine($"[EMAIL] To: {order.CustomerEmail}");
        Console.WriteLine($"[EMAIL] Subject: Order Confirmation - {order.OrderId}");
        Console.WriteLine($"[EMAIL] Body: Thank you for your order. Total: ${order.TotalAmount:F2}");
        Console.WriteLine($"[EMAIL] Email sent successfully");
    }
}

/// <summary>
/// PROBLEM: This class violates Dependency Inversion Principle
/// High-level module (OrderService) depends on low-level modules (SqlOrderRepository, EmailService)
/// Both high-level and low-level modules should depend on abstractions
/// </summary>
public class OrderService
{
    // Direct dependencies on concrete implementations
    private readonly SqlOrderRepository _repository;
    private readonly EmailService _emailService;

    public OrderService()
    {
        // OrderService creates and owns its dependencies
        // Tightly coupled to specific implementations
        _repository = new SqlOrderRepository();
        _emailService = new EmailService();
    }

    public static void ProcessOrder(Order order)
    {
        Console.WriteLine($"\n[ORDER SERVICE] Processing order {order.OrderId}");

        // Calculate total
        order.TotalAmount = order.Items.Sum(i => i.Quantity * i.Price);
        order.OrderDate = DateTime.Now;

        Console.WriteLine($"[ORDER SERVICE] Order total: ${order.TotalAmount:F2}");

        // Save to database - tied to SQL
        SqlOrderRepository.Save(order);

        // Send confirmation - tied to Email
        EmailService.SendConfirmation(order);

        Console.WriteLine($"[ORDER SERVICE] Order {order.OrderId} processed successfully");
    }
}

// What if we want to:
// - Switch from SQL to NoSQL database?
// - Use SMS instead of Email?
// - Add caching layer?
// - Test with mock implementations?
// We must MODIFY OrderService class!
