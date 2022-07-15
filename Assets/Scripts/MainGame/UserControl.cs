using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UserControl : MonoBehaviour
{
    enum State
    {
        Wait,
        MoveUnit,
        AttackUnit,
        CastAbility,
        checkInfo

    }
    #region zmienne
    public GameObject SelectorPrefab;
    public GameObject MoveDisplayPrefab;

    private State currentState;

    private GameObject m_Selector;
    private Unit selectedUnit = null;

    private Vector3Int[] movableCells;
    private Vector3Int[] attackableCells;
    private Vector3Int[] castableCells;

    private int m_DisplayedMoveDisplay;
    private List<GameObject> m_MoveDisplayPool = new List<GameObject>();

    Hero player;
    bool playerSecelcted = false;
    int moveCellsNumber;
    [Header("Ability UI")]
    public GameObject abilityScrollRect;
    public GameObject abilitiesMenu;
    public Button menuAbilityButton;
    
    public GameObject confirmAbilityPanel;
    [Header("Item UI")]
    public GameObject itemScrollRect;
   
    public GameObject confirmItemPanel;
    public Button menuItemButton;
    public GameObject itemMenu;
    bool isAbilityMenuActive = false;
    bool isItemMenuActive = false;
   
    [Header("Action Buttons")]
    public Button moveButton;
    public Button attackButton;
    public Button infoButton;
    public Button itemButton;
    public Button abilityButton;

   


    [SerializeField] Enemy[] enemies;
    bool enemyTurn = false;
    [SerializeField] int enemiesNumber;
    bool enemyEndedMoving = true;
    List<InventoryItem> usableItems = new List<InventoryItem>();

    GameManager gameManager;

    bool canUseAfterClick = false;
    #endregion
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        
        
        
        m_Selector = Instantiate(SelectorPrefab);
        m_Selector.SetActive(false);

        player = FindObjectOfType<Hero>();

        player.SetUserControl(this);

        //we can't get more than HxW move display so instantiate enough in the pool
        int count = Gameboard.Instance.Height * Gameboard.Instance.Width;
        for (int i = 0; i < count; ++i)
        {
            var o = Instantiate(MoveDisplayPrefab);
            o.SetActive(false);
            m_MoveDisplayPool.Add(o);
        }

        m_DisplayedMoveDisplay = 0;
        
        movableCells = new Vector3Int[count];
        attackableCells = new Vector3Int[count];
        castableCells = new Vector3Int[count];
        enemies = FindObjectsOfType<Enemy>();
        enemiesNumber = enemies.Length - 1;
        Gameboard.Instance.SetEnemiesLeft(enemiesNumber);
        foreach (Abilities ab in player.abilities)
        {
            
            if (ab.isLearnt || gameManager.GetUsableAbilities().Find(x => x.abilityName.Contains(ab.abilityName)))
            {
                var createdButton = Instantiate(menuAbilityButton, new Vector3(0, 0, 0), Quaternion.identity, abilitiesMenu.transform);
                var buttonCreated = createdButton.GetComponent<AbilityButton>();
                buttonCreated.ButtonSetAbility(ab);
                buttonCreated.ButtonSetUserControl(this);

            }
            
        }
        for(int i = 0; i < gameManager.GetInventory().GetSize(); i++)
        {
          var item =  gameManager.GetInventory().GetItemInSlot(i);
            if(item != null)
            {
                if (typeof(ActionItem).IsAssignableFrom(item.GetType()))
                {
                    var actionItem = item as ActionItem;
                    if (actionItem.isConsumable())
                    {
                        usableItems.Add(actionItem);
                    }
                }
            }
            
        }

        foreach(ActionItem ai in usableItems)
        {
            var createdButton = Instantiate(menuItemButton, new Vector3(0, 0, 0), Quaternion.identity, itemMenu.transform);
            var buttonCreated = createdButton.GetComponent<ItemButton>();
            buttonCreated.ButtonSetItem(ai);
            buttonCreated.ButtonSetUserControl(this);
        }

    }

    
    void Update()
    {
        
        if (player.isDead)
        {
           
            if (!Gameboard.Instance.GetDefeat())
            {
                Gameboard.Instance.Defeat();
            }
            return;
            
        }
        if(Gameboard.Instance.AnimationSystem.IsAnimating)
            return;
        // AI Actions
        if (Gameboard.Instance.CurrentTeam == Unit.Team.Enemy )
        {

            if (!enemyTurn)
            {
                currentState = State.Wait;
                DeselectUnit();
                playerSecelcted = false;
                enemyTurn = true;
            }

            if (enemyEndedMoving && enemiesNumber >= 0)
            {
                enemyEndedMoving = false;
                
                if (!enemies[enemiesNumber].isDead)
                {
                    StartCoroutine(MakeEnemyDoStuff(enemies[enemiesNumber]));
                    
                }
                else
                {
                    enemyEndedMoving = true;
                    enemiesNumber--;
                    DeselectUnit();
                }
            }

            if(enemyEndedMoving && enemiesNumber < 0)
            {
                enemyTurn = false;
                Gameboard.Instance.SwitchTeam();
                enemiesNumber = enemies.Length - 1;
            }
        }
        //Player Actions
        else if(!enemyTurn){

            if (!playerSecelcted)
            {
                playerSecelcted = true;
                SelectPlayer();
            }
            


            switch (currentState)
            {
                case State.Wait:
                    break;
                case State.AttackUnit:
                    if (Input.GetMouseButtonUp(0))
                    {
                        if(!isAbilityMenuActive)
                       player.AttackEnemyUnit(selectedUnit);
                        
                    }

                    break;
                case State.MoveUnit:
                    if (Input.GetMouseButtonUp(0))
                    {
                        if (!isAbilityMenuActive)
                            player.MoveUnit(selectedUnit,movableCells);
                        

                    }
                    break;
                case State.CastAbility:
                    if (Input.GetMouseButtonUp(0))
                    {
                        
                       player.UseAbilityOnUnit(selectedUnit);
                        
                        
                    }else if(player.chosenAbility.abilityType == Abilities.AbilityType.self_buff || player.chosenAbility.abilityType == Abilities.AbilityType.self_heal || player.chosenAbility.abilityType == Abilities.AbilityType.self_resource)
                    {
                        player.UseSelfAbility();
                    }
                    break;

                case State.checkInfo:
                    if (Input.GetMouseButtonUp(0))
                    {
                        player.CheckUnitInfo(selectedUnit);
                    }
                    break;


            }
        }
        

        
    }

    void DeselectUnit()
    {
        selectedUnit = null;
        DeselectCells();
    }
    

    void SelectPlayer()
    {

        selectedUnit = player;
        
        if (selectedUnit != null)
        {
            var gameboard = Gameboard.Instance;
            m_Selector.SetActive(true);
            m_Selector.transform.position = selectedUnit.transform.position;
            
           
        }

        
    }
    void EnemySelected(Enemy unit)
    {
        selectedUnit = unit;
        
        m_Selector.SetActive(true);
        m_Selector.transform.position = selectedUnit.transform.position;

        FindCellsToMove(Gameboard.Instance);



    }

    private void FindCellsToMove(Gameboard gameboard)
    {
        int count = selectedUnit.GetMoveCells(movableCells, gameboard);
        for (int i = 0; i < count; i++)
        {
            m_MoveDisplayPool[i].SetActive(true);
            m_MoveDisplayPool[i].transform.position = gameboard.Grid.GetCellCenterWorld(movableCells[i]);
        }



        CleanMoveIndicator(count, m_DisplayedMoveDisplay);

        m_DisplayedMoveDisplay = count;
    }

    private void FindCellsToAttack(Gameboard gameboard)
    {
        attackableCells = new Vector3Int[Gameboard.Instance.Height * Gameboard.Instance.Width];
        int count = selectedUnit.GetAttackCells(attackableCells, gameboard);
       
        /* for (int i = 0; i < count; i++)
         {
             m_MoveDisplayPool[i].SetActive(true);
             m_MoveDisplayPool[i].transform.position = gameboard.Grid.GetCellCenterWorld(movableCells[i]);
         }*/


    }
    private void FindCellsToCast(Gameboard gameboard, Abilities ability)
    {
        castableCells = new Vector3Int[Gameboard.Instance.Height * Gameboard.Instance.Width];
        int count = selectedUnit.GetAbilityCells(castableCells, gameboard,ability);
        
       
       /* for (int i = 0; i < count; i++)
        {
            m_MoveDisplayPool[i].SetActive(true);
            m_MoveDisplayPool[i].transform.position = gameboard.Grid.GetCellCenterWorld(movableCells[i]);
        }*/


    }

    IEnumerator MakeEnemyDoStuff(Enemy unit)
    {
        EnemySelected(unit);
        
        yield return new WaitForSeconds(0.5f);
        FindCellsToAttack(Gameboard.Instance);
        if (unit.abilities != null)
        {
            foreach (Abilities ab in unit.abilities)
            {
                FindCellsToCast(Gameboard.Instance, ab);
                unit.FindCastableAbilities(castableCells, ab);
                
                
            }

            unit.ChooseAbilityToCast();
        }
        
        if (unit.chosenAbility != null)
        {
            unit.CastChoosenAbility();
        }
        else if (unit.canAttack(attackableCells))
        {
            unit.AttackPlayer();
        }
        else
        {
            FindCellsToMove(Gameboard.Instance);
            unit.FindClosestMoveCell(movableCells);
        }
        
        enemiesNumber--;
        enemyEndedMoving = true;
        DeselectUnit();
        
    }
    void CleanMoveIndicator(int lowerBound, int upperBound)
    {
        for (int i = lowerBound; i < upperBound; ++i)
        {
            m_MoveDisplayPool[i].SetActive(false);
        }
    }

    public void DeselectCells()
    {
        m_Selector.gameObject.SetActive(false);
        CleanMoveIndicator(0, m_DisplayedMoveDisplay);
        m_DisplayedMoveDisplay = 0;
    }

    public void MoveButtonClicked()
    {
        var gameboard = Gameboard.Instance;

        int count = selectedUnit.GetMoveCells(movableCells, gameboard);
        for (int i = 0; i < count; i++)
        {
            m_MoveDisplayPool[i].SetActive(true);
            m_MoveDisplayPool[i].transform.position = gameboard.Grid.GetCellCenterWorld(movableCells[i]);
        }

        CleanMoveIndicator(count, m_DisplayedMoveDisplay);

        m_DisplayedMoveDisplay = count;
        DisableAbilityMenu();
        DisableItemMenu();
        Gameboard.Instance.HideUnitInfo();
        currentState = State.MoveUnit;
    }
    public void AttackButtonClicked()
    {
        var gameboard = Gameboard.Instance;

        int count = selectedUnit.GetAttackCells(movableCells, gameboard);
        for (int i = 0; i < count; i++)
        {
            m_MoveDisplayPool[i].SetActive(true);
            m_MoveDisplayPool[i].transform.position = gameboard.Grid.GetCellCenterWorld(movableCells[i]);
        }
        CleanMoveIndicator(count, m_DisplayedMoveDisplay);

        m_DisplayedMoveDisplay = count;
        DisableAbilityMenu();
        DisableItemMenu();
        Gameboard.Instance.HideUnitInfo();
        

        currentState = State.AttackUnit;
    }
    public void InfoButtonClicked()
    {
        var gameboard = Gameboard.Instance;


        int count = selectedUnit.GetInfoCells(movableCells, gameboard);
        for (int i = 0; i < count; i++)
        {
            m_MoveDisplayPool[i].SetActive(true);
            m_MoveDisplayPool[i].transform.position = gameboard.Grid.GetCellCenterWorld(movableCells[i]);
        }
        CleanMoveIndicator(count, m_DisplayedMoveDisplay);

        m_DisplayedMoveDisplay = count;
        DisableAbilityMenu();
        DisableItemMenu();




        currentState = State.checkInfo;
    }
    public void DisableAbilityMenu()
    {
        abilityScrollRect.SetActive(false);
        confirmAbilityPanel.SetActive(false);
        
        isAbilityMenuActive = false;
      
    }
    private void DisableItemMenu()
    {
        itemScrollRect.SetActive(false);
        confirmItemPanel.SetActive(false);
        isItemMenuActive = false;
    }

    public void AbilityButtonClicked()
    {
        if (!abilityScrollRect.activeSelf)
        {
            abilityScrollRect.SetActive(true);
            DisableItemMenu();
            isAbilityMenuActive = true;
            currentState = State.Wait;
        }
        else
        {
            abilityScrollRect.SetActive(false);
            isAbilityMenuActive = false;
        }
        

    }
    public void ItemButtonClicked()
    {
        if (!itemScrollRect.activeSelf)
        {
            itemScrollRect.SetActive(true);
            DisableAbilityMenu();
            isItemMenuActive = true;
            currentState = State.Wait;
        }
        else
        {
            itemScrollRect.SetActive(false);
            isItemMenuActive = false;
        }
        
        

    }


    public void AbilityButtonFromMenuClicked()
    {
        var gameboard = Gameboard.Instance;
        if(Gameboard.Instance.CurrentTeam == Unit.Team.Player)
        {
            int count = selectedUnit.GetAbilityCells(movableCells, gameboard,player.chosenAbility);
            for (int i = 0; i < count; i++)
            {
                m_MoveDisplayPool[i].SetActive(true);
                m_MoveDisplayPool[i].transform.position = gameboard.Grid.GetCellCenterWorld(movableCells[i]);
            }
            CleanMoveIndicator(count, m_DisplayedMoveDisplay);

            m_DisplayedMoveDisplay = count;
            abilityScrollRect.SetActive(false);

            currentState = State.CastAbility;
            

        }
        

    }

    public Hero GetPlayer()
    {
        return player;
    }

    public GameManager GetGameManager()
    {
        return gameManager;
    }

    

}
