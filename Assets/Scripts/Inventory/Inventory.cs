using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    private IA_ThirdPersonController _inventoryActionAsset;
    //private InputAction _addToMix;
    //private InputAction _resetMix;

    [SerializeField] private Pestle _pestle;

    [Tooltip("The time it takes to an ingredient to move into the pestle, in seconds")]
    [SerializeField] private float _moveToMixTime = 1f;

    private Dictionary<InventoryItemSO, InventorySlot> _inventorySlots;

    private void Awake()
    {
        _inventorySlots = new Dictionary<InventoryItemSO, InventorySlot>();
        InventorySlot[] slots = FindObjectsOfType<InventorySlot>();

        foreach (InventorySlot slot in slots)
        {
            if (_inventorySlots.TryAdd(slot.ItemData, slot))
            {
                Debug.Log($"{slot.ItemData.Name} SLOT added to Inventory.");
            }
        }
        _inventoryActionAsset = new IA_ThirdPersonController();
    }

    private void OnEnable()
    {
        _inventoryActionAsset.Inventory.AddToMix.performed += OnAddToMix;
        _inventoryActionAsset.Inventory.ResetMix.performed += OnResetMix;
        _inventoryActionAsset.Inventory.Enable();
    }

    private void OnDisable()
    {
        _inventoryActionAsset.Inventory.Disable();
    }

    public bool AddItem(InventoryItemSO item)
    {
        InventorySlot slot;        
        bool result = _inventorySlots.TryGetValue(item, out slot);

        if (result)
        {
            return slot.AddItem();            
        }
        return false;
    }

    public bool RemoveItem(InventoryItem item)
    {
        InventorySlot slot;
        bool result = _inventorySlots.TryGetValue(item.Data, out slot);
        if (result)
        {
            return slot.RemoveItem();
        }
        return false;
    }

    private void OnAddToMix(InputAction.CallbackContext context)
    {
        AddToMix();
    }

    public void AddToMix()
    {
        RaycastHit hit;

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Raycast hit object " + hit.transform.name + " at the position of " + hit.transform.position);
            InventoryItem item = hit.transform.GetComponent<InventoryItem>();
            if (item && !_pestle.Ingredients.Contains(item)) 
            {
                if (_pestle.IsFull) {
                    Debug.Log("Cannot add more ingredients to the mix, the pestle is full...");
                } else
                {
                    item.Slot.RemoveItem();
                    StartCoroutine(MoveToMix(item));
                    Debug.Log($"AddToMix");
                }               
            } else
            {
                Debug.Log($"Not an InventoryItem");
            }
            
        }

    }

    private void OnResetMix(InputAction.CallbackContext context)
    {
        ResetMix();
    }

    public void ResetMix()
    {
        _pestle.ResetMix();
    }

    IEnumerator MoveToMix(InventoryItem p_item)
    {
        GameObject item = Instantiate(p_item.Data.Prefab, transform);
        item.transform.position = p_item.transform.position;
        float time = 0;
        Vector3 startPosition = item.transform.position;        
        Vector3 endPosition = _pestle.DropPoint + new Vector3(Random.value, 0f, Random.value);
        while (time < _moveToMixTime)
        {
            item.transform.position = Vector3.Lerp(startPosition, endPosition, time / _moveToMixTime);
            time += Time.deltaTime;
            yield return null;
        }
        item.transform.position = endPosition;
        _pestle.AddIngredient(item.GetComponent<InventoryItem>());
    }    

}
