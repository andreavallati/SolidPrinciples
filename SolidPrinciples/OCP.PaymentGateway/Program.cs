using OCP.PaymentGateway.After.Processors;
using OCP.PaymentGateway.Before;
using AfterPaymentRequest = OCP.PaymentGateway.After.Models.PaymentRequest;

Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine("SOLID PRINCIPLES: Open/Closed Principle (OCP)");
Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine();

Console.WriteLine("PRINCIPLE:");
Console.WriteLine("Software entities should be OPEN for extension but CLOSED for modification.");
Console.WriteLine("You should be able to add new functionality without changing existing code.");
Console.WriteLine();

// ============================
// BEFORE: Violation of OCP
// ============================
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine("BEFORE: Payment Processor with Switch Statement");
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine();

Console.WriteLine("PROBLEMS:");
Console.WriteLine("Adding a new payment method requires MODIFYING the PaymentProcessor class");
Console.WriteLine("The switch statement grows with each new payment type");
Console.WriteLine("Risk of breaking existing functionality when adding new features");
Console.WriteLine("All payment logic is coupled in one place");
Console.WriteLine("Difficult to test individual payment methods");
Console.WriteLine();

var beforeProcessor = new PaymentProcessor();

var creditCardRequest = new PaymentRequest
{
    TransactionId = "TXN-001",
    Amount = 299.99m,
    Method = PaymentMethod.CreditCard,
    PaymentDetails = new() { { "CardNumber", "**** **** **** 1234" } }
};

var payPalRequest = new PaymentRequest
{
    TransactionId = "TXN-002",
    Amount = 149.50m,
    Method = PaymentMethod.PayPal,
    PaymentDetails = new() { { "PayPalEmail", "customer@example.com" } }
};

beforeProcessor.ProcessPayment(creditCardRequest);
beforeProcessor.ProcessPayment(payPalRequest);

Console.WriteLine("\nWhat if we want to add Cryptocurrency, ApplePay, or GooglePay?");
Console.WriteLine("We must MODIFY the PaymentProcessor class and add more cases!");

Console.WriteLine();
Console.WriteLine();

// ============================
// AFTER: Following OCP
// ============================
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine("AFTER: Extensible Payment Gateway with Strategy Pattern");
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine();

Console.WriteLine("BENEFITS:");
Console.WriteLine("New payment processors can be added WITHOUT modifying existing code");
Console.WriteLine("Each payment processor is independent and testable");
Console.WriteLine("PaymentGateway class remains unchanged when adding new processors");
Console.WriteLine("Easy to add/remove payment methods at runtime");
Console.WriteLine("Follows Strategy pattern for clean architecture");
Console.WriteLine();

var gateway = new OCP.PaymentGateway.After.PaymentGateway();

// Register available payment processors
gateway.RegisterProcessor("creditcard", new CreditCardProcessor());
gateway.RegisterProcessor("paypal", new PayPalProcessor());
gateway.RegisterProcessor("stripe", new StripeProcessor());
gateway.RegisterProcessor("banktransfer", new BankTransferProcessor());

// NEW: Add cryptocurrency support WITHOUT modifying PaymentGateway!
gateway.RegisterProcessor("crypto", new CryptocurrencyProcessor());

Console.WriteLine($"Available payment processors: {string.Join(", ", gateway.GetAvailableProcessors())}");

var afterCreditCardRequest = new AfterPaymentRequest
{
    TransactionId = "TXN-003",
    Amount = 499.99m,
    PaymentDetails = new() { { "CardNumber", "**** **** **** 5678" } }
};

var afterStripeRequest = new AfterPaymentRequest
{
    TransactionId = "TXN-004",
    Amount = 89.99m,
    PaymentDetails = new() { { "StripeToken", "tok_1234567890" } }
};

var cryptoRequest = new AfterPaymentRequest
{
    TransactionId = "TXN-005",
    Amount = 1999.99m,
    PaymentDetails = new()
    {
        { "CryptoType", "Bitcoin" },
        { "WalletAddress", "1A2B3C4D5E6F7G8H9I" }
    }
};

var result1 = gateway.ProcessPayment("creditcard", afterCreditCardRequest);
Console.WriteLine($"Result: {result1.Message}\n");

var result2 = gateway.ProcessPayment("stripe", afterStripeRequest);
Console.WriteLine($"Result: {result2.Message}\n");

var result3 = gateway.ProcessPayment("crypto", cryptoRequest);
Console.WriteLine($"Result: {result3.Message}");
Console.WriteLine("Cryptocurrency processor added WITHOUT modifying PaymentGateway!");

Console.WriteLine();
Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine("KEY TAKEAWAY:");
Console.WriteLine("By using interfaces and composition, we can extend functionality by adding");
Console.WriteLine("new classes rather than modifying existing ones. This reduces bugs and makes");
Console.WriteLine("the system more maintainable and scalable.");
Console.WriteLine("=".PadRight(80, '='));
