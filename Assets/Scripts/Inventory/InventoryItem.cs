using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private InventoryItemData _data;

    public InventoryItemData Data { get => _data; set => _data = value; }
}
