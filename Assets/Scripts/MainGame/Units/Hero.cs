using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hero : Unit
{
    UserControl userControl;

    public enum HeroClass
    {
        Warrior,
        Mage,
        Archer
    }

    [Header("Stat Growth")]
    public int attackGrowth = 1;
    public int magickAttackGrowth = 1;
    public int vitalityGrowth = 1;
    public int wisdomGrowth = 1;
    [Header("Skill Tree")]
    public GameObject skillTreeObject;
    protected GameManager gameManager;

    public HeroClass heroClass;


    public void MoveUnit(Unit selectedUnit, Vector3Int[] movableCells)
    {
        
        if (Gameboard.Instance.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
            out Vector3Int clickedCell))
        {
            
            if (movableCells.Contains(clickedCell))
            {
                var unit = Gameboard.Instance.GetUnit(clickedCell);

                
                if (unit == null && speed > 0)
                {
                    MoveCommand cmd = new MoveCommand(selectedUnit.CurrentCell, clickedCell);
                    CommandManager.Instance.AddCommand(cmd);
                    userControl.DeselectCells();
                }


            }

        }
    }

    public void AttackEnemyUnit(Unit selectedUnit)
    {
        if (Gameboard.Instance.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
               out Vector3Int clickedCell))
        {
            var unit = Gameboard.Instance.GetUnit(clickedCell);

            if (unit != null && unit != selectedUnit && Vector3.Distance(selectedUnit.CurrentCell, clickedCell) <= selectedUnit.range + 0.5)
            {
                AttackCommand cmd = new AttackCommand(selectedUnit.CurrentCell, clickedCell);
                CommandManager.Instance.AddCommand(cmd);
                userControl.DeselectCells();
                if (unit.isDead)
                {

                    IfDead(clickedCell, unit);
                }
            }

        }


    }

    public void UseAbilityOnUnit(Unit selectedUnit)
    {
        if (Gameboard.Instance.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
               out Vector3Int clickedCell))
        {
            var unit = Gameboard.Instance.GetUnit(clickedCell);

            if (unit != null && Vector3.Distance(selectedUnit.CurrentCell, clickedCell) <= selectedUnit.chosenAbility.abilityRange + 0.5)
            {
                if (unit != selectedUnit)
                {
                    
                    CastAbilityCommand cmd = new CastAbilityCommand(selectedUnit.CurrentCell, clickedCell);
                    CommandManager.Instance.AddCommand(cmd);
                    userControl.DeselectCells();
                }

                IfDead(clickedCell, unit);
            }

        }
        
    }

    public void UseSelfAbility()
    {
        if ((chosenAbility.abilityType == Abilities.AbilityType.self_buff || chosenAbility.abilityType == Abilities.AbilityType.self_heal || chosenAbility.abilityType == Abilities.AbilityType.self_resource))
        {
            CastAbilityCommand cmdself = new CastAbilityCommand(CurrentCell, Gameboard.Instance.GetClosestCell(transform.position));
            CommandManager.Instance.AddCommand(cmdself);
            userControl.DeselectCells();
        }
    }
    public void CheckUnitInfo(Unit selectedUnit)
    {
        if (Gameboard.Instance.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
              out Vector3Int clickedCell))
        {
            var unit = Gameboard.Instance.GetUnit(clickedCell);
            if(unit != null)
            {

                CheckInfoCommand info = new CheckInfoCommand(clickedCell);
                CommandManager.Instance.AddCommand(info);
                userControl.DeselectCells();

            }


        }
        }

    private static void IfDead(Vector3Int clickedCell, Unit unit)
    {
        
    }


    public void SetUserControl(UserControl user)
    {
        userControl = user;
    }
    
    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
