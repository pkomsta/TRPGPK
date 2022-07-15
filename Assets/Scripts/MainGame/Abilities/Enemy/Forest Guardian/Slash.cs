using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Abilities
{
    public override void Ability(Unit unit)
    {
        unit.TakeDamage(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack), false);
        unit.TakeDamage((int)( abilityBaseModifier * GetCaster().magickAttack), true);
    }

    public override string AbilityLogMessage()
    {
        return " <color=red> " + GetCaster().unitName + "<color=white> " + " used " + " <color=yellow> " + abilityName + "<color=white> " + " on " + " <color=green> " + GetTarget().unitName + "<color=white> " + " dealing "
                    + " <color=red> " + GetTarget().CountDamageTaken(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().attack), false) + "<color=white> + <color=blue> " + GetTarget().CountDamageTaken(abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack), true);
    }
}
