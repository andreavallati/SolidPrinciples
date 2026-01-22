using ISP.OfficeDevices.After.Interfaces;

namespace ISP.OfficeDevices.After.Devices;

/// <summary>
/// Modern wireless printer with cloud capabilities
/// Implements IPrinter and IEmailSender (can print from email)
/// </summary>
public class WirelessCloudPrinter : IPrinter, IEmailSender
{
    public void Print(string document)
    {
        Console.WriteLine($"[CLOUD PRINTER] Printing: {document}");
    }

    public void SendEmail(string document, string emailAddress)
    {
        Console.WriteLine($"[CLOUD PRINTER] Sending print job notification to {emailAddress}");
    }
}
