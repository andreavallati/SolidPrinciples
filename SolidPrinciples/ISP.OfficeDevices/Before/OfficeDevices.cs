namespace ISP.OfficeDevices.Before;

/// <summary>
/// PROBLEM: This interface violates Interface Segregation Principle
/// It forces all implementing classes to implement ALL methods
/// Even if they don't need or support all functionality
/// </summary>
public interface IMultiFunctionDevice
{
    void Print(string document);
    void Scan(string document);
    void Fax(string document, string phoneNumber);
    void Copy(string document);
    void Email(string document, string emailAddress);
}

/// <summary>
/// Full-featured multifunction printer - can implement all methods
/// </summary>
public class HighEndMultiFunctionPrinter : IMultiFunctionDevice
{
    public void Print(string document)
    {
        Console.WriteLine($"[HIGH-END MFP] Printing: {document}");
    }

    public void Scan(string document)
    {
        Console.WriteLine($"[HIGH-END MFP] Scanning: {document}");
    }

    public void Fax(string document, string phoneNumber)
    {
        Console.WriteLine($"[HIGH-END MFP] Faxing {document} to {phoneNumber}");
    }

    public void Copy(string document)
    {
        Console.WriteLine($"[HIGH-END MFP] Copying: {document}");
    }

    public void Email(string document, string emailAddress)
    {
        Console.WriteLine($"[HIGH-END MFP] Emailing {document} to {emailAddress}");
    }
}

/// <summary>
/// Simple printer - forced to implement methods it doesn't support
/// Violates ISP by having "fat interface"
/// </summary>
public class BasicPrinter : IMultiFunctionDevice
{
    public void Print(string document)
    {
        Console.WriteLine($"[BASIC PRINTER] Printing: {document}");
    }

    // Forced to implement methods that make no sense for a basic printer
    public void Scan(string document)
    {
        throw new NotSupportedException("Basic printer cannot scan");
    }

    public void Fax(string document, string phoneNumber)
    {
        throw new NotSupportedException("Basic printer cannot fax");
    }

    public void Copy(string document)
    {
        throw new NotSupportedException("Basic printer cannot copy");
    }

    public void Email(string document, string emailAddress)
    {
        throw new NotSupportedException("Basic printer cannot email");
    }
}

/// <summary>
/// Scanner-only device - forced to implement printing methods
/// </summary>
public class StandaloneScanner : IMultiFunctionDevice
{
    public void Print(string document)
    {
        throw new NotSupportedException("Scanner cannot print");
    }

    public void Scan(string document)
    {
        Console.WriteLine($"[SCANNER] Scanning: {document}");
    }

    public void Fax(string document, string phoneNumber)
    {
        throw new NotSupportedException("Scanner cannot fax");
    }

    public void Copy(string document)
    {
        throw new NotSupportedException("Scanner cannot copy");
    }

    public void Email(string document, string emailAddress)
    {
        // Scanner can email scanned documents
        Console.WriteLine($"[SCANNER] Emailing scanned {document} to {emailAddress}");
    }
}

/// <summary>
/// Old fax machine - forced to implement modern methods
/// </summary>
public class OldFaxMachine : IMultiFunctionDevice
{
    public void Print(string document)
    {
        // Old fax can print received faxes
        Console.WriteLine($"[FAX] Printing received fax: {document}");
    }

    public void Scan(string document)
    {
        throw new NotSupportedException("Old fax machine cannot scan");
    }

    public void Fax(string document, string phoneNumber)
    {
        Console.WriteLine($"[FAX] Sending fax {document} to {phoneNumber}");
    }

    public void Copy(string document)
    {
        throw new NotSupportedException("Old fax machine cannot copy");
    }

    public void Email(string document, string emailAddress)
    {
        throw new NotSupportedException("Old fax machine cannot email");
    }
}
