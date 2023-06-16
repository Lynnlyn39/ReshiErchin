using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace CustomUI
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected TabGroup _tabGroup;
        [SerializeField] protected List<GameObject> _objectsToEnable;
        protected Image _icon;

        public Image Icon { get => _icon; set => _icon = value; }
        public List<GameObject> ObjectsToEnable { get => _objectsToEnable; set => _objectsToEnable = value; }

        private void Start()
        {
            Icon = GetComponent<Image>();
            _tabGroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _tabGroup.OnTabSelected(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tabGroup.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tabGroup.OnTabExit(this);
        }
    }
}