using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyItem : MonoBehaviour, ItemHolder
{
    InventoryItem item;
    int price;
    Image icon;
    TextMeshProUGUI priceText;
    GameManager gameManager;

    
    public void BuyItemButton()
    {
        /*if(gameManager.GetInventory().CanBeAddedToInventory(item) && price <= gameManager.money)
        {
            gameManager.GetInventory().AddToFirstEmptySlot(item, 1);
            gameManager.money -= price;
        }*/
    }

    public void SetItem(InventoryItem it)
    {
        item = it;
        SetImage(it.GetIcon());
        SetPrice(it.GetPrice());
    }

    private void SetImage(Sprite sprite)
    {
        icon = gameObject.transform.Find("Image").GetComponent<Image>();
        icon.sprite = sprite;
    }

    public void SetPrice(int pr)
    {
        priceText = gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        price = pr;
        priceText.text = price.ToString();

    }

    public void SetGameManager(GameManager gm)
    {
        gameManager = gm;
    }

    public InventoryItem GetItem()
    {
        return item;
    }
}
