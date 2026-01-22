using OCP.PaymentGateway.After.Models;

namespace OCP.PaymentGateway.After.Processors;

public class BankTransferProcessor : IPaymentProcessor
{
    public string PaymentMethodName => "Bank Transfer";

    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        Console.WriteLine($"Bank account: {request.PaymentDetails.GetValueOrDefault("AccountNumber", "****")}");
        Console.WriteLine($"IBAN validation");
        Console.WriteLine($"Initiating transfer");
        Console.WriteLine($"Transfer queued");

        return new PaymentResult
        {
            IsSuccess = true,
            Message = "Bank transfer initiated successfully",
            TransactionId = request.TransactionId
        };
    }
}
