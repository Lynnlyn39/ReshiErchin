using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookDetail : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image[] _info;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;

    public Image Icon { get => _icon; set => _icon = value; }
    public TextMeshProUGUI Title { get => _title; set => _title = value; }
    public TextMeshProUGUI Description { get => _description; set => _description = value; }
    public Image[] Info { get => _info; set => _info = value; }
}
