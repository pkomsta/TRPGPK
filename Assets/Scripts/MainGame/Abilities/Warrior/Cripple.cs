using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cripple : Abilities
{

    public StatusEffect statusEffect;
    public override void Ability(Unit unit)
    {
        unit.TakeDamage(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack), false);
        var spawnEffect = Instantiate(statusEffect, unit.transform);
        spawnEffect.SetAbiliti(this,unit);
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " on " + " <color=red> " + GetTarget().unitName + "<color=white> dealing <color=red> "
            + GetTarget().CountDamageTaken(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack), false) + " <color=white> damage and applying "+ " <color=red> " + statusEffect.effectType + "<color=white> to the target";
    }

    public override string Describtion()
    {

        return"Cost: <color=red>" + resourceCost + " \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
            + "<color=white>Damage: <color=red>" + (abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack)) + " \n"
            + "<color=white>Range: " + abilityRange + " \n"
            + "Bleed Duration: " + statusEffect.effectDuration + " \n"
            + "Bleed Damage: " + ((int)((statusEffect as Bleed).bleedDamageRatio * (int)(abilityBaseModifier * GetCaster().attack)));
    }
}
