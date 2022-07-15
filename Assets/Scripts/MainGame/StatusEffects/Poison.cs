using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : StatusEffect
{
    public float poisonDamageRatio = 0.2f;
    public override string EffectBattleLogMessage()
    {
        return "<color=red>" + target.unitName + "<color=white> took <color=red>" + target.CountDamageTaken((int)((ability.abilityBaseModifier * ability.GetCaster().attack) * poisonDamageRatio), false)
            + "<color=white> from <color=red> " + effectType;
    }

    public override void EffectTick(Unit unit)
    {
        SetEffectValue((int)((ability.abilityBaseModifier * ability.GetCaster().attack) * poisonDamageRatio));

        unit.TakeDamage(effectValue, false);
    }
}
