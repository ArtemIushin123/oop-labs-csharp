using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Modifiers;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Spells;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class CreatureDecoratorTests
{
    private class TestCreatureDecorator : CreatureDecorator
    {
        public TestCreatureDecorator(ICreature creature) : base(creature) { }
    }

    [Fact]
    public void CreatureWithDoubleStrikeModifier_ShouldDealDoubleDamage()
    {
        var creature = new Creature(5, 10);
        var decorator = new TestCreatureDecorator(creature);
        decorator.AddModifier(new DoubleStrikeModifier());

        var enemy = new Creature(2, 10);
        decorator.Strike(enemy);

        Assert.Equal(0, enemy.Health);
    }

    [Fact]
    public void StrengthPotion_ShouldIncreaseDamage_WhenCastOnCreature()
    {
        var creature = new Creature(2, 10);
        var decorator = new TestCreatureDecorator(creature);

        decorator.AddSpell(new StrengthPotion());

        Assert.Equal(7, creature.Damage);
    }

    [Fact]
    public void MagicShieldModifier_ShouldNegateFirstHit()
    {
        var creature = new Creature(5, 10);
        var decorator = new TestCreatureDecorator(creature);
        decorator.AddModifier(new MagicShieldModifier());

        decorator.TakeDamage(999);

        Assert.Equal(10, decorator.Health);
    }
}
