using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    public bool IsEmpty => item == null;

    [Header("Item")]
    public InventoryItem item;
    public int quantity;
    
    [Header("UI slot")]
    [SerializeField] private UiInventorySlot slot;

    private UnityEvent AddnRemoveItemEvent;

    private void Start()
    {
        if (AddnRemoveItemEvent == null)
            AddnRemoveItemEvent = new UnityEvent();

        AddnRemoveItemEvent.AddListener(OnAddnRemoveItem);
    }

    /// <summary>
    /// Adds item to slot and parent them
    /// </summary>
    /// <param name="newItem">Item</param>
    /// <param name="amount">Amount of items</param>
    public void AddItem(InventoryItem newItem, int amount)
    {
        item = newItem;
        quantity += amount;

        if (quantity == 1)
        {
            var newPosition = gameObject.transform.position;
            newItem.gameObject.transform.position = newPosition;
            var newRotation = gameObject.transform.rotation;
            newItem.gameObject.transform.rotation = newRotation;
            newItem.transform.SetParent(gameObject.transform, true);
        }

        AddnRemoveItemEvent?.Invoke();
        slot.SetItemInfo(item, quantity);
    }

    /// <summary>
    /// Removes items from slot
    /// </summary>
    /// <param name="amount">Amount of removing items</param>
    public void RemoveItem(int amount)
    {
        quantity -= amount;
        slot.UpdItemCount(quantity);

        if (quantity <= 0)
        {
            ClearSlot();
        }
        AddnRemoveItemEvent?.Invoke();
    }

    /// <summary>
    /// Clears slot from any item
    /// </summary>
    public void ClearSlot()
    {
        item = null;
        quantity = 0;

        slot.RemoveItemInfo();
    }

    private void OnAddnRemoveItem()
    {
        StartCoroutine(ServerRequestSender.SendRequest(item));
    }
}
