using UnityEngine;
using UnityEngine.EventSystems;


   
    public class Tooltip : MonoBehaviour, IPointerClickHandler
    {
       
        [Tooltip("The prefab of the tooltip to spawn.")]
        [SerializeField] GameObject tooltipPrefab = null;

    
    GameObject tooltip;
   
   

    
    public bool CanCreateTooltip()
    {
        var item = GetComponent<ItemHolder>().GetItem();
        if (!item) return false;

        return true;
    }

    public void UpdateTooltip(GameObject tooltip)
    {
        var itemTooltip = tooltip.GetComponent<ItemTooltip>();
        if (!itemTooltip) return;

        var item = GetComponent<ItemHolder>().GetItem();

        itemTooltip.Setup(item);
    }

  

    private void OnDestroy()
        {
            ClearTooltip();
        }

        private void OnDisable()
        {
            ClearTooltip();
        }


        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            var parentCanvas = GetComponentInParent<Canvas>();

            if (tooltip && !CanCreateTooltip())
            {
                ClearTooltip();
            
            }

            if (!tooltip && CanCreateTooltip())
            {
            var numberOfTooltips = FindObjectsOfType<ItemTooltip>();

            if (numberOfTooltips.Length > 0)
            {
                foreach(ItemTooltip it in numberOfTooltips)
                {
                    Destroy(it.gameObject);
                }
            }
            tooltip = Instantiate(tooltipPrefab, parentCanvas.transform);
            tooltip.GetComponent<ItemTooltip>().SetInventorySlotUI(GetComponent<InventorySlotUI>());
            } 
        if (tooltip)
        {
            UpdateTooltip(tooltip);

        }

           
        }

        



        private void ClearTooltip()
        {
            if (tooltip)
            {
            Destroy(tooltip.gameObject);
        }
        }
    }
