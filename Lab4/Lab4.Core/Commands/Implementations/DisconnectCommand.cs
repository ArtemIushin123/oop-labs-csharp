using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementations;

public class DisconnectCommand : ICommand
{
    private readonly FileSystemConnection _connection;

    public DisconnectCommand(FileSystemConnection connection)
    {
        _connection = connection;
    }

    public Task<CommandResult> ExecuteAsync()
    {
        if (_connection.State is ConnectionState.Disconnected)
            return Task.FromResult(CommandResult.Fail("Already disconnected"));

        _connection.Disconnect();

        return Task.FromResult(CommandResult.Ok("Disconnected"));
    }
}