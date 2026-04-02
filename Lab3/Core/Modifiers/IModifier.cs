namespace Itmo.ObjectOrientedProgramming.Lab3.Core.Modifiers;

public interface IModifier
{
    bool IsExpired { get; }

    IModifier Clone();
}
