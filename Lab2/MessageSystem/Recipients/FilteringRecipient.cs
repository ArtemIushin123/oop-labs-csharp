using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Recipients;

public class FilteringRecipient : IRecipient
{
    private readonly IRecipient _innerRecipient;
    private readonly ImportanceLevel _minImportance;

    public FilteringRecipient(IRecipient innerRecipient, ImportanceLevel minImportance)
    {
        _innerRecipient = innerRecipient;
        _minImportance = minImportance;
    }

    public void Receive(Message message)
    {
        if (IsImportanceEnough(message.Importance))
        {
            _innerRecipient.Receive(message);
        }
    }

    private bool IsImportanceEnough(ImportanceLevel messageLevel)
    {
        return messageLevel.Priority >= _minImportance.Priority;
    }
}