using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrain : Abilities
{
    public float manaPercent = 0.05f;
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
        GetCaster().GainHealth((int)unit.CountDamageTaken(damage, true));
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " on " + " <color=red> " + GetTarget().unitName + "<color=white> " + " dealing "
                    + " <color=blue> " + GetTarget().CountDamageTaken(damage, true) + "<color=white> damage and healing you by<color=green> "
                    + ((int)GetTarget().CountDamageTaken(damage, true));
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + (resourceCost + (int)GetCaster().maxAbilityResource * manaPercent) +  " \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
           + "<color=white>Damage: <color=blue>" + (abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack)) + " + " + ((int)GetCaster().maxAbilityResource * manaPercent) + " \n"
           + "<color=white> Heal Value: <color=green>" + (int)((abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack)) + ((int)GetCaster().maxAbilityResource * manaPercent)) + " \n"
           + "<color=white>Range: " + abilityRange;
    }
}
