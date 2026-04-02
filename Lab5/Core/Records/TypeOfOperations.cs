namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Records;

public record TypeOfOperations
{
    private TypeOfOperations() { }

    public sealed record Deposit : TypeOfOperations;

    public sealed record Withdraw : TypeOfOperations;

    public sealed record TransferIn : TypeOfOperations;

    public sealed record TransferOut : TypeOfOperations;
}