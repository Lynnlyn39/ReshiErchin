using CustomUI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Recipe", menuName = "ReshiErchin/Recipe")]
public class Recipe : InventoryItemSO
{
    [SerializeField] private PreparationType _preparationType;
    [SerializeField] private List<RecipeIngredient> _ingredients = new List<RecipeIngredient>();
    public List<RecipeIngredient> Ingredients { get => _ingredients; set => _ingredients = value; }
}
