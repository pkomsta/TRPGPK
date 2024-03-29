using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


   
    [RequireComponent(typeof(Image))]
    public class InventoryItemIcon : MonoBehaviour
    {
       
        [SerializeField] GameObject textContainer = null;
        [SerializeField] TextMeshProUGUI itemNumber = null;

    Image iconImage;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
    }


    public void SetItem(InventoryItem item)
        {
        
            SetItem(item, 0);
        }

        public void SetItem(InventoryItem item, int number)
        {
        
       
        
        if (iconImage != null) {
            if (item == null)
            {

                iconImage.enabled = false;
                
            }
            else
            {
                iconImage.enabled = true;
                iconImage.sprite = item.GetIcon();
                
            }
        }
            
       
        if (itemNumber)
            {
                if (number <= 1)
                {
                    textContainer.SetActive(false);
                }
                else
                {
                    textContainer.SetActive(true);
                    itemNumber.text = number.ToString();
                }
            }
        
    }
    }
