using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    public int maxSlots = 20;

    public void AddItem(InventoryItem item, int quantity)
    {
        foreach (var slot in slots)
        {
            if (slot.item != null && slot.item.ID == item.ID)
            {
                slot.AddItem(item, quantity);

                return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.AddItem(item, quantity);

                return;
            }
        }
    }

    public void RemoveItem(InventoryItem item, int quantity)
    {
        foreach (var slot in slots)
        {
            if (slot.item != null && slot.item.ID == item.ID)
            {
                if (slot.quantity >= quantity)
                {
                    slot.RemoveItem(quantity);
                    return;
                }
                else
                {
                    quantity -= slot.quantity;
                    slot.ClearSlot();
                }
            }
        }
    }
}
