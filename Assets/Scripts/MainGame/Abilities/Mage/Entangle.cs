using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entangle : Abilities
{
    public StatusEffect statusEffect;
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
        var spawnEffect = Instantiate(statusEffect, unit.transform);
        spawnEffect.SetAbiliti(this, unit);
    }

    public override string AbilityLogMessage()
    {
        return " <color=green> " + GetCaster().unitName + "<color=white> " + " used " + " <color=red> " + abilityName + "<color=white> " + " on " + " <color=red> " + GetTarget().unitName + "<color=white> " + " dealing "
                    + " <color=blue> " + GetTarget().CountDamageTaken(damage, true) + "<color=white> damage and applying  " + statusEffect.effectType;
    }

    public override string Describtion()
    {
        return "Cost: <color=red>" + (resourceCost + (int)GetCaster().maxAbilityResource * manaPercent) + " \n"
            + "<color=white> Cooldown: " + cooldown + " \n"
           + "<color=white>Damage: <color=blue>" + (abilityBaseValue + (int)(abilityBaseModifier * GetCaster().magickAttack)) + " + " + ((int)GetCaster().maxAbilityResource * manaPercent) + " \n"
           + "<color=white>Range: " + abilityRange
           + "<color=white>Effect Duration: " + statusEffect.effectDuration;
    }
}
