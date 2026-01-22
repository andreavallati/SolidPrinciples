namespace OCP.PaymentGateway.Before;

public enum PaymentMethod
{
    CreditCard,
    PayPal,
    Stripe,
    BankTransfer
}

public class PaymentRequest
{
    public string TransactionId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public Dictionary<string, string> PaymentDetails { get; set; } = [];
}

/// <summary>
/// PROBLEM: This class violates Open/Closed Principle
/// Every time we add a new payment method, we must MODIFY this class
/// The class is OPEN for modification but NOT CLOSED for modification
/// </summary>
public class PaymentProcessor
{
    public bool ProcessPayment(PaymentRequest request)
    {
        Console.WriteLine($"\n[PAYMENT] Processing {request.Method} payment for ${request.Amount:F2}");

        // This switch statement must be modified every time we add a new payment method
        return request.Method switch
        {
            PaymentMethod.CreditCard => ProcessCreditCard(request),
            PaymentMethod.PayPal => ProcessPayPal(request),
            PaymentMethod.Stripe => ProcessStripe(request),
            PaymentMethod.BankTransfer => ProcessBankTransfer(request),
            // What if we want to add Cryptocurrency, ApplePay, GooglePay?
            // We must modify this class and add more cases!
            _ => throw new NotSupportedException($"Payment method {request.Method} is not supported"),
        };
    }

    private static bool ProcessCreditCard(PaymentRequest request)
    {
        Console.WriteLine($"Validating credit card number: {request.PaymentDetails.GetValueOrDefault("CardNumber", "****")}");
        Console.WriteLine($"Checking CVV and expiry date");
        Console.WriteLine($"Contacting credit card processor");
        Console.WriteLine($"Transaction approved");
        return true;
    }

    private static bool ProcessPayPal(PaymentRequest request)
    {
        Console.WriteLine($"Redirecting to PayPal");
        Console.WriteLine($"PayPal account: {request.PaymentDetails.GetValueOrDefault("PayPalEmail", "user@example.com")}");
        Console.WriteLine($"Authorizing payment");
        Console.WriteLine($"Transaction completed");
        return true;
    }

    private static bool ProcessStripe(PaymentRequest request)
    {
        Console.WriteLine($"Initializing Stripe payment");
        Console.WriteLine($"Token: {request.PaymentDetails.GetValueOrDefault("StripeToken", "tok_****")}");
        Console.WriteLine($"Creating charge");
        Console.WriteLine($"Payment successful");
        return true;
    }

    private static bool ProcessBankTransfer(PaymentRequest request)
    {
        Console.WriteLine($"Bank account: {request.PaymentDetails.GetValueOrDefault("AccountNumber", "****")}");
        Console.WriteLine($"IBAN validation");
        Console.WriteLine($"Initiating transfer");
        Console.WriteLine($"Transfer queued");
        return true;
    }
}
