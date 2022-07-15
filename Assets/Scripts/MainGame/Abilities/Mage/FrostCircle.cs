using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostCircle : Abilities
{
    public StatusEffect statusEffect;
    int damage = 0;
    public float manaPercent = 0.05f;
    public override void Ability(Unit unit)
    {
        
        if(GetCaster().canSpendResource((int)(GetCaster().maxAbilityResource * manaPercent))){
           damage = (int)(GetCaster().maxAbilityResource * manaPercent) + abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack);
            GetCaster().SpendResource((int)(GetCaster().maxAbilityResource * manaPercent));
        }
        else
        {
            damage = abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack);
        }
        List<Unit> hitUnits = new List<Unit>();
        Vector3Int pos = Gameboard.Instance.GetClosestCell(GetCaster().transform.position);
        for (int i = -abilityRadius; i < abilityRadius; i++)
        {

            for (int j = -abilityRadius; j < abilityRadius; j++)
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
            u.TakeDamage(damage, true);
            var spawnEffect = Instantiate(statusEffect, u.transform);
            spawnEffect.SetAbiliti(this, u);
        }

    }


    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " on " + " <color=red> " + GetTarget().unitName + "<color=white> " + " dealing "
                    + " <color=blue> " + GetTarget().CountDamageTaken(damage, true) + "<color=white> to all nerbay enemies and applying to them " + statusEffect.effectType;
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + (resourceCost + (int)(GetCaster().maxAbilityResource * manaPercent)) + " \n"
           + "<color=white> Cooldown: " + cooldown + " \n"
           + "<color=white>Damage: <color=red>" + (abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack)) + " + " + ((int)GetCaster().maxAbilityResource * manaPercent) + " \n"
           + "<color=white>Range: " + abilityRange
           + "<color=white>Radius: " + abilityRadius
           + "<color=white>Duration: " + statusEffect.effectDuration;


    }
}
