using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private InventoryItemSO _data;
    private InventorySlot _slot;

    public InventoryItemSO Data { get => _data; set => _data = value; }
    public InventorySlot Slot { get => _slot; set => _slot = value; }

    private void Awake()
    {
        _slot = GetComponentInParent<InventorySlot>();
    }
}
