namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Recipients;

public record MessageStatus
{
    private MessageStatus() { }

    public sealed record Read() : MessageStatus;

    public sealed record Unread() : MessageStatus;
}