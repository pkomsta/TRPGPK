using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class SkillTree
{

    public event EventHandler<OnSkillUnlocked> onSkillUnlocked;
    public class OnSkillUnlocked : EventArgs
    {
        public SkillType skillType;
    }
    public event EventHandler<OnAnySkillUnlocked> onAnySkillUnlocked;
    public class OnAnySkillUnlocked : EventArgs
    {
        public SkillType skillType;
    }

    public enum SkillType
    {
        None,
        ActiveSkill_0,
        ActiveSkill_1,
        ActiveSkill_2,
        ActiveSkill_3,
        ActiveSkill_4,
        ActiveSkill_5,
        ActiveSkill_6,
        ActiveSkill_7,
        ActiveSkill_8,
        PassiveSkill_0,
        PassiveSkill_1,
        PassiveSkill_2,
        PassiveSkill_3,
        PassiveSkill_4,
        PassiveSkill_5,
        PassiveSkill_6,
        PassiveSkill_7,
        PassiveSkill_8

    }
    List<SkillType> unlockedActiveSkills;
    List<SkillType> unlockedPassiveSkills;

    int skillPoints = 1;

    public SkillTree()
    {
        unlockedActiveSkills = new List<SkillType>();
        unlockedPassiveSkills = new List<SkillType>();
    }
    void UnlockedSkill(SkillType skillType)
    {
        if (!IsSkillUnlocked(skillType)){
            if (skillType.ToString().Contains("Passive"))
            {
                unlockedPassiveSkills.Add(skillType);
            }
            else
            {
                unlockedActiveSkills.Add(skillType);
                // ?. sprawdza czy null
                onSkillUnlocked?.Invoke(this, new OnSkillUnlocked{skillType = skillType});
            }
            DecreaseSkillPoints();

        }
        onAnySkillUnlocked?.Invoke(this, new OnAnySkillUnlocked { skillType = skillType });
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedActiveSkills.Contains(skillType) || unlockedPassiveSkills.Contains(skillType);
    }


    public abstract SkillType GetSkillRequierment(SkillType skillType); 

       
    

    public bool canSkillBeUnlocked(SkillType skillType)
    {
        SkillType skillRequierment = GetSkillRequierment(skillType);
        if (skillRequierment != SkillType.None)
        {
            if (IsSkillUnlocked(skillRequierment))
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
           
            return true;
        }
        


    }
    
    public bool TryUnlockingSkill(SkillType skillType)
    {
        SkillType skillRequierment = GetSkillRequierment(skillType);
        if(skillRequierment != SkillType.None)
        {
            if (IsSkillUnlocked(skillRequierment) && skillPoints > 0)
            {
                
                UnlockedSkill(skillType);
               
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if(skillPoints > 0)
            {
                
                UnlockedSkill(skillType);
                
                return true;
            }
            
        }
        return false;
    }


    public void AddSkillPoint()
    {
        skillPoints++;
    }
    void DecreaseSkillPoints()
    {
        skillPoints--;
    }
    public int GetSkillPoints()
    {
        return skillPoints;
    }
}
