using System.Collections.Generic;
using UnityEngine;

public class Pestle : MonoBehaviour
{
    [SerializeField] private int _maxIngredients;
    private List<InventoryItem> _ingredients;
    private Inventory _inventory;
    [SerializeField] private Transform _dropPoint;
    [SerializeField] private Transform[] _dropPoints;
    private int _nextDropPointIndex = 0;

    public Vector3 DropPoint => _dropPoints[_nextDropPointIndex++ % _dropPoints.Length].position;

    public List<InventoryItem> Ingredients => _ingredients;
    public int MaxIngredients => _maxIngredients;

    public bool IsFull => _ingredients.Count == MaxIngredients;

    private void Awake()
    {
        _ingredients = new List<InventoryItem>();
        _inventory = FindObjectOfType<Inventory>();
    }

    public void AddIngredient(InventoryItem ingredient)
    {
        if (Ingredients.Count < MaxIngredients)
        {
            Ingredients.Add(ingredient);
        }
    }

    public void ResetMix()
    {
        foreach (InventoryItem item in Ingredients)
        {
            _inventory.AddItem(item.Data);
            item.gameObject.SetActive(false);
            Destroy(item, 1f);
        }
        _ingredients.Clear();
    }
}
