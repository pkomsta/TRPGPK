using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    public enum Team
    {
        Enemy,
        Player
    }
    public enum Resource
    {
        Mana,
        Rage,
        Stamina

    }
    public Vector3Int CurrentCell
    {
        get => m_CurrentCell;
        set => m_CurrentCell = value;
    }
    #region zmienne
    public int BaseAttack { get => baseAttack; set => baseAttack = value; }
    public int BaseMagickAttack { get => baseMagickAttack; set => baseMagickAttack = value; }
    public int BaseVitality { get => baseVitality; set => baseVitality = value; }
    public int BaseWisdom { get => baseWisdom; set => baseWisdom = value; }
    public int BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public int BaseDefense { get => baseDefense; set => baseDefense = value; }
    public int BaseMagicDefense { get => baseMagicDefense; set => baseMagicDefense = value; }
    public int BaseRange { get => baseRange; set => baseRange = value; }

    

    public Resource resource;
    public Team Side;
    

    protected Vector3Int m_CurrentCell;

    [Header("Statistics")]
    public int maxHealth = 50;
    public int health = 50;
    public int maxAbilityResource = 50;
    public int abilityResource = 50;
    public int resourceRegeneration = 0;
    public int attack = 10;
    public int magickAttack = 10;
    public int vitality = 10;
    public int wisdom = 0;
    public int speed = 1;
    public int defense = 10;
    public int magicDefense = 10;
    public int range = 1;
    int baseAttack = 10;
    int baseMagickAttack = 10;
    int baseVitality = 10;
    int baseWisdom = 0;
    int baseSpeed = 1;
    int baseDefense = 10;
    int baseMagicDefense = 10;
    int baseRange = 1;
    [Header("Level and Name")]
    public string unitName;
    public string unitDescribtion;
    public int level = 1;
    public int expGiven = 10;
    
    [Header("UI")]
    public Slider hpSlider;
    public Slider mpSlider;
    public Image mpFill;
    [Header("Abilities")]
    public Abilities[] abilities;
    public Abilities chosenAbility;

    int hpGrowth = 10;
    int mpGrowth = 10;

    bool canDie = true;
    public bool isDead = false;
    #endregion
    protected virtual void Start()
    {

        Gameboard.Instance.onEndTurn.AddListener(ResourceChangeStamina);
        m_CurrentCell = Gameboard.Instance.GetClosestCell(transform.position);
        Gameboard.Instance.SetUnit(m_CurrentCell, this);
        transform.position = Gameboard.Instance.Grid.GetCellCenterWorld(m_CurrentCell);
        abilities = GetComponents<Abilities>();
        SetUnitHPAndMP();
        ChangeMPBarColor();
        HPBarCheck();
        MPBarCheck();
        baseAttack = attack;
        baseDefense = defense;
        baseMagicDefense = magicDefense;
        baseMagickAttack = magickAttack;
        baseVitality = vitality;
        BaseWisdom = wisdom;
        baseSpeed = speed;
        baseRange = range;
        
        
    }
    public int GetMoveCells(Vector3Int[] result, Gameboard board)
    {
        int count = 0;

        for (int y = -speed; y <= speed; ++y)
        {
            for (int x = -speed; x <= speed; ++x)
            {
                if (x == 0 && y == 0)
                    continue;

                if (Mathf.Abs(x) + Mathf.Abs(y) > speed + 1)
                    continue;

                var idx = m_CurrentCell + new Vector3Int(x, 0, y);
                if (board.IsOnBoard(idx))
                {
                    result[count] = idx;
                    count++;
                }
            }
        }

        return count;
    }
    public int GetInfoCells(Vector3Int[] result, Gameboard board)
    {
        int count = 0;

        for (int y = -99; y <= 99; ++y)
        {
            for (int x = -99; x <= 99; ++x)
            {
                if (x == 0 && y == 0)
                    continue;

                if (Mathf.Abs(x) + Mathf.Abs(y) > 99)
                    continue;

                var idx = m_CurrentCell + new Vector3Int(x, 0, y);
                if (board.IsOnBoard(idx))
                {
                    result[count] = idx;
                    count++;
                }
            }
        }

        return count;
    }

    public int GetAttackCells(Vector3Int[] result, Gameboard board)
    {
        int count = 0;

        for (int y = -range; y <= range; ++y)
        {
            for (int x = -range; x <= range; ++x)
            {
                if (x == 0 && y == 0)
                    continue;

                if (Mathf.Abs(x) + Mathf.Abs(y) > range + 1)
                    continue;

                var idx = m_CurrentCell + new Vector3Int(x, 0, y);
                if (board.IsOnBoard(idx))
                {
                    result[count] = idx;
                    count++;
                }
            }
        }

        return count;
    }
    public int GetAbilityCells(Vector3Int[] result, Gameboard board, Abilities ability)
    {
        int count = 0;

        int abilityRange = ability.abilityRange;
        if(ability != null)
        {
            for (int y = -abilityRange; y <= abilityRange; ++y)
            {
                for (int x = -abilityRange; x <= abilityRange; ++x)
                {
                    if (x == 0 && y == 0)
                        continue;

                    if (Mathf.Abs(x) + Mathf.Abs(y) > abilityRange + 1)
                        continue;

                    var idx = m_CurrentCell + new Vector3Int(x, 0, y);
                    if (board.IsOnBoard(idx))
                    {
                        result[count] = idx;
                        count++;
                    }
                }
            }
        }
        

        return count;
    }
    public virtual void AttackUnit(Unit u)
    {
        switch (Side)
        {
            case Team.Enemy:
                Gameboard.Instance.UpdateBattleLog(" <color=red> " + unitName + "<color=white> " + " attacked " + " <color=green> " + u.unitName + "<color=white> " + " dealing "
                    + " <color=red> " + CountDamageTaken(attack, false) + " ");
                break;
            case Team.Player:
                Gameboard.Instance.UpdateBattleLog(PlayerAttackMessage(u));
                break;
        }

        ResourceChangeRage();
        u.TakeDamage(attack, false);

    }
    public virtual string PlayerAttackMessage(Unit u)
    {
        return " <color=green> " + unitName + "<color=white> " + " attacked " + " <color=red> " + u.unitName + "<color=white> " + " dealing "
                            + " <color=red> " + CountDamageTaken(attack, false) + " ";
    }
    public virtual void TakeDamage(int damage, bool isMagic)
    {

            health -= CountDamageTaken(damage,isMagic);

        HPBarCheck();
        if (health <= 0)
        {
            isDead = true;
            if(Side == Team.Enemy && canDie)
            {
                canDie = false;
                
                
                Gameboard.Instance.SetUnit(Gameboard.Instance.GetClosestCell(transform.position), null);
                
                Gameboard.Instance.EnemyDies(this as Enemy);
                Gameboard.Instance.EnemyKilled();
                Gameboard.Instance.areThereAnyEnemiesLeft();
                
                
            }
            
            gameObject.SetActive(false);
        }


    }
    public void TakeTrueDamage(int damage)
    {
        health -= damage;
        HPBarCheck();
        if (health <= 0)
        {
            isDead = true;
            if (Side == Team.Enemy && canDie)
            {
                canDie = false;

                Debug.Log(Gameboard.Instance.GetClosestCell(transform.position));
                Gameboard.Instance.SetUnit(Gameboard.Instance.GetClosestCell(transform.position), null);

                Gameboard.Instance.EnemyDies(this as Enemy);
                Gameboard.Instance.EnemyKilled();
                Gameboard.Instance.areThereAnyEnemiesLeft();


            }

            gameObject.SetActive(false);
        }
    }
    public int CountDamageTaken(int damage, bool isMagic)
    {
        float damageReduction;
        if (isMagic)
        {
            if (magicDefense <= 0)
            {
                damageReduction = 1;
            }
            else
                damageReduction = 100 / (float)(100 + magicDefense);
        }
        else
        {
            if(defense <= 0)
            {
                damageReduction = 1;
            }
            else
            damageReduction = 100 / (float)(100 + defense);
        }
            

        return (int)(damage * damageReduction);

    }
    private void HPBarCheck()
    {
        hpSlider.value = (health / (float)maxHealth);
    }
    protected void MPBarCheck()
    {
        mpSlider.value = abilityResource / (float)maxAbilityResource;
    }
    private void ChangeMPBarColor()
    {
        switch (resource)
        {
            case Resource.Mana:
                mpFill.color = Color.blue;
                break;
            case Resource.Rage:
                mpFill.color = new Color(0.4f, 0f, 0.101f);
                break;
            case Resource.Stamina:
                mpFill.color = Color.yellow;
                break;
        }
        
    }

    public virtual void ResourceChangeStamina()
    {
        if(resource == Resource.Stamina)
        {
            if (Gameboard.Instance.CurrentTeam == Side)
            {
                RegenerateResource();
                MPBarCheck();
            }
        }
  
    }

    public virtual void ResourceChangeRage()
    {
        if(resource == Resource.Rage)
        {
            RegenerateResource();
            MPBarCheck();
        }
    }

    private void RegenerateResource()
    {
        
        abilityResource += resourceRegeneration;
        ResourceCheck();
        MPBarCheck();
    }

    public virtual void GainResource(int gain)
    {
        abilityResource += gain;
        ResourceCheck();
        MPBarCheck();
    }

    public bool canSpendResource(int spend)
    {
        if(spend > abilityResource)
        {
            return false;
        }
        else
        {
            
            return true;
        }
    }

    public virtual void SpendResource(int spend)
    {
        abilityResource -= spend;

        MPBarCheck();
    }

    public virtual void GainHealth(int heal)
    {
        if(health+heal > maxHealth)
        {
            health = maxHealth;
        }else
        health += heal;
        HPBarCheck();
    }

    private void ResourceCheck()
    {
        if (abilityResource >= maxAbilityResource)
        {
            abilityResource = maxAbilityResource;
        }else if( abilityResource < 0)
        {
            abilityResource = 0;
        }
    }

    private void SetUnitHPAndMP()
    {
        maxHealth = hpGrowth * vitality;
        health = maxHealth;
        switch (resource)
        {
            case Resource.Mana:
                maxAbilityResource = mpGrowth * wisdom;
                abilityResource = maxAbilityResource;
                break;
            case Resource.Rage:
                abilityResource = 0;
                break;
            case Resource.Stamina:
                abilityResource = maxAbilityResource;
                break;
        }
        
    }

    public void SetHpGrowth(int hp)
    {
        hpGrowth = hp;
    }
    public void SetMpGrowth(int mp)
    {
        mpGrowth = mp;
    }
}
