using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Modifiers;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class ConcreteCreatureTests
{
    [Fact]
    public void BattleAnalyst_AppliesOneTimeAttackBoostBeforeFirstStrike()
    {
        var baseC = new Creature(2, 4);
        var analyst = new BattleAnalystAbilityDecorator(baseC);

        var enemy = new Creature(1, 10);

        int before = analyst.Damage;
        analyst.Strike(enemy);
        Assert.Equal(before + 2, analyst.Damage);
    }

    [Fact]
    public void EvilFighter_DoublesAttackOnNonLethalDamage()
    {
        var baseC = new Creature(1, 6);
        var evil = new EvilFighterAbilityDecorator(baseC);

        evil.TakeDamage(1);
        Assert.Equal(5, evil.Health);
        Assert.Equal(2, evil.Damage);
    }

    [Fact]
    public void EvilFighter_DoesNotDoubleOnLethalDamage()
    {
        var baseC = new Creature(1, 2);
        var evil = new EvilFighterAbilityDecorator(baseC);

        evil.TakeDamage(5);
        Assert.Equal(0, evil.Health);
        Assert.Equal(1, evil.Damage);
    }

    [Fact]
    public void MimicCopiesMaxStatsBeforeStrike()
    {
        var mimicBase = new Creature(1, 1);
        var mimic = new MimicAbilityDecorator(mimicBase);

        var enemy = new Creature(5, 2);

        mimic.Strike(enemy);

        Assert.Equal(5, mimic.Damage);
        Assert.Equal(2, mimic.Health);
    }

    [Fact]
    public void ImmortalHorror_RebornsOnceWithOneHealth()
    {
        var baseC = new Creature(4, 4);
        var imm = new ImmortalHorrorAbilityDecorator(baseC);

        imm.TakeDamage(10);
        Assert.Equal(1, imm.Health);

        imm.TakeDamage(10);
        Assert.Equal(0, imm.Health);
    }

    [Fact]
    public void AmuletMaster_HasDefaultModifiersAndTheyWorkTogether()
    {
        var baseC = new Creature(5, 2);
        var amulet = new AmuletMasterAbilityDecorator(baseC);

        Assert.Contains(amulet.Modifiers, m => m is DoubleStrikeModifier);
        Assert.Contains(amulet.Modifiers, m => m is MagicShieldModifier);

        var enemy = new Creature(1, 10);
        amulet.Strike(enemy);

        Assert.Equal(0, enemy.Health);
    }
}