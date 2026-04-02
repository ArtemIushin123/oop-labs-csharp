using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Recipients;

public class RecipientArchiver : IRecipient
{
    private readonly IArchiver _archiver;

    public RecipientArchiver(IArchiver archiver)
    {
        _archiver = archiver;
    }

    public void Receive(Message message)
    {
        _archiver.Archive(message);
    }
}
