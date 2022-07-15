using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingSword : Abilities
{
    public float abilityBaseModifierMagick = 2.0f;
    public override void Ability(Unit unit)
    {
        unit.TakeDamage((int)(abilityBaseModifier * GetCaster().attack), false);
        unit.TakeDamage(abilityBaseValue + (int)(abilityBaseModifierMagick * GetCaster().attack), true);
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " on " + " <color=red> " + GetTarget().unitName + "<color=white> " + " dealing "
                    + " <color=red> " + GetTarget().CountDamageTaken((int)(abilityBaseModifier * GetCaster().attack), false) + "<color=white> + <color=blue> "
                    + GetTarget().CountDamageTaken((int)(abilityBaseValue + abilityBaseModifier * GetCaster().attack), true) + "<color=white> damage";
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + resourceCost + " \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
           + "<color=white>Damage: <color=red>" + ((int)(abilityBaseModifier * GetCaster().attack)) + " \n"
           + "<color=white>Damage: <color=blue>" +( abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack)) + " \n"
           + "<color=white>Range: " + abilityRange;
    }
}
