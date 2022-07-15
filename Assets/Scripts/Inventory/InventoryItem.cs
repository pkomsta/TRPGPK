using System;
using System.Collections.Generic;
using UnityEngine;


    
    public abstract class InventoryItem : ScriptableObject
    {        
        
        [SerializeField] string displayName = null;
        [SerializeField] [TextArea] string description = null;
        [SerializeField] Sprite icon = null;
        [SerializeField] bool stackable = false;
        [SerializeField] int dropChance = 100;
        [SerializeField] int price = 0;

        public Sprite GetIcon()
        {
            return icon;
        }

        public bool IsStackable()
        {
            return stackable;
        }

        public string GetDisplayName()
        {
            return displayName;
        }

        public string GetDescription()
        {
            return description;
        }

    public int GetDropChance()
    {
        return dropChance;
    }

    public int GetPrice()
    {
        return price;
    }

    }

