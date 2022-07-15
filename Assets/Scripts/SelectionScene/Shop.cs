using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Shop : MonoBehaviour
{
    public GameObject shopWindow;
    public InventoryItem[] items;
    public BuyItem itemButton;
    public TextMeshProUGUI moneyText;
    GameManager gameManager;

    

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        foreach(InventoryItem i in items)
        {

            var shopItem = Instantiate(itemButton, shopWindow.transform);
            shopItem.SetGameManager(gameManager);
            shopItem.SetItem(i);

            shopItem.transform.SetParent(shopWindow.transform);
        }
        gameManager.moneyUppdate += UpdateMoneyText;
        moneyText.text = gameManager.money.ToString();
    }

    private void UpdateMoneyText()
    {
        if(moneyText != null)
        moneyText.text = gameManager.money.ToString();
    }

    

}
