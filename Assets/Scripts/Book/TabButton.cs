using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace CustomUI
{ 
    public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected TabGroup _tabGroup;
        [SerializeField] protected List<GameObject> _objectsToEnable;
        [SerializeField] protected Image _icon;

        public Image Icon { get => _icon; set => _icon = value; }
        public List<GameObject> ObjectsToEnable { get => _objectsToEnable; set => _objectsToEnable = value; }

        private void Start()
        {
            if (!Icon)
                Icon = GetComponent<Image>();

            _tabGroup.Subscribe(this);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("OnPointerClick");
            _tabGroup.OnTabSelected(this);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnTabEnter");
            _tabGroup.OnTabEnter(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit");
            _tabGroup.OnTabExit(this);
        }
    }
}