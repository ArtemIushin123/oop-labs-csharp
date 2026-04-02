namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Exceptions;

public class InvalidPathException : FileSystemException
{
    public InvalidPathException(string path)
        : base($"Path '{path}' is invalid.")
    { }
}