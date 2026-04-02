using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Modifiers;
using Itmo.ObjectOrientedProgramming.Lab3.Game;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class ModifiersBehaviorTests
{
    private class TestDecorator : CreatureDecorator
    {
        public TestDecorator(ICreature creature) : base(creature) { }
    }

    [Fact]
    public void MultipleDoubleStrikeModifiers_ShouldStackAsSeparateModifiers()
    {
        var baseCreature = new Creature(2, 10);
        var decorator = new TestDecorator(baseCreature);

        decorator.AddModifier(new DoubleStrikeModifier());
        decorator.AddModifier(new DoubleStrikeModifier());

        var enemy = new Creature(0, 8);

        decorator.Strike(enemy);

        Assert.Equal(2, enemy.Health);
    }

    [Fact]
    public void MagicShieldModifiers_ShouldBeIndependentAfterClone()
    {
        var baseCreature = new Creature(1, 10);
        var decorator = new TestDecorator(baseCreature);
        decorator.AddModifier(new MagicShieldModifier());

        var table = new PlayerTable();
        table.AddCreature(decorator);

        decorator.TakeDamage(5);
        Assert.Equal(10, decorator.Health);

        ICreature clone = table.Creatures[0];
        clone.TakeDamage(5);
        Assert.Equal(5, clone.Health);

        decorator.TakeDamage(2);
        Assert.Equal(8, decorator.Health);

        clone.TakeDamage(2);
        Assert.Equal(3, clone.Health);
    }
}
