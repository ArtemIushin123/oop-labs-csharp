namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public sealed record StationSpeedTooHighResult(double Time = 0)
    : RouteResult(false, Time);
