using ISP.OfficeDevices.After.Interfaces;

namespace ISP.OfficeDevices.After.Devices;

/// <summary>
/// Old fax machine - implements IPrinter and IFax
/// Only what it can actually do
/// </summary>
public class OldFaxMachine : IPrinter, IFax
{
    public void Print(string document)
    {
        Console.WriteLine($"[FAX] Printing received fax: {document}");
    }

    public void SendFax(string document, string phoneNumber)
    {
        Console.WriteLine($"[FAX] Sending fax {document} to {phoneNumber}");
    }
}
