using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Models;

public class FileEntity : BaseEntity, IFile
{
    public FileEntity(string name, string systemPath)
        : base(name)
    {
        SystemPath = systemPath;
    }

    public override IEntity Clone()
    {
        return new FileEntity(Name, SystemPath);
    }

    public string SystemPath { get; }
}