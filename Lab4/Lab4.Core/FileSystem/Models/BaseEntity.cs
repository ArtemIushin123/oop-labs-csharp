using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Models;

public abstract class BaseEntity : IEntity
{
    public string Name { get; }

    public IFolder? Parent { get; private set; }

    protected BaseEntity(string name)
    {
        Name = name;
    }

    public void SetParent(IFolder? parent)
    {
        Parent = parent;
    }

    public abstract IEntity Clone();
}