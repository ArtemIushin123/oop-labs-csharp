using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Recipients;

public class RecipientNotifier : IRecipient
{
    private readonly INotification _notification;
    private readonly List<string> _triggerWords;

    public RecipientNotifier(INotification notification, IEnumerable<string> triggerWords)
    {
        _notification = notification;
        _triggerWords = triggerWords.ToList();
    }

    public void Receive(Message message)
    {
        foreach (string word in _triggerWords)
        {
            if (message.Body.Contains(word, StringComparison.OrdinalIgnoreCase))
            {
                _notification.Notify();
                break;
            }
        }
    }
}
