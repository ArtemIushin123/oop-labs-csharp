using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Topics;

public class Topic
{
    private readonly List<IRecipient> _recipients = new();

    public string Name { get; }

    public Topic(string name)
    {
        Name = name;
    }

    public void AddRecipient(IRecipient recipient)
    {
        if (!_recipients.Contains(recipient))
            _recipients.Add(recipient);
    }

    public void RemoveRecipient(IRecipient recipient)
    {
        _recipients.Remove(recipient);
    }

    public void Send(Message message)
    {
        foreach (IRecipient recipient in _recipients)
        {
            recipient.Receive(message);
        }
    }

    public IReadOnlyCollection<IRecipient> GetRecipients() => _recipients.AsReadOnly();
}