using DIP.OrderManagement.Before;
using AfterOrder = DIP.OrderManagement.After.Models.Order;

Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine("SOLID PRINCIPLES: Dependency Inversion Principle (DIP)");
Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine();

Console.WriteLine("PRINCIPLE:");
Console.WriteLine("High-level modules should not depend on low-level modules.");
Console.WriteLine("Both should depend on abstractions.");
Console.WriteLine("Abstractions should not depend on details.");
Console.WriteLine("Details should depend on abstractions.");
Console.WriteLine();

// ============================
// BEFORE: Violation of DIP
// ============================
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine("BEFORE: Direct Dependencies on Concrete Implementations");
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine();

Console.WriteLine("PROBLEMS:");
Console.WriteLine("OrderService (high-level) directly creates SqlOrderRepository (low-level)");
Console.WriteLine("OrderService directly creates EmailService (low-level)");
Console.WriteLine("Tightly coupled - cannot swap implementations");
Console.WriteLine("Hard to test - must use real database and email");
Console.WriteLine("Violates Open/Closed - must modify OrderService to change implementations");
Console.WriteLine();

var beforeOrder = new Order
{
    OrderId = "ORD-2024-001",
    CustomerName = "John Smith",
    CustomerEmail = "john.smith@example.com",
    Items =
    [
        new() { ProductName = "Laptop", Quantity = 1, Price = 1299.99m },
        new() { ProductName = "Mouse", Quantity = 2, Price = 29.99m }
    ]
};

var beforeService = new OrderService();
OrderService.ProcessOrder(beforeOrder);

Console.WriteLine("\nWhat if we want to:");
Console.WriteLine("Switch from SQL Server to MongoDB?");
Console.WriteLine("Use SMS instead of Email?");
Console.WriteLine("Add caching or logging?");
Console.WriteLine("Test with mock implementations?");
Console.WriteLine("We must MODIFY the OrderService class!");

Console.WriteLine();
Console.WriteLine();

// ============================
// AFTER: Following DIP
// ============================
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine("AFTER: Dependencies Injected Through Abstractions");
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine();

Console.WriteLine("BENEFITS:");
Console.WriteLine("OrderService depends on IOrderRepository and INotificationService interfaces");
Console.WriteLine("Concrete implementations injected via constructor");
Console.WriteLine("Easy to swap implementations without modifying OrderService");
Console.WriteLine("Easy to test with mock implementations");
Console.WriteLine("Loose coupling, high flexibility");
Console.WriteLine();

// Example 1: Using SQL + Email (same as before, but properly decoupled)
Console.WriteLine("\nScenario 1: SQL Database + Email Notifications");
var order1 = new AfterOrder
{
    OrderId = "ORD-2024-002",
    CustomerName = "Jane Doe",
    CustomerContact = "jane.doe@example.com",
    Items =
    [
        new() { ProductName = "Smartphone", Quantity = 1, Price = 899.99m },
        new() { ProductName = "Case", Quantity = 1, Price = 19.99m }
    ]
};

var sqlRepo = new DIP.OrderManagement.After.Implementations.SqlOrderRepository();
var emailNotifier = new DIP.OrderManagement.After.Implementations.EmailNotificationService();
var logger = new DIP.OrderManagement.After.Implementations.ConsoleLogger();

var service1 = new DIP.OrderManagement.After.OrderService(sqlRepo, emailNotifier, logger);
service1.ProcessOrder(order1);

// Example 2: Switching to MongoDB + SMS (NO changes to OrderService!)
Console.WriteLine("\n" + "─".PadRight(80, '─'));
Console.WriteLine("Scenario 2: MongoDB Database + SMS Notifications");
Console.WriteLine("(OrderService unchanged - just different implementations injected!)");
var order2 = new AfterOrder
{
    OrderId = "ORD-2024-003",
    CustomerName = "Mike Johnson",
    CustomerContact = "+1-555-0123",
    Items =
    [
        new() { ProductName = "Tablet", Quantity = 2, Price = 499.99m },
        new() { ProductName = "Stylus", Quantity = 2, Price = 79.99m }
    ]
};

var mongoRepo = new DIP.OrderManagement.After.Implementations.MongoOrderRepository("mongodb://localhost:27017/orders");
var smsNotifier = new DIP.OrderManagement.After.Implementations.SmsNotificationService();

var service2 = new DIP.OrderManagement.After.OrderService(mongoRepo, smsNotifier, logger);
service2.ProcessOrder(order2);

// Example 3: Using Push Notifications (mobile app scenario)
Console.WriteLine("\n" + "─".PadRight(80, '─'));
Console.WriteLine("Scenario 3: SQL Database + Push Notifications (Mobile App)");
Console.WriteLine("(OrderService still unchanged - just another implementation!)");
var order3 = new AfterOrder
{
    OrderId = "ORD-2024-004",
    CustomerName = "Sarah Williams",
    CustomerContact = "device_token_abc123xyz",
    Items =
    [
        new() { ProductName = "Headphones", Quantity = 1, Price = 199.99m },
        new() { ProductName = "Charging Cable", Quantity = 3, Price = 12.99m }
    ]
};

var pushNotifier = new DIP.OrderManagement.After.Implementations.PushNotificationService();

var service3 = new DIP.OrderManagement.After.OrderService(sqlRepo, pushNotifier, logger);
service3.ProcessOrder(order3);

Console.WriteLine();
Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine("KEY TAKEAWAY:");
Console.WriteLine("Depend on abstractions (interfaces), not concrete implementations.");
Console.WriteLine("Inject dependencies rather than creating them internally.");
Console.WriteLine("This enables flexibility, testability, and maintainability.");
Console.WriteLine();
Console.WriteLine("Real-world usage: Dependency Injection containers (e.g., ASP.NET Core DI)");
Console.WriteLine("automatically inject the right implementations at runtime.");
Console.WriteLine("=".PadRight(80, '='));
