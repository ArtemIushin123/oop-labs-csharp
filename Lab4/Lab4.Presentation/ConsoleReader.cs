namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public class ConsoleReader : IConsoleReader
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}
