using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeIngredient
{
    [SerializeField] private IngredientSO _ingredient;
    [SerializeField] private int _quantity;

    public IngredientSO Ingredient { get => _ingredient; set => _ingredient = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }
}
