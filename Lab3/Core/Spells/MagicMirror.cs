using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Spells;

public class MagicMirror : ISpell
{
    public void CastSpell(ICreature creature)
    {
        int oldDamage = creature.Damage;
        int oldHealth = creature.Health;

        creature.ChangeDamage(oldHealth);
        creature.ChangeHealth(oldDamage);
    }
}