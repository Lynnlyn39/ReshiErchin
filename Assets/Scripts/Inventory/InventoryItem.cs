using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InventoryItemSO _data;
    private InventorySlot _slot;

    public InventoryItemSO Data { get => _data; set => _data = value; }
    public InventorySlot Slot { get => _slot; set => _slot = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"{_data.Name} clicked");
    }

    private void Awake()
    {
        _slot = GetComponentInParent<InventorySlot>();
    }
}
