using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

   
    public class EquipmentSlotUI : MonoBehaviour,ItemHolder
    {
      

        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] EquipLocation equipLocation = EquipLocation.Weapon;

        
        Equipment playerEquipment;

        

        private void Awake()
        {
        var player = GameObject.FindObjectOfType<GameManager>();
            playerEquipment = player.GetComponent<Equipment>();
            playerEquipment.equipmentUpdated += RedrawUI;
        }

        private void Start()
        {
            RedrawUI();
        }

      

        public int MaxAcceptable(InventoryItem item)
        {
            EquipableItem equipableItem = item as EquipableItem;
            if (equipableItem == null) return 0;
            if (equipableItem.GetAllowedEquipLocation() != equipLocation) return 0;
            if (GetItem() != null) return 0;

            return 1;
        }

        public void AddItems(InventoryItem item, int number)
        {
        
            playerEquipment.AddItem(equipLocation, (EquipableItem)item);
        }

        public InventoryItem GetItem()
        {
            return playerEquipment.GetItemInSlot(equipLocation);
        }

        public int GetNumber()
        {
            if (GetItem() != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void RemoveItems(int number)
        {
            playerEquipment.RemoveItem(equipLocation);
        }
    public EquipLocation GetEquipLocation()
    {
        return equipLocation;
    }

       

        void RedrawUI()
        {
        
            icon.SetItem(playerEquipment.GetItemInSlot(equipLocation));
        }
    }