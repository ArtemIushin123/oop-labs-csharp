namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public sealed record ExceededMaxForceResult(double Time = 0)
    : RouteResult(false, Time);
