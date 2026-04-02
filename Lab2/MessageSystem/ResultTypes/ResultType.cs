namespace Itmo.ObjectOrientedProgramming.Lab2.MessageSystem.ResultTypes;

public record ResultType
{
    private ResultType() { }

    public sealed record Success() : ResultType;

    public sealed record TheMessageHasAlreadyBeenRead() : ResultType;
}