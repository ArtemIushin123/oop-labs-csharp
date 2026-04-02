using Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Core.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Notifications;

public class TextNotification : INotification
{
    private readonly string _message;

    public TextNotification(string message)
    {
        _message = message;
    }

    public void Notify()
    {
        Console.WriteLine($"{_message}");
    }
}