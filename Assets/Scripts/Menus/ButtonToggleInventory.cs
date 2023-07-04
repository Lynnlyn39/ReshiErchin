using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonToggleInventory : Button
{
    [SerializeField] private InventoryManager _inventoryManager;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        _inventoryManager.ToggleInventory();
    }
}
