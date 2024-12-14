using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem : MonoBehaviour
{
    public float Weight;
    public string Name;
    public int ID;
    public ItemType Type;

    public enum ItemType
    {
        Consumable,
        Weapon,
        Miscellaneous
    }

    public bool IsInInventory;
}
