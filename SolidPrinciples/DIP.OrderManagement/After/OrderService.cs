using DIP.OrderManagement.After.Abstractions;
using DIP.OrderManagement.After.Models;

namespace DIP.OrderManagement.After;

/// <summary>
/// Following DIP: High-level module depending on abstractions
/// Dependencies are injected through constructor
/// Open for extension, closed for modification
/// </summary>
public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly INotificationService _notificationService;
    private readonly ILogger _logger;

    // Dependencies injected via constructor (Dependency Injection)
    public OrderService(
        IOrderRepository repository,
        INotificationService notificationService,
        ILogger logger)
    {
        _repository = repository;
        _notificationService = notificationService;
        _logger = logger;
    }

    public void ProcessOrder(Order order)
    {
        _logger.Log($"Processing order {order.OrderId}");
        
        // Calculate total
        order.TotalAmount = order.Items.Sum(i => i.Quantity * i.Price);
        order.OrderDate = DateTime.Now;
        
        _logger.Log($"Order total calculated: ${order.TotalAmount:F2}");
        
        // Save to database - works with ANY IOrderRepository implementation
        _repository.Save(order);
        
        // Send notification - works with ANY INotificationService implementation
        _notificationService.SendOrderConfirmation(order);
        
        _logger.Log($"Order {order.OrderId} processed successfully");
    }
}

// Benefits:
// - Can swap SQL for MongoDB without changing this class
// - Can swap Email for SMS without changing this class
// - Easy to test with mock implementations
// - No tight coupling to concrete implementations
// - Follows Dependency Inversion Principle
