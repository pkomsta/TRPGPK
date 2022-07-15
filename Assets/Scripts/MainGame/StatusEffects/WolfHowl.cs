using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfHowl : StatusEffect
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
            effectValue = ability.abilityBaseValue;
            target.attack += effectValue;
            target.speed++;

        }
        isApplied = true;

    }

    public override void UpdateOnDestroy()
    {
        target.attack -= effectValue;
        target.speed--;
        base.UpdateOnDestroy();
    }
}
