using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Catalog;

public class CreatureCatalog
{
    private readonly Dictionary<string, ICreatureFactory> _factories;

    public CreatureCatalog()
    {
        _factories = new Dictionary<string, ICreatureFactory>();
    }

    public void RegisterCreature(string name, ICreatureFactory factory)
    {
        _factories[name] = factory;
    }

    public ResultType CreateCreature(string name)
    {
        if (!_factories.TryGetValue(name, out ICreatureFactory? factory))
            return new ResultType.NameNotFoundInCatalog();

        ICreature creature = factory.Create();
        return new ResultType.Success(creature);
    }
}