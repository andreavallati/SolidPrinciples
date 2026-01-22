using OCP.PaymentGateway.After.Models;

namespace OCP.PaymentGateway.After;

/// <summary>
/// Interface that defines the contract for payment processing
/// Following Open/Closed Principle: Open for extension, Closed for modification
/// </summary>
public interface IPaymentProcessor
{
    string PaymentMethodName { get; }
    PaymentResult ProcessPayment(PaymentRequest request);
}
