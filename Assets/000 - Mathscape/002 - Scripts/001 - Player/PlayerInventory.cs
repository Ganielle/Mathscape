using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<int> KeyItems
    {
        get => keyItems;
    }

    //  =====================

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private List<int> keyItems;

    public void RemoveItemOnInventoryKey(int index)
    {
        if (!KeyItems.Contains(index)) return;

        keyItems.Remove(index);
    }

    public void RemoveAllItemsOnInventoryKey()
    {
        keyItems.Clear();
    }

    public void AddKeyItem(int index)
    {
        if (KeyItems.Contains(index)) return;

        keyItems.Add(index);
    }
}
