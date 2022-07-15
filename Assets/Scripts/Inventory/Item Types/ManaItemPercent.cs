using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("InventorySystem/Mana Item %"))]
public class ManaItemPercent : ActionItem
{

    [SerializeField] float manaPercent = 0.25f;
    public override void Use(Unit unit)
    {
        if(unit.resource == Unit.Resource.Mana)
        {
            unit.GainResource((int)(unit.maxAbilityResource * manaPercent));
            Gameboard.Instance.UpdateBattleLog("<color=green> " + unit.unitName + "<color=white> used <color=green>" + this.GetDisplayName() + "<color=white> healing " +
                "<color=blue> " + (int)(unit.maxAbilityResource * manaPercent) + "<color=white> of thier current health");
        }
       
    }
}
