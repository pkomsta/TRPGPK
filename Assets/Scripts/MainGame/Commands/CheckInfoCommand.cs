using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInfoCommand : CommandManager.ICommand
{
    private Vector3Int m_To;


    public CheckInfoCommand(Vector3Int end)
    {
        m_To = end;
    }

    public void Execute()
    {
        var unit = Gameboard.Instance.GetUnit(m_To);
        Gameboard.Instance.ShowUnitInfo(unit);

    }
}

