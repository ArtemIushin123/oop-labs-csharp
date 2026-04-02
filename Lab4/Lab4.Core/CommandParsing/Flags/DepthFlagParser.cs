namespace Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Flags;

public class DepthFlagParser : IFlagParser
{
    public string FlagName => "-d";

    public bool CanParse(string token)
    {
        if (string.IsNullOrEmpty(token)) return false;
        if (token == "-d") return true;
        return token.StartsWith("-d=");
    }

    public void Apply(string token, ParsedCommand target)
    {
        string? value = null;

        if (token == "-d")
        {
            value = null;
        }
        else if (token.StartsWith("-d="))
        {
            value = token.Substring(3);
        }
        else if (token.Length > 2)
        {
            value = token.Substring(2);
        }

        target.SetFlag(FlagName, value);
    }
}