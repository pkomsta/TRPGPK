using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("InventorySystem/Mana Item"))]
public class ManaItem : ActionItem
{
    [SerializeField] int manaValue = 0;
    public override void Use(Unit unit)
    {
        if(unit.resource == Unit.Resource.Mana)
        {
            unit.GainResource(manaValue);
            Gameboard.Instance.UpdateBattleLog("<color=green> " + unit.unitName + "<color=white> used <color=green>" + this.GetDisplayName() + "<color=white> healing " +
                "<color=blue> " + manaValue + "<color=white> of thier current health");
        }
        
    }
}
