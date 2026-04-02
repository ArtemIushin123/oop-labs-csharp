namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

public class Creature : ICreature
{
    public int Damage { get; private set; }

    public int Health { get; private set; }

    public Creature(int damage, int health)
    {
        Damage = damage;
        Health = health;
    }

    public void Strike(ICreature enemy)
    {
        if (Damage <= 0 || Health <= 0) return;
        enemy.TakeDamage(Damage);
    }

    public void TakeDamage(int damage)
    {
        if (Health <= 0) return;
        Health -= damage;
        if (Health < 0) Health = 0;
    }

    public void ChangeHealth(int newHealth)
    {
        Health = newHealth;
    }

    public void ChangeDamage(int newDamage)
    {
        Damage = newDamage;
    }

    public ICreature Clone()
    {
        return new Creature(Damage, Health);
    }
}