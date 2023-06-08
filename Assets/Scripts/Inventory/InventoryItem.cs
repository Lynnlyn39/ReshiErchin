using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private InventoryItemSO _data;

    public InventoryItemSO Data { get => _data; set => _data = value; }


    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
