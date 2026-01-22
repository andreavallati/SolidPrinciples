using OCP.PaymentGateway.After.Models;

namespace OCP.PaymentGateway.After;

/// <summary>
/// Payment Gateway that follows Open/Closed Principle
/// OPEN for extension: New payment processors can be added
/// CLOSED for modification: No need to change this class when adding new processors
/// </summary>
public class PaymentGateway
{
    private readonly Dictionary<string, IPaymentProcessor> _processors = [];

    public void RegisterProcessor(string key, IPaymentProcessor processor)
    {
        _processors[key.ToLower()] = processor;
    }

    public PaymentResult ProcessPayment(string processorKey, PaymentRequest request)
    {
        Console.WriteLine($"\n[PAYMENT] Processing payment using {processorKey} for ${request.Amount:F2}");

        if (!_processors.TryGetValue(processorKey.ToLower(), out var processor))
        {
            return new PaymentResult
            {
                IsSuccess = false,
                Message = $"Payment processor '{processorKey}' not found",
                TransactionId = request.TransactionId
            };
        }

        return processor.ProcessPayment(request);
    }

    public IEnumerable<string> GetAvailableProcessors()
    {
        return _processors.Keys;
    }
}
