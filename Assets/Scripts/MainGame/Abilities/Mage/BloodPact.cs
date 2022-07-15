using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPact : Abilities
{
    public override void Ability(Unit unit)
    {
        unit.TakeTrueDamage((int)(unit.health / 2));
        unit.GainResource((int)(unit.maxAbilityResource/2));
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " losing<color=green> "
            + (int)(GetTarget().health / 2) + "<color=white> and gaining <color=blue> " + (int)(GetTarget().maxAbilityResource / 2) + "<color=white> " + GetTarget().resource;

    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + resourceCost + " \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
           + "<color=white>Resource Gain: <color=blue> 50%<color=white> of current health";

    }
}
