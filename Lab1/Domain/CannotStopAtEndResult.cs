namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public sealed record CannotStopAtEndResult(double Time)
    : RouteResult(false, Time);