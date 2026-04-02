namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Records;

public record ResultType
{
    private ResultType() { }

    public sealed record Success : ResultType;

    public sealed record Failure(string Str) : ResultType;
}