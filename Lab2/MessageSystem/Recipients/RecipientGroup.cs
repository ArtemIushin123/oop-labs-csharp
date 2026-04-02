using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Recipients;

public class RecipientGroup : IRecipient
{
    private readonly List<IRecipient> _recipients = new();

    public RecipientGroup() { }

    public RecipientGroup(IEnumerable<IRecipient> recipients)
    {
        _recipients.AddRange(recipients);
    }

    public void AddRecipient(IRecipient recipient) => _recipients.Add(recipient);

    public void RemoveRecipient(IRecipient recipient) => _recipients.Remove(recipient);

    public void Receive(Message message)
    {
        foreach (IRecipient recipient in _recipients)
        {
            recipient.Receive(message);
        }
    }
}