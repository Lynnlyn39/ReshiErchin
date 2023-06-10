using TMPro;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{    
    [SerializeField] private InventoryItemSO _itemData;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _stackText;

    private GameObject _instancedItem;

    private int _stack = 0;

    public InventoryItemSO ItemData { get => _itemData; set => _itemData = value; }
    public int Stack
    {
        get => _stack;
        set
        {
            _stack = value;
            _stackText.text = $"({_stack})";
        }
    }

    private void Start()
    {
        _nameText.text = ItemData.Name;
        _nameText.gameObject.SetActive(false);
        _stackText.gameObject.SetActive(false);
    }

    public bool AddItem()
    {
        bool result = false;
        if (Stack < ItemData.MaxStackSize)
        {
            if (Stack == 0)
            {
                _instancedItem = Instantiate(_itemData.Prefab, transform);
                _nameText.gameObject.SetActive(true);
                _stackText.gameObject.SetActive(true);
            }
            Stack++;    
        } 
        else
        {
            Debug.Log("Inventory stack is full");
        }
        return result;
    }

    public bool RemoveItem()
    {
        if (Stack == 0)
            return false;

        Stack--;

        if (Stack == 0)
        {
            Destroy(_instancedItem);
        }
        return true;
    }
}
