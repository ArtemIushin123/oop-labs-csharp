namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Records;

public record SessionType
{
    private SessionType() { }

    public sealed record Admin : SessionType;

    public sealed record User : SessionType;
}