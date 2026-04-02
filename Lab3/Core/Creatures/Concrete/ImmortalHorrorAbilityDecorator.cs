using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;

public class ImmortalHorrorAbilityDecorator : CreatureDecorator
{
    public ImmortalHorrorAbilityDecorator(ICreature creature) : base(creature)
    { }

    private bool _hasReborned;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (Creature.Health <= 0 && !_hasReborned)
        {
            _hasReborned = true;
            int healthAfterDie = 1;
            Creature.ChangeHealth(healthAfterDie);
        }
    }
}
