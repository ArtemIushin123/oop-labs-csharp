using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Modifiers;

public interface IDefenceModifier : IModifier
{
    int MagicShield(ICreature creature, int damage);
}