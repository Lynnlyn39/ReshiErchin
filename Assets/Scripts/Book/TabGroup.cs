using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CustomUI
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] protected List<TabButton> _tabs;
        [Tooltip("Tab graphical states at group level. If they are defined at tab level then tab level ones have precedence.")]
        [SerializeField] protected Sprite _tabIdle;
        [SerializeField] protected Sprite _tabHover;
        [SerializeField] protected Sprite _tabActive;
        [SerializeField] protected TabButton _selectedTab;

        protected virtual void Start()
        {
            if (_selectedTab)
                OnTabSelected(_selectedTab);
        }
        public void Subscribe(TabButton tab)
        {
            if (_tabs == null)
                _tabs = new List<TabButton>();
            
            _tabs.Add(tab);
        }

        public virtual void OnTabEnter(TabButton tab)
        {
            ResetTabs();
            if (_selectedTab == null || tab != _selectedTab)
            {
                tab.Icon.sprite = tab.IconHover ? tab.IconHover : _tabHover;
            }
        }

        public void OnTabExit(TabButton tab)
        {
            //ResetTabs();            
            if (_selectedTab != tab)
                tab.Icon.sprite = tab.IconIdle ? tab.IconIdle : _tabIdle;
        }

        public virtual void OnTabSelected(TabButton tab)
        {
            _selectedTab = tab;
            ResetTabs();
            tab.Icon.sprite = tab.IconSelected ? tab.IconSelected : _tabActive;
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
                tab.Icon.sprite = tab.IconIdle ? tab.IconIdle : _tabIdle;
                foreach (GameObject go in tab.ObjectsToEnable)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}