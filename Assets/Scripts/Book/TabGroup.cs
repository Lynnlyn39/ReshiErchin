using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CustomUI
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] protected List<TabButton> _tabs;
        [SerializeField] protected Sprite _tabIdle;
        [SerializeField] protected Sprite _tabHover;
        [SerializeField] protected Sprite _tabActive;
        [SerializeField] protected TabButton _selectedTab;

        public void Subscribe(TabButton tab)
        {
            if (_tabs == null)
                _tabs = new List<TabButton>();
            
            _tabs.Add(tab);
        }

        public void OnTabEnter(TabButton tab)
        {
            ResetTabs();
            if (_selectedTab == null || tab != _selectedTab)
            {
                tab.Icon.sprite = _tabHover;
            }
        }

        public void OnTabExit(TabButton tab)
        {
            ResetTabs();            
            tab.Icon.sprite = _tabIdle;
        }

        public virtual void OnTabSelected(TabButton tab)
        {
            _selectedTab = tab;
            ResetTabs();
            tab.Icon.sprite = _tabActive;
            foreach (GameObject go in tab.ObjectsToEnable)
            {
                go.SetActive(true);
            }
        }

        public virtual void ResetTabs()
        {
            foreach(TabButton tab in _tabs)
            {
                if (_selectedTab != null && tab == _selectedTab) { continue; }
                tab.Icon.sprite = _tabIdle;
                foreach (GameObject go in tab.ObjectsToEnable)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}