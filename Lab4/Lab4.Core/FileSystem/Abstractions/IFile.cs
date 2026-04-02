namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

public interface IFile : IEntity
{
    string SystemPath { get; }
}