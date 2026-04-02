using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Core.ResultTypes;

public record ResultType
{
    private ResultType() { }

    public sealed record Success(ICreature Creature) : ResultType;

    public sealed record NameNotFoundInCatalog : ResultType;
}