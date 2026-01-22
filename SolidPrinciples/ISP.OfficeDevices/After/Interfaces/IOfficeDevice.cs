namespace ISP.OfficeDevices.After.Interfaces;

/// <summary>
/// Following ISP: Segregated interfaces for specific capabilities
/// Clients depend only on the interfaces they need
/// </summary>

public interface IPrinter
{
    void Print(string document);
}

public interface IScanner
{
    void Scan(string document);
}

public interface IFax
{
    void SendFax(string document, string phoneNumber);
}

public interface ICopier
{
    void Copy(string document);
}

public interface IEmailSender
{
    void SendEmail(string document, string emailAddress);
}
