namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Exceptions;

public class EntityNotFoundException : FileSystemException
{
    public EntityNotFoundException(string name)
        : base($"Entity '{name}' was not found.")
    { }
}