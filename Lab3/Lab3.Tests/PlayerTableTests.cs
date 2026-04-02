using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Game;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class PlayerTableTests
{
    [Fact]
    public void Table_ShouldAddCreature_Successfully()
    {
        var table = new PlayerTable();
        var creature = new Creature(3, 5);

        table.AddCreature(creature);

        Assert.Single(table.Creatures);
    }

    [Fact]
    public void Table_ShouldReject_WhenExceedingMaxCount()
    {
        var table = new PlayerTable();
        for (int i = 0; i < 7; i++)
            table.AddCreature(new Creature(1, 1));

        var extra = new Creature(1, 1);
        table.AddCreature(extra);

        Assert.Equal(7, table.Creatures.Count);
    }

    [Fact]
    public void Table_ShouldReturnIndependentCopies()
    {
        var table = new PlayerTable();

        var creature = new Creature(2, 4);
        table.AddCreature(creature);

        creature.ChangeHealth(0);

        Assert.NotEqual(creature.Health, table.Creatures[0].Health);
    }
}