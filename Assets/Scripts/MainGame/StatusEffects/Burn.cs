using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : StatusEffect
{
    public float burnDamageRatio = 0.4f;
    public override string EffectBattleLogMessage()
    {
        return "<color=red>" + target.unitName + "<color=white> took <color=blue>" + target.CountDamageTaken((int)((ability.abilityBaseModifier * ability.GetCaster().magickAttack) * burnDamageRatio), true)
            + "<color=white> from <color=orange> " + effectType;
    }

    public override void EffectTick(Unit unit)
    {
        SetEffectValue((int)((ability.abilityBaseModifier * ability.GetCaster().magickAttack) * burnDamageRatio));

        unit.TakeDamage(effectValue, true);
    }
}
