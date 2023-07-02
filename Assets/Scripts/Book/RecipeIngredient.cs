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

    public bool Match(RecipeIngredient recipeIngredient)
    {
        if (!recipeIngredient.Ingredient.Equals(Ingredient))
            return false;
        if (!recipeIngredient.Quantity.Equals(Quantity))
            return false;

        return true;
    }

    public bool Match(List<InventoryItem> items)
    {
        // match ingredient occurrences
        int count = 0;
        foreach(InventoryItem item in items)
        {
            IngredientSO ing = (IngredientSO) item.Data;
            if (ing.Name.Equals(Ingredient.Name))
                count++;            
        }
        if (count != 0 && count == Quantity)
            return true;
        else
            return false;
    }
}
