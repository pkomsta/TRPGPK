using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : StatusEffect
{
    
    public override string EffectBattleLogMessage()
    {
        return "<color=green>" + target.unitName + "<color=white> was healed by <color=red>" + (int)(target.wisdom/2)
            + "<color=white> from <color=red> " + effectType;
    }

    public override void EffectTick(Unit unit)
    {
        SetEffectValue((int)(unit.wisdom));

        unit.GainHealth(effectValue);
    }
}
