namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

public interface IPathResolver
{
    string Resolve(string rootAbsolutePath, string currentLocalPath, string providedPath);

    bool IsAbsolute(string path);

    string NormalizeAbsolute(string absolutePath);
}