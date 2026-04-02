namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public class Station : IRouteSection
{
    private readonly double _parkingTime;
    private readonly double _maxArrivalSpeed;

    public Station(double parkingTime, double maxArrivalSpeed)
    {
        _parkingTime = parkingTime;
        _maxArrivalSpeed = maxArrivalSpeed;
    }

    public RouteResult PassThrough(Train train, IDynamicsCalculator calculator, double precision)
    {
        if (train.Speed > _maxArrivalSpeed)
            return new StationSpeedTooHighResult(0);

        double time = _parkingTime;
        return new SuccessResult(time);
    }
}