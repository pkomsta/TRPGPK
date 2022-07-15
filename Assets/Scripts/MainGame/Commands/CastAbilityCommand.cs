using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastAbilityCommand : CommandManager.ICommand
{
    private Vector3Int m_From;
    private Vector3Int m_To;

    public CastAbilityCommand(Vector3Int start, Vector3Int end)
    {
        m_From = start;
        m_To = end;
    }

    public void Execute()
    {
        var castingUnit = Gameboard.Instance.GetUnit(m_From);
        var targetUnit = Gameboard.Instance.GetUnit(m_To);
        if(castingUnit.chosenAbility.GetCanBeCasted() && castingUnit.canSpendResource(castingUnit.chosenAbility.resourceCost))
        {
            if (castingUnit.chosenAbility.abilityType == Abilities.AbilityType.self_buff || castingUnit.chosenAbility.abilityType == Abilities.AbilityType.self_heal || castingUnit.chosenAbility.abilityType == Abilities.AbilityType.self_resource)
            {
                if (castingUnit != null)
                {
                    Gameboard.Instance.CastAbility(castingUnit, castingUnit);
                    if (Gameboard.Instance.CurrentTeam == Unit.Team.Player)
                        Gameboard.Instance.SwitchTeam();
                }
            }
            else
            {

                if (castingUnit != null && targetUnit != null)
                {
                    Gameboard.Instance.CastAbility(castingUnit, targetUnit);
                    if (Gameboard.Instance.CurrentTeam == Unit.Team.Player)
                        Gameboard.Instance.SwitchTeam();
                }

            }
        }
        

        
        
        
    }
}
