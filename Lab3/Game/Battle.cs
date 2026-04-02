using Itmo.ObjectOrientedProgramming.Lab3.Core.Creatures.Base;

namespace Itmo.ObjectOrientedProgramming.Lab3.Game;

public class Battle
{
    private readonly PlayerTable _player1Table;
    private readonly PlayerTable _player2Table;

    public Battle(PlayerTable player1Table, PlayerTable player2Table)
    {
        _player1Table = player1Table;
        _player2Table = player2Table;
    }

    public BattleResult Start()
    {
        bool player1Turn = true;

        if (!_player1Table.HasAttackingCreatures() && !_player2Table.HasAttackingCreatures())
            return new BattleResult.Draw();

        while (_player1Table.HasAliveCreatures() && _player2Table.HasAliveCreatures())
        {
            PlayerTable attackerTable = player1Turn ? _player1Table : _player2Table;
            PlayerTable defenderTable = player1Turn ? _player2Table : _player1Table;

            ICreature? attacker = attackerTable.GetNextAttacker();
            ICreature? defender = defenderTable.GetNextDefender();

            if (attacker is null && defender is null)
            {
                return new BattleResult.Draw();
            }

            if (attacker is null)
            {
                player1Turn = !player1Turn;
                continue;
            }

            if (defender is null)
            {
                return player1Turn ? new BattleResult.Player1Wins() : new BattleResult.Player2Wins();
            }

            attacker.Strike(defender);

            defenderTable.RemoveDeadCreatures();

            player1Turn = !player1Turn;
        }

        if (!_player1Table.HasAliveCreatures() && !_player2Table.HasAliveCreatures())
            return new BattleResult.Draw();

        if (_player1Table.HasAliveCreatures())
            return new BattleResult.Player1Wins();

        return new BattleResult.Player2Wins();
    }
}