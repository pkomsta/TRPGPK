using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("InventorySystem/Healing Item"))]
public class HealingItem : ActionItem
{
    [SerializeField] int healValue = 0;
    public override void Use(Unit unit)
    {
        unit.GainHealth(healValue);
        Gameboard.Instance.UpdateBattleLog("<color=green> " + unit.unitName + "<color=white> used <color=green>" + this.GetDisplayName() + "<color=white>and got healed by " +
            "<color=green> " + healValue + " hp");
    }
}
