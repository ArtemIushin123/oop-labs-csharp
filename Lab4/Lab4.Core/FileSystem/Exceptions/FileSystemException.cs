namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Exceptions;

public class FileSystemException : Exception
{
    public FileSystemException(string message)
        : base(message)
    { }
}