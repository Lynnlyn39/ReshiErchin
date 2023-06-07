using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "ReshiErchin/InventoryItemData")]
public class InventoryItemData : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private InventoryItemType _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;

    public string Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public InventoryItemType Type { get => _type; set => _type = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
}
