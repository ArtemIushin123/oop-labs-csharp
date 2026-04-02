namespace Itmo.ObjectOrientedProgramming.Lab1.Domain;

public class Train
{
    private readonly double _weight;
    private readonly double _maxAllowedForce;

    public Train(double weight, double maxAllowedForce)
    {
        _weight = weight;
        _maxAllowedForce = maxAllowedForce;
        Speed = 0;
        Acceleration = 0;
    }

    public double Speed { get; private set; }

    public double Acceleration { get; private set; }

    public bool ApplyForce(double force)
    {
        if (force > _maxAllowedForce) return false;
        Acceleration = force / _weight;
        return true;
    }

    public void UpdateState(double newSpeed) => Speed = newSpeed;

    public void Stop() => Speed = 0;
}