namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public interface IConsoleWriter
{
    void Write(string text);

    void WriteLine(string? text = "");
}