using ISP.OfficeDevices.After.Interfaces;
using ISP.OfficeDevices.Before;

Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine("SOLID PRINCIPLES: Interface Segregation Principle (ISP)");
Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine();

Console.WriteLine("PRINCIPLE:");
Console.WriteLine("Clients should not be forced to depend on interfaces they do not use.");
Console.WriteLine("Many specific interfaces are better than one general-purpose interface.");
Console.WriteLine();

// ============================
// BEFORE: Violation of ISP
// ============================
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine("BEFORE: Fat Interface for All Devices");
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine();

Console.WriteLine("PROBLEMS:");
Console.WriteLine("IMultiFunctionDevice forces ALL implementations to support ALL methods");
Console.WriteLine("BasicPrinter must implement Scan, Fax, Copy, Email - throws exceptions");
Console.WriteLine("StandaloneScanner must implement Print, Fax, Copy - throws exceptions");
Console.WriteLine("Clients can't rely on interface - must catch exceptions");
Console.WriteLine("'Fat interface' - bloated with methods many clients don't need");
Console.WriteLine();

void UseDevice(IMultiFunctionDevice device, string document)
{
    try
    {
        Console.WriteLine($"\nUsing {device.GetType().Name}:");
        device.Print(document);
        device.Scan(document);
        device.Fax(document, "555-1234");
        device.Copy(document);
        device.Email(document, "user@example.com");
        Console.WriteLine("All operations completed");
    }
    catch (NotSupportedException ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
        Console.WriteLine("Interface promised functionality that device doesn't support!");
    }
}

var beforeHighEnd = new ISP.OfficeDevices.Before.HighEndMultiFunctionPrinter();
UseDevice(beforeHighEnd, "Report.pdf");

var beforeBasic = new ISP.OfficeDevices.Before.BasicPrinter();
UseDevice(beforeBasic, "Letter.docx"); // Exceptions

var beforeScanner = new ISP.OfficeDevices.Before.StandaloneScanner();
UseDevice(beforeScanner, "Photo.jpg"); // Exceptions

var beforeFax = new ISP.OfficeDevices.Before.OldFaxMachine();
UseDevice(beforeFax, "Contract.pdf"); // Exceptions

Console.WriteLine();
Console.WriteLine();

// ============================
// AFTER: Following ISP
// ============================
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine("AFTER: Segregated Interfaces by Capability");
Console.WriteLine("─".PadRight(80, '─'));
Console.WriteLine();

Console.WriteLine("BENEFITS:");
Console.WriteLine("Each device implements ONLY the interfaces it supports");
Console.WriteLine("No NotSupportedException - compile-time safety");
Console.WriteLine("Clients depend only on interfaces they need");
Console.WriteLine("Easy to add new devices with different capability combinations");
Console.WriteLine("Clear contracts - interface = capability");
Console.WriteLine();

// Clients work with specific interfaces
void UsePrinter(IPrinter printer, string document)
{
    Console.WriteLine($"Printing with {printer.GetType().Name}");
    printer.Print(document);
}

void UseScanner(IScanner scanner, string document)
{
    Console.WriteLine($"Scanning with {scanner.GetType().Name}");
    scanner.Scan(document);
}

void UseFax(IFax fax, string document)
{
    Console.WriteLine($"Faxing with {fax.GetType().Name}");
    fax.SendFax(document, "555-1234");
}

void UseEmailSender(IEmailSender emailSender, string document)
{
    Console.WriteLine($"Emailing with {emailSender.GetType().Name}");
    emailSender.SendEmail(document, "user@example.com");
}

var afterHighEnd = new ISP.OfficeDevices.After.Devices.HighEndMultiFunctionPrinter();
var afterBasic = new ISP.OfficeDevices.After.Devices.BasicPrinter();
var afterScanner = new ISP.OfficeDevices.After.Devices.StandaloneScanner();
var afterFax = new ISP.OfficeDevices.After.Devices.OldFaxMachine();
var afterCloud = new ISP.OfficeDevices.After.Devices.WirelessCloudPrinter();

Console.WriteLine("\nPrinting documents (IPrinter):");
UsePrinter(afterHighEnd, "Report.pdf");
UsePrinter(afterBasic, "Letter.docx");
// afterScanner doesn't implement IPrinter - compile-time safety!
UsePrinter(afterFax, "Contract.pdf");
UsePrinter(afterCloud, "Presentation.pptx");

Console.WriteLine("\nScanning documents (IScanner):");
UseScanner(afterHighEnd, "Photo.jpg");
// afterBasic doesn't implement IScanner - compile-time safety!
UseScanner(afterScanner, "Document.pdf");

Console.WriteLine("\nSending faxes (IFax):");
UseFax(afterHighEnd, "Invoice.pdf");
// afterBasic doesn't implement IFax - compile-time safety!
UseFax(afterFax, "Contract.pdf");

Console.WriteLine("\nSending emails (IEmailSender):");
UseEmailSender(afterHighEnd, "Report.pdf");
UseEmailSender(afterScanner, "Scanned_Document.pdf");
UseEmailSender(afterCloud, "Print_Job.pdf");

Console.WriteLine("\nExample: Office workflow requiring specific capabilities");
Console.WriteLine("\nWorkflow: Print -> Scan -> Email");

// We can work with any device that has the required capabilities
void ExecuteWorkflow(IPrinter printer, IScanner scanner, IEmailSender emailSender)
{
    Console.WriteLine("Step 1: Print document");
    printer.Print("Workflow_Document.pdf");

    Console.WriteLine("Step 2: Scan document");
    scanner.Scan("Workflow_Document.pdf");

    Console.WriteLine("Step 3: Email scanned document");
    emailSender.SendEmail("Workflow_Document.pdf", "manager@company.com");

    Console.WriteLine("Workflow completed successfully!");
}

Console.WriteLine("\nUsing high-end multifunction printer for entire workflow:");
ExecuteWorkflow(afterHighEnd, afterHighEnd, afterHighEnd);

Console.WriteLine();
Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine("KEY TAKEAWAY:");
Console.WriteLine("Design interfaces based on client needs, not provider capabilities.");
Console.WriteLine("Small, focused interfaces lead to flexible, maintainable code.");
Console.WriteLine("Clients should depend only on the methods they actually use.");
Console.WriteLine("=".PadRight(80, '='));
