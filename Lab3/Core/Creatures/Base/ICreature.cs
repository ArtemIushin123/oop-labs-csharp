namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

public interface ICreature
{
    int Damage { get; }

    int Health { get; }

    void Strike(ICreature enemy);

    void TakeDamage(int damage);

    void ChangeHealth(int newHealth);

    void ChangeDamage(int newDamage);

    ICreature Clone();
}