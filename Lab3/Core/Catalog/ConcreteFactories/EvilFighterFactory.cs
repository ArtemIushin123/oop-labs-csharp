using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Catalog.ConcreteFactories;

public class EvilFighterFactory : ICreatureFactory
{
    public ICreature Create()
    {
        ICreature baseCreature = new Creature(1, 6);
        return new EvilFighterAbilityDecorator(baseCreature);
    }
}