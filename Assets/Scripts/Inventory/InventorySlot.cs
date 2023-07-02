using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class InventorySlot : MonoBehaviour
{    
    [SerializeField] private InventoryItemSO _data;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _stackText;
    [SerializeField] private Canvas _canvas;
    private GameObject _instancedItem;


    [SerializeField] private int _stack = 0;

    public InventoryItemSO Data { get => _data; set => _data = value; }
    public int Stack
    {
        get => _stack;
        set
        {
            _stack = value;
            _stackText.text = $"({_stack})";
            UpdateSlot();
        }
    }

    private void Start()
    {
        Stack = 0;
        UpdateSlot();
    }

    public bool AddItem()
    {
        if (Data.Unlimited)
        {
            if (!_instancedItem || !_instancedItem.activeSelf)
                ShowSlot();

            return true;
        }

        bool result = false;
        if (Stack < Data.MaxStackSize)
        {
            Stack++;
            if (Stack == 1)
            {                
                Data.IsKnown = true;
                ShowSlot();
            }
            result = true;
        } 
        else
        {
            if (Data.MaxStackSize > 0)
                Debug.Log("Inventory stack is full");
            else
                Debug.LogWarning($"{Data.Name} max stack size is 0, fix it in the SO.");
        }
        return result;
    }

    public bool RemoveItem()
    {
        if (Data.Unlimited)
        {
            return true;
        }
            
        
        if (Stack == 0)
            return false;

        Stack--;

        if (Stack == 0)
        {
            HideSlot();
        }
        return true;
    }

    private void UpdateSlot()
    {
        if (Data)
        {
            _nameText.text = Data.Name;
            if (!_instancedItem && _data.Prefab)
            {
                _instancedItem = Instantiate(_data.Prefab, transform);
            } else
            {
                Debug.LogWarning($"No prefab set in ItemData (InventoryItemSO) {_data.Name}");
            }
        } else
        {
            Debug.LogWarning($"No ItemData set in slot {name}");
        }

        if (Stack == 0 || Data == null)
        {
            HideSlot();
        } else
        {
            ShowSlot();
        }
    }

    private void HideSlot()
    {
        if (_instancedItem)
        {
            _instancedItem.SetActive(false);
        }
        if (_canvas)
        {
            _canvas.gameObject.SetActive(false);
        }        
    }

    private void ShowSlot()
    {
        if (_instancedItem)
        {
            _instancedItem.SetActive(true);
        }
        if (_canvas)
        {
            _canvas.gameObject.SetActive(true);
        }
    }
}
