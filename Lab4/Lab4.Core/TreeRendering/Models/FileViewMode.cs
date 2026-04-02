namespace Itmo.ObjectOrientedProgramming.Lab4.Core.TreeRendering.Models;

public record FileViewMode
{
    private FileViewMode() { }

    public sealed record TextMode : FileViewMode;

    public sealed record InfoMode : FileViewMode;
}