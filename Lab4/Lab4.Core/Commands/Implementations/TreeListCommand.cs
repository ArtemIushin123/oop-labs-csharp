using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Local;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementations;

public class TreeListCommand : ICommand
{
    private readonly FileSystemConnection _connection;
    private readonly ITreeRenderer _renderer;
    private readonly TreeRenderOptions _options;
    private readonly IPathResolver _pathResolver;

    public TreeListCommand(FileSystemConnection connection, ITreeRenderer renderer, TreeRenderOptions options, IPathResolver pathResolver)
    {
        _connection = connection;
        _renderer = renderer;
        _options = options;
        _pathResolver = pathResolver;
    }

    public Task<CommandResult> ExecuteAsync()
    {
        if (_connection.State is ConnectionState.Disconnected)
            return Task.FromResult(CommandResult.Fail("Not connected"));

        string localUnix;
        try
        {
            localUnix = _pathResolver.Resolve(_connection.ConnectionRoot, _connection.CurrentLocalPath, ".");
        }
        catch (Exception ex)
        {
            return Task.FromResult(CommandResult.Fail(ex.Message));
        }

        string systemPath = ConvertUnixToSystem(_connection.ConnectionRoot, localUnix);
        var folderAdapter = new LocalFolderAdapter(systemPath);

        string tree = _renderer.RenderTree(folderAdapter, _options);
        return Task.FromResult(CommandResult.Ok(tree));
    }

    private static string ConvertUnixToSystem(string connectionRootSystemPath, string unixAbsolutePath)
    {
        string rel = unixAbsolutePath.TrimStart('/');
        if (string.IsNullOrEmpty(rel)) return Path.GetFullPath(connectionRootSystemPath);
        return Path.GetFullPath(Path.Combine(connectionRootSystemPath, rel.Replace('/', Path.DirectorySeparatorChar)));
    }
}