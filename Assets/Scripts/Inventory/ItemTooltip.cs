using UnityEngine;
using TMPro;

    
    public class ItemTooltip : MonoBehaviour
    {
        
        [SerializeField] TextMeshProUGUI titleText = null;
        [SerializeField] TextMeshProUGUI bodyText = null;
        [SerializeField] bool isShop = false;
        int index;
        EquipableItem itemToEquip = null;
        ActionItem itemToUse = null;
        InventoryItem itemToSet = null;
        InventorySlotUI InventorySlotUI;



        public void Setup(InventoryItem item)
        {
        
            titleText.text = item.GetDisplayName();
            bodyText.text = item.GetDescription();
        if (!isShop)
        {
            if (typeof(EquipableItem).IsAssignableFrom(item.GetType()))
            {
                itemToEquip = item as EquipableItem;
            }
            if (typeof(ActionItem).IsAssignableFrom(item.GetType()))
            {
                itemToUse = item as ActionItem;
                if (itemToUse.isPermanent())
                {
                    gameObject.transform.Find("Equip").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Use";
                }
                else
                    gameObject.transform.Find("Equip").gameObject.SetActive(false);
            }
        }else
        {
            
            gameObject.transform.Find("Equip").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Buy";
        }
        itemToSet = item;

    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
    public void Equip()
    {
        
        var eq = FindObjectsOfType<EquipmentSlotUI>();
        var i = FindObjectOfType<Inventory>();
        var gm = FindObjectOfType<GameManager>();
        if (!isShop)
        {
            if(itemToEquip != null) {
                EquipmentSlotUI choosenEqSlot = null;

                foreach (EquipmentSlotUI equi in eq)
                {
                    if (itemToEquip.GetAllowedEquipLocation() == equi.GetEquipLocation())
                    {
                        choosenEqSlot = equi;
                        break;
                    }
                }

                var item = choosenEqSlot.GetItem();

                if (choosenEqSlot != null)
                {

                    if (item == null)
                    {

                        choosenEqSlot.AddItems(itemToEquip, 1);
                    }
                    else
                    {

                        i.AddToFirstEmptySlot(item, 1);
                        choosenEqSlot.AddItems(itemToEquip, 1);
                    }

                    if (InventorySlotUI)
                    {
                        InventorySlotUI.RemoveItems(1);
                    }
                    else
                    {
                        choosenEqSlot.RemoveItems(1);


                    }

                    Destroy(this.gameObject);
                }
            }else if(itemToUse != null )
            {
                if (itemToUse.isPermanent())
                {
                    itemToUse.Use(null);
                    InventorySlotUI.RemoveItems(1);

                }
                
            }
            
        }else{

            if (gm.GetInventory().CanBeAddedToInventory(itemToSet) && itemToSet.GetPrice() <= gm.money)
            {
                gm.GetInventory().AddToFirstEmptySlot(itemToSet, 1);
                gm.money -= itemToSet.GetPrice();
                gm.MoneyUppdate();

                Destroy(this.gameObject);
            }
        }
            
        
        
        

        
    }

    public void Sell()
    {
        
        var gm = FindObjectOfType<GameManager>();
        InventorySlotUI.RemoveItems(1);
        gm.AddMoney((int)(itemToSet.GetPrice() * 0.25f));
        gm.MoneyUppdate();

    }

    public void SetInventorySlotUI(InventorySlotUI  iui)
    {
        InventorySlotUI = iui;
    }
    }

