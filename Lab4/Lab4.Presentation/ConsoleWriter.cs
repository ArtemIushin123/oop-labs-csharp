namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public class ConsoleWriter : IConsoleWriter
{
    public void Write(string text)
    {
        Console.Write(text);
    }

    public void WriteLine(string? text = "")
    {
        Console.WriteLine(text);
    }
}