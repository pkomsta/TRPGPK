using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageBite: Abilities
{
    public StatusEffect statusEffect;
    public override void Ability(Unit unit)
    {
        unit.TakeDamage(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack),false);
        var spawnEffect = Instantiate(statusEffect, unit.transform);
        spawnEffect.SetAbiliti(this,unit);
    }

    public override string AbilityLogMessage()
    {
        return " <color=red> " + GetCaster().unitName + "<color=white>" + " used " + " <color=yellow> " + abilityName + "<color=white> " + " on " + " <color=green> " + GetTarget().unitName + "<color=white> " + " dealing "
                    + " <color=red> " + GetTarget().CountDamageTaken(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack),false)+ "<color=white>  and applying <color=red> " + statusEffect.effectType;
    }

    
}
