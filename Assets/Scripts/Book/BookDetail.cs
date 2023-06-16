using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookDetail : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    public Image Icon { get => icon; set => icon = value; }
    public TextMeshProUGUI Title { get => title; set => title = value; }
    public TextMeshProUGUI Description { get => description; set => description = value; }
}
