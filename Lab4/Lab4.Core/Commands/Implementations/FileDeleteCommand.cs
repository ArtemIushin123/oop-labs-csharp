using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementations;

public class FileDeleteCommand : ICommand
{
    private readonly FileSystemConnection _connection;
    private readonly string _filePath;

    public FileDeleteCommand(FileSystemConnection connection, string filePath)
    {
        _connection = connection;
        _filePath = filePath;
    }

    public Task<CommandResult> ExecuteAsync()
    {
        if (_connection.State is ConnectionState.Disconnected)
            return Task.FromResult(CommandResult.Fail("Not connected"));

        try
        {
            _connection.FileSystem.Delete(_filePath);
            return Task.FromResult(CommandResult.Ok($"Deleted '{_filePath}'"));
        }
        catch (Exception ex)
        {
            return Task.FromResult(CommandResult.Fail(ex.Message));
        }
    }
}