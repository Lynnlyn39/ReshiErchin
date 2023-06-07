using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{    
    [SerializeField] private InventoryItemType _type;
    [SerializeField] private Transform[] _transforms;
    private List<InventoryItem> _items;

    public void Awake()
    {
        Items = new List<InventoryItem>();
    }

    public InventoryItemType Type { get => _type; set => _type = value; }
    public List<InventoryItem> Items { get => _items; set => _items = value; }

    public void AddItem(InventoryItem item)
    {
        if (item.Data.Type == Type)
        {
            if (Items.Count < _transforms.Length)
            {

                item.transform.SetParent(transform);
                item.transform.position = _transforms[Items.Count].position;
                item.transform.rotation = _transforms[Items.Count].rotation;
                //item.transform.localPosition = new Vector3(Random.value, 0.5f, Random.value);
                //item.transform.localRotation = Quaternion.Euler(0f, Random.Range(0f, 120f), 0f);
                Items.Add(item);
            } else
            {
                Debug.Log("Inventory is full");
            }
        } else
        {
            Debug.LogError($"Wrong item type => slot={Type} item={item.Data.Type}");
        }
    }

    public bool RemoveItem(InventoryItem item)
    {        
        if (Items.Remove(item))
        {
            GameObject.Destroy(item);
            //item.gameObject.SetActive(false);
            return true;
        } else
        {
            return false;
        }
    }
}
