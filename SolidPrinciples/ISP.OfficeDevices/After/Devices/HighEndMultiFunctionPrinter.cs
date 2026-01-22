using ISP.OfficeDevices.After.Interfaces;

namespace ISP.OfficeDevices.After.Devices;

/// <summary>
/// High-end multifunction printer implementing all capabilities
/// Only implements interfaces it actually supports
/// </summary>
public class HighEndMultiFunctionPrinter : IPrinter, IScanner, IFax, ICopier, IEmailSender
{
    public void Print(string document)
    {
        Console.WriteLine($"[HIGH-END MFP] Printing: {document}");
    }

    public void Scan(string document)
    {
        Console.WriteLine($"[HIGH-END MFP] Scanning: {document}");
    }

    public void SendFax(string document, string phoneNumber)
    {
        Console.WriteLine($"[HIGH-END MFP] Faxing {document} to {phoneNumber}");
    }

    public void Copy(string document)
    {
        Console.WriteLine($"[HIGH-END MFP] Copying: {document}");
    }

    public void SendEmail(string document, string emailAddress)
    {
        Console.WriteLine($"[HIGH-END MFP] Emailing {document} to {emailAddress}");
    }
}
