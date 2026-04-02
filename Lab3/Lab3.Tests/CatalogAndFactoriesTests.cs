using Itmo.ObjectOrientedProgramming.Lab3.Core.Catalog;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Catalog.ConcreteFactories;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.ResultTypes;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class CatalogAndFactoriesTests
{
    [Fact]
    public void Catalog_ShouldReturnSuccessWithCreature_WhenFactoryExists()
    {
        var catalog = new CreatureCatalog();
        catalog.RegisterCreature("Analyst", new BattleAnalystFactory());

        ResultType result = catalog.CreateCreature("Analyst");

        ResultType.Success success = Assert.IsType<ResultType.Success>(result);
        Assert.NotNull(success.Creature);
    }

    [Fact]
    public void Catalog_ShouldReturnNameNotFound_WhenFactoryMissing()
    {
        var catalog = new CreatureCatalog();

        ResultType result = catalog.CreateCreature("Unknown");

        Assert.IsType<ResultType.NameNotFoundInCatalog>(result);
    }

    [Fact]
    public void Factories_ShouldReturnDecoratedCreature_WithProperDefaults()
    {
        var factory = new AmuletMasterFactory();
        ICreature creature = factory.Create();

        Assert.Equal(5, creature.Damage);
        Assert.Equal(2, creature.Health);
    }
}