using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CustomUI
{
    public class BookSlotGroup : TabGroup
    {
        [SerializeField] protected Image _detailIcon;
        [SerializeField] protected TextMeshProUGUI _detailTitle;
        [SerializeField] protected TextMeshProUGUI _detailDescription;

        public void OnTabSelected(BookSlot tab)
        {
            _selectedTab = tab;
            ResetTabs();
            tab.Icon.sprite = _tabActive;
            _detailIcon = tab.BookEntry.Icon;
            _detailTitle.text = tab.BookEntry.Name;
            _detailDescription.text = tab.BookEntry.Description;
        }

        public override void ResetTabs()
        {
            foreach (TabButton tab in _tabs)
            {
                if (_selectedTab != null && tab == _selectedTab) { continue; }
                tab.Icon.sprite = _tabIdle;
                _detailIcon = null;
                _detailTitle.text = "";
                _detailDescription.text = "";

            }
        }
    }
}