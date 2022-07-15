using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseStanceAbility : Abilities
{
    public StatusEffect statusEffect;
    public override void Ability(Unit unit)
    {
        var spawnEffect = Instantiate(statusEffect, unit.transform);
        spawnEffect.SetAbiliti(this,null);
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> increasing his defense by <color=green>"
             + (int)((statusEffect as DefensiveStance).defenseChange * GetCaster().defense) + "<color=white> but decreasing his <color=red> attack <color=white> by <color=red>"
             + (int)((statusEffect as DefensiveStance).attackChange * GetCaster().attack);
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + resourceCost+" \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
            + "<color=white>Defanse Increase: <color=red>" + (int)((statusEffect as DefensiveStance).defenseChange * GetCaster().defense)+" \n"
            + "<color=white>Attack Decrease: " + (int)((statusEffect as DefensiveStance).attackChange * GetCaster().attack);

    }

}
