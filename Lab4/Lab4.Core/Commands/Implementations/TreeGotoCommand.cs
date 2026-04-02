using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementations;

public class TreeGotoCommand : ICommand
{
    private readonly FileSystemConnection _connection;
    private readonly IPathResolver _pathResolver;
    private readonly string _targetLocalOrAbsolute;

    public TreeGotoCommand(FileSystemConnection connection, IPathResolver pathResolver, string targetLocalOrAbsolute)
    {
        _connection = connection;
        _pathResolver = pathResolver;
        _targetLocalOrAbsolute = targetLocalOrAbsolute;
    }

    public Task<CommandResult> ExecuteAsync()
    {
        if (_connection.State is ConnectionState.Disconnected)
            return Task.FromResult(CommandResult.Fail("Not connected"));

        string resolvedLocal;
        try
        {
            resolvedLocal = _pathResolver.Resolve(_connection.ConnectionRoot, _connection.CurrentLocalPath, _targetLocalOrAbsolute);
        }
        catch (Exception ex)
        {
            return Task.FromResult(CommandResult.Fail(ex.Message));
        }

        if (!_connection.FileSystem.Exists(resolvedLocal) || !_connection.FileSystem.IsDirectory(resolvedLocal))
            return Task.FromResult(CommandResult.Fail($"Directory '{_targetLocalOrAbsolute}' does not exist"));

        _connection.ChangeDirectory(resolvedLocal);

        return Task.FromResult(CommandResult.Ok(message: $"Current path: {_connection.CurrentLocalPath}"));
    }
}
