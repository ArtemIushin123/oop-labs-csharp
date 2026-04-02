using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Local;

public class LocalFileSystem : IFileSystem
{
    private readonly string _rootSystemPath;

    public LocalFileSystem(string rootSystemPath)
    {
        if (string.IsNullOrEmpty(rootSystemPath)) throw new ArgumentNullException(nameof(rootSystemPath));
        _rootSystemPath = Path.GetFullPath(rootSystemPath);
    }

    public bool Exists(string absolutePath)
    {
        string sys = MapUnixAbsoluteToSystem(absolutePath);
        return File.Exists(sys) || Directory.Exists(sys);
    }

    public bool IsDirectory(string absolutePath)
    {
        string sys = MapUnixAbsoluteToSystem(absolutePath);
        return Directory.Exists(sys);
    }

    public bool IsFile(string absolutePath)
    {
        string sys = MapUnixAbsoluteToSystem(absolutePath);
        return File.Exists(sys);
    }

    public IEnumerable<string> List(string absoluteDirectoryPath)
    {
        string sys = MapUnixAbsoluteToSystem(absoluteDirectoryPath);
        if (!Directory.Exists(sys)) throw new DirectoryNotFoundException(absoluteDirectoryPath);
        foreach (string dir in Directory.EnumerateDirectories(sys))
            yield return "/" + Path.GetRelativePath(_rootSystemPath, dir).Replace(Path.DirectorySeparatorChar, '/').TrimStart('/');
        foreach (string file in Directory.EnumerateFiles(sys))
            yield return "/" + Path.GetRelativePath(_rootSystemPath, file).Replace(Path.DirectorySeparatorChar, '/').TrimStart('/');
    }

    public byte[] ReadFile(string absoluteFilePath)
    {
        string sys = MapUnixAbsoluteToSystem(absoluteFilePath);
        if (!File.Exists(sys)) throw new FileNotFoundException(absoluteFilePath);
        return File.ReadAllBytes(sys);
    }

    public void Copy(string sourceAbsolutePath, string targetAbsolutePath)
    {
        string srcSys = MapUnixAbsoluteToSystem(sourceAbsolutePath);
        string dstSys = MapUnixAbsoluteToSystem(targetAbsolutePath);

        if (!File.Exists(srcSys)) throw new FileNotFoundException(sourceAbsolutePath);
        if (Directory.Exists(dstSys))
        {
            string fileName = Path.GetFileName(srcSys);
            string destFile = Path.Combine(dstSys, fileName);
            if (File.Exists(destFile)) throw new IOException("Name collision: " + destFile);
            File.Copy(srcSys, destFile);
            return;
        }

        string? dstDir = Path.GetDirectoryName(dstSys);
        if (dstDir == null) throw new IOException("Destination invalid");
        if (!Directory.Exists(dstDir)) throw new DirectoryNotFoundException(targetAbsolutePath);
        if (File.Exists(dstSys)) throw new IOException("Name collision: " + dstSys);
        File.Copy(srcSys, dstSys);
    }

    public void Move(string sourceAbsolutePath, string targetAbsolutePath)
    {
        string srcSys = MapUnixAbsoluteToSystem(sourceAbsolutePath);
        string dstSys = MapUnixAbsoluteToSystem(targetAbsolutePath);

        if (!File.Exists(srcSys)) throw new FileNotFoundException(sourceAbsolutePath);
        if (Directory.Exists(dstSys))
        {
            string fileName = Path.GetFileName(srcSys);
            string destFile = Path.Combine(dstSys, fileName);
            if (File.Exists(destFile)) throw new IOException("Name collision: " + destFile);
            File.Move(srcSys, destFile);
            return;
        }

        string? dstDir = Path.GetDirectoryName(dstSys);
        if (dstDir == null) throw new IOException("Destination invalid");
        if (!Directory.Exists(dstDir)) throw new DirectoryNotFoundException(targetAbsolutePath);
        if (File.Exists(dstSys)) throw new IOException("Name collision: " + dstSys);
        File.Move(srcSys, dstSys);
    }

    public void Delete(string absolutePath)
    {
        string sys = MapUnixAbsoluteToSystem(absolutePath);
        if (File.Exists(sys))
        {
            File.Delete(sys);
            return;
        }

        if (Directory.Exists(sys))
        {
            Directory.Delete(sys, recursive: false);
            return;
        }

        throw new FileNotFoundException(absolutePath);
    }

    public void Rename(string absolutePath, string newName)
    {
        ArgumentException.ThrowIfNullOrEmpty(newName);
        if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) throw new ArgumentException("Invalid new name");

        string sys = MapUnixAbsoluteToSystem(absolutePath);

        if (!File.Exists(sys)) throw new FileNotFoundException(absolutePath);

        string dir = Path.GetDirectoryName(sys) ?? throw new IOException("Parent directory not found");
        string dest = Path.Combine(dir, newName);
        if (File.Exists(dest)) throw new IOException("Name collision: " + dest);
        File.Move(sys, dest);
    }

    private string MapUnixAbsoluteToSystem(string unixAbsolutePath)
    {
        string normalized = unixAbsolutePath == "/" ? "/" : unixAbsolutePath;
        string rel = normalized.TrimStart('/');
        if (string.IsNullOrEmpty(rel))
            return _rootSystemPath;
        return Path.GetFullPath(Path.Combine(_rootSystemPath, rel.Replace('/', Path.DirectorySeparatorChar)));
    }
}
