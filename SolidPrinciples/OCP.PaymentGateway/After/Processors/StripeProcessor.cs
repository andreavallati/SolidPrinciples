using OCP.PaymentGateway.After.Models;

namespace OCP.PaymentGateway.After.Processors;

public class StripeProcessor : IPaymentProcessor
{
    public string PaymentMethodName => "Stripe";

    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        Console.WriteLine($"Initializing Stripe payment");
        Console.WriteLine($"Token: {request.PaymentDetails.GetValueOrDefault("StripeToken", "tok_****")}");
        Console.WriteLine($"Creating charge");
        Console.WriteLine($"Payment successful");

        return new PaymentResult
        {
            IsSuccess = true,
            Message = "Stripe payment processed successfully",
            TransactionId = request.TransactionId
        };
    }
}
