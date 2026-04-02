namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public sealed record SuccessResult(double TotalTime)
    : RouteResult(true, TotalTime);