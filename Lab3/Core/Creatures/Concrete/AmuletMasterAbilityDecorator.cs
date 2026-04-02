using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;

public class AmuletMasterAbilityDecorator : CreatureDecorator
{
    public AmuletMasterAbilityDecorator(ICreature creature)
        : base(creature)
    {
        AddModifier(new DoubleStrikeModifier());
        AddModifier(new MagicShieldModifier());
    }
}
