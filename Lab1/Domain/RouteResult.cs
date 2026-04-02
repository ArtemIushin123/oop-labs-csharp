namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public abstract record RouteResult
{
    public double Time { get; init; }

    public bool Success { get; init; }

    protected RouteResult(bool success, double time)
    {
        Success = success;
        Time = time;
    }
}