using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Spells;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class SpellsTests
{
    private class TestDecorator : CreatureDecorator
    {
        public TestDecorator(ICreature creature) : base(creature) { }
    }

    [Fact]
    public void StrengthPotion_MultipleCasts_Stack()
    {
        var c = new Creature(1, 5);
        var d = new TestDecorator(c);

        d.AddSpell(new StrengthPotion());
        d.AddSpell(new StrengthPotion());

        Assert.Equal(11, c.Damage);
    }

    [Fact]
    public void EndurancePotion_IncreasesHealth()
    {
        var c = new Creature(1, 3);
        var d = new TestDecorator(c);

        d.AddSpell(new EndurancePotion());

        Assert.Equal(8, c.Health);
    }

    [Fact]
    public void MagicMirror_SwapsAttackAndHealth()
    {
        var c = new Creature(2, 7);
        var d = new TestDecorator(c);

        d.AddSpell(new MagicMirror());

        Assert.Equal(7, c.Damage);
        Assert.Equal(2, c.Health);
    }

    [Fact]
    public void ProtectionAmulet_AddsMagicShieldModifier()
    {
        var c = new Creature(1, 5);
        var d = new TestDecorator(c);

        d.AddSpell(new ProtectionAmulet());

        d.TakeDamage(10);
        Assert.Equal(5, d.Health);
    }
}