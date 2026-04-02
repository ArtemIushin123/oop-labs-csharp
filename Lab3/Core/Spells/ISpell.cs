using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Spells;

public interface ISpell
{
    void CastSpell(ICreature creature);
}