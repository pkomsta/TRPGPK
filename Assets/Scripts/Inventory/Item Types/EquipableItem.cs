using UnityEngine;


   
    [CreateAssetMenu(menuName = ("InventorySystem/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
       
        [Tooltip("Where are we allowed to put this item.")]
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.Weapon;

    [Header("Statisstics")]
    [SerializeField] int attack = 0;
    [SerializeField] int magicAttack = 0;
    [SerializeField] int vitality = 0;
    [SerializeField] int wisdom = 0;
    [SerializeField] int defense = 0;
    [SerializeField] int magickDefense = 0;
    [SerializeField] int speed = 0;

    public int Attack { get => attack; set => attack = value; }
    public int MagicAttack { get => magicAttack; set => magicAttack = value; }
    public int Vitality { get => vitality; set => vitality = value; }
    public int Wisdom { get => wisdom; set => wisdom = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Speed { get => speed; set => speed = value; }
    public int MagickDefense { get => magickDefense; set => magickDefense = value; }

    public EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }
    
    
    }
