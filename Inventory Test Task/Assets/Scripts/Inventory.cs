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

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject.GetComponent<InventoryItem>();
        var objDrag = other.gameObject.GetComponent<DragingController>();
        if (obj && !objDrag.isDragging && !obj.IsInInventory)
        {
            AddItem(obj, 1);
            obj.IsInInventory = true;
            objDrag.SetFreezePosition(true);
            Debug.LogError($"add {obj.Name}");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject.GetComponent<InventoryItem>();
        var objDrag = other.gameObject.GetComponent<DragingController>();
        if (objDrag.isDragging && obj.IsInInventory)
        {
            obj.IsInInventory = false;
            objDrag.SetFreezePosition(false);
            RemoveItem(obj, 1);
            Debug.LogError($"remove {obj.Name}");
        }
    }

    public void AddItem(InventoryItem item, int quantity)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item.ID == item.ID)
            {
                slot.AddItem(item, quantity);
                Debug.LogError("a");
                return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item, quantity);
                Debug.LogError("b");
                return;
            }
        }
    }

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
}
