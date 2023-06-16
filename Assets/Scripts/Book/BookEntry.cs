using UnityEngine;
using UnityEngine.UI;

public abstract class BookEntry : ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected Image _icon;
    [SerializeField] private Image[] info;
    [SerializeField] protected GameObject _prefab;

    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public Image Icon { get => _icon; set => _icon = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
    protected Image[] Info { get => info; set => info = value; }
}
