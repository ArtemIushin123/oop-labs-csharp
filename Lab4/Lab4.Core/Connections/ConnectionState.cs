namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;

public record ConnectionState
{
    private ConnectionState() { }

    public sealed record Connected : ConnectionState;

    public sealed record Disconnected : ConnectionState;
}