using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest: Abilities
{
    public override void Ability(Unit unit)
    {
        unit.GainResource(abilityBaseValue);
    }

    public override string AbilityLogMessage()
    {
        return " <color=red> " + GetCaster().unitName + "<color=white> " + " used " + " <color=yellow> " + abilityName + " <color=white>" + " restoring " +  abilityBaseValue + "<color=yellow> " + GetCaster().resource ;
    }

   
}
