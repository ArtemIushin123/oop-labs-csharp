using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Catalog.ConcreteFactories;

public class ImmortalHorrorFactory : ICreatureFactory
{
    public ICreature Create()
    {
        ICreature baseCreature = new Creature(4, 4);

        return new ImmortalHorrorAbilityDecorator(baseCreature);
    }
}