using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagickPoison : StatusEffect
{
    public float poisonDamageRatio = 0.2f;
    public override string EffectBattleLogMessage()
    {
        return "<color=red>" + target.unitName + "<color=white> took <color=blue>" + target.CountDamageTaken((int)((ability.abilityBaseModifier * ability.GetCaster().magickAttack) * poisonDamageRatio), true)
            + "<color=white> from <color=red> " + effectType;
    }

    public override void EffectTick(Unit unit)
    {
        SetEffectValue((int)((ability.abilityBaseModifier * ability.GetCaster().magickAttack) * poisonDamageRatio));

        unit.TakeDamage(effectValue, true);
    }
}
