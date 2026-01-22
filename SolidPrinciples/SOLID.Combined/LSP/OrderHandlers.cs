using SOLID.Combined.Models;

namespace SOLID.Combined.LSP;

/// <summary>
/// LSP: Liskov Substitution Principle
/// All implementations can be substituted for the base interface
/// No implementation throws NotSupportedException or violates expected behavior
/// </summary>
public interface IOrderHandler
{
    OrderType SupportedType { get; }
    void PrepareForShipment(Order order);
}

/// <summary>
/// Standard order handler - basic processing
/// </summary>
public class StandardOrderHandler : IOrderHandler
{
    public OrderType SupportedType => OrderType.Standard;

    public void PrepareForShipment(Order order)
    {
        Console.WriteLine($"[STANDARD] Preparing standard order {order.OrderId}");
        Console.WriteLine($"[STANDARD] Standard packaging applied");
        Console.WriteLine($"[STANDARD] Estimated delivery: 5 business days");

        order.Status = OrderStatus.ReadyToShip;
    }
}

/// <summary>
/// Express order handler - faster processing with priority
/// Substitutable for IOrderHandler - no violations
/// </summary>
public class ExpressOrderHandler : IOrderHandler
{
    public OrderType SupportedType => OrderType.Express;

    public void PrepareForShipment(Order order)
    {
        Console.WriteLine($"[EXPRESS] Preparing express order {order.OrderId}");
        Console.WriteLine($"[EXPRESS] Priority packaging applied");
        Console.WriteLine($"[EXPRESS] Marked as high priority");
        Console.WriteLine($"[EXPRESS] Estimated delivery: 2 business days");

        order.Status = OrderStatus.ReadyToShip;
    }
}

/// <summary>
/// International order handler - additional customs processing
/// Substitutable for IOrderHandler - no violations
/// </summary>
public class InternationalOrderHandler : IOrderHandler
{
    public OrderType SupportedType => OrderType.International;

    public void PrepareForShipment(Order order)
    {
        Console.WriteLine($"[INTERNATIONAL] Preparing international order {order.OrderId}");
        Console.WriteLine($"[INTERNATIONAL] Customs documentation prepared");
        Console.WriteLine($"[INTERNATIONAL] Export compliance check completed");
        Console.WriteLine($"[INTERNATIONAL] International packaging applied");
        Console.WriteLine($"[INTERNATIONAL] Estimated delivery: 10-14 business days");

        order.Status = OrderStatus.ReadyToShip;
    }
}

/// <summary>
/// Factory to get appropriate handler based on order type
/// Demonstrates polymorphic substitution
/// </summary>
public class OrderHandlerFactory
{
    private readonly Dictionary<OrderType, IOrderHandler> _handlers = new()
    {
        { OrderType.Standard, new StandardOrderHandler() },
        { OrderType.Express, new ExpressOrderHandler() },
        { OrderType.International, new InternationalOrderHandler() }
    };

    public IOrderHandler GetHandler(OrderType orderType)
    {
        return _handlers[orderType];
    }
}
