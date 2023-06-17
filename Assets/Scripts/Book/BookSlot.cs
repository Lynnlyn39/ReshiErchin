using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CustomUI
{
    public class BookSlot : TabButton
    {
        [SerializeField] private BookEntry _bookEntry;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private GameObject _selected;

        public BookEntry BookEntry { get => _bookEntry; set => _bookEntry = value; }
        public GameObject Selected { get => _selected; set => _selected = value; }

        private void Start()
        {
            if (_bookEntry)
            {
                Icon.sprite = _bookEntry.Icon;
                _name.text = _bookEntry.Name;
            }
            _selected.SetActive(false);
        }
    }
}
