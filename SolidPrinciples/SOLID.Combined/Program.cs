using SOLID.Combined;
using SOLID.Combined.DIP;
using SOLID.Combined.ISP;
using SOLID.Combined.LSP;
using SOLID.Combined.Models;
using SOLID.Combined.OCP;
using SOLID.Combined.SRP;

Console.WriteLine("=".PadRight(100, '='));
Console.WriteLine("SOLID PRINCIPLES: COMBINED EXAMPLE - E-Commerce Order Fulfillment System");
Console.WriteLine("=".PadRight(100, '='));
Console.WriteLine();

Console.WriteLine("This example demonstrates ALL 5 SOLID principles working together:");
Console.WriteLine("[SRP] Single Responsibility - Each class has one job");
Console.WriteLine("[OCP] Open/Closed - Extensible through strategies");
Console.WriteLine("[LSP] Liskov Substitution - Order handlers are substitutable");
Console.WriteLine("[ISP] Interface Segregation - Capabilities through separate interfaces");
Console.WriteLine("[DIP] Dependency Inversion - Depends on abstractions, not concrete classes");
Console.WriteLine();
Console.WriteLine("=".PadRight(100, '='));
Console.WriteLine();

// ============================================================================
// SCENARIO 1: Standard Order with VIP Customer
// ============================================================================
Console.WriteLine("=".PadRight(100, '='));
Console.WriteLine("SCENARIO 1: STANDARD ORDER - VIP CUSTOMER");
Console.WriteLine("=".PadRight(100, '='));

var order1 = new Order
{
    OrderId = "ORD-2024-001",
    CustomerId = "CUST-VIP-001",
    CustomerName = "Alice Johnson",
    CustomerEmail = "alice.johnson@example.com",
    ShippingAddress = "123 Main St, New York, NY 10001",
    Type = OrderType.Standard,
    Items =
    [
        new() { ProductId = "PROD-001", ProductName = "Laptop", Quantity = 1, UnitPrice = 1299.99m },
        new() { ProductId = "PROD-002", ProductName = "Mouse", Quantity = 2, UnitPrice = 29.99m },
        new() { ProductId = "PROD-003", ProductName = "Keyboard", Quantity = 1, UnitPrice = 89.99m }
    ]
};

// DIP: Configure dependencies (can easily swap implementations)
var validator = new OrderValidator();
var calculator = new OrderPricingCalculator();
var repository = new InMemoryOrderRepository();
var inventoryService = new InventoryService();
var notificationService = new EmailNotificationService();
var logger = new ConsoleLogger();
var orderHandlerFactory = new OrderHandlerFactory();

var fulfillmentService = new OrderFulfillmentService(
    validator,
    calculator,
    repository,
    inventoryService,
    notificationService,
    logger,
    orderHandlerFactory
);

// OCP: Use different strategies for different scenarios
var vipDiscount = new VIPCustomerDiscount(["CUST-VIP-001", "CUST-VIP-002"]);
var standardShipping = new StandardShipping();
var creditCardPayment = new CreditCardPayment();

var success1 = fulfillmentService.ProcessOrder(order1, vipDiscount, standardShipping, creditCardPayment);

if (success1)
{
    Console.WriteLine("\nORDER COMPLETED SUCCESSFULLY");
    Console.WriteLine($"Order ID: {order1.OrderId}");
    Console.WriteLine($"Status: {order1.Status}");
    Console.WriteLine($"Total: ${order1.Total:F2}");
    Console.WriteLine($"Tracking: {order1.TrackingNumber}");

    // ISP: Use segregated interfaces to demonstrate capabilities
    Console.WriteLine("\n--- ISP: Testing Order Capabilities ---");
    var standardOrder = new StandardOrder(order1);

    if (standardOrder is ICancellableOrder cancellable)
    {
        Console.WriteLine($"Order can be cancelled: {cancellable.CanBeCancelled()}");
    }

    if (standardOrder is IModifiableOrder modifiable)
    {
        Console.WriteLine($"Order can be modified: {modifiable.CanBeModified()}");
        modifiable.UpdateShippingAddress("456 New Address, Boston, MA 02101");
    }
}

// ============================================================================
// SCENARIO 2: Express Order with Bulk Discount
// ============================================================================
Console.WriteLine();
Console.WriteLine("=".PadRight(100, '='));
Console.WriteLine("SCENARIO 2: EXPRESS ORDER - BULK DISCOUNT");
Console.WriteLine("=".PadRight(100, '='));

var order2 = new Order
{
    OrderId = "ORD-2024-002",
    CustomerId = "CUST-002",
    CustomerName = "Bob Smith",
    CustomerEmail = "bob.smith@example.com",
    ShippingAddress = "789 Oak Ave, San Francisco, CA 94102",
    Type = OrderType.Express,
    Items =
    [
        new() { ProductId = "PROD-004", ProductName = "Office Chair", Quantity = 25, UnitPrice = 199.99m }
    ]
};

// OCP: Different strategies for this order
var bulkDiscount = new BulkOrderDiscount();
var expressShipping = new ExpressShipping();
var paypalPayment = new PayPalPayment();

