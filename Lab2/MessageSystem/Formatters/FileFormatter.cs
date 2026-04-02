using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Formatters;

public class FileFormatter : IFormatter
{
    private readonly string _filePath;

    public FileFormatter(string filePath)
    {
        _filePath = filePath;

        if (!File.Exists(_filePath))
            File.Create(_filePath).Dispose();
    }

    public void WriteHeader(string header)
    {
        using var writer = new StreamWriter(_filePath, append: true);
        writer.WriteLine($"# {header}");
    }

    public void WriteBody(string body)
    {
        using var writer = new StreamWriter(_filePath, append: true);
        writer.WriteLine(body);
    }
}