using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CustomUI
{
    public class BookSlotGroup : TabGroup
    {
        [SerializeField] protected BookDetail _detail;
        [Tooltip("Image used by default and when the item is not known")]
        [SerializeField] protected Image _defaultIcon;

        public void OnTabSelected(BookSlot tab)        
        {
            if (tab.IsKnown)
            {
                _selectedTab = tab;
                ResetTabs();
                tab.Icon = _tabActive;
                if (tab.BookEntry)
                {
                    if (_detail.Icon)
                        _detail.Icon.sprite = tab.BookEntry.Icon;

                    _detail.Title.text = tab.BookEntry.Name;
                    _detail.Description.text = tab.BookEntry.Description;
                    
                    for (int i=0; i < tab.BookEntry.Info.Length; i++)
                    {
                        _detail.Info[i].sprite = tab.BookEntry.Info[i];
                    }

                } else
                {
                    Debug.LogWarning($"{tab.gameObject.name} is missing a BookEntry (SO) reference.");
                }
                tab.Selected.SetActive(true);
            }
        }

        public override void ResetTabs()
        {
            foreach (BookSlot tab in _tabs)
            {
                if (_selectedTab != null && tab == _selectedTab) { continue; }
                //tab.Icon = _tabIdle;
                _detail.Icon = null;
                _detail.Title.text = "";
                _detail.Description.text = "";
                tab.Selected.SetActive(false);
            }
        }

        public void OnTabExit(BookSlot tab)
        {

        }
    }
}