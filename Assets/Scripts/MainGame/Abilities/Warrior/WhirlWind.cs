using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlWind : Abilities
{
    public override void Ability(Unit unit)
    {
        List<Unit> hitUnits = new List<Unit>();
        Vector3Int pos = Gameboard.Instance.GetClosestCell(GetCaster().transform.position);
        for (int i = -1; i < abilityRadius; i++)
        {
            
                for(int j = -1; j < abilityRadius; j++)
            {
                Vector3Int newPos = new Vector3Int(pos.x + i, pos.y, pos.z + j);
                
                if(newPos != pos) {
                    var addUnit = Gameboard.Instance.GetUnit(newPos);
                    if (addUnit != null)
                    {
                        hitUnits.Add(addUnit);
                        
                    }
                }
                
               
            }

        }
        foreach(Unit u in hitUnits)
        {
            u.TakeDamage(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack), false);
        }

    }


    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " on " + " <color=red> " + GetTarget().unitName + "<color=white> " + " dealing "
                    + " <color=red> " + GetTarget().CountDamageTaken(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack), false) + "<color=white> to all nerbay enemies ";
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + resourceCost +" \n"
           + "<color=white> Cooldown: " + cooldown + " \n"
           + "<color=white>Damage: <color=red>" + (abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack)) + " \n"
           + "<color=white>Range: " + abilityRange;
    }
}
