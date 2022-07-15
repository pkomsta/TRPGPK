using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_SkillTree : SkillTree
{
    public override SkillType GetSkillRequierment(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.PassiveSkill_1: return SkillType.PassiveSkill_0;
            case SkillType.ActiveSkill_0: return SkillType.PassiveSkill_1;
            case SkillType.PassiveSkill_2: return SkillType.PassiveSkill_1;
            case SkillType.PassiveSkill_3: return SkillType.ActiveSkill_0;
            case SkillType.ActiveSkill_1: return SkillType.PassiveSkill_4;
            case SkillType.ActiveSkill_2: return SkillType.ActiveSkill_1;
            case SkillType.PassiveSkill_5: return SkillType.ActiveSkill_2;
            case SkillType.PassiveSkill_6: return SkillType.ActiveSkill_3;
            case SkillType.ActiveSkill_4: return SkillType.PassiveSkill_6;
            case SkillType.ActiveSkill_5: return SkillType.PassiveSkill_7;
            case SkillType.PassiveSkill_8: return SkillType.ActiveSkill_5;
            case SkillType.ActiveSkill_6: return SkillType.ActiveSkill_5;
            case SkillType.ActiveSkill_7: return SkillType.PassiveSkill_8;
            case SkillType.ActiveSkill_8: return SkillType.ActiveSkill_6;


        }
        return SkillType.None;
    }
}
