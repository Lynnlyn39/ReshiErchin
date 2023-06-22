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
        [SerializeField] private Sprite iconIdle;
        [SerializeField] private Sprite iconHover;
        [SerializeField] private Sprite iconSelected;

        public Image Icon { get => _icon; set => _icon = value; }
        public List<GameObject> ObjectsToEnable { get => _objectsToEnable; set => _objectsToEnable = value; }
        public Sprite IconIdle { get => iconIdle; set => iconIdle = value; }
        public Sprite IconHover { get => iconHover; set => iconHover = value; }
        public Sprite IconSelected { get => iconSelected; set => iconSelected = value; }

        private void Start()
        {
            if (IconIdle)
                Icon.sprite = IconIdle;
            else if (!Icon)
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