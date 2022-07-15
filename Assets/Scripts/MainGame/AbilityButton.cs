using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AbilityButton : MonoBehaviour
{

    Abilities ability;
    UserControl userControl;
    Button button;
    public TextMeshProUGUI buttonText;


    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenAbilityPanelAndSetValues);
        

        buttonText.text = ability.abilityName;
        
    }
    public void ButtonSetAbility(Abilities ab)
    {
        ability = ab;
    }

    public void ButtonSetUserControl(UserControl user)
    {
        userControl = user;
    }

    private void ButtonFuncionality()
    {
       
        userControl.GetPlayer().chosenAbility = ability;
        
        StartCoroutine(WaitToAddFunc());
        
    }
    IEnumerator WaitToAddFunc()
    {
        yield return new WaitForSeconds(0.1f);
        userControl.AbilityButtonFromMenuClicked();
        userControl.DisableAbilityMenu();
    }

    private void OpenAbilityPanelAndSetValues()
    {
        var abilityPanel = userControl.confirmAbilityPanel.transform;
        var confirmButton = abilityPanel.Find("Ok").GetComponent<Button>();
        abilityPanel.Find("AbilityDescribtion").GetComponent<TextMeshProUGUI>().text = ability.desc;
        var abilityName = abilityPanel.Find("AbilityName");
        abilityName.Find("AbilityNameText").GetComponent<TextMeshProUGUI>().text = ability.abilityName;
        var canBeCasted = abilityPanel.Find("CanBeCasted");
        var canBeCastedText = canBeCasted.Find("CanBeCastedText").GetComponent<TextMeshProUGUI>();
        if (ability.GetCanBeCasted() && ability.GetCaster().canSpendResource(ability.resourceCost))
        {
            canBeCastedText.color = Color.green;
            canBeCastedText.text = "Can Be Casted";
            
            confirmButton.onClick.AddListener(ButtonFuncionality);

        }
        else if(!ability.GetCanBeCasted())
        {
            
            canBeCastedText.color = Color.red;
            canBeCastedText.text = "On Cooldown";
            confirmButton.onClick.RemoveAllListeners();
        }
        else
        {
            canBeCastedText.color = Color.red;
            canBeCastedText.text = "No " + ability.GetCaster().resource;
            confirmButton.onClick.RemoveAllListeners();
        }
        

        userControl.confirmAbilityPanel.SetActive(true);
    }

    
}
