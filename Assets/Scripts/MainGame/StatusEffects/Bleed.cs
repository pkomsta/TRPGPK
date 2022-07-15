using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : StatusEffect
{
    public float bleedDamageRatio = 0.2f;
    public override string EffectBattleLogMessage()
    {
        return "<color=red>" + target.unitName + "<color=white> took <color=red>" + target.CountDamageTaken((int)((ability.abilityBaseModifier * ability.GetCaster().attack) * bleedDamageRatio),false)
            + "<color=white> from <color=red> " + effectType;
    }

    public override void EffectTick(Unit unit)
    {
        SetEffectValue((int)((ability.abilityBaseModifier * ability.GetCaster().attack) * bleedDamageRatio));

        unit.TakeDamage(effectValue,false);
    }
}
