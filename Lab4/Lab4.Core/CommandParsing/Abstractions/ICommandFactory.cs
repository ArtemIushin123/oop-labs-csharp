using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Abstractions;

public interface ICommandFactory
{
    ICommand CreateCommand(ParsedCommand parsed);
}