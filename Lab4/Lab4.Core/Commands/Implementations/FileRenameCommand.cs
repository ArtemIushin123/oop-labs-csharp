using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementations;

public class FileRenameCommand : ICommand
{
    private readonly FileSystemConnection _connection;
    private readonly string _filePath;
    private readonly string _newName;

    public FileRenameCommand(FileSystemConnection connection, string filePath, string newName)
    {
        _connection = connection;
        _filePath = filePath;
        _newName = newName;
    }

    public Task<CommandResult> ExecuteAsync()
    {
        if (_connection.State is ConnectionState.Disconnected)
            return Task.FromResult(CommandResult.Fail("Not connected"));

        try
        {
            _connection.FileSystem.Rename(_filePath, _newName);
            return Task.FromResult(CommandResult.Ok($"Renamed '{_filePath}' to '{_newName}'"));
        }
        catch (Exception ex)
        {
            return Task.FromResult(CommandResult.Fail(ex.Message));
        }
    }
}
