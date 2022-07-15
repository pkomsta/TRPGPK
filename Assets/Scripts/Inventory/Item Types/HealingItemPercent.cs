using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("InventorySystem/Healing Item %"))]
public class HealingItemPercent : ActionItem
{
    [SerializeField] float healPercent = 0.25f;
    public override void Use(Unit unit)
    {
        unit.GainHealth((int)(unit.maxHealth* healPercent));
        Gameboard.Instance.UpdateBattleLog("<color=green> " + unit.unitName + "<color=white> used <color=green>" + this.GetDisplayName() + "<color=white> healing " +
            "<color=green> " + (int)(unit.maxHealth * healPercent) + "<color=white> of thier current health");
    }
}
