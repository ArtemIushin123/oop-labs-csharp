using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Catalog;

public interface ICreatureFactory
{
    ICreature Create();
}