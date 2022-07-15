using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : Abilities
{
    int healedvalue;
    public override void Ability(Unit unit)
    {
        healedvalue = (int)(unit.maxHealth * (unit.abilityResource / (float)(unit.maxAbilityResource * 2)));
        unit.GainHealth(healedvalue);
        unit.SpendResource(unit.abilityResource);
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " restoring <color=green>"
           + healedvalue + "<color=green> health ";

    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + GetCaster().abilityResource +"\n"
             + "<color=white> Cooldown: " + cooldown + " \n"
           + "<color=red>Health Gain: <color=green> 0.5% <color=white> per  1 rage";

    }
}
