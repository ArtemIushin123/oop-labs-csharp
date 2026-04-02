namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

public interface IEntity
{
    string Name { get; }

    IFolder? Parent { get; }

    void SetParent(IFolder? parent);

    IEntity Clone();
}