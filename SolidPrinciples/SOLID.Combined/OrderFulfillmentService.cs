using SOLID.Combined.DIP;
using SOLID.Combined.LSP;
using SOLID.Combined.Models;
using SOLID.Combined.OCP;
using SOLID.Combined.SRP;

namespace SOLID.Combined;

/// <summary>
/// Main orchestrator demonstrating ALL SOLID principles working together
/// 
/// SRP: Each class has single responsibility (validation, calculation, etc.)
/// OCP: Extensible through strategies (discount, shipping, payment)
/// LSP: Order handlers are substitutable
/// ISP: Segregated interfaces for different capabilities
/// DIP: Depends on abstractions (IOrderRepository, INotificationService, etc.)
/// </summary>
public class OrderFulfillmentService
{
    // DIP: Depend on abstractions, not concrete implementations
    private readonly OrderValidator _validator;
    private readonly OrderPricingCalculator _calculator;
    private readonly IOrderRepository _repository;
    private readonly IInventoryService _inventoryService;
    private readonly INotificationService _notificationService;
    private readonly ILogger _logger;
    private readonly OrderHandlerFactory _orderHandlerFactory;

    // All dependencies injected through constructor (Dependency Injection)
    public OrderFulfillmentService(
        OrderValidator validator,
        OrderPricingCalculator calculator,
        IOrderRepository repository,
        IInventoryService inventoryService,
        INotificationService notificationService,
        ILogger logger,
        OrderHandlerFactory orderHandlerFactory)
    {
        _validator = validator;
        _calculator = calculator;
        _repository = repository;
        _inventoryService = inventoryService;
        _notificationService = notificationService;
        _logger = logger;
        _orderHandlerFactory = orderHandlerFactory;
    }

    /// <summary>
    /// Main workflow orchestrating all steps
    /// Uses OCP strategies for flexible behavior
    /// </summary>
    public bool ProcessOrder(
        Order order,
        IDiscountStrategy discountStrategy,
        IShippingProvider shippingProvider,
        IPaymentProcessor paymentProcessor)
    {
        _logger.LogInfo($"Starting order fulfillment for {order.OrderId}");

        try
        {
            // STEP 1: Validation (SRP - separate validator)
            Console.WriteLine("\n--- STEP 1: VALIDATION ---");
            var validationResult = _validator.Validate(order);
            if (!validationResult.IsValid)
            {
                _logger.LogError($"Validation failed: {string.Join(", ", validationResult.Errors)}");
                return false;
            }
            order.Status = OrderStatus.Validated;
            _logger.LogInfo("Order validated successfully");

            // STEP 2: Inventory Check (DIP - injected service)
            Console.WriteLine("\n--- STEP 2: INVENTORY CHECK ---");
            if (!_inventoryService.CheckAvailability(order.Items))
            {
                _logger.LogError("Insufficient inventory");
                return false;
            }
            _inventoryService.ReserveStock(order.Items);

            // STEP 3: Apply Discount (OCP - extensible strategy)
            Console.WriteLine("\n--- STEP 3: APPLY DISCOUNT ---");
            order.DiscountAmount = discountStrategy.CalculateDiscount(order);
            _logger.LogInfo($"Discount applied: {discountStrategy.Name}");

            // STEP 4: Calculate Shipping (OCP - extensible provider)
            Console.WriteLine("\n--- STEP 4: CALCULATE SHIPPING ---");
            var shippingInfo = shippingProvider.CalculateShipping(order);
            order.ShippingCost = shippingInfo.Cost;
            _logger.LogInfo($"Shipping calculated: {shippingProvider.Name}");

            // STEP 5: Calculate Totals (SRP - separate calculator)
            Console.WriteLine("\n--- STEP 5: CALCULATE PRICING ---");
            _calculator.Calculate(order);

            // STEP 6: Process Payment (OCP - extensible processor)
            Console.WriteLine("\n--- STEP 6: PROCESS PAYMENT ---");
            order.Payment = paymentProcessor.ProcessPayment(order);
            order.Status = OrderStatus.PaymentProcessed;
            _logger.LogInfo($"Payment processed: {paymentProcessor.Name}");

            // STEP 7: Prepare for Shipment (LSP - substitutable handlers)
            Console.WriteLine("\n--- STEP 7: PREPARE SHIPMENT ---");
            var orderHandler = _orderHandlerFactory.GetHandler(order.Type);
            orderHandler.PrepareForShipment(order);

            // Generate tracking number
            order.TrackingNumber = $"{shippingInfo.Carrier}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
            order.Status = OrderStatus.Shipped;
            order.ShippedDate = DateTime.Now;

            // STEP 8: Save Order (DIP - injected repository)
            Console.WriteLine("\n--- STEP 8: SAVE ORDER ---");
            _repository.Save(order);

            // STEP 9: Send Notifications (DIP - injected notification service)
            Console.WriteLine("\n--- STEP 9: SEND NOTIFICATIONS ---");
            _notificationService.SendOrderConfirmation(order);
            _notificationService.SendShippingNotification(order);

            _logger.LogInfo($"Order {order.OrderId} fulfilled successfully!");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Order processing failed: {ex.Message}");
            return false;
        }
    }
}
