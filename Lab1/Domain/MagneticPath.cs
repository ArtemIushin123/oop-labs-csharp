namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public class MagneticPath : IRouteSection
{
    private readonly double _distance;

    public MagneticPath(double distance)
    {
        _distance = distance;
    }

    public RouteResult PassThrough(Train train, IDynamicsCalculator calculator, double precision)
    {
        return calculator.Travel(train, _distance, precision);
    }
}