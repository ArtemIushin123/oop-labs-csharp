using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Local;

public class LocalFileAdapter : IFile
{
    public string SystemPath { get; }

    public LocalFileAdapter(string systemPath)
    {
        SystemPath = Path.GetFullPath(systemPath);
    }

    public string Name => Path.GetFileName(SystemPath);

    public IFolder? Parent { get; private set; }

    public void SetParent(IFolder? parent) => Parent = parent;

    public IEntity Clone() => new LocalFileAdapter(SystemPath);

    public override string ToString() => Name;
}