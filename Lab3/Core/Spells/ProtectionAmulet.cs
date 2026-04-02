using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Spells;

public class ProtectionAmulet : ISpell
{
    public void CastSpell(ICreature creature)
    {
        if (creature is CreatureDecorator decorator)
        {
            decorator.AddModifier(new MagicShieldModifier());
        }
    }
}
