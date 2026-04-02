using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;

public class MimicAbilityDecorator : CreatureDecorator
{
    public MimicAbilityDecorator(ICreature creature) : base(creature)
    { }

    public override void Strike(ICreature enemy)
    {
        int newDamage = Math.Max(Creature.Damage, enemy.Damage);
        int newHealth = Math.Max(Creature.Health, enemy.Health);

        Creature.ChangeDamage(newDamage);
        Creature.ChangeHealth(newHealth);
        base.Strike(enemy);
    }
}