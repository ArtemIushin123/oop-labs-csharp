namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public class PowerMagneticPath : IRouteSection
{
    private readonly double _distance;
    private readonly double _force;

    public PowerMagneticPath(double distance, double force)
    {
        _distance = distance;
        _force = force;
    }

    public RouteResult PassThrough(Train train, IDynamicsCalculator calculator, double precision)
    {
        bool applied = train.ApplyForce(_force);
        if (!applied)
            return new ExceededMaxForceResult(0);

        return calculator.Travel(train, _distance, precision);
    }
}