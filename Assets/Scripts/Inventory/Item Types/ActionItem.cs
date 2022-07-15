using System;
using UnityEngine;


    
    [CreateAssetMenu(menuName = ("InventorySystem/Action Item"))]
    public class ActionItem : InventoryItem
    {
       
        
        [SerializeField] bool consumable = false;
        [SerializeField] bool permanent = false;

       

       
        public virtual void Use(Unit unit)
        {
            Debug.Log("Using action: " + this);
        }

        public bool isConsumable()
        {
            return consumable;
        }

    public bool isPermanent()
    {
        return permanent;
    }
    }
