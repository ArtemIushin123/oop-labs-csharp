using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Spells;

public class StrengthPotion : ISpell
{
    private const int StrengthPotionEffect = 5;

    public void CastSpell(ICreature creature)
    {
        creature.ChangeDamage(creature.Damage + StrengthPotionEffect);
    }
}