var success2 = fulfillmentService.ProcessOrder(order2, bulkDiscount, expressShipping, paypalPayment);

if (success2)
{
    Console.WriteLine("\nORDER COMPLETED SUCCESSFULLY");
    Console.WriteLine($"Order ID: {order2.OrderId}");
    Console.WriteLine($"Status: {order2.Status}");
    Console.WriteLine($"Total: ${order2.Total:F2}");
    Console.WriteLine($"Tracking: {order2.TrackingNumber}");
}

// ============================================================================
// SCENARIO 3: International Order
// ============================================================================
Console.WriteLine();
Console.WriteLine("=".PadRight(100, '='));
Console.WriteLine("SCENARIO 3: INTERNATIONAL ORDER");
Console.WriteLine("=".PadRight(100, '='));

var order3 = new Order
{
    OrderId = "ORD-2024-003",
    CustomerId = "CUST-003",
    CustomerName = "Carlos Rodriguez",
    CustomerEmail = "carlos@example.com",
    ShippingAddress = "Avenida Principal 123, Madrid, Spain",
    Type = OrderType.International,
    Items =
    [
        new() { ProductId = "PROD-005", ProductName = "Tablet", Quantity = 3, UnitPrice = 599.99m }
    ]
};

// OCP: International order configuration
var percentageDiscount = new PercentageDiscount(5); // 5% off
var internationalShipping = new InternationalShipping();
var creditCard2 = new CreditCardPayment();

// DIP: Same service, different implementations can be injected
// Let's use SMS notifications instead of Email
var smsNotificationService = new SmsNotificationService();
var fulfillmentService2 = new OrderFulfillmentService(
    validator,
    calculator,
    repository,
    inventoryService,
    smsNotificationService,  // Different implementation!
    logger,
    orderHandlerFactory
);

var success3 = fulfillmentService2.ProcessOrder(order3, percentageDiscount, internationalShipping, creditCard2);

if (success3)
{
    Console.WriteLine("\nORDER COMPLETED SUCCESSFULLY");
    Console.WriteLine($"Order ID: {order3.OrderId}");
    Console.WriteLine($"Status: {order3.Status}");
    Console.WriteLine($"Total: ${order3.Total:F2}");
    Console.WriteLine($"Tracking: {order3.TrackingNumber}");

    // ISP: Shipped order has different capabilities
    Console.WriteLine("\n--- ISP: Shipped Order Capabilities ---");
    var shippedOrder = new ShippedOrder(order3);

    if (shippedOrder is ITrackableOrder trackable)
    {
        Console.WriteLine($"Tracking Number: {trackable.GetTrackingNumber()}");
        Console.WriteLine($"Tracking URL: {trackable.GetTrackingUrl()}");
    }

    if (shippedOrder is IRefundableOrder refundable)
    {
        Console.WriteLine($"Can be refunded: {refundable.CanBeRefunded()}");
        refundable.ProcessRefund(order3.Total / 2); // Partial refund
    }
}

// ============================================================================
// SUMMARY
// ============================================================================
Console.WriteLine("\n\n" + "=".PadRight(100, '='));
Console.WriteLine("SOLID PRINCIPLES SUMMARY");
Console.WriteLine("=".PadRight(100, '='));
Console.WriteLine();
Console.WriteLine("[SRP] Single Responsibility Principle:");
Console.WriteLine("- OrderValidator - only validates");
Console.WriteLine("- OrderPricingCalculator - only calculates prices");
Console.WriteLine("- OrderFulfillmentService - only orchestrates workflow");
Console.WriteLine();
Console.WriteLine("[OCP] Open/Closed Principle:");
Console.WriteLine("- Added VIPCustomerDiscount without modifying existing code");
Console.WriteLine("- Easily add new payment methods, shipping providers, discount strategies");
Console.WriteLine("- New order types can be added without changing core logic");
Console.WriteLine();
Console.WriteLine("[LSP] Liskov Substitution Principle:");
Console.WriteLine("- StandardOrderHandler, ExpressOrderHandler, InternationalOrderHandler");
Console.WriteLine("- All can be substituted for IOrderHandler without breaking behavior");
Console.WriteLine();
Console.WriteLine("[ISP] Interface Segregation Principle:");
Console.WriteLine("- ICancellableOrder, ITrackableOrder, IRefundableOrder, IModifiableOrder");
Console.WriteLine("- Orders implement only interfaces for operations they support");
Console.WriteLine("- No NotSupportedException - compile-time safety");
Console.WriteLine();
Console.WriteLine("[DIP] Dependency Inversion Principle:");
Console.WriteLine("- OrderFulfillmentService depends on IOrderRepository, INotificationService");
Console.WriteLine("- Easily swapped EmailNotificationService for SmsNotificationService");
Console.WriteLine("- Easy to test with mock implementations");
Console.WriteLine();
Console.WriteLine("=".PadRight(100, '='));
Console.WriteLine("All 5 SOLID principles working together in harmony!");
Console.WriteLine("=".PadRight(100, '='));
