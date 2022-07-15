using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemButton : MonoBehaviour
{

    ActionItem item;
    UserControl userControl;
    Button button;
    public TextMeshProUGUI buttonText;
    int slot;


    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenItemPanelAndSetValues);

        buttonText.text = item.GetDisplayName();

    }
    public void ButtonSetItem(ActionItem ai)
    {
        item = ai;
    }

    public void ButtonSetUserControl(UserControl user)
    {
        userControl = user;
    }

    private void ButtonFuncionality()
    {
        UseItemCommand cmd = new UseItemCommand(userControl.GetPlayer(), item);
        CommandManager.Instance.AddCommand(cmd);
        userControl.GetGameManager().GetInventory().RemoveFromSlot(slot,1);
        userControl.confirmItemPanel.SetActive(false);
        userControl.itemScrollRect.SetActive(false);
    }

    private void OpenItemPanelAndSetValues()
    {
        var abilityPanel = userControl.confirmItemPanel.transform;
        var confirmButton = abilityPanel.Find("Ok").GetComponent<Button>();
        abilityPanel.Find("ItemDescribtion").GetComponent<TextMeshProUGUI>().text = item.GetDescription();
        var abilityName = abilityPanel.Find("ItemName");
        abilityName.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = item.GetDisplayName();
        var canBeCasted = abilityPanel.Find("NumberLeft");
        var canBeCastedText = canBeCasted.Find("NumberLeftText").GetComponent<TextMeshProUGUI>();
        int slotIndex = userControl.GetGameManager().GetInventory().FindStack(item);
        slot = slotIndex;
        if(slotIndex != -1)
        {
            if (userControl.GetGameManager().GetInventory().GetNumberInSlot(slotIndex) > 0)
            {
                canBeCastedText.color = Color.green;
                canBeCastedText.text = "Items Left: " + userControl.GetGameManager().GetInventory().GetNumberInSlot(slotIndex);
                confirmButton.onClick.RemoveAllListeners();
                confirmButton.onClick.AddListener(ButtonFuncionality);

            }

        }else
        {

            canBeCastedText.color = Color.red;
            canBeCastedText.text = "Items Left: 0";
            confirmButton.onClick.RemoveListener(ButtonFuncionality);
        }
        


        userControl.confirmItemPanel.SetActive(true);
    }


}
