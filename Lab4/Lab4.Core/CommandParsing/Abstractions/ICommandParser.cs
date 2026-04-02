namespace Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Abstractions;

public interface ICommandParser
{
    ParsedCommand Parse(string input);
}