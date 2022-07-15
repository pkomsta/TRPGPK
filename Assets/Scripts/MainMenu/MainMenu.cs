using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject chooseHeroPanel;
    public Image heroImage;
    public Button heroButton;
    public GameObject heroInfoPanel;
    bool isHeroInfoOpen = false;

    public Button left, right;

    Sprite heroSprite;

    int heroIndex = 0;

    private void Start()
    {
        GerHeroSprite(heroIndex);
        UpdateSprite();

    }

    

    private void GerHeroSprite(int index)
    {
        heroSprite = GameManager.Manager.heroesToChooseFrom[index].gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite;
    }

    private void UpdateSprite()
    {
       
        GerHeroSprite(heroIndex);
        heroImage.sprite = heroSprite;
    }

    public void OpenHeroInfo()
    {
        heroInfoPanel.GetComponent<HeroDescribtionPanel>().FillTextFilds(GameManager.Manager.heroesToChooseFrom[heroIndex]);
        isHeroInfoOpen = true;
        heroInfoPanel.SetActive(true);

       
    }
    public void CloseHeroInfo()
    {
        isHeroInfoOpen = false;
        heroInfoPanel.SetActive(false);
    }
    public void ActivateChooseHeroScreen()
    {
        chooseHeroPanel.SetActive(true);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LeftButtonClicked()
    {
        if(heroIndex< GameManager.Manager.heroesToChooseFrom.Length-1)
        {
            heroIndex++;
            UpdateSprite();
        }
        else
        {

        }
    }
    public void RightButtonClicked()
    {
        if (heroIndex > 0)
        {
            heroIndex--;
            UpdateSprite();
        }
        else
        {

        }
    }

    public void ChooseHeroAndStartGame()
    {
        GameManager.Manager.ChooseHero(heroIndex);
        LoadNextScene();
    }

    public void Quit()
    {
        Application.Quit();
    }




}
