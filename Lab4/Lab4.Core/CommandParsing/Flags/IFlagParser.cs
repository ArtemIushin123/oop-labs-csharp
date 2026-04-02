namespace Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Flags;

public interface IFlagParser
{
    string FlagName { get; }

    bool CanParse(string token);

    void Apply(string token, ParsedCommand target);
}