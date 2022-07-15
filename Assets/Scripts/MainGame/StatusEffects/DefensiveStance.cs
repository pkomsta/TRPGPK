using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveStance : StatusEffect
{
    public float attackChange = 0.5f;
    public float defenseChange = 2.0f;
    bool isApplied = false;
    public override string EffectBattleLogMessage()
    {
        return "";    
    }

    public override void EffectTick(Unit unit)
    {
        if (!isApplied)
        {
            caster.attack = (int)(caster.attack * attackChange);
            caster.defense = (int)(caster.defense * defenseChange);
        }
        isApplied = true;

    }

    public override void UpdateOnDestroy()
    {
        caster.attack = caster.BaseAttack;
        caster.defense = caster.BaseDefense;
        base.UpdateOnDestroy();
    }
}
