using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : Abilities
{
    public override void Ability(Unit unit)
    {
        unit.GainHealth((int)(unit.vitality/2.0f));

    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " restoring "
           + "<color=green> " + GetCaster().vitality + " <color=white>health";

    }

    public override string Describtion()
    {
        return 
           "<color=green>Heal: <color=red>" + (int)(GetCaster().vitality / 2.0f);

    }
}
