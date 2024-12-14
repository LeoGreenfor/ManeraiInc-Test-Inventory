using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UiInventorySlot : MonoBehaviour
{
    [SerializeField] private TMP_Text naming;
    [SerializeField] private TMP_Text quantity;
    [SerializeField] private TMP_Text weight;
    [SerializeField] private Image icon;

    [SerializeField] private Sprite[] typeOfItemsIcons;

    public void SetItemInfo(InventoryItem item, int count)
    {
        naming.text = item.Name;
        quantity.text = count.ToString();
        weight.text = "Weight: " + item.Weight + " kg";

        switch(item.Type)
        {
            case ItemType.Consumable:
                icon.sprite = typeOfItemsIcons[0]; 
                break;
            case ItemType.Weapon:
                icon.sprite = typeOfItemsIcons[1];
                break;
            case ItemType.Miscellaneous:
                icon.sprite = typeOfItemsIcons[2];
                break;
            default: 
                break;
        }

        gameObject.SetActive(true);
    }

    public void UpdItemCount(int count)
    {
        quantity.text = count.ToString();
    }

    public void RemoveItemInfo()
    {
        naming.text = "";
        weight.text = "";
        quantity.text = "";
        icon.sprite = null;

        gameObject.SetActive(false);
    }
}
