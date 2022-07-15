using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagickMissle : Abilities
{
    public float manaPercent = 0.01f;
    int damage;
    public override void Ability(Unit unit)
    {
        if (GetCaster().canSpendResource((int)(GetCaster().maxAbilityResource * manaPercent)))
        {
            damage = (int)(GetCaster().maxAbilityResource * manaPercent) + abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack);
            GetCaster().SpendResource((int)(GetCaster().maxAbilityResource * manaPercent));
        }
        else
        {
            damage = abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack);
        }
        unit.TakeDamage(damage, true);
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " on " + " <color=red> " + GetTarget().unitName + "<color=white> " + " dealing "
                    + " <color=blue> " + GetTarget().CountDamageTaken(damage, true) + "<color=white> damage ";
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + (resourceCost + (int)GetCaster().maxAbilityResource * manaPercent) + " \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
           + "<color=white>Damage: <color=blue>" + (abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack)) + " + " + ((int)GetCaster().maxAbilityResource * manaPercent) + " \n"
           + "<color=white>Range: " + abilityRange;
    }
}
