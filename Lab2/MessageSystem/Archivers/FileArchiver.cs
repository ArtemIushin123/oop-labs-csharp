using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Archivers;

public class FileArchiver : IArchiver
{
    private readonly string _filePath;

    public FileArchiver(string filePath)
    {
        _filePath = filePath;

        if (!File.Exists(_filePath))
            File.Create(_filePath).Dispose();
    }

    public void Archive(Message message)
    {
        using var writer = new StreamWriter(_filePath, append: true);
        writer.WriteLine($"# {message.Title}");
        writer.WriteLine(message.Body);
        writer.WriteLine($"_Importance: {message.Importance.GetType().Name}_");
        writer.WriteLine($"**Saved at:** {DateTime.Now}");
        writer.WriteLine();
    }
}