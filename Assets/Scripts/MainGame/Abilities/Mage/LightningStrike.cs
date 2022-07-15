using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : Abilities
{
    public float manaPercent = 0.05f;
    int damage;
    public override void Ability(Unit unit)
    {

        if (GetCaster().canSpendResource((int)(GetCaster().maxAbilityResource * manaPercent)))
        {
            damage = (int)(GetCaster().maxAbilityResource * manaPercent) + abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack);
            GetCaster().SpendResource((int)(GetCaster().maxAbilityResource * manaPercent));
        }
        else
        {
            damage = abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack);
        }
        
        List<Unit> hitUnits = new List<Unit>();
        Vector3Int pos = Gameboard.Instance.GetClosestCell(unit.transform.position);
        for (int i = -1; i < abilityRadius; i++)
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
                u.TakeDamage(damage, true);
            }
            
        }
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " on " + " <color=red> " + GetTarget().unitName + "<color=white> " + " dealing "
                    + " <color=blue> " + GetTarget().CountDamageTaken(damage, true) + "<color=white> damage to the target and around it";
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + (resourceCost + (int)(GetCaster().maxAbilityResource * manaPercent)) + " \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
           + "<color=white>Damage: <color=blue>" + (abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack)) + " + " + ((int)GetCaster().maxAbilityResource * manaPercent) + " \n"
           + "<color=white>Range: " + abilityRange
           + "<color=white>Radius: " + abilityRadius;
    }
}
