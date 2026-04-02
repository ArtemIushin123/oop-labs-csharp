namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public class Route
{
    private readonly IReadOnlyList<IRouteSection> _sections;
    private readonly double _maxEndSpeed;
    private readonly IDynamicsCalculator _calculator;

    public Route(IReadOnlyList<IRouteSection> sections, double maxEndSpeed, IDynamicsCalculator calculator)
    {
        _sections = sections;
        _maxEndSpeed = maxEndSpeed;
        _calculator = calculator;
    }

    public RouteResult Simulate(Train train, double precision)
    {
        double totalTime = 0;

        foreach (IRouteSection section in _sections)
        {
            RouteResult result = section.PassThrough(train, _calculator, precision);

            if (!result.Success)
                return result with { Time = totalTime + result.Time };

            totalTime += result.Time;

            if (train.Speed > _maxEndSpeed)
                return new CannotStopAtEndResult(totalTime);
        }

        if (train.Speed > _maxEndSpeed)
            return new CannotStopAtEndResult(totalTime);

        train.Stop();
        return new SuccessResult(totalTime);
    }
}