using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    bool isFurious = false;
    protected override void Start()
    {
        
        base.Start();

        WarriorsVitality();
            StoneSkin();
        SwordMastery();
        RunicBlade();
        NeverEndingRage();


    }
    public override void AttackUnit(Unit u)
    {
        VampiricBlade();
        BuildingAnger();
        base.AttackUnit(u);
        RunicAttack(u);
    }

    

    public override void TakeDamage(int damage, bool isMagic)
    {
        damage = ClearMind(damage);
        base.TakeDamage(damage, isMagic);
        Furious();
        

    }

    public override void GainHealth(int heal)
    {
        base.GainHealth(heal);
        Furious();
    }
    public override string PlayerAttackMessage(Unit u)
    {
        if (gameManager.isPassive_5Learnt())
        {
            return " <color=green> " + unitName + "<color=white> " + " attacked " + " <color=red> " + u.unitName + "<color=white> " + " dealing "
                            + " <color=red> " + CountDamageTaken(attack, false) + "<color=white>+<color=blue> " + CountDamageTaken(magickAttack, true) + "<color=white> damage";
        }
        else
        {
            return base.PlayerAttackMessage(u);
        }
        
    }
    



    private int ClearMind(int damage)
    {
        if (gameManager.isPassive_0Learnt())
        {
            if (abilityResource <= 0)
            {
                damage = damage / 2;
                Gameboard.Instance.UpdateBattleLog("<color=green> " + unitName + "<color=white> reduced incoming damage by <color=green>" + damage + " <color=white> thanks to Clear Mind");
            }
        }
        return damage;
    }

    void WarriorsVitality()
    {
        if (gameManager.isPassive_1Learnt())
        {
            maxHealth = (int)(maxHealth * 1.1f);
            health = maxHealth;
            
        }
        
    }

    void StoneSkin()
    {
        if (gameManager.isPassive_2Learnt())
        {
            defense = (int)(defense * 1.1f);
            BaseDefense = defense;
        }
    }

    void SwordMastery()
    {
        if (gameManager.isPassive_3Learnt())
        {
            attack = (int)(attack * 1.1);
            BaseAttack = attack;
        }
    }
    void Furious()
    {
        if (gameManager.isPassive_4Learnt())
        {
            if (health < maxHealth / 2 && !isFurious)
            {
                isFurious = true;
                attack += (int)(BaseAttack * 0.5f);
            }
            else if (health > maxHealth / 2 && isFurious)
            {
                isFurious = false;
                attack -= (int)(BaseAttack * 0.5f);
            }
        }

    }

    void RunicBlade()
    {
        if (gameManager.isPassive_5Learnt())
        {
            magickAttack += (int)(attack * 0.1f);
            BaseMagickAttack = magickAttack;
        }
    }
    private void RunicAttack(Unit u)
    {
        if (gameManager.isPassive_5Learnt())
        {
            u.TakeDamage(magickAttack, true);
        }
        
    }

    void NeverEndingRage()
    {
        if (gameManager.isPassive_6Learnt())
        {
            GainResource(20);
        }
    }

    void BuildingAnger()
    {
        if (gameManager.isPassive_7Learnt())
        {
            if(abilityResource <= 0)
            {
                abilityResource += resourceRegeneration;
            }
        }
    }

    void VampiricBlade()
    {
        if (gameManager.isPassive_8Learnt())
        {
            if(abilityResource == maxAbilityResource)
            {
                GainHealth(vitality);
                Gameboard.Instance.UpdateBattleLog( "<color=green> " + unitName + "<color=white> " + " has been healed by " + " <color=green> " + vitality + "<color=white> health"
             +  "");
            }
        }
    }




}
