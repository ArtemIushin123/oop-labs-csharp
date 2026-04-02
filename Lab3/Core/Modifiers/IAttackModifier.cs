using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Modifiers;

public interface IAttackModifier : IModifier
{
    void DoubleStrike(ICreature creature, ICreature target);
}