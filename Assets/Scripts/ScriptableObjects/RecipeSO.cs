using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Recipe", menuName = "ReshiErchin/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Image _image;
    //[SerializeField] private GameObject _prefab;
    [SerializeField] private List<RecipeIngredient> _ingredients = new List<RecipeIngredient>();

    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public Image Image { get => _image; set => _image = value; }
    //public GameObject Prefab { get => _prefab; set => _prefab = value; }
    public List<RecipeIngredient> Ingredients { get => _ingredients; set => _ingredients = value; }
}
