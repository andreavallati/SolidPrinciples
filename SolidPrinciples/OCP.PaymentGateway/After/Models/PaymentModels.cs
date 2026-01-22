namespace OCP.PaymentGateway.After.Models;

public class PaymentRequest
{
    public string TransactionId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public Dictionary<string, string> PaymentDetails { get; set; } = [];
}

public class PaymentResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
}
