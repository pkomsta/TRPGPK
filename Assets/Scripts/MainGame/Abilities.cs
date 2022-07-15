using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abilities : MonoBehaviour
{
    public enum AbilityType
    {
        single,
        aoe,
        self_buff,
        self_heal,
        self_resource
    }


    public int abilityBaseValue = 10;
    public float abilityBaseModifier = 1;
    public int abilityRange = 1;
    public int abilityRadius = 1;
    public int cooldown = 1;
    int currentCooldown = 0;
    public int resourceCost = 10;
    public AbilityType abilityType;
    Unit caster;
    Unit target;
    public bool isLearnt = true;
    bool canBeCasted = true;
    [Header("Description")]
    public string abilityName;
    public string desc;


    void Start()
    {
        Gameboard.Instance.onEndTurn.AddListener(ReduceCooldown);
        
        caster = GetComponent<Unit>();
        desc += " \n" + Describtion();
    }

    public void CastAbility(Unit unit)
    {
        SetTarget(unit);
        
        if (caster.canSpendResource(resourceCost))
        {
            if (canBeCasted)
            {
                
                caster.SpendResource(resourceCost);
                Ability(target);
                GoOnCooldown();
                Gameboard.Instance.UpdateBattleLog(AbilityLogMessage());
                if(currentCooldown != 0)
                {
                    canBeCasted = false;
                }

            }
        }
        
    }
    public abstract void Ability(Unit unit);

 
    void ReduceCooldown()
    {
        if(Gameboard.Instance.CurrentTeam == caster.Side  && !canBeCasted)
        {
            if (currentCooldown > 0)
            {
                currentCooldown--;
            }
            if(currentCooldown == 0)
            {                
                canBeCasted = true;
            }
            
        }
        
    }

    void GoOnCooldown()
    {
        currentCooldown = cooldown;
    }

    public void SetTarget(Unit unit)
    {
        target = unit;
    }

    public Unit GetCaster()
    {
        return caster;
    }

    public bool GetCanBeCasted()
    {
        return canBeCasted;
    }

    public Unit GetTarget()
    {
        return target;
    }

    public abstract string AbilityLogMessage();

    public virtual string Describtion()
    {
        return " ";
    }
}
