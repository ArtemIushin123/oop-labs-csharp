namespace Itmo.ObjectOrientedProgramming.Lab4.Core.CommandParsing.Flags;

public class ModeFlagParser : IFlagParser
{
    public string FlagName => "-m";

    public bool CanParse(string token)
    {
        if (string.IsNullOrEmpty(token)) return false;
        if (token == "-m") return true;
        return token.StartsWith("-m=");
    }

    public void Apply(string token, ParsedCommand target)
    {
        string? value = null;

        if (token == "-m")
        {
            value = null;
        }
        else if (token.StartsWith("-m="))
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