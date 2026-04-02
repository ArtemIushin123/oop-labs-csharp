using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementations;

public class FileCopyCommand : ICommand
{
    private readonly FileSystemConnection _connection;
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileCopyCommand(FileSystemConnection connection, string sourcePath, string destinationPath)
    {
        _connection = connection;
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public Task<CommandResult> ExecuteAsync()
    {
        if (_connection.State is ConnectionState.Disconnected)
            return Task.FromResult(CommandResult.Fail("Not connected"));

        try
        {
            _connection.FileSystem.Copy(_sourcePath, _destinationPath);
            return Task.FromResult(CommandResult.Ok($"Copied '{_sourcePath}' to '{_destinationPath}'"));
        }
        catch (Exception ex)
        {
            return Task.FromResult(CommandResult.Fail(ex.Message));
        }
    }
}