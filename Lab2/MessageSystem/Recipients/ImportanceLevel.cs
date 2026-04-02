namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.Recipients;

public abstract record ImportanceLevel(int Priority)
{
    public sealed record Low() : ImportanceLevel(1);

    public sealed record Normal() : ImportanceLevel(2);

    public sealed record High() : ImportanceLevel(3);

    public sealed record Critical() : ImportanceLevel(4);
}
