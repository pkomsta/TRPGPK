using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class Gameboard : MonoBehaviour
{
    #region zmienne
    public static Gameboard Instance => s_Instance;
    private static Gameboard s_Instance;

    public UnityEvent onEndTurn;
    public UnityEvent onStartPlayerTurn;
    int enemiesLeft;

    public int Width;
    public int Height;

    public TextMeshProUGUI TurnIndicatorText;
    public GameObject unitInfo;


    public Grid Grid => m_Grid;
    public AnimationSystem AnimationSystem => m_AnimSystem;
    public Unit.Team CurrentTeam => m_CurrentTeam;
    
    private Unit[,] m_Content;

    private Grid m_Grid;
    private AnimationSystem m_AnimSystem;
    [Header("Spawn Points")]
    public Transform playerSpawn;
    public Transform[] enemySpawns;
    [Header("Log")]
    public Button logButton;
    public GameObject battleLog;
    public TextMeshProUGUI logText;
    [Header("End battle")]
    public GameObject endScreen;
    public Image itemIconPref;

    private Plane m_Plane;
    
    private Unit.Team m_CurrentTeam = Unit.Team.Player;

    bool victory = false;
    bool defeat = false;

    List<InventoryItem> dropedItems = new List<InventoryItem>();
    int expGained = 0;
    int moneyGained = 0;
    GameManager gameManager;
    int messageNumber;
    #endregion
    void Awake()
    {
        s_Instance = this;
        m_Grid = GetComponent<Grid>();
        m_Content = new Unit[Width,Height];
        m_AnimSystem = new AnimationSystem();
        gameManager = FindObjectOfType<GameManager>();
        m_Plane = new Plane(Vector3.up, Vector3.zero);
        gameManager.SetChoosenLevel();
        gameManager.SpawnPlayerAndSetStats(playerSpawn);
        gameManager.SpawnEnemys(enemySpawns);
    }

    private void Start()
    {
        
        if(onEndTurn == null)
        {
            onEndTurn = new UnityEvent();
        }

        
        UpdateTurnIndicator();
        
    }

    private void Update()
    {
        m_AnimSystem.Update();
        if (victory)
        {
            
            
        }
        

    }

    public void SetUnit(Vector3Int cell, Unit unit)
    {
        if(!IsOnBoard(cell))
            return;
        
        m_Content[cell.x, cell.z] = unit;
    }

    public Unit GetUnit(Vector3Int cell)
    {
        if (!IsOnBoard(cell))
            return null;

        return m_Content[cell.x, cell.z];
    }

    public Vector3Int GetClosestCell(Vector3 pos)
    {
        var idx = m_Grid.WorldToCell(pos);

        if (idx.x == 0) idx.x = 0;
        else if (idx.x >= Width) idx.x = Width - 1;
        if (idx.z == 0) idx.z = 0;
        else if (idx.z >= Height) idx.z = Height - 1;

        return idx;
    }

    public bool IsOnBoard(Vector3Int cell)
    {
        return cell.x >= 0 && cell.x < Width && cell.z >= 0 && cell.z < Height;
    }

    public bool Raycast(Ray ray, out Vector3Int cell)
    {
        cell = Vector3Int.zero;
        
        //First raycast against collider to check if we clicked on any unit directly
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Hero u = hit.collider.GetComponentInParent<Hero>();
            if (u != null)
            {
                cell = u.CurrentCell;
                return true;
            }
        }
        
        if (m_Plane.Raycast(ray, out float d))
        {
            var clickedCell = Gameboard.Instance.Grid.WorldToCell(ray.GetPoint(d));

            if (IsOnBoard(clickedCell))
            {
                cell = clickedCell;
                return true;
            }

            return false;
        }

        return false;
    }

    public void MoveUnit(Unit u, Vector3Int to, bool animate = true)
    {
        //unit that aren't on the board have (-1,-1-,1) as their current cell
        if(u.CurrentCell.x != -1)   
            m_Content[u.CurrentCell.x, u.CurrentCell.z] = null;
        
        m_Content[to.x, to.z] = u;
        u.CurrentCell = to;

        if (animate)
        {
            m_AnimSystem.NewAnim(
                u.transform.transform, 
                m_Grid.GetCellCenterWorld(to),
                3.0f);
        }
    }
    public void AttackUnit(Unit u, Unit au)
    {

        u.AttackUnit(au);
        
    }

    public void CastAbility(Unit u, Unit au)
    {
        
        u.chosenAbility.CastAbility(au);

    }

    public void SwitchTeam()
    {
        m_CurrentTeam = m_CurrentTeam == Unit.Team.Player ? Unit.Team.Enemy : Unit.Team.Player;
        onEndTurn.Invoke();
        UpdateTurnIndicator();
        if(m_CurrentTeam == Unit.Team.Player)
        {
            onStartPlayerTurn.Invoke();
        }
        
    }

    public void TakeOutUnit(Unit u)
    {
        u.CurrentCell = new Vector3Int(-1,-1, -1);
        Gameboard.Instance.AnimationSystem.NewAnim(u.transform,
            u.Side == Unit.Team.Player ? new Vector3(-1.0f, 0.0f, 5.0f) : new Vector3(11.0f, 0.0f, 5.0f),
            20.0f);
    }

    void UpdateTurnIndicator()
    {
        TurnIndicatorText.text = (m_CurrentTeam == Unit.Team.Enemy ? "Enemy" : "Player") + " Turn";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        
        for (int x = 0; x < Width; ++x)
        {
            Gizmos.DrawLine(Vector3.right * x, Vector3.right * x + Height * Vector3.forward);
        }
        
        Gizmos.DrawLine(Vector3.right * Width, Vector3.right * Width + Height * Vector3.forward);
        
        for (int y = 0; y < Height; ++y)
        {
            Gizmos.DrawLine(Vector3.forward * y, Vector3.forward * y + Vector3.right * Width);
        }
        
        Gizmos.DrawLine(Vector3.forward * Height, Vector3.forward * Height + Vector3.right * Width);
    }

    public void Victory()
    {
        
        
        victory = true;
        gameManager.IncrementUnlockedLevels();
        foreach(InventoryItem ii in dropedItems)
        {
            
                gameManager.GetComponent<Inventory>().AddToFirstEmptySlotNoUI(ii,1);
                
            
        }
        HideUnitInfo();
        gameManager.AddExperienceAndCheckLevel(expGained);
        gameManager.AddMoney(moneyGained);
        DisplayEndScreen(true);

    }

    public void Defeat()
    {
        HideUnitInfo();
        
        defeat = true;
        DisplayEndScreen(false);
        
    }
    public void DisplayEndScreen(bool win)
    {
        if (!endScreen.activeSelf)
        {
            endScreen.SetActive(true);
            if (win)
            {
                endScreen.transform.Find("Exp").GetComponent<TextMeshProUGUI>().text = "Exp Gained: <color=green>" + expGained;
                endScreen.transform.Find("Money").GetComponent<TextMeshProUGUI>().text = "Money Gained: <color=yellow>" + moneyGained;
                endScreen.transform.Find("EndText").GetComponent<TextMeshProUGUI>().text = "<color=green> Victory";
                var grid = endScreen.transform.Find("GridLayout");
                foreach (InventoryItem it in dropedItems)
                {
                    var item = Instantiate(itemIconPref, grid.transform);
                    item.sprite = it.GetIcon();
                }

            }
            else
            {
                endScreen.transform.Find("Exp").GetComponent<TextMeshProUGUI>().text = "Exp Gained: <color=green>0";
                endScreen.transform.Find("Money").GetComponent<TextMeshProUGUI>().text = "Money Gained: <color=yellow>0";
                endScreen.transform.Find("EndText").GetComponent<TextMeshProUGUI>().text = "<color=red> Defeat";
            }
        }
        
        
        
    }

    public void ShowUnitInfo(Unit unit)
    {
        

        unitInfo.transform.Find("Background").transform.Find("Sprite").GetComponent<Image>().sprite = unit.GetComponentInChildren<SpriteRenderer>().sprite;
        unitInfo.transform.Find("Background").transform.Find("Unit Name").GetComponent<TextMeshProUGUI>().text = unit.unitName;
        unitInfo.transform.Find("Background").transform.Find("Unit HP").GetComponent<TextMeshProUGUI>().text = "HP: " + unit.health +"/" + unit.maxHealth;
        unitInfo.transform.Find("Background").transform.Find("Unit HP").GetComponent<TextMeshProUGUI>().color = Color.red;
        switch (unit.resource)
        {
            case Unit.Resource.Mana:
                unitInfo.transform.Find("Background").transform.Find("Unit MP").GetComponent<TextMeshProUGUI>().text = "MP: " + unit.abilityResource + "/" + unit.maxAbilityResource;
                unitInfo.transform.Find("Background").transform.Find("Unit MP").GetComponent<TextMeshProUGUI>().color = Color.blue;
                break;
            case Unit.Resource.Rage:
                unitInfo.transform.Find("Background").transform.Find("Unit MP").GetComponent<TextMeshProUGUI>().text = "Rage: " + unit.abilityResource + "/" + unit.maxAbilityResource;
                unitInfo.transform.Find("Background").transform.Find("Unit MP").GetComponent<TextMeshProUGUI>().color = new Color(0.4f, 0f, 0.101f);
                break;
            case Unit.Resource.Stamina:
                unitInfo.transform.Find("Background").transform.Find("Unit MP").GetComponent<TextMeshProUGUI>().text = "Stamina: " + unit.abilityResource + "/" + unit.maxAbilityResource;
                unitInfo.transform.Find("Background").transform.Find("Unit MP").GetComponent<TextMeshProUGUI>().color = Color.yellow;
                break;
        }
        string abi = "";
        foreach (Abilities a in unit.abilities)
        {
            abi += a.abilityName + ", ";
        }
        unitInfo.transform.Find("Background").transform.Find("TextMask").transform.Find("Limiter").transform.Find("Info").GetComponent<TextMeshProUGUI>().text = "Attack: " + unit.attack + " \n"
            + "Magick Attack: " + unit.magickAttack + " \n"
            + "Vitality: " + unit.vitality + " \n"
            + "Wisdom: " + unit.wisdom + " \n"
            + "Defense: " + unit.defense + " \n"
            + "Magick Defense: " + unit.magicDefense + " \n"
            + "Speed: " + unit.speed + " \n"
            + "Range: " + unit.range + " \n"
            + " Abilites: \n"
            + abi;
        unitInfo.SetActive(true);


    }

    public void HideUnitInfo()
    {
        unitInfo.SetActive(false);
    }
    public bool GetDefeat()
    {
        return defeat;
    }

    public void LoadSelectionLevel()
    {
        SceneManager.LoadScene(1);
    }

    public int GetEnemiesLeft()
    {
        return enemiesLeft;
    }
    public void SetEnemiesLeft(int value)
    {
        enemiesLeft = value;
    }
    public void EnemyKilled()
    {
        enemiesLeft--;
    }

   public void areThereAnyEnemiesLeft()
    {
        if (enemiesLeft <= -1)
        {
            Victory();
        }

    }

   public void EnemyDies(Enemy unit)
    {
        InventoryItem dropped = null;
        switch (gameManager.GetChoosenHero().heroClass)
        {
            case Hero.HeroClass.Warrior:
                dropped = unit.GetItemDropedWarrior();
                break;
            case Hero.HeroClass.Mage:
                dropped = unit.GetItemDropedMage();
                break;
            default:
                dropped = unit.GetItemDropedWarrior();
                break;
        }
        
        if(dropped != null)
        {
            dropedItems.Add(dropped);
            
        }
        expGained += unit.expGiven;
        moneyGained += unit.moneyOnDeath;
        unit.transform.position = Vector3.zero;

    }

    public void OpenFullBattleLog()
    {
        if (!battleLog.activeSelf)
        {
            battleLog.SetActive(true);
        }
        else
        {
            battleLog.SetActive(false);
        }
        
    }

    public void UpdateBattleLog(string message)
    {
        logButton.transform.Find("LogText").GetComponent<TextMeshProUGUI>().text = message;
        logText.text += message + " \n";
        
    }
}


