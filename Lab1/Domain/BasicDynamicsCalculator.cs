namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public class BasicDynamicsCalculator : IDynamicsCalculator
{
    public RouteResult Travel(Train train, double distance, double precision)
    {
        double remaining = distance;
        double time = 0;
        double speed = train.Speed;

        while (remaining > 0)
        {
            double newSpeed = speed + (train.Acceleration * precision);
            if (newSpeed <= 0)
                return new CannotMoveResult(time);

            double deltaDistance = newSpeed * precision;
            remaining -= deltaDistance;
            speed = newSpeed;
            time += precision;
        }

        train.UpdateState(speed);
        return new SuccessResult(time);
    }
}