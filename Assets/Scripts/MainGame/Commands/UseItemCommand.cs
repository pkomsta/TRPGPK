using System.Collections.Generic;
using UnityEngine;

public class UseItemCommand : CommandManager.ICommand
{
    private Unit unit;
    private ActionItem item;


    public UseItemCommand(Unit u, ActionItem i)
    {
        unit = u;
        item = i;
    }

    public void Execute()
    {
        item.Use(unit);
        Debug.Log("Used");
        if(Gameboard.Instance.CurrentTeam == Unit.Team.Player)
        Gameboard.Instance.SwitchTeam();

    }
}
