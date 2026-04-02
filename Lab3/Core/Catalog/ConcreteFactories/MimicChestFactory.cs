using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Catalog.ConcreteFactories;

public class MimicChestFactory : ICreatureFactory
{
    public ICreature Create()
    {
        ICreature baseCreature = new Creature(1, 1);

        return new MimicAbilityDecorator(baseCreature);
    }
}