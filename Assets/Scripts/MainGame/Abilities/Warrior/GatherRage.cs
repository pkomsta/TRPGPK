using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherRage : Abilities
{
    public override void Ability(Unit unit)
    {
        unit.GainResource(unit.maxAbilityResource);
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " restoring all of his"
           + "<color=red> " + GetCaster().resource + " ";
                    
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + resourceCost + " \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
           + "<color=red>Resource Gain: <color=red>" + GetCaster().maxAbilityResource;
           
    }
}
