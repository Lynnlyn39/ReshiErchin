using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    //private IA_ThirdPersonController _inventoryActionAsset;
    //private InputAction _addToMix;
    //private InputAction _resetMix;

    [SerializeField] private Pestle _pestle;
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private CameraManager _cameraManager;

    [Tooltip("The time it takes to an ingredient to move into the pestle, in seconds")]
    [SerializeField] private float _moveToMixTime = 1f;
    [SerializeField] private LayerMask _inventoryLayerMask;
    private Dictionary<InventoryItemSO, InventorySlot> _inventorySlots;

    public LayerMask InventoryLayerMask { get => _inventoryLayerMask; set => _inventoryLayerMask = value; }

    private void Awake()
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
                        for (int i = 0; i < slot.Stack; i++)
                            AddItem(slot.Data);
                    }
                }
            } else
            {
                Debug.LogWarning($"Inventory slot: {slot.name} is missing an InventoryItemSO reference.");
            }
        }
        //_inventoryActionAsset = new IA_ThirdPersonController();
        _inventoryCanvas.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //_inventoryActionAsset.Inventory.AddToMix.performed += OnAddToMix;
        //_inventoryActionAsset.Inventory.ResetMix.performed += OnResetMix;
        //_inventoryActionAsset.Inventory.ReturnToPlayer.performed += OnReturnToPlayer;
        //_inventoryActionAsset.Inventory.Enable();        
    }

    private void OnDisable()
    {
        //_inventoryActionAsset.Inventory.Disable();
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
        Ray ray = Camera.current.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit, 1000f, InventoryLayerMask))
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
                }               
            } else
            {
                Debug.Log($"Not an InventoryItem");
            }            
        }
    }

    public void ResetMix()
    {
        _pestle.ResetMix();
    }

    IEnumerator MoveToMix(InventoryItem p_item)
    {        
        GameObject item = Instantiate(p_item.Data.Prefab, transform);
        _pestle.AddIngredient(item.GetComponent<InventoryItem>());
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
    }    
}
