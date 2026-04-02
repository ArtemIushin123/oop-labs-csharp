using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Recipients;

public class User : IRecipient
{
    private readonly List<UserMessage> _messages = new();

    public string Name { get; }

    public User(string name)
    {
        Name = name;
    }

    public void Receive(Message message)
    {
        _messages.Add(new UserMessage(message));
    }

    public UserMessage? FindMessage(Message message)
    {
        return _messages.FirstOrDefault(m => m.Message == message);
    }

    public IReadOnlyCollection<UserMessage> GetMessages() => _messages;
}