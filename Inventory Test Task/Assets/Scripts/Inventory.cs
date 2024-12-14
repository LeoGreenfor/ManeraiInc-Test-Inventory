using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] private int maxSlots = 20;
    [SerializeField] private Canvas inventoryCanvas;

    /// <summary>
    /// Adding item to the inventory on touching bag
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject.GetComponent<InventoryItem>();
        var objDrag = other.gameObject.GetComponent<DragingController>();
        if (obj && !objDrag.isDragging && !obj.IsInInventory)
        {
            AddItem(obj, 1);
            obj.IsInInventory = true;
            objDrag.SetFreezePosition(true);
        }
    }

    /// <summary>
    /// Removing item from inventory on exiting bag
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject.GetComponent<InventoryItem>();
        var objDrag = other.gameObject.GetComponent<DragingController>();
        if (objDrag.isDragging && obj.IsInInventory)
        {
            obj.IsInInventory = false;
            objDrag.SetFreezePosition(false);
            RemoveItem(obj, 1);
        }
    }

    /// <summary>
    /// Add item to inventory. If there is same item - stasks them. If not - puts in an empty slot
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <param name="quantity">Count of items to add</param>
    public void AddItem(InventoryItem item, int quantity)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item.ID == item.ID)
            {
                slot.AddItem(item, quantity);
                return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item, quantity);
                return;
            }
        }
    }

    /// <summary>
    /// Removes item from inventory
    /// </summary>
    /// <param name="item">Item to remove</param>
    /// <param name="quantity">Count of items to remove</param>
    public void RemoveItem(InventoryItem item, int quantity)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item.ID == item.ID)
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

    // Set inventory canvas to active
    private void OnMouseDown()
    {
        inventoryCanvas.gameObject.SetActive(true);
    }

    // Set inventory canvas to unactive
    private void OnMouseExit()
    {
        inventoryCanvas.gameObject.SetActive(false);
    }
}
