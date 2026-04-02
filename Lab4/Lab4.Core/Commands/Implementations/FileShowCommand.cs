using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Local;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementations;

public class FileShowCommand : ICommand
{
    private readonly FileSystemConnection _connection;
    private readonly IFileViewer _viewer;
    private readonly IPathResolver _pathResolver;
    private readonly string _filePath;
    private readonly FileViewMode _mode;

    public FileShowCommand(FileSystemConnection connection, IFileViewer viewer, IPathResolver pathResolver, string filePath, FileViewMode mode)
    {
        _connection = connection;
        _viewer = viewer;
        _pathResolver = pathResolver;
        _filePath = filePath;
        _mode = mode;
    }

    public Task<CommandResult> ExecuteAsync()
    {
        if (_connection.State is ConnectionState.Disconnected)
            return Task.FromResult(CommandResult.Fail("Not connected"));

        string localUnixPath;
        try
        {
            localUnixPath = _pathResolver.Resolve(_connection.ConnectionRoot, _connection.CurrentLocalPath, _filePath);
        }
        catch (Exception ex)
        {
            return Task.FromResult(CommandResult.Fail(ex.Message));
        }

        if (!_connection.FileSystem.Exists(localUnixPath) || !_connection.FileSystem.IsFile(localUnixPath))
            return Task.FromResult(CommandResult.Fail($"File '{_filePath}' does not exist"));

        string systemPath = ConvertUnixToSystem(_connection.ConnectionRoot, localUnixPath);
        var fileAdapter = new LocalFileAdapter(systemPath);

        string output = _viewer.ViewFile(fileAdapter, _mode);

        return Task.FromResult(CommandResult.Ok(output));
    }

    private static string ConvertUnixToSystem(string connectionRootSystemPath, string unixAbsolutePath)
    {
        string rel = unixAbsolutePath.TrimStart('/');
        if (string.IsNullOrEmpty(rel)) return Path.GetFullPath(connectionRootSystemPath);
        return Path.GetFullPath(Path.Combine(connectionRootSystemPath, rel.Replace('/', Path.DirectorySeparatorChar)));
    }
}