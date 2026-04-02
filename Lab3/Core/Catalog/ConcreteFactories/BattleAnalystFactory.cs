using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;
using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Concrete;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Catalog.ConcreteFactories;

public class BattleAnalystFactory : ICreatureFactory
{
    public ICreature Create()
    {
        ICreature baseCreature = new Creature(2, 4);
        return new BattleAnalystAbilityDecorator(baseCreature);
    }
}