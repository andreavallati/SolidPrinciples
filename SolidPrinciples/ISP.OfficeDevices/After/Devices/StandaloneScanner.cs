using ISP.OfficeDevices.After.Interfaces;

namespace ISP.OfficeDevices.After.Devices;

/// <summary>
/// Standalone scanner - implements IScanner and IEmailSender
/// Only the capabilities it actually has
/// </summary>
public class StandaloneScanner : IScanner, IEmailSender
{
    public void Scan(string document)
    {
        Console.WriteLine($"[SCANNER] Scanning: {document}");
    }

    public void SendEmail(string document, string emailAddress)
    {
        Console.WriteLine($"[SCANNER] Emailing scanned {document} to {emailAddress}");
    }
}
