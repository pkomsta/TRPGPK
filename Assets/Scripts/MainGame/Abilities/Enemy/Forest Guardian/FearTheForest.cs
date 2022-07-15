using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearTheForest : Abilities
{
    public StatusEffect statusEffect;
    public override void Ability(Unit unit)
    {
        unit.TakeDamage(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack), true);
        GetCaster().GainResource(50);
        var spawnEffect = Instantiate(statusEffect, unit.transform);
        spawnEffect.SetAbiliti(this, unit);
    }

    public override string AbilityLogMessage()
    {
        return " <color=red> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " on " + " <color=red> " + GetTarget().unitName + "<color=white> dealing <color=blue> "
            + GetTarget().CountDamageTaken(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack), true) + " <color=white> damage and applying " + " <color=red> " + statusEffect.effectType + "<color=white> to the target";
    }
}
