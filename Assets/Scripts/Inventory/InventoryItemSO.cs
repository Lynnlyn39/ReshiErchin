using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "ReshiErchin/InventoryItemData")]
public class InventoryItemSO : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _maxStackSize;    
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;

    public string Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
    public int MaxStackSize { get => _maxStackSize; set => _maxStackSize = Mathf.Clamp(value, 1, 1000); }
}
