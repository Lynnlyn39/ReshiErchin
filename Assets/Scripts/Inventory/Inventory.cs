using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<InventoryItemSO, InventorySlot> _inventorySlots;

    private void Awake()
    {
        _inventorySlots = new Dictionary<InventoryItemSO, InventorySlot>();
        InventorySlot[] slots = FindObjectsOfType<InventorySlot>();

        foreach (InventorySlot slot in slots)
        {
            if (_inventorySlots.TryAdd(slot.ItemData, slot))
            {
                Debug.Log($"{slot.ItemData.Name} SLOT added to Inventory.");
            }
        }
    }

    public bool AddItem(InventoryItem item)
    {
        InventorySlot slot;        
        bool result = _inventorySlots.TryGetValue(item.Data, out slot);

        if (result)
        {
            return slot.AddItem();            
        }
        return false;
    }

    public bool RemoveItem(InventoryItem item)
    {
        InventorySlot slot;
        bool result = _inventorySlots.TryGetValue(item.Data, out slot);
        if (result)
        {
            return slot.RemoveItem();
        }
        return false;

    }
}
