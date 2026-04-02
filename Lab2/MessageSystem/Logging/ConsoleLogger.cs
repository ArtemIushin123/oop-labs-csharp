using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Logging;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}