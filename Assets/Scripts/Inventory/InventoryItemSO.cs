using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ReshiErchin/InventoryItem")]
public class InventoryItemSO : BookEntry
{
    [SerializeField] private bool _unlimited = false;
    [SerializeField] private int _maxStackSize = 20;
    public int MaxStackSize { get => _maxStackSize; set => _maxStackSize = Mathf.Clamp(value, 1, 1000); }
    public bool Unlimited { get => _unlimited; set => _unlimited = value; }
}
