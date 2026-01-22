using DIP.OrderManagement.After.Abstractions;

namespace DIP.OrderManagement.After.Implementations;

/// <summary>
/// Console logger implementation
/// </summary>
public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
    }
}
