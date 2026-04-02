namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

public interface IFolder : IEntity
{
    IReadOnlyCollection<IEntity> Children { get; }

    void AddChild(IEntity entity);

    void RemoveChild(IEntity entity);
}