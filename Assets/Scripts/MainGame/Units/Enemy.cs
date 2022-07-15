using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    
    public enum EnemyRarity
    {
        common,
        unique,
        rare,
        epic

    }
    #region zmienne
    [Header("Rarity")]
    public EnemyRarity enemyRarity;
    Hero hero;
    Vector3Int heroCell;
    float distanceToHero;
    List<Vector3Int> movableCellsList;
    Vector3Int playerPos;
    List<Abilities> abilitiesThatCanBeCasted = new List<Abilities>();
    public InventoryItem[] dropableItemsWarrior;
    public InventoryItem[] dropableItemsMage;
    public int moneyOnDeath = 0;
    public int dropOffset = 25;
    public float chanceForNothingToDrop = 0.25f;
    #endregion


    public void FindClosestMoveCell(Vector3Int[] movableCells)
    {

        
        FindValues();

        
        Vector3Int closestCell = movableCells[0];
        movableCellsList = new List<Vector3Int>(movableCells);

        for (int i = movableCellsList.Count - 1; i >= 0; i--)
        {

            if(Gameboard.Instance.GetUnit(movableCells[i]) != null)
            {
                
                movableCellsList.Remove(movableCells[i]);
            }
        }
        foreach (Vector3Int V3I in movableCellsList)
        {
            
            if (Vector3.Distance(V3I, heroCell) < distanceToHero)
            {
                if (Vector3.Distance(V3I, Gameboard.Instance.GetClosestCell(transform.position)) <= speed && Gameboard.Instance.GetUnit(V3I) == null)
                {
                    closestCell = V3I;
                }


                distanceToHero = Vector3.Distance(V3I, heroCell);
            }
        }
        
        if(Gameboard.Instance.GetUnit(closestCell) == null && speed > 0)
        {
            MoveCommand cmd = new MoveCommand(CurrentCell, closestCell);
            CommandManager.Instance.AddCommand(cmd);
        }
        else
        {
            StayCommand cmd = new StayCommand();
            CommandManager.Instance.AddCommand(cmd);
        }
       
    }

    public void AttackPlayer()
    {
        AttackUnit(hero);
    }

    public bool canAttack(Vector3Int[] attackableCells)
    {
        FindValues();
        foreach (Vector3Int V3I in attackableCells)
        {

            //Debug.Log(Gameboard.Instance.GetUnit(V3I));
            if (Gameboard.Instance.GetUnit(V3I) != null && Gameboard.Instance.GetUnit(V3I).GetComponent<Hero>())
            {
                
                    playerPos = V3I;
                    return true;
                
                
            }
        }

        return false;

    }

    public void FindCastableAbilities(Vector3Int[] castableCells, Abilities ability)
    {
        FindValues();
        
        
            if(ability.GetCanBeCasted() && this.canSpendResource(ability.resourceCost))
            {
                switch(ability.abilityType)
                {
                    case Abilities.AbilityType.self_buff:
                        abilitiesThatCanBeCasted.Add(ability);
                        
                        break;
                    case Abilities.AbilityType.self_heal:
                        if(health != maxHealth)
                        {
                            abilitiesThatCanBeCasted.Add(ability);
                            
                        }
                        
                        break;
                    case Abilities.AbilityType.self_resource:
                        if(abilityResource != maxAbilityResource)
                        {
                            abilitiesThatCanBeCasted.Add(ability);
                            
                        }    
                        break;
                    default:
                    foreach (Vector3Int V3I in castableCells)
                    {
                        
                        if (Gameboard.Instance.GetUnit(V3I) != null && Gameboard.Instance.GetUnit(V3I).GetComponent<Hero>() && V3I != new Vector3Int(0, 0, 0))
                        {
                           // Debug.Log(ability.abilityName + V3I);
                            abilitiesThatCanBeCasted.Add(ability);

                        }
                    }
                        break;
                }
            
            }
            
            
        }

    public void ChooseAbilityToCast()
    {
        Abilities ability = null;
        List<Abilities> attackAbilities = new List<Abilities>();
        if(abilitiesThatCanBeCasted != null)
        {
            
            foreach (Abilities ab in abilitiesThatCanBeCasted)
            {
                switch (ab.abilityType)
                {
                    case Abilities.AbilityType.self_buff:
                        if (ability != null && ab.abilityBaseModifier > ability.abilityBaseModifier)
                        {
                            ability = ab;
                        }
                        else if (health / (float)maxHealth > 0.3)
                        {
                            ability = ab;

                        }

                        break;
                    case Abilities.AbilityType.self_heal:
                        if (ability != null && ab.abilityBaseModifier > ability.abilityBaseModifier)
                        {
                            ability = ab;
                        }
                        else if (health / (float)maxHealth <= 0.3)
                        {
                            ability = ab;

                        }

                        break;
                    case Abilities.AbilityType.self_resource:
                        if (ability != null && ab.abilityBaseModifier > ability.abilityBaseModifier)
                        {
                            ability = ab;
                        }
                        else if (abilityResource / (float)maxAbilityResource <= 0.2)
                        {
                            ability = ab;

                        }
                        break;
                    case Abilities.AbilityType.single:
                       
                        attackAbilities.Add(ab);
                        break;
                    default:
                        
                        break;
                }
                
            }
            if (ability == null)
            {
                foreach (Abilities ab in attackAbilities)
                {
                    if (ability == null)
                    {
                        ability = ab;
                    }
                    else if (ability.abilityRange > ab.abilityRange)
                    {
                        ability = ab;
                    }
                    else
                    {
                        int rV1 = (int)Random.Range(0, 100);
                        int rV2 = (int)Random.Range(0, 100);
                        
                        if (rV1 > rV2)
                        {
                            ability = ab;
                        }
                        else
                        {

                        }

                    }

                }
            }

            if (ability != null)
            {
                chosenAbility = ability;
            }
            
        }
        
        
    }

    public void CastChoosenAbility()
    {
        CastAbilityCommand cmd = new CastAbilityCommand(CurrentCell, heroCell);
        CommandManager.Instance.AddCommand(cmd);
        
       
        chosenAbility = null;
        abilitiesThatCanBeCasted = new List<Abilities>();
    }
        
    public void FindValues()
    {
        hero = FindObjectOfType<Hero>();
        heroCell = Gameboard.Instance.GetClosestCell(hero.transform.position);
        distanceToHero = Vector3.Distance(heroCell, Gameboard.Instance.GetClosestCell(transform.position));
    }

    public InventoryItem GetItemDropedWarrior()
    {
        int total = 0;
        foreach(InventoryItem ii in dropableItemsWarrior)
        {
          total +=  ii.GetDropChance();
        }
       
        int emptyValue = (int)(total * chanceForNothingToDrop);
       
        int rd = Random.Range(0, total+emptyValue+dropOffset);
       

        foreach(InventoryItem ii in dropableItemsWarrior)
        {
            if(rd >= total)
            {
                return null;
            }
            else if(rd <= ii.GetDropChance())
            {
                return ii;
            }
            else
            {
                rd -= ii.GetDropChance();
            }

        }

        return null;
        
    }
    public InventoryItem GetItemDropedMage()
    {
        int total = 0;
        foreach (InventoryItem ii in dropableItemsMage)
        {
            total += ii.GetDropChance();
        }

        int emptyValue = (int)(total * chanceForNothingToDrop);

        int rd = Random.Range(0, total + emptyValue + dropOffset);


        foreach (InventoryItem ii in dropableItemsMage)
        {
            if (rd >= total)
            {
                return null;
            }
            else if (rd <= ii.GetDropChance())
            {
                return ii;
            }
            else
            {
                rd -= ii.GetDropChance();
            }

        }

        return null;

    }

}
