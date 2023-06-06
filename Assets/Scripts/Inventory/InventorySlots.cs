using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Inventory slots (transforms) for a defined type of item
/// The inventory will have one set of InventorySlots for each item type it can hold
/// </summary>
public class InventorySlots : MonoBehaviour
{
    [SerializeField] private List<Transform> _slots;

    public List<Transform> Slots { get => _slots; set => _slots = value; }
}
