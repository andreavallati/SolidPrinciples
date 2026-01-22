using SOLID.Combined.Models;

namespace SOLID.Combined.ISP;

/// <summary>
/// ISP: Interface Segregation Principle
/// Clients should not be forced to depend on interfaces they don't use
/// Each interface represents a specific capability
/// </summary>

/// <summary>
/// Base interface - all orders support this
/// </summary>
public interface IOrder
{
    string OrderId { get; }
    OrderStatus Status { get; }
}

/// <summary>
/// Orders that can be cancelled
/// Not all orders support cancellation (e.g., already shipped)
/// </summary>
public interface ICancellableOrder : IOrder
{
    bool CanBeCancelled();
    void Cancel();
}

/// <summary>
/// Orders that can be tracked
/// Only shipped orders support tracking
/// </summary>
public interface ITrackableOrder : IOrder
{
    string? GetTrackingNumber();
    string GetTrackingUrl();
}

/// <summary>
/// Orders that can be refunded
/// Only paid orders can be refunded
/// </summary>
public interface IRefundableOrder : IOrder
{
    bool CanBeRefunded();
    void ProcessRefund(decimal amount);
}

/// <summary>
/// Orders that can be modified
/// Only orders not yet shipped can be modified
/// </summary>
public interface IModifiableOrder : IOrder
{
    bool CanBeModified();
    void UpdateShippingAddress(string newAddress);
}

/// <summary>
/// Standard order - supports most capabilities
/// Implements only the interfaces for operations it can perform
/// </summary>
public class StandardOrder : ICancellableOrder, IRefundableOrder, IModifiableOrder
{
    private readonly Order _order;

    public StandardOrder(Order order)
    {
        _order = order;
    }

    public string OrderId => _order.OrderId;
    public OrderStatus Status => _order.Status;

    // ICancellableOrder
    public bool CanBeCancelled()
    {
        return _order.Status != OrderStatus.Shipped && _order.Status != OrderStatus.Delivered;
    }

    public void Cancel()
    {
        if (CanBeCancelled())
        {
            _order.Status = OrderStatus.Cancelled;
            Console.WriteLine($"[CANCEL] Order {OrderId} has been cancelled");
        }
        else
        {
            Console.WriteLine($"[CANCEL] Order {OrderId} cannot be cancelled (Status: {Status})");
        }
    }

    // IRefundableOrder
    public bool CanBeRefunded()
    {
        return _order.Payment?.IsProcessed == true;
    }

    public void ProcessRefund(decimal amount)
    {
        if (CanBeRefunded())
        {
            Console.WriteLine($"[REFUND] Processing refund of ${amount:F2} for order {OrderId}");
            Console.WriteLine($"[REFUND] Refund completed");
        }
        else
        {
            Console.WriteLine($"[REFUND] Order {OrderId} cannot be refunded");
        }
    }

    // IModifiableOrder
    public bool CanBeModified()
    {
        return _order.Status == OrderStatus.Created || _order.Status == OrderStatus.Validated;
    }

    public void UpdateShippingAddress(string newAddress)
    {
        if (CanBeModified())
        {
            _order.ShippingAddress = newAddress;
            Console.WriteLine($"[MODIFY] Shipping address updated to: {newAddress}");
        }
        else
        {
            Console.WriteLine($"[MODIFY] Order {OrderId} cannot be modified (Status: {Status})");
        }
    }
}

/// <summary>
/// Shipped order - only supports tracking and refunds
/// Does NOT implement ICancellableOrder or IModifiableOrder
/// Following ISP: Only implements what it can actually do
/// </summary>
public class ShippedOrder : ITrackableOrder, IRefundableOrder
{
    private readonly Order _order;

    public ShippedOrder(Order order)
    {
        _order = order;
    }

    public string OrderId => _order.OrderId;
    public OrderStatus Status => _order.Status;

    // ITrackableOrder
    public string? GetTrackingNumber()
    {
        return _order.TrackingNumber;
    }

    public string GetTrackingUrl()
    {
        var carrier = _order.TrackingNumber?.StartsWith("USPS") == true ? "USPS" : "FedEx";
        return $"https://{carrier.ToLower()}.com/track?number={_order.TrackingNumber}";
    }

    // IRefundableOrder
    public bool CanBeRefunded()
    {
        return _order.Payment?.IsProcessed == true && _order.Status != OrderStatus.Delivered;
    }

    public void ProcessRefund(decimal amount)
    {
        if (CanBeRefunded())
        {
            Console.WriteLine($"[REFUND] Processing refund of ${amount:F2} for shipped order {OrderId}");
            Console.WriteLine($"[REFUND] Refund initiated");
        }
        else
        {
            Console.WriteLine($"[REFUND] Shipped order {OrderId} cannot be refunded at this stage");
        }
    }
}

/// <summary>
/// Delivered order - only supports tracking (view-only)
/// Does NOT implement cancellable, refundable, or modifiable
/// Following ISP: Read-only operations only
/// </summary>
public class DeliveredOrder : ITrackableOrder
{
    private readonly Order _order;

    public DeliveredOrder(Order order)
    {
        _order = order;
    }

    public string OrderId => _order.OrderId;
    public OrderStatus Status => _order.Status;

    public string? GetTrackingNumber()
    {
        return _order.TrackingNumber;
    }

    public string GetTrackingUrl()
    {
        return $"https://tracking.example.com/delivered/{_order.TrackingNumber}";
    }
}
