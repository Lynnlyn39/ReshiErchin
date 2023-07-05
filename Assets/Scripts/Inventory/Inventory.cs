using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    [SerializeField] private Pestle _pestle;
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private CameraManager _cameraManager;

    [Tooltip("The time it takes to an ingredient to move into the pestle, in seconds")]
    [SerializeField] private float _moveToMixTime = 0.5f;
    [SerializeField] private LayerMask _inventoryLayerMask;

    [SerializeField] private AudioClip _addIngredientSfx;    
    [SerializeField] private AudioSource _audioSource;   
    
    private Dictionary<InventoryItemSO, InventorySlot> _inventorySlots;

    public LayerMask InventoryLayerMask { get => _inventoryLayerMask; set => _inventoryLayerMask = value; }

    private void Start()
    {
        _inventorySlots = new Dictionary<InventoryItemSO, InventorySlot>();
        InventorySlot[] slots = FindObjectsOfType<InventorySlot>();

        foreach (InventorySlot slot in slots)
        {
            if (slot.Data)
            {
                if (_inventorySlots.TryAdd(slot.Data, slot))
                {
                    //Debug.Log($"{slot.Data.Name} SLOT added to Inventory.");
                    if (slot.Data.Unlimited)
                    {
                        AddItem(slot.Data);
                    } else
                    {
                        for (int i = 0; i < slot.InitialStack; i++)
                            AddItem(slot.Data);
                    }
                }
            } else
            {
                Debug.LogWarning($"Inventory slot: {slot.name} is missing an InventoryItemSO reference.");
            }
        }
        _inventoryCanvas.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _inventoryCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// Add one item to the inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Remove one item from the inventory. Used when it is moved to the pestle mix
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Move one ingredient from the inventory to the pestle
    /// </summary>
    public void AddToMix()
    {
        RaycastHit hit;

        Vector2 mousePosition = Mouse.current.position.ReadValue();        
        Ray ray = _cameraManager.InventoryCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f, InventoryLayerMask))
        {
            InventoryItem item = hit.transform.GetComponent<InventoryItem>();
            if (item && !_pestle.Ingredients.Contains(item)) 
            {
                if (_pestle.IsFull) {
                    Debug.Log("Cannot add more ingredients to the mix, the pestle is full...");
                } else
                {
                    item.Slot.RemoveItem();

                    if (_audioSource && _addIngredientSfx)
                        _audioSource.PlayOneShot(_addIngredientSfx);

                    StartCoroutine(MoveToMix(item));                    
                }               
            } else
            {
                Debug.Log($"{item.Data.Name} is not an ingredient");
            }            
        }
    }

    public void ResetMix()
    {
        _pestle.ResetMix();
    }

    IEnumerator MoveToMix(InventoryItem p_item)
    {   
        if (!p_item.Data.Prefab)
        {
            Debug.Log($"Prefab is missing in the InventoryItemSO {p_item.Data.Name}");            
        }
        GameObject item = Instantiate(p_item.Data.Prefab, p_item.transform.parent);        
        _pestle.AddIngredient(item.GetComponent<InventoryItem>());
        item.transform.position = p_item.transform.position;
        float time = 0;
        Vector3 startPosition = item.transform.position;
        Vector3 endPosition = _pestle.DropPoint;
        while (time < _moveToMixTime)
        {
            item.transform.position = Vector3.Lerp(startPosition, endPosition, time / _moveToMixTime);
            time += Time.deltaTime;
            yield return null;
        }
        item.transform.position = endPosition;
    }
}
