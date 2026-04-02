using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Game;

public class PlayerTable
{
    private const int MaxCreatures = 7;
    private readonly List<ICreature> _creatures;

    public PlayerTable()
    {
        _creatures = new List<ICreature>();
    }

    public IReadOnlyList<ICreature> Creatures => _creatures.AsReadOnly();

    public void AddCreature(ICreature creature)
    {
        if (_creatures.Count >= MaxCreatures) return;
        _creatures.Add(creature.Clone());
    }

    public ICreature? GetNextAttacker()
    {
        return _creatures.FirstOrDefault(creature => creature.Health > 0 && creature.Damage > 0);
    }

    public ICreature? GetNextDefender()
    {
        return _creatures.FirstOrDefault(creature => creature.Health > 0);
    }

    public bool HasAliveCreatures()
    {
        return _creatures.Any(creature => creature.Health > 0);
    }

    public void RemoveDeadCreatures()
    {
        _creatures.RemoveAll(creature => creature.Health <= 0);
    }

    public bool HasAttackingCreatures()
    {
        return _creatures.Any(creature => creature.Health > 0 && creature.Damage > 0);
    }
}