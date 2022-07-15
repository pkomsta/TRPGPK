using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager => s_Manager;

    private int lastChoosenMap1 = 0;

    public int GetlastChoosenMap()
    {
        return lastChoosenMap1;
    }

    public void SetlastChoosenMap(int value)
    {
        lastChoosenMap1 = value;
    }

    static GameManager s_Manager;

    public event Action moneyUppdate;

    public Hero[] heroesToChooseFrom;

    Hero choosenHero;
    List<Abilities> heroUsableAbilities = new List<Abilities>();
    List<Abilities> listForAbilietesToBeLearnt = new List<Abilities>();
    [Header("Locations")]
    #region Location Settings
    public LevelSettings[] locations;
    List<LevelSettings> gateLevels = new List<LevelSettings>();
    LevelSettings choosenLocation;
    int currnetLevelUnlocked = 0;
    #endregion

    [Header("Currency")]
    public int money = 100;

    [Header("Experience")]
    #region Character level variables
    public int level = 0;
    public int maxLevel = 99;
   

    public int levelExpReq = 50;
    public uint baseLevelReq = 100;
    int currentExp = 0;
    int[] heroStats;
    uint[] levelThresholds;
    #endregion

    Inventory inventory;
    Equipment equipment;
   
    SkillTree skillTree;
    GameObject skillTreeObject;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
        equipment = GetComponent<Equipment>();
        s_Manager = this;
        int numberOfManagers = FindObjectsOfType<GameManager>().Length;
        if(numberOfManagers > 1)
        {
            Destroy(gameObject);
        }else
        DontDestroyOnLoad(this.gameObject);
        
        
    }

    private void SkillTree_onSkillUnlocked(object sender, SkillTree.OnSkillUnlocked e)
    {
        string skillName = e.skillType.ToString();
        string lastInt = skillName.Substring(skillName.Length-1);
       
        if (listForAbilietesToBeLearnt.Count > int.Parse(lastInt))
        {
            
            heroUsableAbilities.Add(listForAbilietesToBeLearnt[int.Parse(lastInt)]);
        }
    }

    void Start()
    {
        choosenLocation = locations[0];
        CreateLevelThresholds();
        foreach(LevelSettings ls in locations)
        {
            if (ls.isGateKeeper)
            {
                gateLevels.Add(ls);
                
            }
        }

    }

    public void ChooseHero(int index)
    {
        choosenHero = heroesToChooseFrom[index];
        heroStats = new int[8] {choosenHero.attack,choosenHero.magickAttack,choosenHero.vitality,choosenHero.wisdom,choosenHero.defense,choosenHero.magicDefense,choosenHero.speed,choosenHero.range};
        switch (choosenHero.heroClass)
        {
            case Hero.HeroClass.Warrior:
                skillTree = new Warrior_SkillTree();
                break;
            case Hero.HeroClass.Mage:
                skillTree = new Mage_SkillTree();
                break;
            default:
                skillTree = new Warrior_SkillTree();
                break;
        }
        skillTree.onSkillUnlocked += SkillTree_onSkillUnlocked;
        foreach (Abilities a in choosenHero.GetComponents<Abilities>())
        {
            if (!a.isLearnt)
            {
                listForAbilietesToBeLearnt.Add(a);
            }
        }
        skillTreeObject = choosenHero.skillTreeObject;
    }

    void CreateLevelThresholds()
    {
        levelThresholds = new uint[maxLevel];

        float increaseValue = 1;
        float value = 0.02f;
        levelThresholds[0] = 100;
        levelThresholds[1] = 275;
        for (int i = 2;i < maxLevel; i++)
        {

                increaseValue = increaseValue + (increaseValue * value);
                levelThresholds[i] = (uint)(((baseLevelReq*i) * Mathf.Pow(i, 1.6f)) - (baseLevelReq * i)/2);
           

            
        }

    }

    public void SetChoosenLevel()
    {
        Instantiate(choosenLocation, Vector3.zero, Quaternion.identity);
    }

    public void SpawnPlayerAndSetStats(Transform pos)
    {
        var player = Instantiate(choosenHero, pos.position, Quaternion.identity);
        player.SetGameManager(this);
        int[] bonusStats = SetHeroStats();

        player.attack = heroStats[0] + bonusStats[0] + player.attackGrowth*level;
        player.magickAttack = heroStats[1] + bonusStats[1] + player.magickAttackGrowth* level; 
        player.vitality = heroStats[2] + bonusStats[2] + player.vitalityGrowth * level;
        player.wisdom = heroStats[3] + bonusStats[3] + player.wisdomGrowth * level;
        player.defense = heroStats[4] + bonusStats[4];
        player.magicDefense = heroStats[5] + bonusStats[5];
        player.speed = heroStats[6] + bonusStats[6];
        player.range = heroStats[7];

    }

    private int[] SetHeroStats()
    {
        int[] bonusStats = new int[7];
        bonusStats[0] =(equipment.GetItemInSlot(EquipLocation.Body) != null ? equipment.GetItemInSlot(EquipLocation.Body).Attack : 0) 
            + (equipment.GetItemInSlot(EquipLocation.Weapon) != null ? equipment.GetItemInSlot(EquipLocation.Weapon).Attack : 0) 
            + (equipment.GetItemInSlot(EquipLocation.Necklace) != null ? equipment.GetItemInSlot(EquipLocation.Necklace).Attack : 0);
        bonusStats[1] = (equipment.GetItemInSlot(EquipLocation.Body) != null ? equipment.GetItemInSlot(EquipLocation.Body).MagicAttack : 0) 
            + (equipment.GetItemInSlot(EquipLocation.Weapon) != null ? equipment.GetItemInSlot(EquipLocation.Weapon).MagicAttack : 0)
            + (equipment.GetItemInSlot(EquipLocation.Necklace) != null ? equipment.GetItemInSlot(EquipLocation.Necklace).MagicAttack : 0);
        bonusStats[2] = (equipment.GetItemInSlot(EquipLocation.Body) != null ? equipment.GetItemInSlot(EquipLocation.Body).Vitality : 0)
            + (equipment.GetItemInSlot(EquipLocation.Weapon) != null ? equipment.GetItemInSlot(EquipLocation.Weapon).Vitality : 0)
            + (equipment.GetItemInSlot(EquipLocation.Necklace) != null ? equipment.GetItemInSlot(EquipLocation.Necklace).Vitality : 0);
        bonusStats[3] = (equipment.GetItemInSlot(EquipLocation.Body) != null ? equipment.GetItemInSlot(EquipLocation.Body).Wisdom : 0)
            + (equipment.GetItemInSlot(EquipLocation.Weapon) != null ? equipment.GetItemInSlot(EquipLocation.Weapon).Wisdom : 0)
            + (equipment.GetItemInSlot(EquipLocation.Necklace) != null ? equipment.GetItemInSlot(EquipLocation.Necklace).Wisdom : 0);
        bonusStats[4] = (equipment.GetItemInSlot(EquipLocation.Body) != null ? equipment.GetItemInSlot(EquipLocation.Body).Defense : 0)
            + (equipment.GetItemInSlot(EquipLocation.Weapon) != null ? equipment.GetItemInSlot(EquipLocation.Weapon).Defense : 0)
            + (equipment.GetItemInSlot(EquipLocation.Necklace) != null ? equipment.GetItemInSlot(EquipLocation.Necklace).Defense : 0);
        bonusStats[5] = (equipment.GetItemInSlot(EquipLocation.Body) != null ? equipment.GetItemInSlot(EquipLocation.Body).MagickDefense : 0)
            + (equipment.GetItemInSlot(EquipLocation.Weapon) != null ? equipment.GetItemInSlot(EquipLocation.Weapon).MagickDefense : 0)
            + (equipment.GetItemInSlot(EquipLocation.Necklace) != null ? equipment.GetItemInSlot(EquipLocation.Necklace).MagickDefense : 0);
        bonusStats[6] = (equipment.GetItemInSlot(EquipLocation.Body) != null ? equipment.GetItemInSlot(EquipLocation.Body).Speed : 0)
            + (equipment.GetItemInSlot(EquipLocation.Weapon) != null ? equipment.GetItemInSlot(EquipLocation.Weapon).Speed : 0)
            + (equipment.GetItemInSlot(EquipLocation.Necklace) != null ? equipment.GetItemInSlot(EquipLocation.Necklace).Speed : 0);

        return bonusStats;
    }

    public int[] StatsScreen()
    {
        int[] statsToShow = new int[8];
        int[] bonusStats = SetHeroStats();

        statsToShow[0] = heroStats[0] + bonusStats[0] + choosenHero.attackGrowth * level;
        statsToShow[1] = heroStats[1] + bonusStats[1] + choosenHero.magickAttackGrowth * level;
        statsToShow[2] = heroStats[2] + bonusStats[2] + choosenHero.vitalityGrowth * level;
        statsToShow[3] = heroStats[3] + bonusStats[3] + choosenHero.wisdomGrowth * level;
        statsToShow[4] = heroStats[4] + bonusStats[4];
        statsToShow[5] = heroStats[5] + bonusStats[5];
        statsToShow[6] = heroStats[6] + bonusStats[6];
        statsToShow[7] = heroStats[7];

        return statsToShow;
    }

    public void SpawnEnemys(Transform[] transforms)
    {

        
        List<Enemy> common = new List<Enemy>();
        List<Enemy> unique = new List<Enemy>();
        List<Enemy> rare = new List<Enemy>();
        List<Enemy> epic = new List<Enemy>();
        
        List<Transform> trans;
        trans = transforms.ToList();
       
        int random = UnityEngine.Random.Range(1, transforms.Length+1);

        Transform[] pos = new Transform[random];

        
        
        for(int i =pos.Length - 1;i>= 0;i--)
        {

            int rd = UnityEngine.Random.Range(0, trans.Count);
            pos[i] = trans[rd];
            trans.RemoveAt(rd);
            
            
        }

       
        foreach(Enemy e in choosenLocation.possibleEnemys)
        {
            switch (e.enemyRarity)
            {
                case Enemy.EnemyRarity.common:
                    common.Add(e);
                    break;
                case Enemy.EnemyRarity.unique:
                    unique.Add(e);
                    break;
                case Enemy.EnemyRarity.rare:
                    rare.Add(e);
                    break;
                case Enemy.EnemyRarity.epic:
                    epic.Add(e);
                    break;
            }

        }
       

        if (!choosenLocation.isGateKeeper) {
            foreach (Transform t in pos)
            {
               
                int rd = UnityEngine.Random.Range(0, 101);

                
                if (rd < 56 && common.Count > 0)
                {
                    Instantiate(common[UnityEngine.Random.Range(0, common.Count)], t.position, Quaternion.identity);

                }
                else if (rd < 80 && unique.Count > 0)
                {

                    Instantiate(unique[UnityEngine.Random.Range(0, unique.Count )], t.position, Quaternion.identity);

                }
                else if (rd < 96 && rd >= 80 && rare.Count > 0)
                {

                    Instantiate(rare[UnityEngine.Random.Range(0, rare.Count )], t.position, Quaternion.identity);

                }
                else if (rd < 101 && rd >= 95 && epic.Count > 0)
                {

                    Instantiate(epic[UnityEngine.Random.Range(0, epic.Count )], t.position, Quaternion.identity);

                }
                else
                {
                    if (common.Count > 0)
                        Instantiate(common[UnityEngine.Random.Range(0, common.Count - 1)], t.position, Quaternion.identity);
                }
            }
    
        }else
        {
            Instantiate(choosenLocation.possibleEnemys[0], transforms[2].position, Quaternion.identity);
        }

    }

    public void ChooseLocation(int index)
    {
        choosenLocation = locations[index];
    }

    public void IncrementUnlockedLevels()
    {
        if (choosenLocation.isGateKeeper)
        {
            gateLevels.Remove(choosenLocation);
            currnetLevelUnlocked++;

        }

      
        
    }

    public int GetCurrentLevelUnlocked()
    {
        return currnetLevelUnlocked;
    }

    public void AddMoney(int m)
    {
        money += m;
    }
    public void AddExperienceAndCheckLevel(int exp)
    {
        currentExp += exp;
        for(int i = level;  i < maxLevel-1; i++)
        {
            if(currentExp >= levelThresholds[i+1])
            {
                level++;
                
                
                    skillTree.AddSkillPoint();
                
            }
            else
            {
                return;
            }
        }
    }

    public Hero GetChoosenHero()
    {
        return choosenHero;
    }

    public Equipment GetEquipment()
    {
        return equipment;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public void MoneyUppdate()
    {
        moneyUppdate();
        
    }
    // Skill Tree Gets


    public SkillTree GetSkillTree()
    {
        return skillTree;
    }

    public GameObject GetSkillTreeObject()
    {
        return skillTreeObject;
    }

    public List<Abilities> GetUsableAbilities()
    {
        return heroUsableAbilities;
    }
    public bool isPassive_0Learnt()
    {
       
        return skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_0);
    }
    public bool isPassive_1Learnt()
    {
        return skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_1);

    }
    public bool isPassive_2Learnt()
    {
        return skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_2);
    }
    public bool isPassive_3Learnt()
    {
        return skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_3);
    }
    public bool isPassive_4Learnt()
    {
        return skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_4);
    }
    public bool isPassive_5Learnt()
    {
       
        return skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_5);
    }
    public bool isPassive_6Learnt()
    {
        return skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_6);
    }
    public bool isPassive_7Learnt()
    {
        return skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_7);
    }
    public bool isPassive_8Learnt()
    {
        return skillTree.IsSkillUnlocked(SkillTree.SkillType.PassiveSkill_8);
    }
}
