using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuriousBlow: Abilities
{
    public override void Ability(Unit unit)
    {
        unit.TakeDamage(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack),false);
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " on " + " <color=red> " + GetTarget().unitName + "<color=white> " + " dealing "
                    + " <color=red> " + GetTarget().CountDamageTaken(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack),false) + " ";
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + resourceCost + " \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
            + "<color=white>Damage: <color=red>" + (abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack)) + " \n"
            + "<color=white>Range: " + abilityRange;
    }
}
