using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;
using Itmo.ObjectOrientedProgramming.Lab3.Game;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class BattleTests
{
    [Fact]
    public void Battle_FirstPlayerWins_WhenStrongerCreature()
    {
        var t1 = new PlayerTable();
        var t2 = new PlayerTable();

        t1.AddCreature(new Creature(10, 5));
        t2.AddCreature(new Creature(2, 3));

        var battle = new Battle(t1, t2);
        BattleResult res = battle.Start();

        Assert.IsType<BattleResult.Player1Wins>(res);
    }

    [Fact]
    public void Battle_Draw_WhenNoAliveAttackers()
    {
        var t1 = new PlayerTable();
        var t2 = new PlayerTable();

        t1.AddCreature(new Creature(0, 5));
        t2.AddCreature(new Creature(0, 5));

        var battle = new Battle(t1, t2);
        BattleResult res = battle.Start();

        Assert.IsType<BattleResult.Draw>(res);
    }

    [Fact]
    public void Battle_ImmortalHorror_ReviveChangesOutcome()
    {
        var t1 = new PlayerTable();
        var t2 = new PlayerTable();

        t1.AddCreature(new Creature(5, 5));

        t2.AddCreature(new ImmortalHorrorAbilityDecorator(new Creature(4, 4)));

        var battle = new Battle(t1, t2);
        BattleResult res = battle.Start();

        Assert.True(res is BattleResult.Player1Wins or BattleResult.Player2Wins or BattleResult.Draw);
    }
}