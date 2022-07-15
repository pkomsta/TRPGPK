using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Hero
{
    bool isOverloaded = false;
    bool isDefended = false;


    protected override void Start()
    {
        base.Start();
        ManaShield();
        MagicArmor();
        MagicPower();
        LongStick();
        Overload();
        PurePower();
        Gameboard.Instance.onStartPlayerTurn.AddListener(HealingWounds);
        
    }
    public override void AttackUnit(Unit u)
    {
        base.AttackUnit(u);
        LongStickManaGain();
    }
    public override void SpendResource(int spend)
    {
        base.SpendResource(spend);
        HealingMana(spend);
        Overload();
        MagickBarrier();
    }
    public override void GainResource(int gain)
    {
        base.GainResource(gain);
        Overload();
        MagickBarrier();
    }

    void ManaShield()
    {
        if (gameManager.isPassive_0Learnt())
        {
            maxHealth += (int)(maxAbilityResource * 0.1f);
            health = maxHealth;
        }
    }
    void MagicArmor()
    {
        if (gameManager.isPassive_1Learnt())
        {
            magicDefense += (int)(maxAbilityResource * 0.05f);
            BaseMagicDefense = magicDefense;
        }
    }
   
    void LongStick()
    {
        if (gameManager.isPassive_2Learnt())
        {
            range++;
            BaseRange++;
        }
    }
    void LongStickManaGain()
    {
        if (gameManager.isPassive_2Learnt())
        {
            GainResource((int)((maxAbilityResource) * 0.25f));
        }
    }

    void Overload()
    {
        if (gameManager.isPassive_3Learnt()){ 
        if (abilityResource == maxAbilityResource && !isOverloaded)
        {
                isOverloaded = true;
                magickAttack += BaseMagickAttack;
        }
        else if (isOverloaded && abilityResource < maxAbilityResource)
        {
                isOverloaded = false;
                magickAttack -= BaseMagickAttack;
        }

        }
    }
    void HealingWounds()
    {
        if (gameManager.isPassive_4Learnt())
        {
            GainHealth((int)(wisdom / 5.0f));
            Gameboard.Instance.UpdateBattleLog("<color=green> " + unitName + "<color=white> gained <color=green>" + ((int)(wisdom / 5.0f)) + " <color=white> health from Healing Wounds");
        }
    }
    void HealingMana(int value)
    {
        if (gameManager.isPassive_5Learnt())
        {
            GainHealth((int)(value / 2));
            Gameboard.Instance.UpdateBattleLog("<color=green> " + unitName + "<color=white> gained <color=green>" + ((int)(value / 2)) + " <color=white> health from Healing Mana");
        }
    }
    void MagickBarrier()
    {
        if (gameManager.isPassive_6Learnt()) {
            if (abilityResource < (int)(maxAbilityResource * 0.1f) && !isDefended)
            {
                isDefended = true;
                defense += 100;
                magicDefense += 100;
            }
            else if (abilityResource > (int)(maxAbilityResource * 0.1f) && isDefended)
            {
                isDefended = false;
                defense -= 100;
                magicDefense -= 100;
            }
        }
    }
    void MagicPower()
    {
        if (gameManager.isPassive_7Learnt())
        {
            magickAttack = (int)(magickAttack * 1.1f);
            
            BaseMagickAttack = magickAttack;
        }
    }

    void PurePower()
    {
        if (gameManager.isPassive_8Learnt())
        {
            magickAttack +=(int) (BaseMagicDefense / 2.0f);
            magicDefense = (int)(BaseMagicDefense / 2.0f);
        }
    }

}

