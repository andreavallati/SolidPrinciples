using OCP.PaymentGateway.After.Models;

namespace OCP.PaymentGateway.After.Processors;

public class CreditCardProcessor : IPaymentProcessor
{
    public string PaymentMethodName => "Credit Card";

    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        Console.WriteLine($"Validating credit card number: {request.PaymentDetails.GetValueOrDefault("CardNumber", "****")}");
        Console.WriteLine($"Checking CVV and expiry date");
        Console.WriteLine($"Contacting credit card processor");
        Console.WriteLine($"Transaction approved");

        return new PaymentResult
        {
            IsSuccess = true,
            Message = "Credit card payment processed successfully",
            TransactionId = request.TransactionId
        };
    }
}
