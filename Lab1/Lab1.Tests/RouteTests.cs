using Itmo.ObjectOrientedProgramming.Lab1.Domain;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class RouteTests
{
    private const double Precision = 0.1;
    private readonly IDynamicsCalculator _calculator = new BasicDynamicsCalculator();

    [Fact]
    public void Scenario1_ShouldSucceedWithPowerMagneticPathAndMagneticPath()
    {
        var train = new Train(1000, 100);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPath(100, 50),
            new MagneticPath(200),
        };
        var route = new Route(sections, maxEndSpeed: 50, _calculator);

        RouteResult result = route.Simulate(train, Precision);

        Assert.True(result.Success);
        Assert.IsType<SuccessResult>(result);
    }

    [Fact]
    public void Scenario2_ShouldFailDueToExceededForce()
    {
        var train = new Train(1000, 100);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPath(100, 110),
            new MagneticPath(200),
        };
        var route = new Route(sections, maxEndSpeed: 3, _calculator);

        RouteResult result = route.Simulate(train, Precision);

        Assert.False(result.Success);
        Assert.IsType<ExceededMaxForceResult>(result);
    }

    [Fact]
    public void Scenario3_ShouldSucceedWithPowerMagneticPathAndMagneticPathAndStation()
    {
        var train = new Train(1000, 100);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPath(100, 50),
            new MagneticPath(200),
            new Station(parkingTime: 5, maxArrivalSpeed: 100),
            new MagneticPath(100),
        };
        var route = new Route(sections, maxEndSpeed: 100, _calculator);

        RouteResult result = route.Simulate(train, Precision);

        Assert.True(result.Success);
        Assert.IsType<SuccessResult>(result);
    }

    [Fact]
    public void Scenario4_ShouldFailDueToStationSpeed()
    {
        var train = new Train(1000, 100);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPath(100, 90),
            new Station(parkingTime: 5, maxArrivalSpeed: 2),
            new MagneticPath(100),
        };
        var route = new Route(sections, maxEndSpeed: 90, _calculator);

        RouteResult result = route.Simulate(train, Precision);

        Assert.False(result.Success);
        Assert.IsType<StationSpeedTooHighResult>(result);
    }

    [Fact]
    public void Scenario5_ShouldFailDueToEndSpeed()
    {
        var train = new Train(1000, 10);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPath(100, 9),
            new MagneticPath(100),
            new Station(parkingTime: 5, maxArrivalSpeed: 9),
            new MagneticPath(100),
        };
        var route = new Route(sections, maxEndSpeed: 1, _calculator);

        RouteResult result = route.Simulate(train, Precision);

        Assert.False(result.Success);
        Assert.IsType<CannotStopAtEndResult>(result);
    }

    [Fact]
    public void Scenario6_ShouldSucceedWithAccelerationAndDeceleration()
    {
        var train = new Train(1000, 100);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPath(100, 90),
            new MagneticPath(100),
            new PowerMagneticPath(100, -50),
            new Station(parkingTime: 5, maxArrivalSpeed: 40),
            new MagneticPath(100),
            new PowerMagneticPath(100, 90),
            new MagneticPath(100),
            new PowerMagneticPath(100, -60),
        };
        var route = new Route(sections, maxEndSpeed: 30, _calculator);

        RouteResult result = route.Simulate(train, Precision);

        Assert.True(result.Success);
        Assert.IsType<SuccessResult>(result);
    }

    [Fact]
    public void Scenario7_ShouldFailBecauseTrainCannotMove()
    {
        var train = new Train(1000, 100);
        var sections = new IRouteSection[]
        {
            new MagneticPath(100),
        };
        var route = new Route(sections, maxEndSpeed: 3, _calculator);

        RouteResult result = route.Simulate(train, Precision);

        Assert.False(result.Success);
        Assert.IsType<CannotMoveResult>(result);
    }

    [Fact]
    public void Scenario8_ShouldFailBecauseOfNegativeSpeed()
    {
        var train = new Train(1000, 100);
        var sections = new IRouteSection[]
        {
            new PowerMagneticPath(100, 50),
            new PowerMagneticPath(100, -100),
        };
        var route = new Route(sections, maxEndSpeed: 300, _calculator);

        RouteResult result = route.Simulate(train, Precision);

        Assert.False(result.Success);
        Assert.IsType<CannotMoveResult>(result);
    }
}
