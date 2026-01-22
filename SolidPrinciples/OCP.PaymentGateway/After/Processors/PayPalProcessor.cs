using OCP.PaymentGateway.After.Models;

namespace OCP.PaymentGateway.After.Processors;

public class PayPalProcessor : IPaymentProcessor
{
    public string PaymentMethodName => "PayPal";

    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        Console.WriteLine($"Redirecting to PayPal");
        Console.WriteLine($"PayPal account: {request.PaymentDetails.GetValueOrDefault("PayPalEmail", "user@example.com")}");
        Console.WriteLine($"Authorizing payment");
        Console.WriteLine($"Transaction completed");

        return new PaymentResult
        {
            IsSuccess = true,
            Message = "PayPal payment processed successfully",
            TransactionId = request.TransactionId
        };
    }
}
