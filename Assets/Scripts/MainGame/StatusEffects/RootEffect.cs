using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEffect : StatusEffect
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
            target.speed = 0;

        }
        isApplied = true;

    }

    public override void UpdateOnDestroy()
    {
        target.speed += target.BaseSpeed;
        base.UpdateOnDestroy();
    }
}
