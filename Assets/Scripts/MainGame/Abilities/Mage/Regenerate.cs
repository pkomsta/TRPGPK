using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : Abilities
{
    public StatusEffect statusEffect;
    public float manaPercent = 0.1f;
    public override void Ability(Unit unit)
    {
        
        if (GetCaster().canSpendResource((int)(GetCaster().maxAbilityResource * manaPercent)))
        {
            GetCaster().SpendResource((int)(GetCaster().maxAbilityResource * manaPercent));
        }
        else
        {
            GetCaster().TakeTrueDamage((int)(GetCaster().maxAbilityResource * manaPercent));
        }
        var spawnEffect = Instantiate(statusEffect, unit.transform);
        spawnEffect.SetAbiliti(this, null);


    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> applying <color=green>"
             + statusEffect.effectType + "<color=white> to himself ";
             
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + (resourceCost + (int)(GetCaster().maxAbilityResource * manaPercent)) + " \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
            + "<color=white>Duration: " + statusEffect.effectDuration + " \n"
            + "<color=white>Health Per Turn:<color=green> " +(int) GetCaster().wisdom;

    }
}
