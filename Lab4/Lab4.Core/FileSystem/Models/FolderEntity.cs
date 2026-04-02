using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Models;

public class FolderEntity : BaseEntity, IFolder
{
    private readonly List<IEntity> _children = new();

    public FolderEntity(string name)
        : base(name) { }

    public IReadOnlyCollection<IEntity> Children => _children.AsReadOnly();

    public void AddChild(IEntity entity)
    {
        entity.SetParent(this);
        _children.Add(entity);
    }

    public void RemoveChild(IEntity entity)
    {
        _children.Remove(entity);
        entity.SetParent(null);
    }

    public override IEntity Clone()
    {
        var copy = new FolderEntity(Name);

        foreach (IEntity child in _children)
        {
            IEntity cloned = child.Clone();
            copy.AddChild(cloned);
        }

        return copy;
    }
}