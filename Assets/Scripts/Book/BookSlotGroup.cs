using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CustomUI
{
    public class BookSlotGroup : TabGroup
    {
        [SerializeField] protected BookDetail _detail;
        [Tooltip("Image used by default and when the item is not known")]
        [SerializeField] protected Sprite _defaultIcon;

        public void OnTabSelected(BookSlot tab)        
        {
            if (tab.IsKnown)
            {
                _selectedTab = tab;
                ResetTabs();
                tab.Icon = _tabActive;
                if (tab.BookEntry)
                {
                    if (tab.BookEntry.Icon && _detail.Icon)
                    {
                        _detail.Icon.sprite = tab.BookEntry.Icon;
                        _detail.Icon.color = new Color(_detail.Icon.color.r, _detail.Icon.color.g, _detail.Icon.color.b, 1f);
                    } else
                    {
                        Debug.LogWarning($"{tab.BookEntry.Name} has not Icon defined. Fix it in the SO");
                    }

                    _detail.Title.text = tab.BookEntry.Name;
                    _detail.Description.text = tab.BookEntry.Description;
                    
                    for (int i=0; i < tab.BookEntry.Info.Length; i++)
                    {
                        _detail.Info[i].sprite = tab.BookEntry.Info[i];
                        _detail.Info[i].color = new Color(_detail.Info[i].color.r, _detail.Info[i].color.g, _detail.Info[i].color.b, 1f);
                    }

                } else
                {
                    Debug.LogWarning($"{tab.gameObject.name} is missing a BookEntry (SO) reference.");
                }
                tab.Selected.SetActive(true);
            }
        }

        public void OnTabEnter(BookSlot tab)
        {
            Debug.Log("OnTabEnter");
        }

        public override void ResetTabs()
        {
            foreach (BookSlot tab in _tabs)
            {
                if (_selectedTab != null && tab == _selectedTab) { continue; }
                //tab.Icon = _tabIdle;
                _detail.Icon.sprite = _defaultIcon;
                _detail.Title.text = "";
                _detail.Description.text = "";
                tab.Selected.SetActive(false);

                // Set info sprites transparent since they are optional
                foreach (Image img in _detail.Info)
                {
                    img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
                }

            }
        }

        public void OnTabExit(BookSlot tab)
        {

        }
    }
}