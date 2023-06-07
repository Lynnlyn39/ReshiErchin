using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{    
    [SerializeField] private InventoryItemType _itemType;
    [SerializeField] private Transform _slotTransform;
    private List<InventoryItem> _items;

    public InventorySlot(InventoryItemType type)
    {
        ItemType = type;
        Items = new List<InventoryItem>();
    }

    public InventoryItemType ItemType { get => _itemType; set => _itemType = value; }
    public Transform SlotTransform { get => _slotTransform; set => _slotTransform = value; }
    public List<InventoryItem> Items { get => _items; set => _items = value; }

    public void AddItem(InventoryItem item)
    {
        if (item.Data.Type == ItemType)
        {
            item.transform.SetParent(SlotTransform);
            item.transform.position = new Vector3(Random.value, 0f, Random.value);
            item.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 120f), 0f);
            Items.Add(item);
        } else
        {
            Debug.LogError($"Wrong item type => slot={ItemType} item={item.Data.Type}");
        }
    }

    public bool RemoveItem(InventoryItem item)
    {        
        if (Items.Remove(item))
        {
            GameObject.Destroy(item);
            //item.gameObject.SetActive(false);
            return true;
        } else
        {
            return false;
        }
    }
}
