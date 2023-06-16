using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CustomUI
{
    public class BookSlotGroup : TabGroup
    {
        [SerializeField] protected BookDetail _detail;

        public void OnTabSelected(BookSlot tab)
        {
            _selectedTab = tab;
            ResetTabs();
            tab.Icon = _tabActive;
            _detail.Icon = tab.BookEntry.Icon;
            _detail.Title.text = tab.BookEntry.Name;
            _detail.Description.text = tab.BookEntry.Description;
            tab.Selected.SetActive(true);
        }

        public override void ResetTabs()
        {
            foreach (BookSlot tab in _tabs)
            {
                if (_selectedTab != null && tab == _selectedTab) { continue; }
                tab.Icon = _tabIdle;
                _detail.Icon = null;
                _detail.Title.text = "";
                _detail.Description.text = "";
                tab.Selected.SetActive(false);
            }
        }
    }
}