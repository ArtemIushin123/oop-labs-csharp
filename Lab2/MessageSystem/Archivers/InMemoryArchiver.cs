using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Archivers;

public class InMemoryArchiver : IArchiver
{
    private readonly List<Message> _messages;

    public InMemoryArchiver()
    {
        _messages = new List<Message>();
    }

    public void Archive(Message message)
    {
        _messages.Add(message);
    }

    public IReadOnlyCollection<Message> GetMessages() => _messages.AsReadOnly();
}