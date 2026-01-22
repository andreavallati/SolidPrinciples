using ISP.OfficeDevices.After.Interfaces;

namespace ISP.OfficeDevices.After.Devices;

/// <summary>
/// Basic printer - only implements IPrinter
/// No need to implement unsupported methods
/// No exceptions thrown - clean implementation
/// </summary>
public class BasicPrinter : IPrinter
{
    public void Print(string document)
    {
        Console.WriteLine($"[BASIC PRINTER] Printing: {document}");
    }
}
