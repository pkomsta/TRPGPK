using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningRage : StatusEffect
{
    
    public override string EffectBattleLogMessage()
    {
        return "<color=green>" + caster.unitName + "<color=white> took <color=blue>" + target.CountDamageTaken((int)((ability.abilityBaseModifier * ability.GetCaster().magickAttack)), true)
            + "<color=white> from <color=blue> " + effectType + " ";
    }

    public override void EffectTick(Unit unit)
    {
        SetEffectValue((int)((ability.abilityBaseModifier * ability.GetCaster().magickAttack)));

        unit.TakeDamage(effectValue, true);
        List<Unit> hitUnits = new List<Unit>();
        Vector3Int pos = Gameboard.Instance.GetClosestCell(caster.transform.position);
        for (int i = -1; i <= ability.abilityRadius; i++)
        {

            for (int j = -1; j <= ability.abilityRadius; j++)
            {
                Vector3Int newPos = new Vector3Int(pos.x + i, pos.y, pos.z + j);
                
                if (newPos != pos)
                {
                    var addUnit = Gameboard.Instance.GetUnit(newPos);
                    if (addUnit != null)
                    {
                        hitUnits.Add(addUnit);

                    }
                }


            }

        }
        foreach (Unit u in hitUnits)
        {
            u.TakeDamage(effectValue*4, true);
            Gameboard.Instance.UpdateBattleLog("<color=red>" + u.unitName + "<color=white> took <color=blue>" + u.CountDamageTaken(4*(int)((ability.abilityBaseModifier * ability.GetCaster().magickAttack)), true)
            + "<color=white> from <color=blue> " + effectType + " ");
        }

    }
}
