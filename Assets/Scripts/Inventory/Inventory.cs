using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySlot _potions;
    [SerializeField] private InventorySlot _poultice;
    [SerializeField] private InventorySlot _cream;
    [SerializeField] private InventorySlot _decoction;
    [SerializeField] private InventorySlot _infusion;
    [SerializeField] private InventorySlot _miscellaneous;

    public bool AddItem(InventoryItem item)
    {
        switch (item.Data.Type)
        {
            case InventoryItemType.CREAM:
                _cream.AddItem(item);
                break;
            case InventoryItemType.DECOCTION:
                _decoction.AddItem(item);
                break;
            case InventoryItemType.INFUSION:
                _infusion.AddItem(item);
                break;
            case InventoryItemType.MISCELLANEOUS:
                _miscellaneous.AddItem(item);
                break;
            case InventoryItemType.POTION:
                _potions.AddItem(item);
                break;
            case InventoryItemType.POULTICE:
                _poultice.AddItem(item);
                break;
            default:
                return false;                
        }
        return true;
    }

    public bool RemoveItem(InventoryItem item)
    {
        switch (item.Data.Type)
        {
            case InventoryItemType.CREAM:
                return _cream.RemoveItem(item);
            case InventoryItemType.DECOCTION:
                return _decoction.RemoveItem(item);                
            case InventoryItemType.INFUSION:
                return _infusion.RemoveItem(item);
            case InventoryItemType.MISCELLANEOUS:
                return _miscellaneous.RemoveItem(item);
            case InventoryItemType.POTION:
                return _potions.RemoveItem(item);
            case InventoryItemType.POULTICE:
                return _poultice.RemoveItem(item);
            default:
                return false;
        }
    }
}
