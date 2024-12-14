using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    public InventoryItem item;
    public int quantity;

    public bool IsEmpty => item == null;

    public void AddItem(InventoryItem newItem, int amount)
    {
        item = newItem;
        quantity += amount;

        if (quantity == 1)
        {
            var newPosition = gameObject.transform.position;
            newItem.gameObject.transform.position = newPosition;
        }
    }

    public void RemoveItem(int amount)
    {
        quantity -= amount;
        if (quantity <= 0)
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        item = null;
        quantity = 0;
    }
}
