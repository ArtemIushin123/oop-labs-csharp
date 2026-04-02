using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Helpers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;

public interface ICommand
{
    Task<CommandResult> ExecuteAsync();
}