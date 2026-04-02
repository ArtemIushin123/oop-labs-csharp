using Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Flags;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Implementations;

public class CommandParser : ICommandParser
{
    private readonly IEnumerable<IFlagParser> _flagParsers;

    public CommandParser(IEnumerable<IFlagParser> flagParsers)
    {
        _flagParsers = flagParsers;
    }

    public ParsedCommand Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Command required", nameof(input));

        string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var result = new ParsedCommand();

        if (tokens.Length >= 1) result.CommandGroup = tokens[0];
        if (tokens.Length >= 2) result.CommandName = tokens[1];

        for (int i = 2; i < tokens.Length; i++)
        {
            string token = tokens[i];

            IFlagParser? parser = _flagParsers.FirstOrDefault(p => p.CanParse(token));
            if (parser != null)
            {
                parser.Apply(token, result);
            }
            else
            {
                result.AddPositional(token);
            }
        }

        return result;
    }
}