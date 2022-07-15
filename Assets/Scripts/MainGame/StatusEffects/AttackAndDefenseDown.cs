using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAndDefenseDown : StatusEffect
{
    
    bool isApplied = false;
    public override string EffectBattleLogMessage()
    {
        return "";
    }

    public override void EffectTick(Unit unit)
    {
        if (!isApplied)
        {
            target.defense -= ability.abilityBaseValue;
            target.attack -= ability.abilityBaseValue;

        }
        isApplied = true;

    }

    public override void UpdateOnDestroy()
    {
        target.defense += ability.abilityBaseValue;
        target.attack += ability.abilityBaseValue;
        base.UpdateOnDestroy();
    }
}
