using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

    public class InventorySlotUI : MonoBehaviour,ItemHolder
    {
       
        [SerializeField] InventoryItemIcon icon = null;

        
        int index;
        InventoryItem item;
        Inventory inventory;


    private void OnEnable()
    {
        if(inventory != null)
        {
            inventory.UpdateInventory();
        }
       
    }


    public void Setup(Inventory inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;
        
            icon.SetItem(inventory.GetItemInSlot(index), inventory.GetNumberInSlot(index));
        }

        public int MaxAcceptable(InventoryItem item)
        {
            if (inventory.HasSpaceFor(item))
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void AddItems(InventoryItem item, int number)
        {
            inventory.AddItemToSlot(index, item, number);
        }

        public InventoryItem GetItem()
        {
            return inventory.GetItemInSlot(index);
        }

        public int GetNumber()
        {
            return inventory.GetNumberInSlot(index);
        }

        public void RemoveItems(int number)
        {
        
            inventory.RemoveFromSlot(index, number);
        }
    }

