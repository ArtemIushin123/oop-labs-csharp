namespace Itmo.ObjectOrientedProgramming.Lab3.Game;

public record BattleResult
{
    private BattleResult() { }

    public sealed record Draw : BattleResult;

    public sealed record Player1Wins : BattleResult;

    public sealed record Player2Wins : BattleResult;
}
