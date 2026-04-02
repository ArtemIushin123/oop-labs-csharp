namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Utils;

public class PathUtilsService
{
    public string CombineUnix(string left, string right)
    {
        if (string.IsNullOrEmpty(left) || left == "/") return "/" + right.TrimStart('/');
        return left.TrimEnd('/') + "/" + right.TrimStart('/');
    }

    public string Normalize(string path)
    {
        if (string.IsNullOrEmpty(path)) return "/";
        string[] parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var stack = new Stack<string>();
        foreach (string p in parts)
        {
            if (p == ".") continue;
            if (p == "..")
            {
                if (stack.Count > 0) stack.Pop();
                continue;
            }

            stack.Push(p);
        }

        string[] arr = stack.Reverse().ToArray();
        return "/" + string.Join('/', arr);
    }
}