using Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing;
using Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Flags;
using Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Implementations;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Helpers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Connections;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Filesystem.Local;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Implementations;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Utils;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public class ConsoleRunner
{
    private readonly ConsoleReader _reader;
    private readonly ConsoleWriter _writer;

    private readonly CommandParser _parser;
    private readonly CommandFactory _factory;
    private readonly FileSystemConnection _connection;

    public ConsoleRunner()
    {
        _reader = new ConsoleReader();
        _writer = new ConsoleWriter();

        var utils = new PathUtilsService();
        var pathResolver = new LocalPathResolver(utils);

        IFileSystem fileSystem = new LocalFileSystem("C:/");

        ITreeRenderer treeRenderer = new DefaultTreeRenderer();
        IFileViewer fileViewer = new DefaultFileViewer();

        var flagParsers = new List<IFlagParser>
        {
            new ModeFlagParser(),
        };

        _parser = new CommandParser(flagParsers);

        _connection = new FileSystemConnection(fileSystem, "C:/");

        _factory = new CommandFactory(
            _connection,
            fileSystem,
            pathResolver,
            treeRenderer,
            fileViewer);
    }

    public void Run()
    {
        while (true)
        {
            _writer.Write("> ");
            string? input = _reader.ReadLine();

            if (input is null) continue;

            ParsedCommand parseResult = _parser.Parse(input);

            ICommand command = _factory.CreateCommand(parseResult);

            Task<CommandResult> result = command.ExecuteAsync();

            _writer.WriteLine(result.Result.Output);
        }
    }
}
