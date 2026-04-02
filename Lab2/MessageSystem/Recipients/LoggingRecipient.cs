using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core;
using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Recipients;

public class LoggingRecipient : IRecipient
{
    private readonly IRecipient _innerRecipient;
    private readonly ILogger _logger;

    public LoggingRecipient(IRecipient innerRecipient, ILogger logger)
    {
        _innerRecipient = innerRecipient;
        _logger = logger;
    }

    public void Receive(Message message)
    {
        _logger.Log($"Message received: {message.Title} (Importance: {message.Importance.GetType().Name})");
        _innerRecipient.Receive(message);
    }
}