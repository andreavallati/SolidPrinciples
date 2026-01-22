using OCP.PaymentGateway.After.Models;

namespace OCP.PaymentGateway.After.Processors;

/// <summary>
/// NEW payment processor - demonstrates extensibility
/// We can add this WITHOUT modifying any existing code!
/// </summary>
public class CryptocurrencyProcessor : IPaymentProcessor
{
    public string PaymentMethodName => "Cryptocurrency";

    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        Console.WriteLine($"Cryptocurrency: {request.PaymentDetails.GetValueOrDefault("CryptoType", "Bitcoin")}");
        Console.WriteLine($"Wallet address: {request.PaymentDetails.GetValueOrDefault("WalletAddress", "1A2b3C...")}");
        Console.WriteLine($"Broadcasting transaction to blockchain");
        Console.WriteLine($"Waiting for confirmations");
        Console.WriteLine($"Transaction confirmed");

        return new PaymentResult
        {
            IsSuccess = true,
            Message = "Cryptocurrency payment processed successfully",
            TransactionId = request.TransactionId
        };
    }
}
