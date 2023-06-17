using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CustomUI
{
    public class BookSlot : TabButton
    {
        private BookSlotGroup _slotGroup;
        [SerializeField] private BookEntry _bookEntry;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private GameObject _selected;
        private bool _isKnown = false;

        public BookEntry BookEntry { get => _bookEntry; set => _bookEntry = value; }
        public GameObject Selected { get => _selected; set => _selected = value; }
        public bool IsKnown { get => _isKnown; set => _isKnown = value; }

        private void Start()
        {
            _slotGroup = (BookSlotGroup)_tabGroup;

            if (_bookEntry)
            {
                // Remove this when the book is working
                IsKnown = true;

                if (IsKnown)
                {
                    if (_bookEntry.Icon && Icon)
                        Icon.sprite = _bookEntry.Icon;
                    _name.text = _bookEntry.Name;
                } else
                {
                    // Set defaults
                }
            } 
            _selected.SetActive(false);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            _slotGroup.OnTabSelected(this);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            _slotGroup.OnTabEnter(this);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            _slotGroup.OnTabExit(this);
        }
    }
}
