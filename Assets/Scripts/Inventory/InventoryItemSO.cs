using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ReshiErchin/InventoryItem")]
public class InventoryItemSO : BookEntry
{
    [SerializeField] private int _maxStackSize;    
    public int MaxStackSize { get => _maxStackSize; set => _maxStackSize = Mathf.Clamp(value, 1, 1000); }
}
