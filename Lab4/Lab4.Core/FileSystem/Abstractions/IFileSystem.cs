namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

public interface IFileSystem
{
    bool Exists(string absolutePath);

    bool IsDirectory(string absolutePath);

    bool IsFile(string absolutePath);

    IEnumerable<string> List(string absoluteDirectoryPath);

    byte[] ReadFile(string absoluteFilePath);

    void Copy(string sourceAbsolutePath, string targetAbsolutePath);

    void Move(string sourceAbsolutePath, string targetAbsolutePath);

    void Delete(string absolutePath);

    void Rename(string absolutePath, string newName);
}