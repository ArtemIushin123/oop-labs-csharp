namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Utils;

public class NameCollisionResolver
{
    public string Resolve(string folder, string fileName, Func<string, bool> exists)
    {
        string name = fileName;
        int index = 1;

        while (exists(name))
        {
            name = $"{fileName}_{index}";
            index++;
        }

        return name;
    }
}