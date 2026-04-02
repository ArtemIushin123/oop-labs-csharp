using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Modifiers;

public class DoubleStrikeModifier : IAttackModifier
{
    public bool Used { get; private set; }

    public void DoubleStrike(ICreature creature, ICreature target)
    {
        if (Used || creature.Health <= 0) return;

        Used = true;
        target.TakeDamage(creature.Damage);
    }

    public bool IsExpired => Used;

    public IModifier Clone()
    {
        return new DoubleStrikeModifier();
    }
}