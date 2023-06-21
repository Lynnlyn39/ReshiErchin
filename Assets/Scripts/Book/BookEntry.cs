using UnityEngine;
using UnityEngine.UI;

public abstract class BookEntry : ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected Sprite[] info;
    [SerializeField] protected GameObject _prefab;
    [SerializeField] private bool isKnown = false;

    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
    public Sprite[] Info { get => info; set => info = value; }
    public bool IsKnown { get => isKnown; set => isKnown = value; }
}
