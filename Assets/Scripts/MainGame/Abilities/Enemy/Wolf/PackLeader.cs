using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackLeader : Abilities
{
    public StatusEffect statusEffect;
    public override void Ability(Unit unit)
    {
        List<Unit> hitUnits = new List<Unit>();
        Vector3Int pos = Gameboard.Instance.GetClosestCell(GetCaster().transform.position);
        for (int i = -abilityRadius; i < abilityRadius; i++)
        {

            for (int j = -1; j < abilityRadius; j++)
            {
                Vector3Int newPos = new Vector3Int(pos.x + i, pos.y, pos.z + j);
               
                
                    var addUnit = Gameboard.Instance.GetUnit(newPos);
                    if (addUnit != null)
                    {
                        hitUnits.Add(addUnit);

                    }
                


            }

        }
        foreach (Unit u in hitUnits)
        {
            if (u.GetComponent<Enemy>())
            {
                var spawnEffect = Instantiate(statusEffect, u.transform);
                spawnEffect.SetAbiliti(this, u);
            }
            
        }

    }
    public override string AbilityLogMessage()
    {
        return " <color=red> " + GetCaster().unitName + "<color=white> " + " used " + " <color=yellow> " + abilityName + " <color=white>" + " giving all nerbay allies " + abilityBaseValue + "<color=red> attack <color=white>and +1 speed";
    }
}
