using Itmo.ObjectOrientedProgramming.Lab3.Core.Modifiers;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Spells;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

public abstract class CreatureDecorator : ICreature
{
    protected ICreature Creature { get; }

    private readonly List<IModifier> _modifiers = new();

    protected CreatureDecorator(ICreature creature)
    {
        Creature = creature;
    }

    public int Damage => Creature.Damage;

    public int Health => Creature.Health;

    public virtual void Strike(ICreature enemy)
    {
        _modifiers.RemoveAll(modifer => modifer.IsExpired);

        foreach (IAttackModifier modifier in _modifiers.OfType<IAttackModifier>())
        {
            modifier.DoubleStrike(Creature, enemy);
        }

        Creature.Strike(enemy);
    }

    public virtual void TakeDamage(int damage)
    {
        _modifiers.RemoveAll(m => m.IsExpired);

        foreach (IDefenceModifier modifier in _modifiers.OfType<IDefenceModifier>())
        {
            damage = modifier.MagicShield(Creature, damage);
        }

        Creature.TakeDamage(damage);
    }

    public void ChangeHealth(int newHealth) => Creature.ChangeHealth(newHealth);

    public void ChangeDamage(int newDamage) => Creature.ChangeDamage(newDamage);

    public ICreature Clone()
    {
        ICreature clonedBase = Creature.Clone();

        var decoratorInstance = (CreatureDecorator?)Activator.CreateInstance(GetType(), clonedBase);
        if (decoratorInstance == null)
        {
            return clonedBase;
        }

        foreach (IModifier m in _modifiers)
            decoratorInstance.AddModifier(m);

        return decoratorInstance;
    }

    public ICreature AddModifier(IModifier modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    public ICreature AddSpell(ISpell spell)
    {
        if (Health > 0)
        {
            spell.CastSpell(this);
        }

        return this;
    }

    public IReadOnlyCollection<IModifier> Modifiers => _modifiers.AsReadOnly();
}
