using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing;

public class ParsedCommand
{
    private readonly List<string> _positionals = new();
    private readonly Dictionary<string, string?> _flags = new();

    public string CommandGroup { get; set; } = string.Empty;

    public string CommandName { get; set; } = string.Empty;

    public IReadOnlyList<string> Positionals => _positionals.AsReadOnly();

    public IReadOnlyDictionary<string, string?> Flags => new ReadOnlyDictionary<string, string?>(_flags);

    public void AddPositional(string value)
    {
        _positionals.Add(value);
    }

    public void SetFlag(string name, string? value)
    {
        _flags[name] = value;
    }
}