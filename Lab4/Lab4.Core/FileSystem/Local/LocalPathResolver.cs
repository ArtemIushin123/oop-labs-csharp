using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Utils;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Local;

public class LocalPathResolver : IPathResolver
{
    private readonly PathUtilsService _utils;

    public LocalPathResolver(PathUtilsService utils)
    {
        _utils = utils ?? throw new ArgumentNullException(nameof(utils));
    }

    public string NormalizeAbsolute(string absolutePath)
    {
        if (string.IsNullOrEmpty(absolutePath))
            return "/";

        return _utils.Normalize(absolutePath);
    }

    public string Resolve(string rootAbsolutePath, string currentLocalPath, string providedPath)
    {
        ArgumentNullException.ThrowIfNull(rootAbsolutePath);
        ArgumentNullException.ThrowIfNull(currentLocalPath);
        ArgumentNullException.ThrowIfNull(providedPath);

        string baseLocal = NormalizeAbsolute(currentLocalPath);
        string candidateLocal;

        if (IsAbsolute(providedPath))
        {
            candidateLocal = NormalizeAbsolute(providedPath);
        }
        else
        {
            candidateLocal = _utils.Normalize(PathCombineUnix(baseLocal, providedPath));
        }

        if (!IsWithinRoot(candidateLocal))
            throw new InvalidOperationException("Resolved path is outside of connection root.");

        return candidateLocal;
    }

    public bool IsAbsolute(string path) => !string.IsNullOrEmpty(path) && path.StartsWith('/');

    private static string PathCombineUnix(string a, string b)
    {
        if (string.IsNullOrEmpty(a) || a == "/") return "/" + b.TrimStart('/');
        return a.TrimEnd('/') + "/" + b.TrimStart('/');
    }

    private static bool IsWithinRoot(string localPath)
    {
        return localPath == "/" || localPath.StartsWith('/');
    }
}
