using TMPro;
using UnityEngine;

namespace CustomUI
{
    public class BookSlot : TabButton
    {
        [SerializeField] private BookEntry _bookEntry;
        [SerializeField] private TextMeshProUGUI _name;

        public BookEntry BookEntry { get => _bookEntry; set => _bookEntry = value; }

        private void Start()
        {
            Icon = _bookEntry.Icon;
            _name.text = _bookEntry.Name;
        }
    }
}
