using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SelectionScene : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject inventoryPanel;
    
    public GameObject skillsPanel;
        
    [Header("Inventory")]
    public TextMeshProUGUI statisticsText;
    [Header("Level Selection")]
    public GameObject levelPanel;
    public Image levelImage;
    [Header("Skill tree")]
    public TextMeshProUGUI skillPointsText;

    Sprite levelSprite;

    GameManager gameManager;
    

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        UpdateSprite(gameManager.GetlastChoosenMap());
       var skillTreeObject = Instantiate(gameManager.GetSkillTreeObject(), skillsPanel.transform);
        skillTreeObject.transform.SetParent(skillsPanel.transform);
        DesactivateAllSidePanels();
        gameManager.GetEquipment().equipmentUpdated += UpdateStatsInfo;
        gameManager.GetSkillTree().onAnySkillUnlocked += UpdateSkillPointText;
    }

    private void UpdateSkillPointText(object sender, SkillTree.OnAnySkillUnlocked e)
    {
        skillPointsText.text = "Skill Points: " + gameManager.GetSkillTree().GetSkillPoints();
    }

    private void GetLocationIcon(int index)
    {
        levelSprite = GameManager.Manager.locations[index].levelIcon;
    }

    private void UpdateSprite(int levelIndex)
    {

        GetLocationIcon(levelIndex);
        levelImage.sprite = levelSprite;
    }

    public void LeftButtonClicked()
    {
        if(1+ gameManager.GetlastChoosenMap() < gameManager.locations.Length)
        if (gameManager.GetlastChoosenMap() < (1 + (gameManager.GetCurrentLevelUnlocked()*2)))
        {
            gameManager.SetlastChoosenMap(gameManager.GetlastChoosenMap()+1);
            UpdateSprite(gameManager.GetlastChoosenMap());
        }
        else
        {

        }
        //Debug.Log(gameManager.GetCurrentLevelUnlocked() * 2);
    }
    public void RightButtonClicked()
    {
        if (gameManager.GetlastChoosenMap() > 0)
        {
            gameManager.SetlastChoosenMap(gameManager.GetlastChoosenMap() - 1);
            UpdateSprite(gameManager.GetlastChoosenMap());
        }
        else
        {

        }
    }

    public void DesactivateAllSidePanels()
    {
        shopPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        skillsPanel.SetActive(false);
        levelPanel.SetActive(false);
    }
    public void LevelSelection()
    {
        DesactivateAllSidePanels();
        levelPanel.SetActive(true);
    }

    public void InventoryButton()
    {
        DesactivateAllSidePanels();
        inventoryPanel.SetActive(true);
        UpdateStatsInfo();
    }

   
    private void UpdateStatsInfo()
    {
        int[] stats = gameManager.StatsScreen();

        statisticsText.text = "Level:  " + gameManager.level + " \n"
            + "Attack: " + stats[0] + " \n"
            + "Magick Attack: " + stats[1] + " \n"
            + "Vitality: " + stats[2] + " \n"
            + "Wisdom: " + stats[3] + " \n"
            + "Defense: " + stats[4] + " \n"
            + "Magick Defense: " + stats[5] + " \n"
            + "Speed: " + stats[6] + " \n"
            + "Range: " + stats[7] + " \n";
    }

    public void SkillTreeButton()
    {
        DesactivateAllSidePanels();
        UpdateSkillPoints();
        skillsPanel.SetActive(true);
    }

    public void ShopButton()
    {
        DesactivateAllSidePanels();
        shopPanel.SetActive(true);
    }

    private void UpdateSkillPoints()
    {
        skillPointsText.text = "Skill Points: " + gameManager.GetSkillTree().GetSkillPoints();
    }

    public void LoadChoosenLevel()
    {
        gameManager.ChooseLocation(gameManager.GetlastChoosenMap());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        Destroy(gameManager.gameObject);
        SceneManager.LoadScene(0);
    }
}
