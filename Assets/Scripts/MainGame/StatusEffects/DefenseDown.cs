using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseDown : StatusEffect
{
    public int reduceDefense = 20;
    bool isApplied = false;
    public override string EffectBattleLogMessage()
    {
        return "";    
    }

    public override void EffectTick(Unit unit)
    {
        if (!isApplied)
        {
            target.defense = target.defense - reduceDefense;
            
        }
        isApplied = true;

    }

    public override void UpdateOnDestroy()
    {
        target.defense += reduceDefense;
        base.UpdateOnDestroy();
    }
}
