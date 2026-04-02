namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public sealed record CannotMoveResult(double Time)
    : RouteResult(false, Time);
