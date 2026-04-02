namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Records;

public record Currency
{
    private Currency() { }

    public sealed record Rub : Currency;

    public sealed record Usd : Currency;

    public sealed record Eur : Currency;
}