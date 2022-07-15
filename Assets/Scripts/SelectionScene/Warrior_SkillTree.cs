using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_SkillTree : SkillTree
{
    public override SkillType GetSkillRequierment(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.PassiveSkill_0: return SkillType.ActiveSkill_0;
            case SkillType.PassiveSkill_2: return SkillType.PassiveSkill_1;
            case SkillType.ActiveSkill_1: return SkillType.PassiveSkill_2;
            case SkillType.ActiveSkill_2: return SkillType.ActiveSkill_1;
            case SkillType.PassiveSkill_4: return SkillType.PassiveSkill_3;
            case SkillType.ActiveSkill_3: return SkillType.PassiveSkill_4;
            case SkillType.ActiveSkill_4: return SkillType.ActiveSkill_3;
            case SkillType.ActiveSkill_5: return SkillType.PassiveSkill_5;
            case SkillType.PassiveSkill_7: return SkillType.PassiveSkill_6;
            case SkillType.PassiveSkill_8: return SkillType.PassiveSkill_7;
            case SkillType.ActiveSkill_6: return SkillType.PassiveSkill_8;
            case SkillType.ActiveSkill_7: return SkillType.PassiveSkill_8;
            case SkillType.ActiveSkill_8: return SkillType.ActiveSkill_6;

        }
        return SkillType.None;
    }
}
