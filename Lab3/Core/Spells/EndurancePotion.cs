using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Spells;

public class EndurancePotion : ISpell
{
    private const int EndurancePotionEffect = 5;

    public void CastSpell(ICreature creature)
    {
        creature.ChangeHealth(creature.Health + EndurancePotionEffect);
    }
}