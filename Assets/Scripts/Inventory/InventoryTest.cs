using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField] Inventory _inventory;
    private InventoryItem[] _sampleItems;
    int index = 0;


    // Start is called before the first frame update
    void Start()
    {
        _sampleItems = FindObjectsOfType<InventoryItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem()
    {
        if (index < _sampleItems.Length) 
            _inventory.AddItem(_sampleItems[index++].Data);
    }

    public void ResetMIx()
    {
        _inventory.ResetMix();
    }

}
