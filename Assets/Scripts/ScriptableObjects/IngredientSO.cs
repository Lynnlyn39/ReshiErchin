using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ReshiErchin/Ingredient")]
public class IngredientSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Texture _texture;
    //[SerializeField] private GameObject _prefab;

    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public Texture Image { get => _texture; set => _texture = value; }
    //public GameObject Prefab { get => _prefab; set => _prefab = value; }
}
