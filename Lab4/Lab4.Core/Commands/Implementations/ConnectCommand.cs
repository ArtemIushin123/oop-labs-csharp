using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementations;

public class ConnectCommand : ICommand
{
    private readonly FileSystemConnection _connection;
    private readonly IFileSystem _fileSystem;
    private readonly string _address;

    public ConnectCommand(FileSystemConnection connection, IFileSystem fileSystem, string address)
    {
        _connection = connection;
        _fileSystem = fileSystem;
        _address = address;
    }

    public Task<CommandResult> ExecuteAsync()
    {
        if (_connection.State is ConnectionState.Connected)
            return Task.FromResult(CommandResult.Fail("Already connected"));

        if (!_fileSystem.Exists(_address) || !_fileSystem.IsDirectory(_address))
            return Task.FromResult(CommandResult.Fail($"Path '{_address}' does not exist or is not a directory"));

        _connection.ChangeDirectory(_address);
        _connection.State = new ConnectionState.Connected();

        return Task.FromResult(CommandResult.Ok($"Connected to {_address}"));
    }
}