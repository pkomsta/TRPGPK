using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : CommandManager.ICommand
{
    private Vector3Int m_From;
    private Vector3Int m_To;

    public AttackCommand(Vector3Int start, Vector3Int end)
    {
        m_From = start;
        m_To = end;
    }

    public void Execute()
    {
        var attackingUnit = Gameboard.Instance.GetUnit(m_From);
        var attackedUnit = Gameboard.Instance.GetUnit(m_To);
        if (attackingUnit != null && attackedUnit != null)
        {
            Gameboard.Instance.AttackUnit(attackingUnit, attackedUnit);
            if (Gameboard.Instance.CurrentTeam == Unit.Team.Player)
                Gameboard.Instance.SwitchTeam();
        }
    }
}

