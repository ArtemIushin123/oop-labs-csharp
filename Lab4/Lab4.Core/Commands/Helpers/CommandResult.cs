namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Helpers;

public class CommandResult
{
    public bool Success { get; }

    public string Message { get; }

    public string? Output { get; }

    public string? Error { get; }

    public CommandResult(bool success, string message = "", string? output = null, string? error = null)
    {
        Success = success;
        Message = message;
        Output = output;
        Error = error;
    }

    public static CommandResult Ok(string output = "", string message = "")
    {
        return new CommandResult(true, message, output, null);
    }

    public static CommandResult Fail(string error, string message = "")
    {
        return new CommandResult(false, message, null, error);
    }
}