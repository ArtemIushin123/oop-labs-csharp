using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Local;

public class LocalFolderAdapter : IFolder
{
    public string SystemPath { get; }

    private readonly List<IEntity> _children = new();

    public LocalFolderAdapter(string systemPath)
    {
        SystemPath = Path.GetFullPath(systemPath);
    }

    public string Name => Path.GetFileName(SystemPath);

    public IFolder? Parent { get; private set; }

    public IReadOnlyCollection<IEntity> Children
    {
        get
        {
            _children.Clear();
            if (!Directory.Exists(SystemPath)) return _children.AsReadOnly();

            foreach (string dir in Directory.EnumerateDirectories(SystemPath))
            {
                var folder = new LocalFolderAdapter(dir);
                folder.SetParent(this);
                _children.Add(folder);
            }

            foreach (string file in Directory.EnumerateFiles(SystemPath))
            {
                var f = new LocalFileAdapter(file);
                f.SetParent(this);
                _children.Add(f);
            }

            return _children.AsReadOnly();
        }
    }

    public void AddChild(IEntity entity)
    {
        entity.SetParent(this);
        _children.Add(entity);
    }

    public void RemoveChild(IEntity entity)
    {
        if (_children.Remove(entity))
            entity.SetParent(null);
    }

    public void SetParent(IFolder? parent) => Parent = parent;

    public IEntity Clone() => new LocalFolderAdapter(SystemPath);

    public override string ToString() => Name;
}