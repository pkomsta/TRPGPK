using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningRageAbility : Abilities
{
    public StatusEffect statusEffect;
    public override void Ability(Unit unit)
    {
        var spawnEffect = Instantiate(statusEffect, unit.transform);
        spawnEffect.SetAbiliti(this, null);
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> burning themselfs and emiting aura that burns enemies around.";
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + resourceCost + " \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
             + "<color=white> Effect Duration: " + statusEffect.effectDuration + " \n"
            + "<color=white>Damage Self: <color=blue>" + ((int)((abilityBaseModifier * GetCaster().magickAttack))) + " \n"
            + "<color=white>Damage AOE:  <color=blue>" + (4*((int)((abilityBaseModifier * GetCaster().magickAttack))));

    }
}
