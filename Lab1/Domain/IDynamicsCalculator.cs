namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public interface IDynamicsCalculator
{
    RouteResult Travel(Train train, double distance, double precision);
}