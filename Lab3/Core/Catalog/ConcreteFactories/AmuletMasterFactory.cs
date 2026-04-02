using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Catalog.ConcreteFactories;

public class AmuletMasterFactory : ICreatureFactory
{
    public ICreature Create()
    {
        ICreature baseCreature = new Creature(5, 2);
        return new AmuletMasterAbilityDecorator(baseCreature);
    }
}