using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;

public class EvilFighterAbilityDecorator : CreatureDecorator
{
    public EvilFighterAbilityDecorator(ICreature creature) : base(creature)
    { }

    private const int EvilFighterAttackBoost = 2;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (Creature.Health > 0)
        {
            int newDamage = Creature.Damage * EvilFighterAttackBoost;
            Creature.ChangeDamage(newDamage);
        }
    }
}