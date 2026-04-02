namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public interface IRouteSection
{
    RouteResult PassThrough(Train train, IDynamicsCalculator calculator, double precision);
}