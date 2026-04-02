using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Modifiers;

public class MagicShieldModifier : IDefenceModifier
{
    public bool Used { get; private set; }

    public int MagicShield(ICreature creature, int damage)
    {
        if (Used) return damage;

        Used = true;
        return 0;
    }

    public bool IsExpired => Used;

    public IModifier Clone()
    {
        return new MagicShieldModifier();
    }
}