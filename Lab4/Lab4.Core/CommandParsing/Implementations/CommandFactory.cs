using Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementations;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Implementations;

public class CommandFactory : ICommandFactory
{
    private readonly FileSystemConnection _connection;
    private readonly IFileSystem _fileSystem;
    private readonly IPathResolver _pathResolver;
    private readonly ITreeRenderer _treeRenderer;
    private readonly IFileViewer _fileViewer;

    public CommandFactory(
        FileSystemConnection connection,
        IFileSystem fileSystem,
        IPathResolver pathResolver,
        ITreeRenderer treeRenderer,
        IFileViewer fileViewer)
    {
        _connection = connection;
        _fileSystem = fileSystem;
        _pathResolver = pathResolver;
        _treeRenderer = treeRenderer;
        _fileViewer = fileViewer;
    }

    public ICommand CreateCommand(ParsedCommand parsed)
    {
        if (string.IsNullOrWhiteSpace(parsed.CommandGroup))
            throw new ArgumentException("Command group required", nameof(parsed));

        return parsed.CommandGroup switch
        {
            "connect" => BuildConnect(parsed),
            "disconnect" => new DisconnectCommand(_connection),

            "tree" => BuildTree(parsed),
            "file" => BuildFile(parsed),

            _ => throw new InvalidOperationException($"Unknown command group '{parsed.CommandGroup}'"),
        };
    }

    private ConnectCommand BuildConnect(ParsedCommand parsed)
    {
        if (parsed.Positionals.Count < 1)
            throw new ArgumentException("connect requires Address parameter");

        string address = parsed.Positionals[0];
        return new ConnectCommand(_connection, _fileSystem, address);
    }

    private ICommand BuildTree(ParsedCommand parsed)
    {
        if (string.IsNullOrWhiteSpace(parsed.CommandName))
            throw new ArgumentException("tree subcommand required");

        return parsed.CommandName switch
        {
            "goto" => BuildTreeGoto(parsed),
            "list" => BuildTreeList(parsed),

            _ => throw new InvalidOperationException($"Unknown tree subcommand '{parsed.CommandName}'"),
        };
    }

    private TreeGotoCommand BuildTreeGoto(ParsedCommand parsed)
    {
        if (parsed.Positionals.Count < 1)
            throw new ArgumentException("tree goto requires Path parameter");

        string target = parsed.Positionals[0];
        return new TreeGotoCommand(_connection, _pathResolver, target);
    }

    private TreeListCommand BuildTreeList(ParsedCommand parsed)
    {
        int depth = 1;

        if (parsed.Flags.TryGetValue("-d", out string? dStr) &&
            int.TryParse(dStr, out int parsedDepth))
        {
            depth = parsedDepth;
        }

        var options = new TreeRenderOptions(
            depth,
            folderSymbol: "[D]",
            fileSymbol: "[F]",
            indentUnit: "  ");

        return new TreeListCommand(_connection, _treeRenderer, options, _pathResolver);
    }

    private ICommand BuildFile(ParsedCommand parsed)
    {
        if (string.IsNullOrWhiteSpace(parsed.CommandName))
            throw new ArgumentException("file subcommand required");

        return parsed.CommandName switch
        {
            "show" => BuildFileShow(parsed),
            "move" => BuildFileMove(parsed),
            "copy" => BuildFileCopy(parsed),
            "delete" => BuildFileDelete(parsed),
            "rename" => BuildFileRename(parsed),

            _ => throw new InvalidOperationException($"Unknown file subcommand '{parsed.CommandName}'"),
        };
    }

    private FileShowCommand BuildFileShow(ParsedCommand parsed)
    {
        if (parsed.Positionals.Count < 1)
            throw new ArgumentException("file show requires Path parameter");

        string path = parsed.Positionals[0];

        string modeStr = parsed.Flags.TryGetValue("-m", out string? mStr)
            ? mStr?.ToLowerInvariant() ?? "text"
            : "text";

        FileViewMode mode = modeStr switch
        {
            "text" => new FileViewMode.TextMode(),
            "info" => new FileViewMode.InfoMode(),
            _ => new FileViewMode.TextMode(),
        };

        return new FileShowCommand(_connection, _fileViewer, _pathResolver, path, mode);
    }

    private FileMoveCommand BuildFileMove(ParsedCommand parsed)
    {
        if (parsed.Positionals.Count < 2)
            throw new ArgumentException("file move requires Source and Destination");

        return new FileMoveCommand(
            _connection,
            parsed.Positionals[0],
            parsed.Positionals[1]);
    }

    private FileCopyCommand BuildFileCopy(ParsedCommand parsed)
    {
        if (parsed.Positionals.Count < 2)
            throw new ArgumentException("file copy requires Source and Destination");

        return new FileCopyCommand(
            _connection,
            parsed.Positionals[0],
            parsed.Positionals[1]);
    }

    private FileDeleteCommand BuildFileDelete(ParsedCommand parsed)
    {
        if (parsed.Positionals.Count < 1)
            throw new ArgumentException("file delete requires Path");

        return new FileDeleteCommand(_connection, parsed.Positionals[0]);
    }

    private FileRenameCommand BuildFileRename(ParsedCommand parsed)
    {
        if (parsed.Positionals.Count < 2)
            throw new ArgumentException("file rename requires Path and NewName");

        return new FileRenameCommand(
            _connection,
            parsed.Positionals[0],
            parsed.Positionals[1]);
    }
}
