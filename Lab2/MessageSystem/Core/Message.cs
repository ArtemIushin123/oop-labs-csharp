using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Recipients;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;

public class Message
{
    public string Title { get; }

    public string Body { get; }

    public ImportanceLevel Importance { get; }

    public Message(string title, string body, ImportanceLevel importance)
    {
        Title = title;
        Body = body;
        Importance = importance;
    }
}