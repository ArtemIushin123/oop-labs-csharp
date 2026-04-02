using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;

public class BattleAnalystAbilityDecorator : CreatureDecorator
{
    public BattleAnalystAbilityDecorator(ICreature creature) : base(creature) { }

    private const int BattleAnalystAttackBoost = 2;
    private bool _applied;

    public override void Strike(ICreature enemy)
    {
        if (!_applied)
        {
            Creature.ChangeDamage(Creature.Damage + BattleAnalystAttackBoost);
            _applied = true;
        }

        base.Strike(enemy);
    }
}