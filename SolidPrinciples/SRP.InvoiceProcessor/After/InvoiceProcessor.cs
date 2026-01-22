using SRP.InvoiceProcessor.After.Models;

namespace SRP.InvoiceProcessor.After;

/// <summary>
/// Single Responsibility: Orchestrate the invoice processing workflow
/// This class now has only one reason to change: when the workflow steps change
/// Each individual operation is delegated to specialized classes
/// </summary>
public class InvoiceProcessor
{
    private readonly InvoiceValidator _validator;
    private readonly InvoiceCalculator _calculator;
    private readonly InvoiceRepository _repository;
    private readonly InvoiceNotifier _notifier;

    public InvoiceProcessor(
        InvoiceValidator validator,
        InvoiceCalculator calculator,
        InvoiceRepository repository,
        InvoiceNotifier notifier)
    {
        _validator = validator;
        _calculator = calculator;
        _repository = repository;
        _notifier = notifier;
    }

    public static void ProcessInvoice(Invoice invoice)
    {
        InvoiceValidator.Validate(invoice);
        InvoiceCalculator.Calculate(invoice);
        InvoiceRepository.Save(invoice);
        InvoiceNotifier.SendNotification(invoice);
    }
}
